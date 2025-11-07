namespace FileReaderLibrary.Interfaces
{
    public interface IRoleBasedSecurityStrategy
    {
        bool CanRead(string filePath, string role);
        string FilterContent(string content, string role);
    }
}
