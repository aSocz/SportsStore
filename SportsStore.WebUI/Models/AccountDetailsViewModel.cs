using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class AccountDetailsViewModel
    {
        [Required(ErrorMessage = "Proszę podać nazwę użytkownika")]
        [DisplayName("Nazwa użytkownika")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać adres email")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        public AddressViewModel Address { get; set; }

        public string ReturnUrl { get; set; }
    }
}