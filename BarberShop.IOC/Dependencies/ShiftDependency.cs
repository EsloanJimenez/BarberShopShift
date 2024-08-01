using BarberShop.Domain.Interfaces;
using BarberShop.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.IOC.Dependencies
{
    public static class ShiftDependency
    {
        public static void AddShiftDependency(this IServiceCollection service) { 
            service.AddScoped<IShiftRepository, ShiftRepository>();
        }
    }
}
