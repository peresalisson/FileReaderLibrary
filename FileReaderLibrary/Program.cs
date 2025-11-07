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
            IEncryptionStrategy encryption = new ReverseEncryptionStrategy();
            TextFileReader textReader = new();
            XmlFileReader xmlReader = new();
            EncryptedXmlFileReader encryptedXmlReader = new (encryption);
            EncryptedJsonFileReader encryptedJsonReader = new(encryption);
            JsonFileReader jsonReader = new();

            // Test text file
            Console.WriteLine("=== TEXT FILE ===");
            Console.WriteLine(textReader.ReadFile("TestFiles/sample.txt"));

            // Test XML file
            Console.WriteLine("\n=== XML FILE ===");
            Console.WriteLine(xmlReader.ReadFile("TestFiles/sample.xml"));

            // Test encrypted text file
            EncryptedTextFileReader encryptedReader = new(encryption);
            Console.WriteLine("\n=== ENCRYPTED TEXT FILE ===");
            Console.WriteLine(encryptedReader.ReadFile("TestFiles/encrypted.txt"));

            // Test XML as admin
            Console.WriteLine("\n=== READING AS ADMIN ===");
            SecuredXmlFileReader adminReader = new(security, "admin");
            Console.WriteLine(adminReader.ReadFile("TestFiles/secure.xml"));

            // Test XML regular user
            Console.WriteLine("\n=== READING AS USER ===");
            SecuredXmlFileReader userReader = new(security, "user");
            Console.WriteLine(userReader.ReadFile("TestFiles/secure.xml"));

            // Test unauthorized access for XML
            Console.WriteLine("\n=== TRYING TO READ CONFIDENTIAL FILE AS USER ===");
            try
            {
                Console.WriteLine(userReader.ReadFile("TestFiles/confidential.xml"));
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"ACCESS DENIED: {ex.Message}");
            }

            // Test encrypted XML file
            Console.WriteLine("\n=== ENCRYPTED XML FILE ===");
            Console.WriteLine(encryptedXmlReader.ReadFile("TestFiles/encrypted.xml"));

            // Test Text-File as admin
            Console.WriteLine("\n=== READING TEXT AS ADMIN ===");
            SecuredTextFileReader textFileAdminReader = new(security, "admin");
            Console.WriteLine(textFileAdminReader.ReadFile("TestFiles/secure.txt"));

            // Test Text-File as user
            Console.WriteLine("\n=== READING TEXT AS USER ===");
            SecuredTextFileReader textFileUserReader = new(security, "user");
            Console.WriteLine(textFileUserReader.ReadFile("TestFiles/secure.txt"));

            // Test JSON file
            Console.WriteLine("\n=== JSON FILE ===");
            Console.WriteLine(jsonReader.ReadFile("TestFiles/sample.json"));

            // Test encrypted JSON file
            Console.WriteLine("\n=== ENCRYPTED JSON FILE ===");
            Console.WriteLine(encryptedJsonReader.ReadFile("TestFiles/encrypted.json"));
        }
    }
}