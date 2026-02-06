using TaskManager.Domain.Models;

namespace TaskManager.Application.Contracts.DTOs.Employee
{
    public class LoginEmployeeDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
