namespace StudentRegistration.BusinessLogic.Helpers
{
    public class EncryptionSettings
    {
        public byte[] Salt { get; set; }

        public int KeySize { get; set; }
        public int SaltSize { get; set; }
        public string EncryptionKey { get; set; }
    }
}
