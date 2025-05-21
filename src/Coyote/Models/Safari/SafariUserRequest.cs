using System.ComponentModel.DataAnnotations;

namespace Coyote.Models.Safari
{ 
    public class SafariUserRequest
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int SafariId { get; set; }
    }
}