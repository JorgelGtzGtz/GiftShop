using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;
using GiftShop.Core.Exceptions;
using GiftShop.Core.Generic;
using GiftShop.Core.Security;

namespace GiftShop.Core.Services
{
    public class AuthenticationService : ContextService, IAuthenticationService
    {
        public AuthenticationService() : base()
        {
            
        }

        public User Authenticate(string Username, string Password)
        {
            User _usr = _context.Users.FirstOrDefault(user => user.Username == Username);

            if (_usr == null)
                throw new LoginException(Username, "User does not exist");

            if (_usr.IsLocked)
                throw new LoginException(Username, "The user is Locked. Contact the administrator");

            string pwd = EncryptionService.Instance.EncryptPassword(Password, _usr.Salt);

            if (_usr.HashedPassword != pwd)
                throw new LoginException(_usr.Username, "The password is not valid..");

            return _usr;
        }
    }
}
