using System;
using BusAndSignalRSample.WebApi.Consts;
using BusAndSignalRSample.WebApi.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusAndSignalRSample.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IServiceProvider ServiceProvider { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSignalR();
            services.AddCors(o =>
            {
                o.AddPolicy(AppConst.Policies.EverythingCorsPolicy, p =>
                {
                    p.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            ModuleBootstrapper.Configue(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ServiceProvider = app.ApplicationServices;

            app.UseMvc();
            app.UseCors(AppConst.Policies.EverythingCorsPolicy);

            // Configue SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageRegistrationHub>("messaging");
            });

            // Configue bus
            var bus = app.ApplicationServices.GetRequiredService<IBusControl>();
            bus.Start();
            lifetime.ApplicationStopping.Register(() => bus.Stop());
        }
    }
}
