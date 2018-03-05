using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GiftShop.Core.Security
{
    public sealed class EncryptionService
    {
        private static volatile EncryptionService _encryptor;
        private static object syncRoot = new Object();

        private EncryptionService() { }

        public static EncryptionService Instance
        {
            get
            {
                if (_encryptor == null)
                {
                    lock (syncRoot)
                    {
                        if (_encryptor == null)
                            _encryptor = new EncryptionService();
                    }
                }

                return _encryptor;
            }
        }

        public string CreateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        public string EncryptPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
    }
}
