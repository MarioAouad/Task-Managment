namespace TaskManager.Application.Contracts.DTOs.TaskAssignment
{
    public class TaskAssignmentDto
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long EmployeeId { get; set; }
    }
}
