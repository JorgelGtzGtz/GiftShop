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
    public class CartDataService : ContextService, ICartDataService
    {
        public List<object> ListCartByFilter(int iduser)
        {
            try
            {
                List<Cart> list = new List<Cart>();
                list = _context.Carts.Where(cart => cart.UserID == iduser).AsNoTracking().ToList<Cart>();

                List<object> items = new List<object>();
                foreach (Cart cart in list.OrderBy(o => o.ID))
                {
                    items.Add(new
                    {
                        cart.ID,
                        cart.Product.Name,
                        cart.Product.Description,
                        cart.Quantity,
                        cart.Price,
                        cart.Subtotal,
                        cart.Tax,
                        cart.Total,
                        cart.ProductID,
                        cart.UserID
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

        public bool AddUpdateCart(List<Cart> model, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                List<Cart> cart = _context.Carts.Where(u => u.UserID == model.First().UserID).ToList();

                // Delete children
                foreach (var existingChild in cart)
                {
                    if (!model.Any(c => c.ID == existingChild.ID && c.UserID == existingChild.UserID))
                        _context.Carts.Remove(existingChild);
                }

                // Update and Insert children
                foreach (var childModel in model)
                {
                    var existingChild = cart.SingleOrDefault(c => c.ID == childModel.ID && c.UserID == childModel.UserID && c.ProductID == childModel.ProductID);

                    if (existingChild != null)
                        // Update child
                        _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                    else
                    {
                        // Insert child
                        cart.Add(childModel);
                    }
                }
                _context.SaveChanges();
                result = true;
                errorMessage = "Asignaciones agregadas con exito...";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result;
        }

        public bool RemoveAllCart(int iduser, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                // Delete children
                foreach (var existingChild in _context.Carts.Where(act => act.UserID == iduser))
                {
                    _context.Carts.Remove(existingChild);
                }
                _context.SaveChanges();
                result = true;
                errorMessage = "The cart shop is empty...";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result;
        }
    }
}
