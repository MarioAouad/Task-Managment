using System.Linq.Expressions;
using TaskManager.Application.Contracts.DTOs.TimeSlice;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Contracts.IMgrs;
public interface ITimeSliceMgr
{
    Task<TimeSlice?> GetSingle(long Id);
    System.Threading.Tasks.Task AddAsync(TimeSlice ta);
    System.Threading.Tasks.Task UpdateAsync(TimeSlice ta);
    System.Threading.Tasks.Task DeleteAsync(long Id);
    //Task<IEnumerable<TimeSlice>> GetPaged(string? term, int page, int size);
    //Task<IEnumerable<TimeSlice>> Search(string? term);
    Task<IEnumerable<TimeSlice>> GetPaged(TimeSliceFilterDto? filterDto, int page, int size);
    Task<IEnumerable<TimeSlice>> Search(TimeSliceFilterDto? filterDto);
    Expression<Func<TimeSlice, bool>>? BuildPredicate(TimeSliceFilterDto? filter);
}