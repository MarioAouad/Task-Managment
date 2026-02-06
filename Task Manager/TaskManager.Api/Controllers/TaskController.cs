using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts.DTOs.Task;
using TaskManager.Application.Contracts.IMgrs;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskMgr _taskMgr;

        public TaskController(ITaskMgr taskMgr, IMapper mapper)
        {
            _taskMgr = taskMgr;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetSingle/{id}")]
        public async Task<IActionResult> GetSingle(long id)
        {
            var result = await _taskMgr.GetSingle(id);
            if (result == null) return NotFound();
            var toRet = _mapper.Map<TaskDto>(result);
            return Ok(toRet);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddTaskDto taskdto)
        {
            var task = _mapper.Map<Domain.Models.Task>(taskdto);
            await _taskMgr.AddAsync(task);
            return Ok(task);
        }

        // Make the controller simple
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskDto taskdto)
        {
            var task = _mapper.Map<Domain.Models.Task>(taskdto);

            // This must be done at the domain level 
            //var existing = await _Domain.Models.TaskMgr.GetSingle(id);
            //if (existing == null)
            //    return NotFound();

            //await _taskMgr.UpdateAsync(task);
            //return NoContent();

            var existing = await _taskMgr.GetSingle(taskdto.Id);
            if (existing == null)
            {
                return NotFound();
            }
            _mapper.Map(taskdto, existing);

            await _taskMgr.UpdateAsync(existing);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _taskMgr.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? term, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _taskMgr.GetPaged(term, page, size);
            var toRet = _mapper.Map<TaskDto>(result);
            return Ok(toRet);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetSearch([FromQuery] string? term)
        {
            var result = await _taskMgr.Search(term);
            var toRet = _mapper.Map<TaskDto>(result);
            return Ok(toRet);
        }
    }
}
