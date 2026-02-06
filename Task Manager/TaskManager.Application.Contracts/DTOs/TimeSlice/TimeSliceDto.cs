namespace TaskManager.Application.Contracts.DTOs.TimeSlice
{
    public class TimeSliceDto
    {
        public long Id { get; set; }

        public long TaskAssignmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
