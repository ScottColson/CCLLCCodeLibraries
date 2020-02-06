namespace CCLLC.Core
{
    /// <summary>
    /// Defines encryption service method signatures.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypt a clear text string value into an encrypted string using the supplied encryption key.
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Encrypt(string clearText, string key);

        /// <summary>
        /// Encrypt a clear data byte array value into an encrypted byte array using the supplied encryption key.
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] clearData, string key);

        /// <summary>
        /// Decrypt an encrypted string value using the supplied key and return clear text.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Decrypt(string encryptedText, string key);

        /// <summary>
        /// Decrypt an encrypted byte array using the supplied key and return clear byte array.
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] encryptedData, string key);
    }
}
