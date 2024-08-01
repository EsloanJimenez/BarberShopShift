using BarberShop.Domain.Core;

namespace BarberShop.Domain.Entities
{
    public class Customer : BaseUsers
    {
        public int CustomerId { get; set; }
        public string Phone {  get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
    }
}
