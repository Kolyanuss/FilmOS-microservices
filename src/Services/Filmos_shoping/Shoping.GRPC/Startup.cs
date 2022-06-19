using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shoping.DAL.Infrastructure;
using Shoping.DAL.Interfaces;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.DAL.Repositories.SQL_Repositories;
using Shoping.DAL.Services.SQL_Services;
using Shoping.DAL.sqlunitOfWork;
using Shoping.GRPC.Services;

namespace Shoping.GRPC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<ISQLFilmsRepository, SQLFilmsRepository>();
            services.AddTransient<ISQLBasketFilmsRepository, SQLBasketFilmsRepository>();
            services.AddTransient<ISQLBasketFilmsService, SQLBasketFilmsService>();
            
            services.AddTransient<ISQLUnitOfWork, SQLsqlunitOfWork>();
            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BasketService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Grpc");
                });
            });
        }
    }
}
