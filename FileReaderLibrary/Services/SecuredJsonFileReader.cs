using FileReaderLibrary.Interfaces;
using System.Text.Json;

namespace FileReaderLibrary.Services
{
    public class SecuredJsonFileReader(IRoleBasedSecurityStrategy securityStrategy, string userRole) : IFileReader
    {
        private readonly IRoleBasedSecurityStrategy _securityStrategy = securityStrategy;
        private readonly string _userRole = userRole;
        private static readonly JsonSerializerOptions CachedOptions = new() { WriteIndented = true };

        public string ReadFile(string filePath)
        {
            if (!_securityStrategy.CanRead(filePath, _userRole))
            {
                throw new UnauthorizedAccessException($"Role '{_userRole}' is not authorized to read this file.");
            }

            string jsonContent = File.ReadAllText(filePath);
            var jsonDoc = JsonDocument.Parse(jsonContent);
            string content = JsonSerializer.Serialize(jsonDoc, CachedOptions);
            return _securityStrategy.FilterContent(content, _userRole);
        }
    }
}