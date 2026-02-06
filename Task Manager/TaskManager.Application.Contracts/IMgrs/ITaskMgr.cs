using CTask = TaskManager.Domain.Models.Task;

namespace TaskManager.Application.Contracts.IMgrs;

public interface ITaskMgr
{
    Task<CTask> GetSingle(long id);
    System.Threading.Tasks.Task AddAsync(Domain.Models.Task project);
    System.Threading.Tasks.Task UpdateAsync(Domain.Models.Task project);
    System.Threading.Tasks.Task DeleteAsync(long id);
    Task<IEnumerable<CTask>> GetPaged(string? searchTerm, int page, int size);
    Task<IEnumerable<CTask>> Search(string? term);
}
