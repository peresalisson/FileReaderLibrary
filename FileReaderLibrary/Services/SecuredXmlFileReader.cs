using FileReaderLibrary.Interfaces;
using System;
using System.Xml.Linq;

namespace FileReaderLibrary.Services
{
    public class SecuredXmlFileReader(IRoleBasedSecurityStrategy securityStrategy, string userRole) : IFileReader
    {
        private readonly IRoleBasedSecurityStrategy _securityStrategy = securityStrategy;
        private readonly string _userRole = userRole;

        public string ReadFile(string filePath)
        {
            if (!_securityStrategy.CanRead(filePath, _userRole))
            {
                throw new UnauthorizedAccessException($"Role '{_userRole}' is not authorized to read this file.");
            }

            XDocument doc = XDocument.Load(filePath);
            string content = doc.ToString();
            return _securityStrategy.FilterContent(content, _userRole);
        }
    }
}