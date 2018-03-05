using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Data;
using GiftShop.Core.Generic;
using GiftShop.Core.Security;

namespace GiftShop.Core.Services
{
    public class UsersDataService : ContextService, IUsersDataService
    {
 
        public UsersDataService()
        {
         
        }

        public List<object> ListUserByFilter(string Username, bool? IsLocked, bool? isAdmin)
        {
            try
            {
                List<User> users = new List<User>();
                users = _context.Users.AsNoTracking().ToList<User>();

                if (!string.IsNullOrEmpty(Username))
                {
                    users = users.FindAll(w => w.Username.ToUpper().Contains(Username.ToUpper()));
                }
                if (IsLocked != null)
                {
                    users = users.FindAll(w => w.IsLocked == IsLocked);
                }
                if (isAdmin != null)
                {
                    users = users.FindAll(w => w.IsAdmin == isAdmin);
                }
                List<object> items = new List<object>();
                foreach (User user in users.OrderBy(o => o.ID))
                {
                    items.Add(new
                    {
                        user.ID,
                        user.Username,
                        user.Email,
                        user.Salt,
                        user.HashedPassword,
                        user.IsLocked,
                        user.IsAdmin,
                        user.DateCreated
                    });
                }
                users.Clear();
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddUpdateUser(int ID, string username, string password, string email, bool IsLocked, bool IsAdmin, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                if (ID == -1)
                {
                    if (_context.Users.Any(u => u.Username.ToUpper() == username.ToUpper()))
                    {
                        throw new Exception("Username: " + username + " Existing.");
                    }
                    else
                    {
                        var passwordSalt = EncryptionService.Instance.CreateSalt();

                        User newUser = new User();
                        newUser.Username = username;
                        newUser.Email = email;
                        newUser.Salt = passwordSalt;
                        newUser.HashedPassword = EncryptionService.Instance.EncryptPassword(password, passwordSalt);
                        newUser.IsLocked = IsLocked;
                        newUser.IsAdmin = IsAdmin;
                        newUser.DateCreated = DateTime.Now;

                        _context.Users.Add(newUser);
                        _context.SaveChanges();
                        result = true;
                        errorMessage = "Successfully created user...";
                    }
                }
                else
                {
                    User update = _context.Users.FirstOrDefault(u => u.ID == ID);
                    User currentedituser = _context.Users.FirstOrDefault(u => u.ID == ID);

                    if (currentedituser != null)
                    {
                        currentedituser.Username = username;
                        currentedituser.Email = email;
                        currentedituser.HashedPassword = EncryptionService.Instance.EncryptPassword(password, currentedituser.Salt);
                        currentedituser.IsLocked = IsLocked;
                        currentedituser.IsAdmin = IsAdmin;
                    }

                    _context.Entry(update).CurrentValues.SetValues(currentedituser);
                    _context.SaveChanges();
                    result = true;
                    errorMessage = "User updated successfully...";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            //catch (DbEntityValidationException e)
            //{
            //    string tmperror = String.Empty;
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        tmperror += "Entity of type "+ eve.Entry.Entity.GetType().Name + " in state "+ eve.Entry.State + " has the following validation errors:\n";
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            tmperror += "- Property: "+ ve.PropertyName + ", Error: "+ ve.ErrorMessage;
            //        }
            //    }
            //    errorMessage = tmperror;
            //    throw;
            //    //errorMessage = ex.Message;

            //}
            return result;
        }

        public bool DeleteUser(int ID, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            try
            {
                User update = _context.Users.FirstOrDefault(u => u.ID == ID);
                _context.Entry(update).State = EntityState.Deleted;
                _context.SaveChanges();
                errorMessage = "Successfully deleted user...";
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
