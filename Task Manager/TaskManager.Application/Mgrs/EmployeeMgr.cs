using System.Linq.Expressions;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Models;
using TaskManager.Domain.Interfaces;
using TaskManager.EF;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Mgrs
{
    public class EmployeeMgr : IEmployeeMgr
    {
        #region Members

        private readonly IGenericRepository<Employee> _repository;
        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public EmployeeMgr(IGenericRepository<Employee> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        #endregion

        #region Commands
        public async Task AddAsync(Employee employee)
        {
            bool exists = await _context.Employee.AnyAsync(e => e.Username == employee.Username || e.Email == employee.Email);

            if (exists)
                throw new InvalidOperationException("Username or Email already exists.");
            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _repository.Update(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long Id)
        {
            var employee = await GetSingle(Id);
            if (employee != null)
            {
                _repository.Delete(employee);
                await _repository.SaveChangesAsync();
            }
        }

        //public async Task<bool> RegisterAsync(string username, string email, string password, decimal Phone_Number, DateTime Birthdate, 
        //    string Living_Address, char Gender, int Salary, int RoleId, int MaritalStatusId, long? DepartmentId)
        //{
        //    if (await EmployeeExistsAsync(username))
        //        return false;

        //    var employee = new Employee
        //    {
        //        Username = username,
        //        Email = email,
        //        Password = BCrypt.Net.BCrypt.HashPassword(password),
        //        Phone_Number = Phone_Number,
        //        Birthdate = Birthdate,
        //        Living_Address = Living_Address,
        //        Gender = Gender,
        //        Salary = Salary,
        //        RoleId = RoleId,
        //        DepartmentId = DepartmentId,
        //        MaritalStatusId = MaritalStatusId
        //    };

        //    _context.Employee.Add(employee);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> RegisterAsync(string username, string email, string password, decimal Phone_Number, DateTime Birthdate,
    string Living_Address, char Gender, int Salary, int RoleId, int MaritalStatusId, long? DepartmentId)
        {
            bool exists = await _context.Employee.AnyAsync(e =>
                e.Username.ToLower() == username.ToLower().Trim() ||
                e.Email.ToLower() == email.ToLower().Trim());

            if (exists)
                return false;

            var employee = new Employee
            {
                Username = username.Trim(),
                Email = email.Trim(),
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Phone_Number = Phone_Number,
                Birthdate = Birthdate,
                Living_Address = Living_Address,
                Gender = Gender,
                Salary = Salary,
                RoleId = RoleId,
                DepartmentId = DepartmentId,
                MaritalStatusId = MaritalStatusId
            };

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee?> LoginAsync(string username, string password)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(e => e.Username == username);

            if (employee == null)
                return null;

            bool valid = BCrypt.Net.BCrypt.Verify(password, employee.Password);
            return valid ? employee : null;
        }

        public async Task<bool> EmployeeExistsAsync(string username)
        {
            return await _context.Employee.AnyAsync(e => e.Username == username);
        }

        #endregion

        #region Queries 

        public async Task<Employee> GetSingle(long Id)
        {
            return _context.Employee.FirstOrDefault(e => e.Id == Id);
        }

        public async Task<IEnumerable<Employee>> GetPaged(string? searchTerm, int page, int size)
        {
            Expression<Func<Employee, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filter = e =>
                    e.Username.Contains(searchTerm);
            }

            var employees = await _repository.GetPagedAsync(filter, page, size);

            return employees;
        }

        public async Task<IEnumerable<Employee>> Search(string? searchTerm)
        {
            Expression<Func<Employee, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowered = searchTerm.ToLower();
                filter = e =>
                    e.Username.Contains(searchTerm);
            }
            var employee = await _repository.FindAsync(filter ?? (e => true));
            return employee;
        }

        #endregion
    }
}
