namespace CCLLC.Core
{
    public interface IEncryptionService
    {
        string Encrypt(string clearText, string key);
        byte[] Encrypt(byte[] clearData, string key);
        string Decrypt(string encryptedText, string key);
        byte[] Decrypt(byte[] encryptedData, string key);
    }
}
