namespace SportsStore.Business.Models
{
    public class AccountDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}