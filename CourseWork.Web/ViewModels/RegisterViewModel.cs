using System.ComponentModel.DataAnnotations;

namespace CourseWork.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}