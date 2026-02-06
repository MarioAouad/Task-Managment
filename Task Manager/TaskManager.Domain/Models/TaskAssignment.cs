namespace TaskManager.Domain.Models
{
    public class TaskAssignment
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public Task Task { get; set; } = null!;
    }
}
