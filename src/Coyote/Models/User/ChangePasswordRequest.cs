using System.ComponentModel.DataAnnotations;

namespace Coyote.Models.User
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}