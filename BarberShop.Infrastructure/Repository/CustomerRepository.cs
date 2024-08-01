using BarberShop.Domain.Entities;
using BarberShop.Domain.Interfaces;
using BarberShop.Domain.Models;
using BarberShop.Infrastructure.Context;
using BarberShop.Infrastructure.Core;
using System.Linq.Expressions;

namespace BarberShop.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly BarberShopContext _context;
        public CustomerRepository(BarberShopContext context) : base(context)
        {
            _context = context;
        }
        public List<CustomerModel> GetCustomer()
        {
            var customer = (from cu in _context.Customers
                            orderby cu.CustomerId descending
                            where cu.Deleted == false
                            select new CustomerModel()
                            {
                                CustomerId = cu.CustomerId,
                                FirstName = cu.FirstName,
                                Phone = cu.Phone,
                            }).ToList();

            return customer;
        }
        public override async Task Save(Customer entity)
        {
            if (entity is null)
                throw new ArgumentException("El cliente no puede ser nulo.");

            if (await Exists(cd => cd.UserName == entity.UserName))
                throw new ArgumentException("Ya este nombre de usuario existe.");

            base.Save(entity);
            base.SaveChanges();
        }

        public override async Task Update(Customer entity)
        {
            try
            {
                Customer customerToUpdate = await base.Get(entity.CustomerId);

                if (customerToUpdate is null)
                    throw new ArgumentException("El cliente no existe");

                customerToUpdate.FirstName = entity.FirstName;
                customerToUpdate.UserName = entity.UserName;
                customerToUpdate.Password = entity.Password;
                customerToUpdate.Phone = entity.Phone;
                customerToUpdate.UserMod = entity.UserMod;
                customerToUpdate.ModifyDate = DateTime.Now;

                base.Update(customerToUpdate);
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error al actualizar el cliente.");
            }
        }

        public override Task<List<Customer>> GetAll(Expression<Func<Customer, bool>> entity)
        {
            return base.GetAll(entity);
        }

        public override Task<Customer> Get(int Id)
        {
            return base.Get(Id);
        }

        public override async Task Remove(Customer entity)
        {
            try
            {
                Customer customerToRemove = await base.Get(entity.CustomerId);

                if (customerToRemove is null)
                    throw new ArgumentException("El barbero no existe.");

                customerToRemove.Deleted = true;
                customerToRemove.DeletedDate = DateTime.Now;
                customerToRemove.UserDelete = 1;

                base.Update(customerToRemove);
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error actualizando el cliente.");
            }
        }
    }
}
