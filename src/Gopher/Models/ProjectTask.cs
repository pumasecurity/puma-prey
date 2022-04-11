namespace Gopher.Models
{
    public class ProjectTask
    {
        public Guid ID { set; get; }
        public string Name { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public DateTime Date { set; get; }
        public Priority Priority { set; get; }
        
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }


        public virtual ICollection<ProjectTaskTag> ProjectTaskTags { get; set; } = new List<ProjectTaskTag>();

        public ProjectTask() { }

        public ProjectTask(IEnumerable<ProjectTaskTag> projectTaskTags)
        {
            ProjectTaskTags = projectTaskTags.ToList();
        }
    }
}