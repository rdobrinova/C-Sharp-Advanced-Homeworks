using Diagnostics = System.Diagnostics;
using TimeTrackingApp.DataAccess;
using TimeTrackingApp.Domain;

namespace TimeTrackingApp.Services
{
    public class ActivityService
    {
        private readonly IFileSystemDb<Activity> _activityDb;
        public ActivityService()
        {
            _activityDb = new FileSystemDb<Activity>();
        }

        public void AddActivity()
        {
            Activity activity = new Activity();

            Console.WriteLine("Add activity");

            Console.Write("Name: "); 
            string activityName = Console.ReadLine();

            if(DoesActivityExists(activityName)) throw new Exception($"Activity {activity.Name} already exists!");

            activity.Name = activityName;

            _activityDb.Insert(activity);
        }

        public void DeleteActivity(int id)
        {
            _activityDb.Delete(id);
        }

        public List<Activity> GetAllActivities()
        {
            return _activityDb.GetAll();
        }

        public Activity GetActivityById(int id)
        {
            return _activityDb.GetById(id);
        }

        public bool DoesActivityExists(string name)
        {
            Activity activity = _activityDb.GetAll().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return activity != null;
        }

        public CompletedActivity StartActivity(Activity activity)
        {
            Diagnostics.Stopwatch sw = new Diagnostics.Stopwatch();

            Console.WriteLine($"Press any key to start {activity.Name}...");
            Console.ReadKey();
            Console.Clear();
            sw.Start();
            Console.WriteLine($"Started {activity.Name}");

            Console.WriteLine($"Press any key to finish the {activity.Name}...");
            Console.ReadKey();
            Console.Clear();

            sw.Stop();
            Console.WriteLine($"Finished {activity.Name} in {sw.ElapsedMilliseconds}ms");

            return new CompletedActivity()
            {
                ActivityId = activity.Id,
                Name = activity.Name,
                TimeSpentInMinutes = Math.Round((sw.ElapsedMilliseconds / (decimal)1000) / 60, 2)
            };
        }
    }
}
