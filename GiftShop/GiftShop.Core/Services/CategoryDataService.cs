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
    public class CategoryDataService : ContextService, ICategoryDataService
    {
        public List<object> ListCategoryByFilter(string description)
        {
            try
            {
                List<Category> list = new List<Category>();
                list = _context.Categories.AsNoTracking().ToList<Category>();

                if (!string.IsNullOrEmpty(description))
                {
                    list = list.FindAll(w => w.Description.ToUpper().Contains(description.ToUpper()));
                }

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

        public bool AddUpdateCategory(Category model, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                if (model.ID == -1)
                {
                    if (_context.Categories.Any(u => u.Description == model.Description))
                    {
                        throw new Exception("Category: " + model.Description + " Existing.");
                    }
                    else
                    {
                        Category newmodel = new Category();
                        newmodel.Description = model.Description;

                        _context.Categories.Add(newmodel);
                        _context.SaveChanges();
                        result = true;
                        errorMessage = "Category successfully created...";
                    }
                }
                else
                {
                    Category update = _context.Categories.FirstOrDefault(u => u.ID == model.ID);
                    _context.Entry(update).CurrentValues.SetValues(model);
                    _context.SaveChanges();
                    result = true;
                    errorMessage = "Successfully updated category...";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result;
        }

        public bool DeleteCategory(int ID, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                Category update = _context.Categories.FirstOrDefault(u => u.ID == ID);
                _context.Entry(update).State = EntityState.Deleted;
                _context.SaveChanges();
                errorMessage = "Category successfully removed...";
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
