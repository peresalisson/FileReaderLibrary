using FileReaderLibrary.Interfaces;
using System;

namespace FileReaderLibrary.Strategies
{
    public class ReverseEncryptionStrategy : IEncryptionStrategy
    {
        public string Decrypt(string content)
        {
            char[] charArray = content.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}