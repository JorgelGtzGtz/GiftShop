using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;

namespace GiftShop.Core.Generic
{
    public interface IProductDataService
    {
        List<object> ListProductByFilter(int idcategory, string description);
        bool AddUpdateProduct(Product model, out string errorMessage);
        bool DeleteProduct(int ID, out string errorMessage);
    }
}
