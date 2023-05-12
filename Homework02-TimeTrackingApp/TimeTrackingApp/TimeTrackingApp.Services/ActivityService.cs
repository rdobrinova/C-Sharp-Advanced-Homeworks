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

        public void AddActivity(Activity activity)
        {
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

        public decimal StartActivity(Activity activity)
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

            return Math.Round((sw.ElapsedMilliseconds / (decimal)1000) / 60, 2);
        }
    }
}
