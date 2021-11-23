using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common
{
    /// <summary>
    /// 字符串加密类
    /// </summary>
    public class StringSecurity
    {
        private StringSecurity() { }
        #region DES加密/解密
        private static byte[] key = Encoding.ASCII.GetBytes("uiertysd");//密钥
        private static byte[] iv = Encoding.ASCII.GetBytes("99008855");//向量
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string DESEncrypt(string inputString)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamWriter sw = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                sw = new StreamWriter(cs);
                sw.Write(inputString);
                sw.Flush();
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string DESDecrypt(string inputString)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamReader sr = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                ms = new MemoryStream(Convert.FromBase64String(inputString));
                cs = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            finally
            {
                if (sr != null) sr.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }
        #endregion
    }
}
