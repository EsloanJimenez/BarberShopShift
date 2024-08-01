namespace BarberShop.Domain.Models
{
    public class ShiftModel
    {
        public int ShiftId { get; set; }
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
        public int UserCreation { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
    }
}
