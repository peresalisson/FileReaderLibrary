using FileReaderLibrary.Interfaces;
using System;
using System.IO;
using System.Xml.Linq;

namespace FileReaderLibrary
{
    public class TextFileReader : IFileReader
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }

    public class ReverseEncryptionStrategy : IEncryptionStrategy
    {
        public string Decrypt(string content)
        {
            char[] charArray = content.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    public class EncryptedTextFileReader(IEncryptionStrategy encryptionStrategy) : IFileReader
    {
        private readonly IEncryptionStrategy _encryptionStrategy = encryptionStrategy;

        public string ReadFile(string filePath)
        {
            string encryptedContent = File.ReadAllText(filePath);
            return _encryptionStrategy.Decrypt(encryptedContent);
        }
    }

    public class XmlFileReader : IFileReader
    {
        public string ReadFile(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            return doc.ToString();
        }
    }
}