namespace TaskManager.Application.Contracts.DTOs.TimeSlice
{
    public class AddTimeSliceDto
    {
        public long TaskAssignmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
