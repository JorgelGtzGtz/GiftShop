using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;
using GiftShop.Core.Generic;

namespace GiftShop.Core.Services
{
    public class ProductDataService : ContextService, IProductDataService
    {
        public List<object> ListProductByFilter(int idcategory, string description)
        {
            try
            {
                List<Product> list = new List<Product>();
                list = _context.Products.AsNoTracking().ToList<Product>();

                if (idcategory > 0)
                {
                    list =
                        list.Where(i => i.ID == idcategory).ToList();
                }

                if (!string.IsNullOrEmpty(description))
                {
                    list = list.FindAll(w => w.Description.ToUpper().Contains(description.ToUpper()));
                }

                List<object> items = new List<object>();
                foreach (Product user in list.OrderBy(o => o.ID))
                {
                    items.Add(new
                    {
                        user.ID,
                        user.Name,
                        user.Description,
                        user.Price,
                        user.CategoryID
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

        public bool AddUpdateProduct(Product model, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                if (model.ID == -1)
                {
                    if (_context.Products.Any(u => u.Name == model.Name))
                    {
                        throw new Exception("Product: " + model.Description + " Existing.");
                    }
                    else
                    {
                        Product newmodel = new Product();
                        newmodel.Name = model.Name    ;
                        newmodel.Description = model.Description;
                        newmodel.Price = model.Price;
                        newmodel.CategoryID = model.CategoryID;

                        _context.Products.Add(newmodel);
                        _context.SaveChanges();
                        result = true;
                        errorMessage = "product successfully created...";
                    }
                }
                else
                {
                    Product update = _context.Products.FirstOrDefault(u => u.ID == model.ID);
                    _context.Entry(update).CurrentValues.SetValues(model);
                    _context.SaveChanges();
                    result = true;
                    errorMessage = "Successfully updated product...";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result;
        }

        public bool DeleteProduct(int ID, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                Product update = _context.Products.FirstOrDefault(u => u.ID == ID);
                _context.Entry(update).State = EntityState.Deleted;
                _context.SaveChanges();
                errorMessage = "Product successfully removed...";
                result = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result;
        }
    }
}
