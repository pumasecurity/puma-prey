using System;
using System.ComponentModel.DataAnnotations;

namespace Coyote.Controllers.Authentication.Model
{
    public class SafariRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}