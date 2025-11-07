using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Strategies
{
    public class SimpleRoleBasedSecurityStrategy : IRoleBasedSecurityStrategy
    {
        public bool CanRead(string filePath, string role)
        {
            if (role.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                return true;

            // Non-admins can't read confidential files
            return !filePath.Contains("confidential", StringComparison.CurrentCultureIgnoreCase);
        }

        public string FilterContent(string content, string role)
        {
            if (role.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                return content;

            // Filter out sensitive content for non-admins
            return content.Replace("CONFIDENTIAL", "[REDACTED]");
        }
    }
}