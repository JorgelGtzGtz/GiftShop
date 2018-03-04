using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;

namespace GiftShop.Core.Generic
{
    public interface ICartDataService
    {
        List<object> ListCartByFilter(int iduser);
        bool AddUpdateCart(List<Cart> model, out string errorMessage);
        bool RemoveAllCart(int iduser, out string errorMessage);
    }
}
