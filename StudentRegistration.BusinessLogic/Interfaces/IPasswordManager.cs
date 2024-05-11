namespace StudentRegistration.BusinessLogic.Interfaces
{
    public interface IPasswordManager
    {
        public string Encrypt(string plainText);

        public string Decrypt(string encryptedText);

    }
}
