using BarberShop.Domain.Models;
using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        List<CustomerModel> GetCustomer();
    }
}
