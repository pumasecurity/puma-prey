using System.ComponentModel.DataAnnotations;

namespace Coyote.Models
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}