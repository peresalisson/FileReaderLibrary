using System;
using System.IO;

namespace FileReaderLibrary
{
    public class TextFileReader
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}