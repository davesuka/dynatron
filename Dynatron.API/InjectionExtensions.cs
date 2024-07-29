using Dynatron.API.Context;
using Dynatron.API.Interfaces;
using Dynatron.API.Repository;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dynatron.API
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var assembly = typeof(CustomerContext).Assembly.FullName;

            services.AddDbContext<CustomerContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DBConnection"),
                    b => b.MigrationsAssembly(assembly)
                ),
                ServiceLifetime.Transient
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton(configuration);
            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
