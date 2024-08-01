using BarberShop.Domain.Models;
using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Interfaces
{
    public interface IBarberRepository : IBaseRepository<Barber>
    {
        List<BarberModel> GetBarber();
    }
}
