using BarberShop.Domain.Entities;
using BarberShop.Domain.Interfaces;
using BarberShop.Domain.Models;
using BarberShop.Infrastructure.Context;
using BarberShop.Infrastructure.Core;
using System.Linq.Expressions;

namespace BarberShop.Infrastructure.Repository
{
    public class BarberRepository : BaseRepository<Barber>, IBarberRepository
    {
        private readonly BarberShopContext _context;
        public BarberRepository(BarberShopContext context) : base(context)
        {
            _context = context;
        }

        public List<BarberModel> GetBarber()
        {
            var barber = (from br in _context.Barber
                          orderby br.BarberId descending
                          where br.Deleted == false
                          select new BarberModel()
                          {
                              BarberId = br.BarberId,
                              FirstName = br.FirstName,
                          }).ToList();

            return barber;
        }

        public override async Task Save(Barber entity)
        {
            if (entity is null)
                throw new ArgumentException("Los datos no pueden ser nulos");

            if (await Exists(cd => cd.UserName == entity.UserName))
                throw new ArgumentException("El barbero ya existe.");

            base.Save(entity);
            base.SaveChanges();
        }

        public override async Task Update(Barber entity)
        {
            try
            {
                Barber barberToUpdate = await base.Get(entity.BarberId);

                if (barberToUpdate is null)
                    throw new ArgumentException("El barbero no existe");

                barberToUpdate.FirstName = entity.FirstName;
                barberToUpdate.UserName = entity.UserName;
                barberToUpdate.Password = entity.Password;
                barberToUpdate.UserMod = entity.UserMod;
                barberToUpdate.ModifyDate = DateTime.Now;

                base.Update(barberToUpdate);
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error al actualizar el barbero.");
            }
        }

        public override Task<List<Barber>> GetAll(Expression<Func<Barber, bool>> entity)
        {
            return base.GetAll(entity);
        }

        public override Task<Barber> Get(int Id)
        {
            return base.Get(Id);
        }

        public override async Task Remove(Barber entity)
        {
            try
            {
                Barber barberToRemove = await base.Get(entity.BarberId);

                if(barberToRemove is null)
                    throw new ArgumentException("El barbero no existe.");

                barberToRemove.Deleted = true;
                barberToRemove.DeletedDate = DateTime.Now;
                barberToRemove.UserDelete = 1;

                base.Update(barberToRemove);
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error actualizando el cliente.");
            }
        }
    }
}
