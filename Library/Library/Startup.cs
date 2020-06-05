using GreenPipes;
using Library.Data;
using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Data.Repository.Implementations;
using Library.Logic.EventBus;
using Library.Logic.LogicModels;
using Library.Logic.Logics;
using Library.Services.Services;
using Library.Services.Services.Implementations;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace Library
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Set the new Configuration
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMassTransit(x =>
            {

                x.AddConsumer<MailConsumer>();

            });

            services.AddScoped<MailConsumer>();

            var root = JObject.Parse(File.ReadAllText("mailingsettings.json"));

            var hostRabbit = root.DescendantsAndSelf().
                OfType<JProperty>().
                Where(p => p.Name == "RabbitMQ")
                .Select(p => p.Value);

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {

                var host = cfg.Host(hostRabbit.FirstOrDefault().ToString());

                cfg.SetLoggerFactory(provider.GetService<ILoggerFactory>());
                cfg.AutoDelete = false;
                cfg.Durable = true;
                cfg.AutoStart = true;
                cfg.OverrideDefaultBusEndpointQueueName("Send");

                cfg.ReceiveEndpoint("SendMail", e =>
                {
                    e.BindMessageExchanges = true;
                    e.PrefetchCount = 1;
                    e.UseMessageRetry(x => x.Interval(2, 100));
                    e.Consumer<MailConsumer>(provider);

                    EndpointConvention.Map<IMailSend>(e.InputAddress);
                });
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<IMailSend>());

            services.AddTransient<IActiveHolderRepository, ActiveHolderRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IKeyWordRepository, KeyWordRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IStatusLogRepository, StatusLogRepository>();
            services.AddTransient<IRaitingBooksRepository, RaitingBooksRepository>();

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IKeyWordService, KeyWordService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IHolderService, HolderService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStatusLogService, StatusLogService>();
            services.AddTransient<IRaitingBooksService, RaitingBooksService>();
            services.AddTransient<ISettingsService, SettingsService>();

            services.AddTransient<ILibraryLogic, LibraryLogic>();
            services.AddTransient<IImageLogic, ImageLogic>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LibraryContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            //NpgsqlConnection.GlobalTypeMapper.MapEnum<BookCategory>("some_enum_type");

            services.AddIdentity<UserEntityModel, IdentityRole>(opt =>
            {

                // поменять false на true
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;

            })
                .AddEntityFrameworkStores<LibraryContext>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                app.UseHsts();
            }
            app.UseSession();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Library}/{action=AllBooks}");
            });
        }
    }
}
