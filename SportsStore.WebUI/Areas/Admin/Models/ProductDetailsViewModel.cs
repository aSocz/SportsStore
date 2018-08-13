using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Models
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać opis")]
        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Proszę podać cenę")]
        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Proszę wybrać kategorię")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageType { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}