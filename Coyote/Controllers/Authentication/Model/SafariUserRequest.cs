using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
