using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shoping.DAL.Infrastructure;
using Shoping.DAL.Interfaces;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.DAL.Repositories.SQL_Repositories;
using Shoping.DAL.Services.SQL_Services;
using Shoping.DAL.sqlunitOfWork;

namespace Shoping.WEBAPI
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            #region SQL repositories            
            services.AddTransient<ISQLFilmsRepository, SQLFilmsRepository>();
            services.AddTransient<ISQLBasketFilmsRepository, SQLBasketFilmsRepository>();
            #endregion

            #region SQL services            
            services.AddTransient<ISQLFilmsService, SQLFilmsService>();
            services.AddTransient<ISQLBasketFilmsService, SQLBasketFilmsService>();
            #endregion

            services.AddTransient<ISQLUnitOfWork, SQLsqlunitOfWork>();

            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shoping.WEBAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shoping.WEBAPI v1"));
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
