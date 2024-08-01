using BarberShop.Domain.Entities;
using BarberShop.Domain.Interfaces;
using BarberShop.Domain.Models;
using BarberShop.Infrastructure.Context;
using BarberShop.Infrastructure.Core;
using System.Linq.Expressions;

namespace BarberShop.Infrastructure.Repository
{
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        private readonly BarberShopContext _context;
        public ShiftRepository(BarberShopContext context) : base(context)
        {
            _context = context;
        }
        public List<ShiftModel> GetShift()
        {
            var shifts = (from sh in _context.Shifts
                          join cu in _context.Customers
                          on sh.UserCreation equals cu.CustomerId
                          orderby sh.Position
                          where sh.Deleted == false
                          select new ShiftModel()
                          {
                              ShiftId = sh.ShiftId,
                              Time = sh.Time,
                              Position = sh.Position,
                              UserCreation = sh.ShiftId,
                              FirstName = cu.FirstName,
                              UserName = cu.UserName,
                          }).ToList();

            return shifts;
        }
        public override async Task Save(Shift entity)
        {
            if (entity is null)
                throw new ArgumentException("El turno no puede ser nulo.");
            /*
            if (await Exists(cd => cd.u == entity.UserName))
                throw new ArgumentException("Ya este nombre de usuario existe.");
            */
            base.Save(entity);
            base.SaveChanges();
        }

        public override async Task Update(Shift entity)
        {
            try
            {
                Shift shiftToUpdate = await base.Get(entity.ShiftId);

                if (shiftToUpdate is null)
                    throw new ArgumentException("El cliente no existe");

                shiftToUpdate.Position = entity.Position;
                shiftToUpdate.UserMod = entity.UserMod;
                shiftToUpdate.ModifyDate = DateTime.Now;

                base.Update(shiftToUpdate);
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error al actualizar el cliente.");
            }
        }

        public override Task<List<Shift>> GetAll(Expression<Func<Shift, bool>> entity)
        {
            return base.GetAll(entity);
        }

        public override Task<Shift> Get(int Id)
        {
            return base.Get(Id);
        }

        public override async Task Remove(Shift entity)
        {
            try
            {
                Shift shiftToRemove = await base.Get(entity.ShiftId);

                if (shiftToRemove is null)
                    throw new ArgumentException("El barbero no existe.");

                shiftToRemove.Deleted = true;
                shiftToRemove.DeletedDate = DateTime.Now;
                shiftToRemove.UserDelete = 1;

                base.Update(shiftToRemove);
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error actualizando el cliente.");
            }
        }
    }
}
