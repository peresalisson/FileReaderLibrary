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

    public class XmlFileReader : IFileReader
    {
        public string ReadFile(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            return doc.ToString();
        }
    }
}