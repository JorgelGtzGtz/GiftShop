using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;

namespace GiftShop.Core.Generic
{
    public interface IAuthenticationService
    {
        User Authenticate(string Username, string Password);
    }
}
