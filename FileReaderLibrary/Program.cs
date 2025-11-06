using System;

namespace FileReaderLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new TextFileReader();

            try
            {
                string content = reader.ReadFile("TestFiles/sample.txt");
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}