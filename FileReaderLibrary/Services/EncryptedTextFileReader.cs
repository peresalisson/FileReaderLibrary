using FileReaderLibrary.Interfaces;
using System.IO;

namespace FileReaderLibrary.Services
{
    public class EncryptedTextFileReader(IEncryptionStrategy encryptionStrategy) : IFileReader
    {
        private readonly IEncryptionStrategy _encryptionStrategy = encryptionStrategy;

        public string ReadFile(string filePath)
        {
            string encryptedContent = File.ReadAllText(filePath);
            return _encryptionStrategy.Decrypt(encryptedContent);
        }
    }
}