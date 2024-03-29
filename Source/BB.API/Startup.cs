﻿namespace BE.API
{
    using System;
    using System.Threading.Tasks;
    using BB.Comparer.Business;
    using BB.Infrastructure.Context;
    using BB.Infrastructure.Service;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            this.InitializeIOC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.Env = env;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2"))
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature.Error;

                Log.Logger.Error(exception, "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

                return Task.CompletedTask;
            });
                });
            }

            app.UseFileServer();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void InitializeIOC(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                if (this.Env.IsDevelopment())
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    });
                }
            });

            var bbConnectionString = this.Configuration.GetConnectionString("BB");
            services.AddDbContext<BibleContext>(options => options.UseSqlServer(bbConnectionString));

            services.AddTransient<IBibleService, BibleService>();
            services.AddTransient<ITextComparer, TextComparer>();
        }
    }
}
