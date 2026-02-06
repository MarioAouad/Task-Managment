using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManager.Application.Contracts.DTOs.Employee;
using TaskManager.Application.Contracts.IMgrs;
using TaskManager.Domain.Models;
using TaskManager.EF;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Members

        private readonly IEmployeeMgr _employeeMgr;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        #endregion

        #region Constructor
        public EmployeeController(IEmployeeMgr employeeMgr, IMapper mapper, AppDbContext context)
        {
            _employeeMgr = employeeMgr;
            _mapper = mapper;
            _context = context;
        }
        #endregion

        #region Queries

        [HttpGet]
        [Route("GetSingle/{Id}")]
        public async Task<IActionResult> GetSingle(long Id)
        {
            var result = await _employeeMgr.GetSingle(Id);
            if (result == null) return NotFound();
            var toRet = _mapper.Map<EmployeeDto>(result);
            toRet.Password = "Hidden";
            return Ok(toRet);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddEmployeeDto employeedto)
        {
            var employee = _mapper.Map<Employee>(employeedto);
            employee.Password = BCrypt.Net.BCrypt.HashPassword(employeedto.Password);
            await _employeeMgr.AddAsync(employee);
            return Ok(employee);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeDto employeedto)
        {
            //var employee = _mapper.Map<Employee>(employeedto);
            //if (Id != employee.Id)
            //    return BadRequest("Id mismatch");

            //var existing = await _employeeMgr.GetSingle(Id);
            //if (existing == null)
            //    return NotFound();

            //await _employeeMgr.UpdateAsync(employee);
            //return NoContent();

            var existing = await _employeeMgr.GetSingle(employeedto.Id);
            if (existing == null)
            {
                return NotFound();
            }
            _mapper.Map(employeedto, existing);
            existing.Password = BCrypt.Net.BCrypt.HashPassword(existing.Password);
            await _employeeMgr.UpdateAsync(existing);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var existing = await _employeeMgr.GetSingle(Id);
            if (existing == null)
                return NotFound();

            await _employeeMgr.DeleteAsync(Id);
            return NoContent();
        }

        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? term, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _employeeMgr.GetPaged(term, page, size);
            var toRet = _mapper.Map<EmployeeDto>(result);
            return Ok(toRet);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetSearch([FromQuery] string? term)
        {
            var result = await _employeeMgr.Search(term);
            var toRet = _mapper.Map<EmployeeDto>(result);
            return Ok(toRet);
        }

        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register(RegisterEmployeeDto dto)
        //{
        //    bool success = await _employeeMgr.RegisterAsync(dto.Username, dto.Email, dto.Password, dto.Phone_Number, dto.Birthdate,
        //        dto.Living_Address, dto.Gender, dto.Salary, dto.RoleId, dto.MaritalStatusId, dto.DepartmentId);
        //    if (!success)
        //    {
        //        return BadRequest("Username already exists.");
        //    }
        //    else
        //    {
        //        var employee = new Employee
        //        {
        //            Username = dto.Username,
        //            Email = dto.Email,
        //            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        //            Phone_Number = dto.Phone_Number,
        //            Birthdate = dto.Birthdate,
        //            Living_Address = dto.Living_Address,
        //            Gender = dto.Gender,
        //            Salary = dto.Salary,
        //            RoleId = dto.RoleId,
        //            DepartmentId = dto.DepartmentId,
        //            MaritalStatusId = dto.MaritalStatusId
        //        };
        //        await _employeeMgr.AddAsync(employee);
        //        return Ok("Registered successfully");
        //    }

        //}

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterEmployeeDto dto)
        {
            bool success = await _employeeMgr.RegisterAsync(dto.Username, dto.Email, dto.Password, dto.Phone_Number, dto.Birthdate,
                dto.Living_Address, dto.Gender, dto.Salary, dto.RoleId, dto.MaritalStatusId, dto.DepartmentId);

            if (!success)
                return BadRequest("Username or Email already exists.");

            return Ok("Registered successfully");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginEmployeeDto dto)
        {
            var emp = await _employeeMgr.LoginAsync(dto.Username, dto.Password);
            if (emp == null) return Ok("Invalid credentials");
            //return Ok($"Welcome {emp.Username}!");
            return Ok(new { message = $"Welcome {emp.Username}!", user = emp.Username });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employee.ToListAsync();
            return Ok(employees);
        }

        #endregion

    }
}
