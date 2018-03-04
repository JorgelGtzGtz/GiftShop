using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiftShop.Core.Services;

namespace GiftShop.Core.Exceptions
{
    public abstract class GiftShopException : Exception
    {
        public GiftShopException(string message, Exception ex) : base(message, ex) {
            this.Register();
        }

        public GiftShopException(string message) : this(message, null) { }

        protected void Register()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Message);
                if (this.InnerException != null)
                    sb.AppendLine(this.InnerException.Message);

                LoggerService.Instance.Log.Error(sb.ToString(), this);
            }
            catch { }
        }
    }
}
