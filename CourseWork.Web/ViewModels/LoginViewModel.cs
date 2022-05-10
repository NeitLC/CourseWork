using System.ComponentModel.DataAnnotations;

namespace CourseWork.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}