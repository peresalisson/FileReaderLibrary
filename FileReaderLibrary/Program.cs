using FileReaderLibrary.Factories;
using FileReaderLibrary.Services;

namespace FileReaderLibrary
{
    class Program
    {
        private static readonly FileDiscoveryService _fileDiscovery = new();

        static void Main(string[] args)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("  File Reading Library - Interactive CLI");
            Console.WriteLine("==============================================\n");

            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- New File Reading Session ---\n");

                    string? fileType = GetFileType();
                    if (fileType == null) break;

                    if (!_fileDiscovery.HasFiles(fileType))
                    {
                        Console.WriteLine($"No {fileType.ToUpper()} files found in TestFiles directory.");
                        Console.WriteLine("Please add some test files and try again.");
                        continue;
                    }

                    string? selectedFile = SelectFile(fileType);
                    if (selectedFile == null) continue;

                    bool useEncryption = GetYesNoAnswer("Use encryption?");

                    bool useSecurity = GetYesNoAnswer("Use role-based security?");

                    string? userRole = null;
                    if (useSecurity)
                    {
                        userRole = GetRole();
                    }

                    string filePath = _fileDiscovery.GetFullPath(selectedFile);
                    ReadAndDisplayFile(fileType, filePath, useEncryption, useSecurity, userRole);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"\n[SECURITY ERROR] {ex.Message}");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"\n[FILE ERROR] File not found: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERROR] {ex.Message}");
                }
            }
        }

        private static string? GetFileType()
        {
            Console.WriteLine("Available file types:");
            Console.WriteLine("  1. TEXT");
            Console.WriteLine("  2. XML");
            Console.WriteLine("  3. JSON");
            Console.WriteLine("  0. Exit");
            Console.Write("\nSelect file type (1-3) or 0 to exit: ");

            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    return "text";
                case "2":
                    return "xml";
                case "3":
                    return "json";
                case "0":
                    Console.WriteLine("\nThank you for using File Reading Library. Goodbye!");
                    return null;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    return GetFileType();
            }
        }

        private static string? SelectFile(string fileType)
        {
            var files = _fileDiscovery.GetFilesByType(fileType);

            Console.WriteLine($"\nAvailable {fileType.ToUpper()} files:");
            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {files[i]}");
            }
            Console.WriteLine("  0. Go back");

            Console.Write($"\nSelect a file (1-{files.Count}) or 0 to go back: ");
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int selection))
            {
                if (selection == 0)
                {
                    return null;
                }
                if (selection > 0 && selection <= files.Count)
                {
                    return files[selection - 1];
                }
            }

            Console.WriteLine("Invalid selection. Please try again.");  
            return SelectFile(fileType);
        }

        private static bool GetYesNoAnswer(string question)
        {
            Console.Write($"{question} (y/n): ");
            string? input = Console.ReadLine()?.Trim()?.ToLower();

            if (input == "y" || input == "yes")
                return true;
            if (input == "n" || input == "no")
                return false;

            Console.WriteLine("Please enter 'y' or 'n'.");
            return GetYesNoAnswer(question);
        }

        private static string GetRole()
        {
            Console.WriteLine("\nAvailable roles:");
            Console.WriteLine("  1. Admin (full access)");
            Console.WriteLine("  2. User (limited access)");
            Console.WriteLine("  3. Guest (minimal access)");
            Console.Write("\nSelect your role (1-3): ");

            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    return "admin";
                case "2":
                    return "user";
                case "3":
                    return "guest";
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    return GetRole();
            }
        }

        private static void ReadAndDisplayFile(
            string fileType,
            string filePath,
            bool useEncryption,
            bool useSecurity,
            string? userRole)
        {
            var reader = FileReaderFactory.CreateReader(fileType, useEncryption, useSecurity, userRole);
            string content = reader.ReadFile(filePath);

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("FILE CONTENT");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(content);
            Console.WriteLine(new string('=', 60));

            Console.WriteLine("\nConfiguration:");
            Console.WriteLine($"  File Type: {fileType.ToUpper()}");
            Console.WriteLine($"  Encryption: {(useEncryption ? "Enabled" : "Disabled")}");
            Console.WriteLine($"  Security: {(useSecurity ? $"Enabled (Role: {userRole ?? "Unknown"})" : "Disabled")}");
        }
    }
}