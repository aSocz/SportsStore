using SportsStore.WebUI.Models;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Areas.Admin.Models
{
    public class ManageProductViewModel : ProductViewModel
    {
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
    }
}