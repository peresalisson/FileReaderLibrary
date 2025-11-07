# 📚 File Reading Library

A professional C# library demonstrating the evolution of software design through incremental feature development. This project showcases SOLID principles, design patterns, and clean architecture while implementing a versatile file reading system with encryption and role-based security.

## 🎯 Project Overview

This library evolved through 9 versions, each adding new capabilities while maintaining clean code and proper separation of concerns. It supports reading TEXT, XML, and JSON files with optional encryption and role-based security features.

## ✨ Features

### Advanced Capabilities
- 🔐 **Encryption Support** - Decrypt files using pluggable encryption strategies
- 🔒 **Role-Based Security** - Control access based on user roles (Admin, User, Guest)
- 🏭 **Factory Pattern** - Easy reader instantiation
- 🔄 **Strategy Pattern** - Swappable algorithms for encryption and security
- 💉 **Dependency Injection** - Loose coupling and testable code

### Interactive CLI
- 📋 Automatic file discovery (no manual path entry)
- 🎨 Numbered menus for easy navigation
- 🔍 Lists all available files by type
- ↩️ Navigate back at any step
- 📊 Configuration summary after each operation
  
## 🚀 Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or higher

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/FileReadingLibrary.git
   cd FileReadingLibrary
   ```

2. **Build the project**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

### Quick Start

1. Add your test files to the `TestFiles/` directory
2. Run the application
3. Follow the interactive prompts to:
   - Select file type (TEXT, XML, or JSON)
   - Choose a file from the list
   - Enable/disable encryption
   - Enable/disable role-based security
   - View the file content

## 📖 Usage Examples

### Example 1: Reading a Plain Text File

```
Available file types:
  1. TEXT
  2. XML
  3. JSON
  0. Exit

Select file type (1-3) or 0 to exit: 1

Available TEXT files:
  1. sample.txt
  2. encrypted.txt
  3. secure.txt
  0. Go back

Select a file (1-3) or 0 to go back: 1

Use encryption? (y/n): n
Use role-based security? (y/n): n

============================================================
FILE CONTENT
============================================================
Hello World!
This is a plain text file.
============================================================
```

### Example 2: Reading an Encrypted JSON File

```
Select file type (1-3) or 0 to exit: 3

Available JSON files:
  1. sample.json
  2. encrypted.json

Select a file (1-3) or 0 to go back: 2

Use encryption? (y/n): y
Use role-based security? (y/n): n

============================================================
FILE CONTENT
============================================================
{
  "message": "This is encrypted",
  "status": "secret"
}
============================================================
```

### Example 3: Reading with Role-Based Security

```
Select file type (1-3) or 0 to exit: 2

Available XML files:
  1. sample.xml
  2. secure.xml

Select a file (1-3) or 0 to go back: 2

Use encryption? (y/n): n
Use role-based security? (y/n): y

Available roles:
  1. Admin (full access)
  2. User (limited access)
  3. Guest (minimal access)

Select your role (1-3): 2

============================================================
FILE CONTENT
============================================================
<company>
  <data>Public information</data>
  <salary>[REDACTED]</salary>
</company>
============================================================
```

## 🎨 Design Patterns Used

### 1. Strategy Pattern
Used for both encryption and security strategies, allowing algorithms to be selected and swapped at runtime without changing client code.

```csharp
public interface IEncryptionStrategy
{
    string Decrypt(string content);
}

public interface IRoleBasedSecurityStrategy
{
    bool CanRead(string filePath, string role);
    string FilterContent(string content, string role);
}
```

### 2. Factory Pattern
Centralizes object creation logic and simplifies the instantiation of different file readers.

```csharp
IFileReader reader = FileReaderFactory.CreateReader(
    fileType: "json",
    useEncryption: true,
    useSecurity: false
);
```

### 3. Dependency Injection
Services receive their dependencies through constructor injection, promoting loose coupling and testability.

```csharp
public class EncryptedTextFileReader : IFileReader
{
    private readonly IEncryptionStrategy _encryptionStrategy;

    public EncryptedTextFileReader(IEncryptionStrategy encryptionStrategy)
    {
        _encryptionStrategy = encryptionStrategy;
    }
}
```

## 🛠️ Extending the Library

### Adding a New File Type

1. Create a new reader service in `Services/`:
   ```csharp
   public class CsvFileReader : IFileReader
   {
       public string ReadFile(string filePath) { /* ... */ }
   }
   ```

2. Add encrypted and secured versions if needed

3. Update `FileReaderFactory.cs` to handle the new type

### Adding a New Encryption Algorithm

1. Create a new strategy in `Strategies/`:
   ```csharp
   public class AesEncryptionStrategy : IEncryptionStrategy
   {
       public string Decrypt(string content) { /* ... */ }
   }
   ```

2. Update factory to use the new strategy when needed

### Adding a New Security Model

1. Create a new strategy in `Strategies/`:
   ```csharp
   public class LdapSecurityStrategy : IRoleBasedSecurityStrategy
   {
       public bool CanRead(string filePath, string role) { /* ... */ }
       public string FilterContent(string content, string role) { /* ... */ }
   }
   ```

2. Update factory to use the new strategy when needed
