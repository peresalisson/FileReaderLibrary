using FileReaderLibrary.Interfaces;
using System.Xml.Linq;

namespace FileReaderLibrary.Services
{
    public class EncryptedXmlFileReader(IEncryptionStrategy encryptionStrategy) : IFileReader
    {
        private readonly IEncryptionStrategy _encryptionStrategy = encryptionStrategy;

        public string ReadFile(string filePath)
        {
            string encryptedContent = File.ReadAllText(filePath);
            string decryptedContent = _encryptionStrategy.Decrypt(encryptedContent);

            XDocument doc = XDocument.Parse(decryptedContent);
            return doc.ToString();
        }
    }
}