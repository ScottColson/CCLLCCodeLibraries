namespace CCLLC.Core
{
    /// <summary>
    /// Defines encryption service method signatures.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypt a clear text string value into an encrypted string using the supplied encryption key. 
        /// If a key is not provided, decryption is based on the implementations default key value.
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Encrypt(string clearText, string key = null);

        /// <summary>
        /// Encrypt a clear data byte array value into an encrypted byte array using the supplied encryption
        /// key. If a key is not provided, decryption is based on the implementations default key value.
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] clearData, string key = null);

        /// <summary>
        /// Decrypt an encrypted string value using the supplied key and return clear text. If a key is not 
        /// provided, decryption is based on the implementations default key value.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Decrypt(string encryptedText, string key = null);

        /// <summary>
        /// Decrypt an encrypted byte array using the supplied key and return clear byte array.
        /// If a key is not provided, decryption is based on the implementations default key value.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] encryptedData, string key = null);
    }
}
