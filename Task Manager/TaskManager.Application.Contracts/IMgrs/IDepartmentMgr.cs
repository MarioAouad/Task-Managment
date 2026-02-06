using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Contracts.IMgrs;

public interface IDepartmentMgr
{
    Task<Department> GetSingle(long Id);
    Task AddAsync(Department Department);
    Task UpdateAsync(Department Department);
    Task DeleteAsync(long Id);
    Task<IEnumerable<Department>> GetPagedDepartments(string? searchTerm, int pageNumber, int pageSize);
    Task<IEnumerable<Department>> Search(string? searchTerm);

}