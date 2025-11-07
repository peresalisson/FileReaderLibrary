using FileReaderLibrary.Interfaces;
using System.Text.Json;

namespace FileReaderLibrary.Services
{
    public class EncryptedJsonFileReader(IEncryptionStrategy encryptionStrategy) : IFileReader
    {
        private readonly IEncryptionStrategy _encryptionStrategy = encryptionStrategy;
        private static readonly JsonSerializerOptions CachedOptions = new() { WriteIndented = true };

        public string ReadFile(string filePath)
        {
            string encryptedContent = File.ReadAllText(filePath);
            string decryptedContent = _encryptionStrategy.Decrypt(encryptedContent);

            var jsonDoc = JsonDocument.Parse(decryptedContent);
            return JsonSerializer.Serialize(jsonDoc, CachedOptions);
        }
    }
}
