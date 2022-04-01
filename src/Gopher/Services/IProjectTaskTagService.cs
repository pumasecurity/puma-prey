
namespace Gopher.Services
{
    public interface IProjectTaskTagService
    {
        Task UpdateProjectTaskTagsInByProjectTaskID(Guid projectTaskID, string[] tags);
    }
}