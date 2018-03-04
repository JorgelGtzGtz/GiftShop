using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;
using GiftShop.Core.Generic;

namespace GiftShop.Core.Services
{
    public class OptionDataService : ContextService, IOptionDataService
    {
        public List<object> ListCategory()
        {
            try
            {
                List<Category> list = new List<Category>();
                list = _context.Categories.AsNoTracking().ToList<Category>();

                List<object> items = new List<object>();
                foreach (Category cat in list.OrderBy(o => o.ID))
                {
                    items.Add(new
                    {
                        cat.ID,
                        cat.Description
                    });
                }
                list.Clear();
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
