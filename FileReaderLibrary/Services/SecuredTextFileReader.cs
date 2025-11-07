using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Services
{
    public class SecuredTextFileReader(IRoleBasedSecurityStrategy securityStrategy, string userRole) : IFileReader
    {
        private readonly IRoleBasedSecurityStrategy _securityStrategy = securityStrategy;
        private readonly string _userRole = userRole;

        public string ReadFile(string filePath)
        {
            if (!_securityStrategy.CanRead(filePath, _userRole))
            {
                throw new UnauthorizedAccessException($"Role '{_userRole}' is not authorized to read this file.");
            }

            string content = File.ReadAllText(filePath);
            return _securityStrategy.FilterContent(content, _userRole);
        }
    }
}