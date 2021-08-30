using System.ComponentModel.DataAnnotations;

namespace Coyote.Controllers.Authentication.Model
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
