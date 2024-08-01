using BarberShop.Domain.Entities;
using BarberShop.Domain.Models;

namespace BarberShop.Domain.Interfaces
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        List<ShiftModel> GetShift();
    }
}
