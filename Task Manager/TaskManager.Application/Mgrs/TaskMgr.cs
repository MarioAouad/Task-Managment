using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Interfaces;
using TaskManager.EF;
using CTask = TaskManager.Domain.Models.Task;

namespace TaskManager.Application.Mgrs
{
    public class TaskMgr : ITaskMgr
    {
        #region Members

        private readonly IGenericRepository<Domain.Models.Task> _repository;
        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public TaskMgr(IGenericRepository<Domain.Models.Task> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        #endregion

        #region Commands
        public async System.Threading.Tasks.Task AddAsync(Domain.Models.Task project)
        {
            await _repository.AddAsync(project);
            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Domain.Models.Task project)
        {
            _repository.Update(project);
            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(long id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project != null)
            {
                _repository.Delete(project);
                await _repository.SaveChangesAsync();
            }

        }

        #endregion

        #region Queries 

        public async Task<CTask> GetSingle(long id)
        {
            return _context.Task
               .Include(e => e.Department)
               .FirstOrDefault(e => e.Id == id);
        }


        public async Task<IEnumerable<CTask>> GetPaged(string? searchTerm, int page, int size)
        {
            Expression<Func<Domain.Models.Task, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowered = searchTerm.ToLower();
                filter = p => p.Name.ToLower().Contains(lowered);
            }
            return await _repository.GetPagedAsync(filter, page, page);
        }

        public async Task<IEnumerable<CTask>> Search(string? term)
        {
            Expression<Func<Domain.Models.Task, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(term))
            {
                string lowered = term.ToLower();
                filter = p => p.Name.ToLower().Contains(lowered);
            }
            return await _repository.FindAsync(filter ?? (p => true));
        }

        #endregion
    }
}
