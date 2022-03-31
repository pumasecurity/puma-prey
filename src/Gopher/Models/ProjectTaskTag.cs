using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gopher.Models
{
    [Table("ProjectTaskTag")]
    public class ProjectTaskTag
    {
        public string ID { get; set; }
        public string ProjectTaskID { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public string TagID { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
