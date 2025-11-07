namespace FileReaderLibrary.Services
{
    public class FileDiscoveryService(string baseDirectory = "TestFiles")
    {
        private readonly string _baseDirectory = baseDirectory;

        public List<string> GetFilesByType(string fileType)
        {
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
                return [];
            }

            var extensions = GetExtensionsForType(fileType);
            var files = new List<string>();

            foreach (var extension in extensions)
            {
                var foundFiles = Directory.GetFiles(_baseDirectory, $"*.{extension}")
                    .Select(Path.GetFileName)
                    .Where(f => f is not null)
                    .Cast<string>()
                    .ToList();

                files.AddRange(foundFiles);
            }

            return files.OrderBy(f => f).ToList();
        }

        public string GetFullPath(string fileName)
        {
            return Path.Combine(_baseDirectory, fileName);
        }

        private static List<string> GetExtensionsForType(string fileType)
        {
            switch (fileType.ToLower())
            {
                case "text":
                case "txt":
                    return ["txt"];
                case "xml":
                    return ["xml"];
                case "json":
                    return ["json"];
                default:
                    return [];
            }
        }

        public bool HasFiles(string fileType)
        {
            return GetFilesByType(fileType).Count > 0;
        }
    }
}
