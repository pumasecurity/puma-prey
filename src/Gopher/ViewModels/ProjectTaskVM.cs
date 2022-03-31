using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gopher.Models;

namespace Gopher.ViewModels
{
    public class ProjectTaskVM
    {

        public string ID { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public string Name { get; set; }

        public DateTime Date { set; get; }
        public Priority Priority { set; get; }

        public int ProjectID { get; set; }
        public List<string> TagName { get; set; }
    }

    public class ProjectTaskAndTagVM
    {
        public string ID { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public string Name { get; set; }
        public DateTime Date { set; get; }
        public Priority Priority { set; get; }

        public int ProjectID { get; set; }
        public List<string> TagName { get; set; }

    }
}
