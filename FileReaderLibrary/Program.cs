using System;
using FileReaderLibrary.Interfaces;
using FileReaderLibrary.Services;
using FileReaderLibrary.Strategies;

namespace FileReaderLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            IRoleBasedSecurityStrategy security = new SimpleRoleBasedSecurityStrategy();
            
            // Test text file
            TextFileReader textReader = new();
            Console.WriteLine("=== TEXT FILE ===");
            Console.WriteLine(textReader.ReadFile("TestFiles/sample.txt"));

            // Test XML file
            XmlFileReader xmlReader = new();
            Console.WriteLine("\n=== XML FILE ===");
            Console.WriteLine(xmlReader.ReadFile("TestFiles/sample.xml"));

            // Test encrypted text file
            IEncryptionStrategy encryption = new ReverseEncryptionStrategy();
            EncryptedTextFileReader encryptedReader = new(encryption);
            Console.WriteLine("\n=== ENCRYPTED TEXT FILE ===");
            Console.WriteLine(encryptedReader.ReadFile("TestFiles/encrypted.txt"));

            // Test as admin
            Console.WriteLine("\n=== READING AS ADMIN ===");
            SecuredXmlFileReader adminReader = new(security, "admin");
            Console.WriteLine(adminReader.ReadFile("TestFiles/secure.xml"));

            // Test as regular user
            Console.WriteLine("\n=== READING AS USER ===");
            SecuredXmlFileReader userReader = new(security, "user");
            Console.WriteLine(userReader.ReadFile("TestFiles/secure.xml"));

            // Test unauthorized access
            Console.WriteLine("\n=== TRYING TO READ CONFIDENTIAL FILE AS USER ===");
            try
            {
                Console.WriteLine(userReader.ReadFile("TestFiles/confidential.xml"));
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"ACCESS DENIED: {ex.Message}");
            }
        }
    }
}