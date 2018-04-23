using System.ComponentModel.DataAnnotations;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Areas.Admin.Models
{
    public class ManageProductViewModel : ProductViewModel
    {
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
    }
}