using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        public bool HasThumbnail { get; set; }
    }
}