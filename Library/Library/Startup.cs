﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using Library.Data;
using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Data.Repository.Implementations;
using Library.Logic.EventBus;
using Library.Logic.Models;
using Library.Logic.Services;
using Library.Logic.Services.Implementations;
using Library.Properties;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Library
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<MailConsumer>();
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

            services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "v1" }));
            // доделать долговечные сообщения
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(Resources.RabbitMq_host);

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

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IActiveHoldersRepository, ActiveHoldersRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IKeyWordsRepository, KeyWordsRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IStatusLogRepository, StatusLogRepository>();


            services.AddTransient<ILibraryService, LibraryService>();
            services.AddTransient<IUsersService, UsersService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LibraryContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UsersEntityModel, IdentityRole>()
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                options.RoutePrefix = String.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}