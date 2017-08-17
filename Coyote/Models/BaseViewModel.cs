using System;

namespace Coyote.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public int Version { get; set; }
    }
}