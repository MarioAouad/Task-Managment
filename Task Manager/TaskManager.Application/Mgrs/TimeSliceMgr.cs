using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Application.Contracts.DTOs;
using TaskManager.Application.Contracts.DTOs.TimeSlice;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.EF;
using LinqKit;

namespace TaskManager.Application.Mgrs
{
    public class TimeSliceMgr : ITimeSliceMgr
    {
        #region Members

        private readonly IGenericRepository<TimeSlice> _repository;
        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public TimeSliceMgr(IGenericRepository<TimeSlice> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        #endregion

        #region Commands

        public async System.Threading.Tasks.Task AddAsync(TimeSlice ts)
        {
            await _repository.AddAsync(ts);
            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(TimeSlice ts)
        {
            _repository.Update(ts);
            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(long Id)
        {
            var ts = await GetSingle(Id);
            if (ts != null)
            {
                _repository.Delete(ts);
                await _repository.SaveChangesAsync();
            }
        }

        #endregion

        #region Queries 

        public async Task<TimeSlice?> GetSingle(long Id)
        {
            return _context.TimeSlice
               .FirstOrDefault(w => w.Id == Id);
        }

        //public async Task<IEnumerable<TimeSlice>> GetPaged(string? term, int page, int size)
        //{
        //    Expression<Func<TimeSlice, bool>>? filter = null;

        //    if (!string.IsNullOrWhiteSpace(term))
        //    {
        //        //string lowered = term.ToLower();
        //        //filter = ts =>
        //        //    ts.Employee.Username.ToLower().Contains(lowered) ||
        //        //    ts.Task.Name.ToLower().Contains(lowered);
        //    }
        //    return await _repository.GetPagedAsync(filter, page, size);
        //}

        //public async Task<IEnumerable<TimeSlice>> Search(string? term)
        //{
        //    Expression<Func<TimeSlice, bool>>? filter = null;

        //    if (!string.IsNullOrWhiteSpace(term))
        //    {
        //        //string lowered = term.ToLower();
        //        //filter = ts =>
        //        //    ts.TaskAssignmentId.ToLower().Contains(lowered)
        //    }
        //    return await _repository.FindAsync(filter ?? (w => true));
        //}

        public async Task<IEnumerable<TimeSlice>> GetPaged(TimeSliceFilterDto? filterDto, int page, int size)
        {
            var predicate = BuildPredicate(filterDto);
            return await _repository.GetPagedAsync(predicate, page, size);
        }

        public async Task<IEnumerable<TimeSlice>> Search(TimeSliceFilterDto? filterDto)
        {
            var predicate = BuildPredicate(filterDto);
            return await _repository.FindAsync(predicate ?? (w => true));
        }

        public Expression<Func<TimeSlice, bool>>? BuildPredicate(TimeSliceFilterDto? filter)
        {
            if (filter == null) return null;

            var predicate = PredicateBuilder.New<TimeSlice>(true);

            if (filter.TaskAssignmentId.HasValue)
                predicate = predicate.And(ts => ts.TaskAssignmentId == filter.TaskAssignmentId.Value);

            if (filter.StartDateFrom.HasValue)
                predicate = predicate.And(ts => ts.StartDate >= filter.StartDateFrom.Value);

            if (filter.StartDateTo.HasValue)
                predicate = predicate.And(ts => ts.StartDate <= filter.StartDateTo.Value);

            if (filter.EndDateFrom.HasValue)
                predicate = predicate.And(ts => ts.EndDate >= filter.EndDateFrom.Value);

            if (filter.EndDateTo.HasValue)
                predicate = predicate.And(ts => ts.EndDate <= filter.EndDateTo.Value);

            return predicate;
        }

        #endregion
    }
}
