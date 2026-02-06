using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TaskManager.Application.Contracts.DTOs.TaskAssignment;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentController : ControllerBase
    {
        #region Members

        private readonly ITaskAssignmentMgr _TAMgr;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public TaskAssignmentController(ITaskAssignmentMgr TAMgr, IMapper mapper)
        {
            _TAMgr = TAMgr;
            _mapper = mapper;
        }
        #endregion

        #region Queries

        [HttpGet]
        [Route("GetSingle/{Id}")]
        public async Task<IActionResult> GetSingle(long Id)
        {
            var result = await _TAMgr.GetSingle(Id);
            if (result == null) return NotFound();
            var toRet = _mapper.Map<TaskAssignmentDto>(result);
            return Ok(toRet);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddTaskAssignmentDto tadto)
        {
            var ta = _mapper.Map<TaskAssignment>(tadto);
            await _TAMgr.AddAsync(ta);
            return Ok(ta);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskAssignmentDto tadto)
        {
            //var employee = _mapper.Map<Employee>(employeedto);
            //if (Id != employee.Id)
            //    return BadRequest("Id mismatch");

            //var existing = await _employeeMgr.GetSingle(Id);
            //if (existing == null)
            //    return NotFound();

            //await _employeeMgr.UpdateAsync(employee);
            //return NoContent();

            var existing = await _TAMgr.GetSingle(tadto.Id);
            if (existing == null)
            {
                return NotFound();
            }
            _mapper.Map(tadto, existing);
            await _TAMgr.UpdateAsync(existing);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var existing = await _TAMgr.GetSingle(Id);
            if (existing == null)
                return NotFound();

            await _TAMgr.DeleteAsync(Id);
            return NoContent();
        }

        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? term, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _TAMgr.GetPaged(term, page, size);
            var toRet = _mapper.Map<TaskAssignmentDto>(result);
            return Ok(toRet);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetSearch([FromQuery] string? term)
        {
            var result = await _TAMgr.Search(term);
            var toRet = _mapper.Map<TaskAssignmentDto>(result);
            return Ok(toRet);
        }
        #endregion

    }
}
