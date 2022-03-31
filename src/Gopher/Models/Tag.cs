using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gopher.Models
{
    [Table("Tags")]
    public class Tag
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<ProjectTaskTag> ProjectTaskTags { get; set; }
    }
}
