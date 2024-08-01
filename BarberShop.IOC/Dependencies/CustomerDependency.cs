using BarberShop.Domain.Interfaces;
using BarberShop.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.IOC.Dependencies
{
    public static class CustomerDependency
    {
        public static void AddCustomerDependency(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
