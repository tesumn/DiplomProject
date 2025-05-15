using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DiplomProject.Services
{
    public class EncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(byte[] key) => _key = key;

        public string Encrypt(string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(data);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData)) return string.Empty;

            try
            {
                var dataBytes = Convert.FromBase64String(encryptedData);
                using var aes = Aes.Create();

                aes.Key = _key;
                aes.IV = dataBytes.Take(aes.IV.Length).ToArray();

                using var ms = new MemoryStream(dataBytes, aes.IV.Length, dataBytes.Length - aes.IV.Length);
                using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var reader = new StreamReader(cs);

                return reader.ReadToEnd();
            }
            catch
            {
                return "[DECRYPTION ERROR]";
            }
        }
    }
}