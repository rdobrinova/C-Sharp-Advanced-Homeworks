using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApp.Domain;

namespace TimeTrackingApp.DataAccess
{
    public  interface IUserRepository : IFileSystemDb<User>
    {
         void Update(User user);
    }
}
