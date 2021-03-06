using EventBus.Messages.Common;
using Filmos_Rating_CleanArchitecture.Application;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer.FilmsConsumer;
using Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer.UsersConsumer;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Filmos_Rating_CleanArchitecture.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddPersistence(Configuration);
            services.AddApplication();

            services.Configure<FilmosDatabaseSettings>(
                Configuration.GetSection("FilmosDatabase"));

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Filmos_Rating_CleanArchitecture", Version = "v1" });
            });

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config => {

                config.AddConsumer<FilmsUpsertConsumer>();
                config.AddConsumer<FilmsDeleteConsumer>();
                config.AddConsumer<UsersUpsertConsumer>();
                config.AddConsumer<UsersDeleteConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    //cfg.UseHealthCheck(ctx);

                    cfg.ReceiveEndpoint(EventBusConstants.FilmCheckoutQueue, c => {
                        c.ConfigureConsumer<FilmsUpsertConsumer>(ctx);
                        c.ConfigureConsumer<FilmsDeleteConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint(EventBusConstants.UserCheckoutQueue, c => {
                        c.ConfigureConsumer<UsersUpsertConsumer>(ctx);
                        c.ConfigureConsumer<UsersDeleteConsumer>(ctx);
                    });
                });
            });
            services.AddScoped<FilmsUpsertConsumer>();
            services.AddScoped<FilmsDeleteConsumer>();
            services.AddScoped<UsersUpsertConsumer>();
            services.AddScoped<UsersDeleteConsumer>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Filmos_Rating_CleanArchitecture v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
