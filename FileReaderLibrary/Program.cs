using FileReaderLibrary.Interfaces;
using System;

namespace FileReaderLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Reading Library - Version 2");
            Console.WriteLine("Reading text and XML files...\n");

            // Test text file
            TextFileReader textReader = new();
            Console.WriteLine("=== TEXT FILE ===");
            Console.WriteLine(textReader.ReadFile("TestFiles/sample.txt"));

            // Test XML file
            XmlFileReader xmlReader = new();
            Console.WriteLine("\n=== XML FILE ===");
            Console.WriteLine(xmlReader.ReadFile("TestFiles/sample.xml"));

        }
    }
}