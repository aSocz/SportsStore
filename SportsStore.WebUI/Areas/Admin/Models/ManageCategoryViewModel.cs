using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Areas.Admin.Models
{
    public class ManageCategoryViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        public string Name { get; set; }
    }
}