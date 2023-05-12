using System;
using System.Xml.Linq;
using TimeTrackingApp.Domain;
using TimeTrackingApp.Helpers;
using TimeTrackingApp.Services;

UserService userService = new UserService();
//ActivityService activityService = new ActivityService();
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
            Console.WriteLine("1) Reading");
            Console.WriteLine("2) Exercising");
            Console.WriteLine("3) Working");
            Console.WriteLine("4) Hobbies");
            Console.WriteLine("5) Account Management");
            Console.WriteLine("6) Logout");

            bool isChosenActiviteParsed = int.TryParse(Console.ReadLine(), out int chosenActivity);
            if (isChosenActiviteParsed)
            {
                switch (chosenActivity)
                {
                    case 1:
                        Console.WriteLine($"Coutdown for Reading activity is strated.");
                        double timeSpent = 0;
                        var startTime = DateTime.Now;

                        userService.TrackReading();

                        Console.WriteLine("Press enter to stop activity!");
                        Console.ReadLine();

                        var endTime = DateTime.Now;
                        var duration = (double)(endTime - startTime).TotalMinutes;
                        timeSpent += duration;
                        Console.WriteLine($"You spent {duration} minutes on your Activity");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;

                    case 2:
                        Console.WriteLine($"Coutdown for Reading activity is strated.");
                        double timeSpent1 = 0;
                        var startTime1 = DateTime.Now;

                        userService.TrackExercising();

                        Console.WriteLine("Press enter to stop activity!");
                        Console.ReadLine();

                        var endTime1 = DateTime.Now;
                        var duration1 = (double)(endTime1 - startTime1).TotalMinutes;
                        timeSpent1 += duration1;
                        Console.WriteLine($"You spent {duration1} minutes on your Activity");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine($"Coutdown for Reading activity is strated.");
                        double timeSpent2 = 0;
                        var startTime2 = DateTime.Now;

                        userService.TrackWorking();

                        Console.WriteLine("Press enter to stop activity!");
                        Console.ReadLine();

                        var endTime2 = DateTime.Now;
                        var duration2 = (double)(endTime2 - startTime2).TotalMinutes;
                        timeSpent2 += duration2;
                        Console.WriteLine($"You spent {duration2} minutes on your Activity");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine($"Coutdown for Reading activity is strated.");
                        double timeSpent3 = 0;
                        var startTime3 = DateTime.Now;

                        userService.TrackOtherHobbies();

                        Console.WriteLine("Press enter to stop activity!");
                        Console.ReadLine();

                        var endTime3 = DateTime.Now;
                        var duration3 = (double)(endTime3 - startTime3).TotalMinutes;
                        timeSpent3 += duration3;
                        Console.WriteLine($"You spent {duration3} minutes on your Activity");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        userService.AccountManagementMenu(user);

                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    case 6:
                        user = null;
                        Console.WriteLine("Sucessful Logout!");
                        Thread.Sleep(2000);
                        Console.Clear();
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

