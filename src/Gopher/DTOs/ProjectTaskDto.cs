using Gopher.Models;

namespace Gopher.DTOs
{
    public class ProjectTaskDto
    {
        public Guid ID { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public string Name { get; set; }

        public DateTime Date { set; get; }
        public Priority Priority { set; get; }

        public int ProjectID { get; set; }
        public List<Guid> TagIDs { get; set; }
    }

    public class ProjectTaskAndTagDto
    {
        public Guid ID { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public string Name { get; set; }
        public DateTime Date { set; get; }
        public Priority Priority { set; get; }

        public int ProjectID { get; set; }
        public List<string> TagName { get; set; }

    }
}
