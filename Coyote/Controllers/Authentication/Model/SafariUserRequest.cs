using System.ComponentModel.DataAnnotations;

namespace Coyote.Controllers.Authentication.Model
{
    public class SafariUserRequest
    {

        [Required]
        public int MemberId { get; set; }
        [Required]
        public int SafariId { get; set; }
    }
}
