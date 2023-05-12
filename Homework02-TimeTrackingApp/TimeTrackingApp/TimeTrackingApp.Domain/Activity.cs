namespace TimeTrackingApp.Domain
{
    public class Activity : BaseEntity
    {
        public Activity() { }
        public Activity(string name)
        {
            Name = name;
            //TimeSpent = 0;
        }
        public string Name { get; set; }
        //public int TimeSpent { get; set; }
        //public string ExtraInfo { get; set; }
    }
}
