namespace TaskManager.Domain.Models
{
    public class TimeSlice
    {
        public long Id { get; set; }

        public long TaskAssignmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
