namespace Gopher.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string UserID { get; set; }

        //navigation properties
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

        public Project() { }

        public Project(IEnumerable<ProjectTask> projectTasks)
        {
            ProjectTasks = projectTasks.ToList();
        }
    }
}