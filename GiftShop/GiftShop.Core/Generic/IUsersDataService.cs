using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShop.Core.Generic
{
    public interface IUsersDataService
    {
        List<object> ListUserByFilter(string Username, bool? IsLocked, bool? isAdmin);

        bool AddUpdateUser(int ID, string username, string password, string email, bool IsLocked, bool IsAdmin,
            out string errorMessage);

        bool DeleteUser(int ID, out string errorMessage);
    }
}
