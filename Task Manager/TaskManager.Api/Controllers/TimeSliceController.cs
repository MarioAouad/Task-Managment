using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using TaskManager.Application.Contracts.DTOs.TimeSlice;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSliceController : ControllerBase
    {
        #region Members

        private readonly ITimeSliceMgr _TSMgr;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public TimeSliceController(ITimeSliceMgr TSMgr, IMapper mapper)
        {
            _TSMgr = TSMgr;
            _mapper = mapper;
        }
        #endregion

        #region Queries

        [HttpGet]
        [Route("GetSingle/{Id}")]
        public async Task<IActionResult> GetSingle(long Id)
        {
            var result = await _TSMgr.GetSingle(Id);
            if (result == null) return NotFound();
            var toRet = _mapper.Map<TimeSliceDto>(result);
            return Ok(toRet);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddTimeSliceDto tsdto)
        {
            var ts = _mapper.Map<TimeSlice>(tsdto);
            await _TSMgr.AddAsync(ts);
            return Ok(ts);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateTimeSliceDto tsdto)
        {
            var existing = await _TSMgr.GetSingle(tsdto.Id);
            if (existing == null)
            {
                return NotFound();
            }
            _mapper.Map(tsdto, existing);
            await _TSMgr.UpdateAsync(existing);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var existing = await _TSMgr.GetSingle(Id);
            if (existing == null)
                return NotFound();

            await _TSMgr.DeleteAsync(Id);
            return NoContent();
        }

        //[HttpGet]
        //[Route("GetPaged")]
        //public async Task<IActionResult> GetPaged([FromQuery] string? term, [FromQuery] int page = 1, [FromQuery] int size = 10)
        //{
        //    var result = await _TSMgr.GetPaged(term, page, size);
        //    var toRet = _mapper.Map<TimeSliceDto>(result);
        //    return Ok(toRet);
        //}

        //[HttpGet]
        //[Route("Search")]
        //public async Task<IActionResult> GetSearch([FromQuery] string? term)
        //{
        //    var result = await _TSMgr.Search(term);
        //    var toRet = _mapper.Map<TimeSliceDto>(result);
        //    return Ok(toRet);
        //}
        #endregion

        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? filterJson, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            TimeSliceFilterDto? filter = null;
            if (string.IsNullOrEmpty(filterJson))
            {
                filter = null;
            }
            else if (long.TryParse(filterJson, out var taskId))
            {
                filter = new TimeSliceFilterDto { TaskAssignmentId = taskId };
            }
            else
            {
                filter = JsonSerializer.Deserialize<TimeSliceFilterDto>(filterJson);
            }

            var result = await _TSMgr.GetPaged(filter, page, size);
            var toRet = _mapper.Map<IEnumerable<TimeSliceDto>>(result);
            return Ok(toRet);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] string? filterJson)
        {
            TimeSliceFilterDto? filter = null;
            if (string.IsNullOrEmpty(filterJson))
            {
                filter = null;
            }
            else if (long.TryParse(filterJson, out var taskId))
            {
                filter = new TimeSliceFilterDto { TaskAssignmentId = taskId };
            }
            else
            {
                filter = JsonSerializer.Deserialize<TimeSliceFilterDto>(filterJson);
            }

            var result = await _TSMgr.Search(filter);
            var toRet = _mapper.Map<IEnumerable<TimeSliceDto>>(result);
            return Ok(toRet);
        }

    }
}
