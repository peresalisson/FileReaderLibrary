using FileReaderLibrary.Factories;

namespace FileReaderLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var textReader = FileReaderFactory.CreateReader("txt", false, false, "");
            var xmlReader = FileReaderFactory.CreateReader("xml", false, false, "");
            var encryptedXmlReader = FileReaderFactory.CreateReader("xml", true, false, "");
            var encryptedJsonReader = FileReaderFactory.CreateReader("json", true, false, "");
            var jsonReader = FileReaderFactory.CreateReader("json", false, false, "");

            // Test text file
            Console.WriteLine("=== TEXT FILE ===");
            Console.WriteLine(textReader.ReadFile("TestFiles/sample.txt"));

            // Test XML file
            Console.WriteLine("\n=== XML FILE ===");
            Console.WriteLine(xmlReader.ReadFile("TestFiles/sample.xml"));

            // Test encrypted text file
            var encryptedReader = FileReaderFactory.CreateReader("txt", true, false, "");
            Console.WriteLine("\n=== ENCRYPTED TEXT FILE ===");
            Console.WriteLine(encryptedReader.ReadFile("TestFiles/encrypted.txt"));

            // Test XML as admin
            Console.WriteLine("\n=== READING AS ADMIN ===");
            var adminReader = FileReaderFactory.CreateReader("xml", false, true, "admin");
            Console.WriteLine(adminReader.ReadFile("TestFiles/secure.xml"));

            // Test XML regular user
            Console.WriteLine("\n=== READING AS USER ===");
            var userReader = FileReaderFactory.CreateReader("xml", false, true, "user");
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
            var textFileAdminReader = FileReaderFactory.CreateReader("txt", false, true, "admin");
            Console.WriteLine(textFileAdminReader.ReadFile("TestFiles/secure.txt"));

            // Test Text-File as user
            Console.WriteLine("\n=== READING TEXT AS USER ===");
            var textFileUserReader = FileReaderFactory.CreateReader("txt", false, true, "user");
            Console.WriteLine(textFileUserReader.ReadFile("TestFiles/secure.txt"));

            // Test JSON file
            Console.WriteLine("\n=== JSON FILE ===");
            Console.WriteLine(jsonReader.ReadFile("TestFiles/sample.json"));

            // Test encrypted JSON file
            Console.WriteLine("\n=== ENCRYPTED JSON FILE ===");
            Console.WriteLine(encryptedJsonReader.ReadFile("TestFiles/encrypted.json"));

            Console.WriteLine("\n=== SECURED JSON (User) ===");
            var userJsonReader = FileReaderFactory.CreateReader("json", false, true, "user");
            Console.WriteLine(userJsonReader.ReadFile("TestFiles/secure.json"));

            Console.WriteLine("\n=== SECURED JSON (Admin) ===");
            var adminJsonReader = FileReaderFactory.CreateReader("json", false, true, "admin");
            Console.WriteLine(adminJsonReader.ReadFile("TestFiles/secure.json"));
        }
    }
}