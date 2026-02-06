namespace TaskManager.Domain.Models
{
    public class Task
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public long? DepartmentId { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public DateTime ClosureDate { get; set; }

        public int StatusId { get; set; }

        public Department? Department { get; set; }
    }
}
