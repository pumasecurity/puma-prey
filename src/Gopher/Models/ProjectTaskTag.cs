using System.ComponentModel.DataAnnotations.Schema;

namespace Gopher.Models
{
    [Table("ProjectTaskTag")]
    public class ProjectTaskTag
    {
        public Guid ID { get; set; }
        public Guid ProjectTaskID { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public Guid TagID { get; set; }
        public virtual Tag Tag { get; set; }
    }
}