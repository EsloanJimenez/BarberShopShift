using BarberShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Context
{
    public class BarberShopContext : DbContext
    {
        public BarberShopContext(DbContextOptions<BarberShopContext> dbContext) : base(dbContext) { }

        #region "Entities"
        public DbSet<Barber> Barber { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        #endregion
    }
}
