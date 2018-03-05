using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShop.Core.Data
{
    public abstract class ContextService
    {
        protected GiftShopEntities _context;

        public ContextService()
        {
            _context = new GiftShopEntities();
        }

        ~ContextService()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
