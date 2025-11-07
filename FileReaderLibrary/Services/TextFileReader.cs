using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Services
{
    public class TextFileReader : IFileReader 
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}