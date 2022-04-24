using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }
    }
}