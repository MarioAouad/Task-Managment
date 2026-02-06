using TaskManager.Domain.Models;

namespace TaskManager.Application.Contracts.IMgrs;

public interface ITaskAssignmentMgr
{
    Task<TaskAssignment?> GetSingle(long Id);
    System.Threading.Tasks.Task AddAsync(TaskAssignment ta);
    System.Threading.Tasks.Task UpdateAsync(TaskAssignment ta);
    System.Threading.Tasks.Task DeleteAsync(long Id);
    Task<IEnumerable<TaskAssignment>> GetPaged(string? term, int page, int size);
    Task<IEnumerable<TaskAssignment>> Search(string? term);
}
