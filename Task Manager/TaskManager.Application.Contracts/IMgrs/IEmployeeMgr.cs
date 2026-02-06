using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Contracts.IMgrs;

public interface IEmployeeMgr
{
    Task<Employee> GetSingle(long Id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(long Id);
    Task<IEnumerable<Employee>> GetPaged(string? searchTerm, int page, int size);
    Task<IEnumerable<Employee>> Search(string? searchTerm);
    Task<bool> RegisterAsync(string username, string email, string password, decimal Phone_Number, DateTime Birthdate, string Living_Address,
        char Gender, int Salary, int RoleId, int MaritalStatusId, long? DepartmentId);
    Task<Employee?> LoginAsync(string username, string password);
    Task<bool> EmployeeExistsAsync(string username);
}
