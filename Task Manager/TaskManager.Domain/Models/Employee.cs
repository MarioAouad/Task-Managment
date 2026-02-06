namespace TaskManager.Domain.Models
{
    public class Employee
    {
        public long Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal Phone_Number { get; set; }

        public DateTime Birthdate { get; set; }

        public string Living_Address { get; set; } = null!;

        public char Gender { get; set; }

        public int Salary { get; set; }

        public int RoleId { get; set; } 

        public int MaritalStatusId { get; set; }

        public long? DepartmentId { get; set; } = null!;

        public Department? Department { get; set; }
    }
}
