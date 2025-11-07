using FileReaderLibrary.Interfaces;
using FileReaderLibrary.Services;
using FileReaderLibrary.Strategies;

namespace FileReaderLibrary.Factories
{
    public class FileReaderFactory
    {
        public static IFileReader CreateReader(
            string fileType,
            bool useEncryption,
            bool useSecurity,
            string? userRole = null)
        {
            IEncryptionStrategy encryptionStrategy = new ReverseEncryptionStrategy();
            IRoleBasedSecurityStrategy securityStrategy = new SimpleRoleBasedSecurityStrategy();

            // Determine the appropriate file reader based on parameters
            switch (fileType.ToLower())
            {
                case "text":
                case "txt":
                    if (useEncryption)
                        return new EncryptedTextFileReader(encryptionStrategy);
                    else if (useSecurity)
                        return new SecuredTextFileReader(securityStrategy, userRole ?? throw new ArgumentNullException(nameof(userRole), "User role must be provided for secured file reading."));
                    else
                        return new TextFileReader();

                case "xml":
                    if (useEncryption)
                        return new EncryptedXmlFileReader(encryptionStrategy);
                    else if (useSecurity)
                        return new SecuredXmlFileReader(securityStrategy, userRole ?? throw new ArgumentNullException(nameof(userRole), "User role must be provided for secured file reading."));
                    else
                        return new XmlFileReader();

                case "json":
                    if (useEncryption)
                        return new EncryptedJsonFileReader(encryptionStrategy);
                    else if (useSecurity)
                        return new SecuredJsonFileReader(securityStrategy, userRole ?? throw new ArgumentNullException(nameof(userRole), "User role must be provided for secured file reading."));
                    else
                        return new JsonFileReader();

                default:
                    throw new NotSupportedException($"File type '{fileType}' is not supported.");
            }
        }
    }
}