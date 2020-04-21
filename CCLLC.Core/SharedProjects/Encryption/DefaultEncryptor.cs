using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CCLLC.Core
{
    /// <summary>
    /// Implements <see cref="IEncryptionService"/> using the Rijndael algorithm.
    /// </summary>
    public class DefaultEncryptor : IEncryptionService
    {
        protected string DefaultKey { get; set; }
        protected byte[] Salt { get; set; }
      
        public DefaultEncryptor() 
            : this("7a5a64brEgaceqenuyegac7era3Ape6aWatrewegeka94waqegayathudrebuc7t")
        {           
        }

        public DefaultEncryptor(string key)
        {
            DefaultKey = key;
            Salt = new byte[] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 };
        }

        public virtual string Decrypt(string cipherText, string key = null)
        {
            byte[] cipherData = Convert.FromBase64String(cipherText);
            byte[] clearData = this.Decrypt(cipherData, key ?? DefaultKey);
            return Encoding.Unicode.GetString(clearData);
        }

        public virtual byte[] Decrypt(byte[] cipherData, string key = null)
        {
            PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(key ?? DefaultKey, Salt);
            return this.Decrypt(cipherData, passwordDeriveByte.GetBytes(32), passwordDeriveByte.GetBytes(16));
        }

        private byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael key = Rijndael.Create();
            key.Key = Key;
            key.IV = IV;
            CryptoStream cryptoStream = new CryptoStream(memoryStream, key.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, (int)cipherData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public virtual string Encrypt(string clearText, string key = null)
        {
            byte[] clearData = Encoding.Unicode.GetBytes(clearText);
            byte[] encryptedData = this.Encrypt(clearData, key ?? DefaultKey);
            return Convert.ToBase64String(encryptedData);
        }

        public virtual byte[] Encrypt(byte[] clearData, string key = null)
        {
            PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(key ?? DefaultKey, Salt);
            return this.Encrypt(clearData, passwordDeriveByte.GetBytes(32), passwordDeriveByte.GetBytes(16));
        }

        private byte[] Encrypt(byte[] clearData, byte[] key, byte[] vector)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael encryptor = Rijndael.Create();
            encryptor.Key = key;
            encryptor.IV = vector;
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(clearData, 0, (int)clearData.Length);
            cryptoStream.Close();
            byte[] array = memoryStream.ToArray();
            byte[] numArray = array;
            return numArray;
        }
    }
}