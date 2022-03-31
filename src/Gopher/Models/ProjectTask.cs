using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gopher.Models
{
    public class ProjectTask
    {
        public string ID { set; get; }
        public string Name { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public DateTime Date { set; get; }
        public Priority Priority { set; get; }
        public virtual IEnumerable<ProjectTaskTag> ProjectTaskTags { get; set; }

        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

    }
}
