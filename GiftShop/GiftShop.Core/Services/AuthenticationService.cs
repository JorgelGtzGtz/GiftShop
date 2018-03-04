using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;
using GiftShop.Core.Exceptions;
using GiftShop.Core.Generic;

namespace GiftShop.Core.Services
{
    public class AuthenticationService : ContextService, IAuthenticationService
    {
        private readonly IEncryptionService _encryptionService;

        public AuthenticationService(IEncryptionService encryptionService) : base()
        {
            _encryptionService = encryptionService;
        }

        public User Authenticate(string Username, string Password)
        {
            User _usr = _context.Users.FirstOrDefault(user => user.Username == Username);

            if (_usr == null)
                throw new LoginException(Username, "User does not exist"); 

            if (isUserValid(_usr, Password))
            {
                if (!_usr.IsLocked)
                    throw new LoginException(Username, "The user is inactive. Contact the administrator"); 
            }
            else
            {
                throw new LoginException(Username, "The password is invalid");
            }
            return _usr;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
    }
}
