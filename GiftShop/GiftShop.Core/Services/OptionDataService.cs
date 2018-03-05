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
        public List<object> ListProduct()
        {
            try
            {
                List<Product> list = new List<Product>();
                list = _context.Products.AsNoTracking().ToList<Product>();

                List<object> items = new List<object>();
                foreach (Product prod in list.OrderBy(o => o.ID))
                {
                    items.Add(new
                    {
                        prod.ID,
                        prod.Name,
                        prod.Description,
                        prod.Price,
                        prod.CategoryID
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
