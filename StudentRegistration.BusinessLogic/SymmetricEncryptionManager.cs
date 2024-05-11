using Microsoft.Extensions.Options;
using StudentRegistration.BusinessLogic.Helpers;
using StudentRegistration.BusinessLogic.Interfaces;
using System.Security.Cryptography;

public class SymmetricEncryptionManager : IPasswordManager
{
    private readonly IOptions<EncryptionSettings> _config;

    public SymmetricEncryptionManager(IOptions<EncryptionSettings> config) 
    {
        _config = config;
    }

    public string Encrypt(string plainText)
    {
        byte[] key = GenerateKey(_config.Value.EncryptionKey, _config.Value.Salt, _config.Value.KeySize);

        using (var aesAlg = Aes.Create())
        {
            aesAlg.KeySize = _config.Value.KeySize;
            aesAlg.Key = key;
            aesAlg.GenerateIV();

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (var msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(BitConverter.GetBytes(_config.Value.Salt.Length), 0, sizeof(int));
                msEncrypt.Write(_config.Value.Salt, 0, _config.Value.Salt.Length);
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        using (var aesAlg = Aes.Create())
        {
            var encryptedText = Convert.FromBase64String(cipherText);
            byte[] salt = new byte[_config.Value.SaltSize];
            Array.Copy(encryptedText, sizeof(int), salt, 0, _config.Value.SaltSize);
            byte[] key = GenerateKey(_config.Value.EncryptionKey, salt, _config.Value.KeySize);

            aesAlg.KeySize = _config.Value.KeySize;
            aesAlg.Key = key;
            byte[] iv = new byte[aesAlg.BlockSize / 8];
            Array.Copy(encryptedText, sizeof(int) + _config.Value.SaltSize, iv, 0, iv.Length);
            aesAlg.IV = iv;

            using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            using (var msDecrypt = new MemoryStream(encryptedText, sizeof(int) + _config.Value.SaltSize + iv.Length, cipherText.Length - sizeof(int) - _config.Value.SaltSize - iv.Length))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }

    private static byte[] GenerateKey(string encryptionKey, byte[] salt, int keySize)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(encryptionKey, salt, 10000))
        {
            return rfc2898.GetBytes(keySize / 8);
        }
    }
}
