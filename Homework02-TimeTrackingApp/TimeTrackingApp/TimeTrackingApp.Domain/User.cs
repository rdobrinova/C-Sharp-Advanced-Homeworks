namespace TimeTrackingApp.Domain
{
    public class User : BaseEntity
    {
        public User(string firstName, string lastName, int age, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UserName = userName;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<CompletedActivity> CompletedActivities { get; set; } = new List<CompletedActivity>();
    }
}

