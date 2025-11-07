namespace FileReaderLibrary.Interfaces
{
    public interface IEncryptionStrategy
    {
        string Decrypt(string content);
    }
}
