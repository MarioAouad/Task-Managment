namespace TaskManager.Application.Contracts.DTOs.Task
{
    public class AddTaskDto
    {
        public string Name { get; set; } = null!;

        public long? DepartmentId { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public DateTime ClosureDate { get; set; }

        public int StatusId { get; set; }
    }
}
