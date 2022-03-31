using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gopher.ViewModels
{
    public class TagVM
    {
        public string Name { get; set; }
    }
    public class TagAndProjectTaskVM
    {
        public string Name { get; set; }
        public List<string> ProjectTaskDescription { get; set; }
    }
}
