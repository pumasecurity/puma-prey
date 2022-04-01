using Gopher.DTOs;
using Gopher.Models;

namespace Gopher.Services
{
    public interface IProjectTaskService
    {
        Task AddProjectTaskAsync(ProjectTask projectTask);
        Task DeleteProjectTaskAsync(Guid ID);
        Task<List<ProjectTaskAndTagDto>> GetAll();
        IQueryable<ProjectTask> GetAllByProjectID(int ID);
        Task<ProjectTaskAndTagDto?> GetProjectTaskByIdAsync(Guid ID);
        Task<ProjectTask> Update(ProjectTask projectTaskUpdate);
    }
}