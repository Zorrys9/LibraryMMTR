using AutoMapper;
using GreenPipes;
using Library.Data;
using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Data.Repository.Implementations;
using Library.Hubs;
using Library.Logic.EventBus;
using Library.Logic.LogicModels;
using Library.Logic.Logics;
using Library.Middelwares;
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
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Google.Apis.Books.v1;
using Microsoft.AspNetCore.Authentication.Google;
using Google.Apis.Services;
using Library.Common.Models;

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

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddSignalR();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MailConsumer>();
            });
            services.AddScoped<MailConsumer>();
            
            var root = JObject.Parse(File.ReadAllText("mailingsettings.json"));
            var hostRabbit = root.DescendantsAndSelf()
                .OfType<JProperty>()
                .Where(p => p.Name == "RabbitMQ")
                .Select(p => p.Value);

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(hostRabbit.FirstOrDefault().ToString());

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

            services.AddSwaggerGen(c=> 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                });
                var xmlFile = "Library.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();
            services.AddSingleton(provider => provider.GetRequiredService<IBus>().CreateRequestClient<IMailSend>());

            services.AddScoped<IActiveHolderRepository, ActiveHolderRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IKeyWordRepository, KeyWordRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IStatusLogRepository, StatusLogRepository>();
            services.AddScoped<IRaitingBooksRepository, RaitingBooksRepository>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IKeyWordService, KeyWordService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IHolderService, HolderService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStatusLogService, StatusLogService>();
            services.AddScoped<IRaitingBooksService, RaitingBooksService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IBookApiService, BookApiService>();

            services.AddScoped<ILibraryLogic, LibraryLogic>();
            services.AddScoped<IImageLogic, ImageLogic>();

            var apiSettingsFile = File.ReadAllText("apisettings.json");
            var settingApi = JsonConvert.DeserializeObject<SettingApiModel>(apiSettingsFile);

            BooksService googleService = new BooksService(new BaseClientService.Initializer
            {
                ApplicationName = settingApi.AppName,
                ApiKey = settingApi.Key
            });

            services.AddSingleton(googleService);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LibraryContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserEntityModel, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<LibraryContext>();
        }
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

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseSession();
            app.UseAuthentication();

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<BookListHub>("/BookList");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Library}/{action=AllBooks}");
            });
        }
    }
}
