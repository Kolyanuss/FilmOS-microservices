using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Filmos_Rating_CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); мб пригодиться

            return services;
        }
    }
}
