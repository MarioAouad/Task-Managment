using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.EF;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Mgrs
{
    public class DepartmentMgr:IDepartmentMgr
    {
        #region Members

        private readonly AppDbContext _context;
        private readonly IGenericRepository<Department> _repository;

        #endregion

        #region Constructor

        public DepartmentMgr(IGenericRepository<Department> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        #endregion

        #region Commands

        public async Task AddAsync(Department department)
        {            
            await _repository.AddAsync(department);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync( Department department)
        {            
            _repository.Update(department);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long Id)
        {
            var Department = await GetSingle(Id);
            if (Department != null)
            {
                _repository.Delete(Department);
                await _repository.SaveChangesAsync();
            }
        }

        #endregion

        #region Queries 

        public async Task<Department> GetSingle(long Id)
        {
            return _context.Department
               .FirstOrDefault(d => d.Id == Id);
        }
      
        public async Task<IEnumerable<Department>> GetPagedDepartments(string? searchTerm, int pageNumber, int pageSize)
        {
            Expression<Func<Department, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filter = d => d.Name.Contains(searchTerm);
            }
            var departments = await _repository.GetPagedAsync(filter, pageNumber, pageSize);
            return departments;
        }

        public async Task<IEnumerable<Department>> Search(string? searchTerm)
        {
            Expression<Func<Department, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowered = searchTerm.ToLower();
                filter = d => d.Name.Contains(lowered);
            }

            var departments = await _repository.FindAsync(filter ?? (e => true));
            return departments;
        }

        #endregion
    }
}