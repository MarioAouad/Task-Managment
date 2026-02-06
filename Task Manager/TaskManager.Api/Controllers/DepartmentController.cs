using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using TaskManager.Application.Contracts.DTOs.Departments;
using TaskManager.Application.Contracts.DTOs.TimeSlice;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Models;
using TaskManager.EF;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Members

        private readonly IMapper _mapper;
        private readonly IDepartmentMgr _depMgr;
        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public DepartmentController(IDepartmentMgr depMgr, IMapper mapper, AppDbContext context)
        {
            _depMgr = depMgr;
            _mapper = mapper;
            _context = context;
        }

        #endregion

        #region Queries

        [HttpGet]
        [Route("GetSingle/{Id}")]
        public async Task<IActionResult> GetSingle(long Id)
        {
            var result = await _depMgr.GetSingle(Id);
            if (result == null) return NotFound();
            var toRet = _mapper.Map<DepartmentDto>(result);
            return Ok(toRet);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddDepartmentDto depdto)
        {
            var toCreate = new Department { Name = depdto.Name };
            await _depMgr.AddAsync(toCreate);
            return Ok(toCreate);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentDto depdto)
        {
            var existing = await _depMgr.GetSingle(depdto.Id);
            if (existing == null)
            {
                return NotFound();
            }
            _mapper.Map(depdto, existing);

            await _depMgr.UpdateAsync(existing);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var existing = await _depMgr.GetSingle(Id);
            if (existing == null)
                return NotFound();

            await _depMgr.DeleteAsync(Id);
            return Ok();
        }

        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? searchTerm, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _depMgr.GetPagedDepartments(searchTerm, page, size);
            var toRet = _mapper.Map<IEnumerable<UpdateDepartmentDto>>(result);
            return Ok(toRet);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] string? term)
        {
            var result = await _depMgr.Search(term);
            var toRet = _mapper.Map<IEnumerable<UpdateDepartmentDto>>(result);
            return Ok(toRet);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _context.Department.ToListAsync();
            return Ok(departments);
        }

        #endregion

    }
}