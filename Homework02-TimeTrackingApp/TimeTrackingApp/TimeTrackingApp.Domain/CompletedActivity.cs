namespace TimeTrackingApp.Domain
{
    public class CompletedActivity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public decimal TimeSpentInMinutes { get; set; }
    }
}
