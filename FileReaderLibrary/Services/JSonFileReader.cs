using FileReaderLibrary.Interfaces;
using System.Text.Json;

namespace FileReaderLibrary.Services
{
    public class JsonFileReader : IFileReader
    {
        private static readonly JsonSerializerOptions CachedOptions = new JsonSerializerOptions { WriteIndented = true };

        public string ReadFile(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            var jsonDoc = JsonDocument.Parse(jsonContent);
            return JsonSerializer.Serialize(jsonDoc, CachedOptions);
        }
    }
}