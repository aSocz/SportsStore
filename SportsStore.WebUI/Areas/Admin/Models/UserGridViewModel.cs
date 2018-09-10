using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Areas.Admin.Models
{
    public class UserGridViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public string Roles { get; set; }
    }
}