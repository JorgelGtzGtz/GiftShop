using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;

namespace GiftShop.Core.Generic
{
    public interface ICategoryDataService
    {
        List<object> ListCategoryByFilter(string description);
        bool AddUpdateCategory(Category model, out string errorMessage);
        bool DeleteCategory(int ID, out string errorMessage);
    }
}
