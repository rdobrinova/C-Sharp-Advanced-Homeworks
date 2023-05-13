using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApp.Domain;

namespace TimeTrackingApp.DataAccess
{
    public class UserRepository : FileSystemDb<User>, IUserRepository
    {
        public void Update(User user)
        {
            
            List<User> users = Reader();
            User selectedUser = users.SingleOrDefault(x => x.Id == user.Id);
            if (user == null)
            {
                throw new Exception("");
            }
            //selectedUser = user
            selectedUser.Password = user.Password;
            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;

            Writer(users);
        }
    }
}
