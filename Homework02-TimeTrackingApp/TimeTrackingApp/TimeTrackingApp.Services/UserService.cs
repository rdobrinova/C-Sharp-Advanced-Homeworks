using System.Security.Principal;
using TimeTrackingApp.DataAccess;
using TimeTrackingApp.Domain;
using TimeTrackingApp.Services.Interfaces;

namespace TimeTrackingApp.Services
{
    public class UserService : IUserService
    {
        public User CurrentUser { get; set; }
        private IFileSystemDb<User> _userDatabase;

        public UserService()
        {
            _userDatabase = new FileSystemDb<User>();
        }

        public User GetUserByUsername(string username)
        {
            return _userDatabase.GetAll().FirstOrDefault(x => x.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _userDatabase.GetAll().FirstOrDefault(x => x.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase) && x.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase));
        }
        public User GetUserByPassword(string password)
        {
            return _userDatabase.GetAll().FirstOrDefault(x => x.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase));
        }

        public User Login()
        {
            int attempts = 0;
            while (attempts < 3)
            {
                Console.Write("UserName: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                var user = GetUserByUsernameAndPassword(username, password);
                if (user != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login successful!", Console.ForegroundColor);
                    Console.ResetColor();

                    CurrentUser = user;
                    return user;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect username or password. Please try again.");
                    attempts++;
                    continue;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Too many failed login attempts.", Console.ForegroundColor);
            Console.WriteLine("Goodbye!", Console.ForegroundColor);
            Console.ResetColor();
            return null;
            //Environment.Exit(0);
        }

        public User Register()
        {
            try
            {
                Console.Write("FirstName: ");
                string firstName = Console.ReadLine();

                Console.Write("LastName: ");
                string lastName = Console.ReadLine();

                Console.Write("Age: ");
                bool isAgeParsed = int.TryParse(Console.ReadLine(), out int age);

                Console.Write("Username: ");
                string userName = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                User newUser = new User(firstName, lastName, isAgeParsed ? age : -1, userName, password);
                ValidateUser(newUser);

                _userDatabase.Insert(newUser);
                Console.WriteLine("Registered successful!");
                return newUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ValidateUser(User user)
        {

            if (GetUserByUsername(user.UserName) != null)
                throw new Exception("User already exists!");

            if (user.FirstName.Any(char.IsDigit) || user.LastName.Any(char.IsDigit))
                throw new Exception("First Name and Last Name cannot contain a digit!");

            if (user.FirstName.Length < 2 || user.LastName.Length < 2)
                throw new Exception("First Name and Last Name should not be shorter than 2 characters");

            if (user.Age < 0)
                throw new Exception("Invalid age!");

            if (user.Age < 18 || user.Age > 120)
                throw new Exception("The user should not be less than 18 years or over 120");

            if (user.UserName.Length < 5)
                throw new Exception("Username should be at least 5 characters long!");

            if (user.Password.Length < 6)
                throw new Exception("Password should be at least 6 characters long!");

            if (!user.Password.Any(char.IsUpper) || !user.Password.Any(char.IsDigit))
                throw new Exception("Password should contain at least one capital letter and at least one number!");
        }

        public void UpdateUser(User user)
        {
            _userDatabase.Update(user);
        }

        public void TrackReading()
        {
            Console.WriteLine("Please choose a type of reading:");
            Console.WriteLine("1. Belles Lettres");
            Console.WriteLine("2. Fiction");
            Console.WriteLine("3. Professional Literature");
            bool isChosenOptionParsed = int.TryParse(Console.ReadLine(), out int choice);
            if (isChosenOptionParsed)
            {

                string type = "";
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the number of pages you read:");
                        bool isPagesParsed = int.TryParse(Console.ReadLine(), out int pages);
                        if (isPagesParsed)
                        {
                            type = "Belles Lettres";
                            Console.WriteLine("You tracked " + pages + " pages of " + type + " reading");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter the number of pages you read:");
                        bool isPagesParsed1 = int.TryParse(Console.ReadLine(), out int pages1);
                        if (isPagesParsed1)
                        {
                            type = "Fiction";
                            Console.WriteLine("You tracked " + pages1 + " pages of " + type + " reading");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter the number of pages you read:");
                        bool isPagesParsed2 = int.TryParse(Console.ReadLine(), out int pages2);
                        if (isPagesParsed2)
                        {
                            type = "Professional Literature";
                            Console.WriteLine("You tracked " + pages2 + " pages of " + type + " reading");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }

        public void TrackExercising()
        {
            Console.WriteLine("Please select a type of exercise:");
            Console.WriteLine("1. General");
            Console.WriteLine("2. Running");
            Console.WriteLine("3. Sport");
            bool isChosenOptionParsed = int.TryParse(Console.ReadLine(), out int choice);
            if (isChosenOptionParsed)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the duration of your general exercise in minutes:");
                        int duration = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("You tracked " + duration + " minutes of general exercise");
                        break;
                    case 2:
                        Console.WriteLine("Enter the distance you ran in kilometers:");
                        double distance = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("You tracked " + distance + " kilometers of running");
                        break;
                    case 3:
                        Console.WriteLine("Enter the name of the sport you played:");
                        string sport = Console.ReadLine();
                        Console.WriteLine("You tracked " + sport + " sport activity"); ;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        public void TrackWorking()
        {
            Console.WriteLine("Please select a type of work:");
            Console.WriteLine("1. At the office");
            Console.WriteLine("2. From home");
            bool isChosenParsed = int.TryParse(Console.ReadLine(), out int choice);
            if (isChosenParsed)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter duration in minutes:");
                        int duration = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("You have tracked work done at the office.");
                        Console.WriteLine($"You spent {duration} minutes working at the office.");
                        break;
                    case 2:
                        Console.WriteLine("Enter duration in minutes:");
                        int duration1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("You have tracked work done from home.");
                        Console.WriteLine($"You spent {duration1} minutes working from home.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void TrackOtherHobbies()
        {
            Console.WriteLine("Enter name of hobby:");
            string hobby = Console.ReadLine();

            Console.WriteLine("Enter duration in minutes:");
            bool isDurationParsed = int.TryParse(Console.ReadLine(), out int duration);

            Console.WriteLine($"You spent {duration} minutes {hobby}");
        }
        public User AccountManagementMenu()
        {
            Console.WriteLine("1) Change password");
            Console.WriteLine("2) Change FirstName");
            Console.WriteLine("3) Change LastName");
            bool isChosenOptionParsed = int.TryParse(Console.ReadLine(), out int chosenOption);

            if (isChosenOptionParsed)
            {
                switch (chosenOption)
                {
                    case 1:
                        Console.WriteLine("Your old password: ");
                        string oldPassword = Console.ReadLine();

                        if (CurrentUser != null)
                        {
                            Console.WriteLine("New Password:");
                            string newPassword = Console.ReadLine();
                            CurrentUser.Password = newPassword;

                            _userDatabase.Update(CurrentUser);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successful change password!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Password change unsuccessful. Please try again");
                            Console.ResetColor();
                        }

                        break;
                    case 2:
                        Console.WriteLine("Your old FirstName: ");
                        string oldFirstName = Console.ReadLine();

                        if (CurrentUser != null)
                        {
                            Console.WriteLine("New FirstName:");
                            string newFirstName = Console.ReadLine();
                            CurrentUser.FirstName = newFirstName;

                            _userDatabase.Update(CurrentUser);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successful change password!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Password change unsuccessful. Please try again");
                            Console.ResetColor();
                        }
                        break;
                    case 3:
                        Console.WriteLine("Your old LastName: ");
                        string oldLastName = Console.ReadLine();

                        if (CurrentUser != null)
                        {
                            Console.WriteLine("New LastName:");
                            string newLastName = Console.ReadLine();
                            CurrentUser.LastName = newLastName;

                            _userDatabase.Update(CurrentUser);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successful change password!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Password change unsuccessful. Please try again");
                            Console.ResetColor();
                        }
                        break;
                    default:
                        break;
                }
            }


            return null;
        }
    }
}


