using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BBGymManagement.Helpers
{
    public static class MD5EncryptionCustom
    {
        public static string MD5Encryption(string encryptionText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
             
            byte[] array = Encoding.UTF8.GetBytes(encryptionText);
             
            array = md5.ComputeHash(array);
             
            StringBuilder sb = new StringBuilder();
            

            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            
            return sb.ToString();
        }

    }
}
