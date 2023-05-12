using System.Security.Principal;
using TimeTrackingApp.DataAccess;
using TimeTrackingApp.Domain;
using TimeTrackingApp.Services.Interfaces;

namespace TimeTrackingApp.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userDatabase;

        public UserService()
        {
            _userDatabase = new UserRepository();
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

            string choice = Console.ReadLine();
            string type = "";
            switch (choice)
            {
                case "1":
                    type = "Belles Lettres";
                    break;
                case "2":
                    type = "Fiction";
                    break;
                case "3":
                    type = "Professional Literature";
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        }

        public void TrackExercising()
        {
            Console.WriteLine("Please select a type of exercise:");
            Console.WriteLine("1. General");
            Console.WriteLine("2. Running");
            Console.WriteLine("3. Sport");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("You have tracked a general exercise session.");
                    break;
                case 2:
                    Console.WriteLine("You have tracked a running session.");
                    break;
                case 3:
                    Console.WriteLine("Please enter the name of the sport:");
                    string sportName = Console.ReadLine();
                    Console.WriteLine("You have tracked a " + sportName + " session.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        public void TrackWorking()
        {
            Console.WriteLine("Please select a type of work:");
            Console.WriteLine("1. At the office");
            Console.WriteLine("2. From home");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("You have tracked work done at the office.");
                    break;
                case 2:
                    Console.WriteLine("You have tracked work done from home.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public void TrackOtherHobbies()
        {
            Console.WriteLine("Please enter the name of your hobby:");
            string hobbyName = Console.ReadLine();
            Console.WriteLine("You have tracked time spent on " + hobbyName + ".");
        }
        public User AccountManagementMenu( )
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
                        
                        if (user != null)
                        {
                            Console.WriteLine("New Password:");
                            string newPassword = Console.ReadLine();
                            List<User> allUsers = _userDatabase.GetAll();
                            var tempUser = allUsers.SingleOrDefault(x => x.Id == user.Id);
                            tempUser.Password = newPassword;


                            _userDatabase.Update(tempUser);


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
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }


            return null;
        }
    }
}


