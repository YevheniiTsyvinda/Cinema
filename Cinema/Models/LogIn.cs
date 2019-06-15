using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class LogIn
    {
        
        [MinLength(4, ErrorMessage = "Login should be 4 symbols at least")]
        [Required]
        [DisplayName("Login:")]
        public string Login { get; set; }

        
        [MinLength(4, ErrorMessage = "Password should be 4 symbols at least")]
        [Required]
        [DisplayName("Password:")]
        public string Password { get; set; }
    }
}