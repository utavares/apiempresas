using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ApiEmpresas.Domain.Helpers
{
    /// <summary>
    /// Classe para criptografia de dados no padrão MD5
    /// </summary>
    public class MD5Helper
    {
        public static string Encrypt(string value)
        {
            var hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;
            foreach (var item in hash)
                result += item.ToString("X2"); //Hexadecimal

            return result;
        }
    }
}