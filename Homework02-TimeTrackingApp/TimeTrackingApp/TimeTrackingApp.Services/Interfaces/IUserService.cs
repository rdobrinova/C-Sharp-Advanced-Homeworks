using TimeTrackingApp.Domain;

namespace TimeTrackingApp.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserByUsername(string username);
        User GetUserByUsernameAndPassword(string username, string password);
        User Login();
        User Register();
        void ValidateUser(User user);
    }
}