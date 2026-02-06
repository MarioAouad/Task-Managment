using TaskManager.Domain.Models;
using AutoMapper;
using TaskManager.Application.Contracts.DTOs.Departments;
using TaskManager.Application.Contracts.DTOs.Employee;
using TaskManager.Application.Contracts.DTOs.Task;
using TaskManager.Application.Contracts.DTOs.TaskAssignment;
using TaskManager.Application.Contracts.DTOs.TimeSlice;

namespace TaskManager.Application
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, EmployeeDto>()        //Employee to EmployeeDTO
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone_Number, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.Living_Address, opt => opt.MapFrom(src => src.Living_Address))
                .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(src => src.MaritalStatusId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department != null ? src.Department.Id : 0));
            //.ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.WorksOns.Select(w => w.Project)));

            CreateMap<EmployeeDto, Employee>()         //EmployeeDTO to Employee
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone_Number, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.Living_Address, opt => opt.MapFrom(src => src.Living_Address))
                .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(src => src.MaritalStatusId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForPath(dest => dest.Department.Id, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<AddEmployeeDto, Employee>()         //AddEmployeeDTO to Employee
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone_Number, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.Living_Address, opt => opt.MapFrom(src => src.Living_Address))
                .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(src => src.MaritalStatusId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<UpdateEmployeeDto, Employee>()         //UpdateEmployeeDTO to Employee
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone_Number, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.Living_Address, opt => opt.MapFrom(src => src.Living_Address))
                .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(src => src.MaritalStatusId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            // Department ↔ DTO
            CreateMap<DepartmentDto, Department>()          //DEPARTMENT to DepartmentDto                
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Department, DepartmentDto>()          //DEPARTMENT to DepartmentDto                
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdateDepartmentDto,Department>()          //DEPARTMENT to DepartmentDto                
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            // Task ↔ DTO
            CreateMap<Domain.Models.Task, TaskDto>()                //PROJECT to ProjectDTO
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.ClosureDate, opt => opt.MapFrom(src => src.ClosureDate))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId));

            CreateMap<TaskDto, Domain.Models.Task>()                //ProjectDTO to PROJECT
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.ClosureDate, opt => opt.MapFrom(src => src.ClosureDate))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId));

            CreateMap<AddTaskDto, Domain.Models.Task>()                //ProjectDTO to PROJECT
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.ClosureDate, opt => opt.MapFrom(src => src.ClosureDate))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId));

            CreateMap<UpdateTaskDto, Domain.Models.Task>()                //ProjectDTO to PROJECT
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.ClosureDate, opt => opt.MapFrom(src => src.ClosureDate))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId));

            // TaskAssignment → DTO
            CreateMap<TaskAssignment, TaskAssignmentDto>()              //WORKS_ON to Works_onDto
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            CreateMap<TaskAssignmentDto, TaskAssignment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            CreateMap<AddTaskAssignmentDto, TaskAssignment>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            CreateMap<UpdateTaskAssignmentDto, TaskAssignment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            // TimeSlice → DTO
            CreateMap<TimeSlice, TimeSliceDto>()              //WORKS_ON to Works_onDto
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskAssignmentId, opt => opt.MapFrom(src => src.TaskAssignmentId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));


            CreateMap<TimeSliceDto, TimeSlice>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskAssignmentId, opt => opt.MapFrom(src => src.TaskAssignmentId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

            CreateMap<AddTimeSliceDto, TimeSlice>()
                .ForMember(dest => dest.TaskAssignmentId, opt => opt.MapFrom(src => src.TaskAssignmentId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

            CreateMap<UpdateTimeSliceDto, TimeSlice>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskAssignmentId, opt => opt.MapFrom(src => src.TaskAssignmentId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        }
    }
}