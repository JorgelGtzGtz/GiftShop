using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShop.Core.Exceptions
{
    public class LoginException : GiftShopException
    {
        public LoginException(string userName, string message, Exception ex)
            : base(string.Format("{0} : {1}", userName, message), ex)
        {
        }

        public LoginException(string userName, string message) : this(userName, message, null) { }
    }
}
