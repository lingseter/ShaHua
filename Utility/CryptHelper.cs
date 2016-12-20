using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class CryptHelper
    {
        /// <summary>
        /// des encrypt
        /// </summary>
        /// <param name="strText">source text</param>
        /// <param name="strEncrKey">8 bytes</param>
        /// <returns></returns>
        public static string DesEncrypt(string strText, string strEncrKey)
        {
            if (string.IsNullOrEmpty(strText) || string.IsNullOrEmpty(strEncrKey))
                return strText;
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, strEncrKey.Length));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// des decrypt
        /// </summary>
        /// <param name="strText">encrypted text</param>
        /// <param name="sDecrKey">8 bytes</param>
        /// <returns></returns>
        public static string DesDecrypt(string strText, string sDecrKey)
        {
            if (string.IsNullOrEmpty(strText) || string.IsNullOrEmpty(sDecrKey)) return strText;
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new byte[strText.Length];
            byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = new UTF8Encoding();
            return encoding.GetString(ms.ToArray());
        }

        /// <summary>
        /// tripledes encrypt
        /// </summary>
        /// <param name="strText">source text</param>
        /// <param name="encryptKey">24 bytes</param>
        /// <returns></returns>
        public static string TripleDesEncrypt(string strText, string encryptKey)
        {
            if (string.IsNullOrEmpty(strText) || string.IsNullOrEmpty(encryptKey))
                return string.Empty;
            byte[] byteKey = Encoding.UTF8.GetBytes(encryptKey);
            byte[] iV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = byteKey;
                tdsAlg.IV = iV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = tdsAlg.CreateEncryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(strText);
                        }
                        return Encoding.UTF8.GetString(msEncrypt.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// tripledes decrypt
        /// </summary>
        /// <param name="strText">encrypt text</param>
        /// <param name="encryptKey">24 bytes</param>
        /// <returns></returns>
        public static string TripleDesDecrypt(string strText, string encryptKey)
        {
            if (string.IsNullOrEmpty(strText) || string.IsNullOrEmpty(encryptKey))
                return string.Empty;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            byte[] byteKey = Encoding.UTF8.GetBytes(encryptKey);
            byte[] iV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] cipherText = Encoding.UTF8.GetBytes(strText);
            // Create an TripleDESCryptoServiceProvider object
            // with the specified key and IV.
            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = byteKey;
                tdsAlg.IV = iV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.UTF8.GetString(result);
        }

        public static string MD5Decrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.TransformFinalBlock(Encoding.Default.GetBytes(strText), 0, strText.Length);
            return Encoding.UTF8.GetString(result);
        }

        public static string SHA1Encrypt(string strText)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(Encoding.UTF8.GetBytes(strText));
            return BitConverter.ToString(result);
        }
    }
}
