namespace TaskManager.Application.Contracts.DTOs.TimeSlice
{
    public class TimeSliceFilterDto
    {
        public long? TaskAssignmentId { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
    }
}
