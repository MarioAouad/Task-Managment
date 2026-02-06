using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Application.Contracts.DTOs;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.EF;

namespace TaskManager.Application.Mgrs
{
    public class TaskAssignmentMgr : ITaskAssignmentMgr
    {
        #region Members

        private readonly IGenericRepository<TaskAssignment> _repository;
        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public TaskAssignmentMgr(IGenericRepository<TaskAssignment> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        #endregion

        #region Commands

        public async System.Threading.Tasks.Task AddAsync(TaskAssignment ta)
        {
            await _repository.AddAsync(ta);
            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(TaskAssignment ta)
        {
            _repository.Update(ta);
            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(long Id)
        {
            var ta = await GetSingle(Id);
            if (ta != null)
            {
                _repository.Delete(ta);
                await _repository.SaveChangesAsync();
            }
        }

        #endregion

        #region Queries 

        public async Task<TaskAssignment?> GetSingle(long Id)
        { 
            return _context.TaskAssignment
               .Include(ta => ta.Task)
               .Include(ta => ta.Employee)
               .FirstOrDefault(ta => ta.Id == Id);
        }

        public async Task<IEnumerable<TaskAssignment>> GetPaged(string? term, int page, int size)
        {
            Expression<Func<TaskAssignment, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(term))
            {
                string lowered = term.ToLower();
                filter = ta =>
                    ta.Employee.Username.ToLower().Contains(lowered) ||
                    ta.Task.Name.ToLower().Contains(lowered);
            }
            return await _repository.GetPagedAsync(filter, page, size);
        }

        public async Task<IEnumerable<TaskAssignment>> Search(string? term)
        {
            Expression<Func<TaskAssignment, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(term))
            {
                string lowered = term.ToLower();
                filter = ta =>
                    ta.Employee.Username.ToLower().Contains(lowered) ||
                    ta.Task.Name.ToLower().Contains(lowered);
            }
            return await _repository.FindAsync(filter ?? (w => true));
        }

        #endregion
    }
}
