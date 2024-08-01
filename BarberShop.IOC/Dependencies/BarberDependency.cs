using BarberShop.Domain.Interfaces;
using BarberShop.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.IOC.Dependencies
{
    public static class BarberDependency
    {
        public static void AddBarberDependency(this IServiceCollection services)
        {
            services.AddScoped<IBarberRepository, BarberRepository>();
        }
    }
}
