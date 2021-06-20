using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NguyenVanHai.Common
{
    public class Encryptor
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                builder.Append(result[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}