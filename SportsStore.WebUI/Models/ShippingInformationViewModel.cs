using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class ShippingInformationViewModel
    {
        [Required(ErrorMessage = "Proszę podać imię i nazwisko")]
        [Display(Name = "Imie i Nazwisko:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać adres email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public AddressViewModel Address { get; set; }

        [Display(Name = "Zapakuj jako prezent")]
        public bool GiftWrap { get; set; }
    }
}