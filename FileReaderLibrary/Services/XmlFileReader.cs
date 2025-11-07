using FileReaderLibrary.Interfaces;
using System.Xml.Linq;

namespace FileReaderLibrary.Services
{
    public class XmlFileReader : IFileReader
    {
        public string ReadFile(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            return doc.ToString();
        }
    }
}