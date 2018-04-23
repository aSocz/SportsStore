﻿using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Proszę podać adres")]
        [Display(Name = "Adres:")]
        public string Line1 { get; set; }

        [Display(Name = "Adres cz.2:")]
        public string Line2 { get; set; }

        [Display(Name = "Adres cz.3:")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Proszę podać miasto")]
        [Display(Name = "Miasto:")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę podać województwo")]
        [Display(Name = "Województwo:")]
        public string State { get; set; }

        [Display(Name = "Kod pocztowy:")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Proszę podać kraj")]
        [Display(Name = "Kraj:")]
        public string Country { get; set; }

        [Display(Name = "Zapakuj jako prezent")]
        public bool GiftWrap { get; set; }
    }
}