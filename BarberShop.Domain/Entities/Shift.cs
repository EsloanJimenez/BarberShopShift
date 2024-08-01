using BarberShop.Domain.Core;

namespace BarberShop.Domain.Entities
{
    public class Shift : BaseAuditory
    {
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
    }
}
