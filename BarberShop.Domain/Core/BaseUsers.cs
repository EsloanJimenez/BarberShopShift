namespace BarberShop.Domain.Core
{
    public class BaseUsers : BaseAuditory
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
