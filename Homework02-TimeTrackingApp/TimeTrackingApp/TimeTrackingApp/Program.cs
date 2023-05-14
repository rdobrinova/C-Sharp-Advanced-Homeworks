using System;
using System.Xml.Linq;
using TimeTrackingApp.Domain;
using TimeTrackingApp.Helpers;
using TimeTrackingApp.Services;

UserService userService = new UserService();
ActivityService activityService = new ActivityService();
try
{
    User user = null;
    while (user == null)
    {
        Console.Clear();
        Console.WriteLine("WELCOME");
        Console.WriteLine("1) Login \n2) Register");
        Console.Write("> ");
        bool isChosenOptionParsed = int.TryParse(Console.ReadLine(), out int chosenOption);
        if (isChosenOptionParsed)
        {
            switch (chosenOption)
            {
                case 1:
                    user = userService.Login();
                    break;
                case 2:
                    user = userService.Register();
                    break;
                default:
                    Console.WriteLine("Chosen option does not exist!");
                    break;
            }
            Thread.Sleep(2000);
            Console.Clear();
        }

        while (user != null)
        {
            Console.WriteLine("1) Start Activity");
            Console.WriteLine("2) Activity Management");
            Console.WriteLine("3) Account Management");
            Console.WriteLine("4) Logout");

            bool isValidMenuChoice = int.TryParse(Console.ReadLine(), out int menuChoice);
            if (isValidMenuChoice)
            {
                switch (menuChoice)
                {
                    case 1:
                        List<Activity> activities = activityService.GetAllActivities();

                        for (int i = 0; i < activities.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {activities[i].Name}");
                        }

                        Console.Write("Choose activity: ");
                        bool isActivityValid = int.TryParse(Console.ReadLine(), out int validActivityId);

                        if (!isActivityValid || validActivityId > activities.Count) throw new Exception("Invalid activity!");

                        Activity chosenActivity = activityService.GetActivityById(validActivityId);

                        CompletedActivity completedActivity = activityService.StartActivity(chosenActivity);

                        user.CompletedActivities.Add(completedActivity);

                        userService.UpdateUser(user);


                        break;

                    case 2:
                        activityService.AddActivity();


                        break;
                    case 3:
                        Console.Clear();
                        userService.AccountManagementMenu();

                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    case 4:
                        user = null;
                        Console.Clear();
                        Console.WriteLine("Sucessful Logout!");
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }
    }
}
catch (Exception ex)
{
    ConsoleHelper.WriteLineInColor($"[ERROR] {ex.Message}", ConsoleColor.DarkRed);
}

