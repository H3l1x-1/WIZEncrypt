using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WIZEncrypt
{
    internal class EncryptionUtility
    {

        public static void EncryptFile(string sourceFilePath, string targetFilePath, string password, string salt)
        {
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(password, salt);
                aesAlg.IV = GenerateIV(salt);

                using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (var targetStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(targetStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    sourceStream.CopyTo(cryptoStream);
                }
            }
        }

        public static void DecryptFile(string sourceFilePath, string targetFilePath, string password, string salt)
        {
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(password, salt);
                aesAlg.IV = GenerateIV(salt);

                using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (var targetStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(targetStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    sourceStream.CopyTo(cryptoStream);
                }
            }
        }

        private static byte[] GenerateKey(string password, string salt)
        {
            using (var keyGenerator = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000))
            {
                return keyGenerator.GetBytes(256 / 8); // 256 bits for AES-256
            }
        }

        private static byte[] GenerateIV(string salt)
        {
            using (var keyGenerator = new Rfc2898DeriveBytes(salt, Encoding.UTF8.GetBytes(salt), 10000))
            {
                return keyGenerator.GetBytes(128 / 8); // 128 bits for AES IV
            }
        }
    }
}
