# 🔐 Argon2id Password Hasher for CSharp

A secure password hashing implementation using the Argon2id algorithm - winner of the Password Hashing Competition and recommended by OWASP.

## 📋 Features

- ✅ Uses Argon2id - the most secure password hashing algorithm
- ✅ Generates cryptographically secure random salts (128-bit)
- ✅ Produces 256-bit password hashes for maximum security
- ✅ Configurable memory, iteration, and parallelism parameters
- ✅ Simple API with just two methods: Hash and Verify
- ✅ Base64 encoding for easy storage in databases
- ✅ Full XML documentation for IntelliSense support
- ✅ Protection against GPU and ASIC attacks
- ✅ Side-channel attack resistance

## 🛠️ Requirements

- .NET Core 3.1+ or .NET Framework 4.7.2+
- Konscious.Security.Cryptography.Argon2 NuGet package
- Windows, Linux, or macOS

## 🚀 Installation

### Install via NuGet Package Manager

```bash
Install-Package Konscious.Security.Cryptography -Version 1.3.0
```

### Or via .NET CLI

```bash
dotnet add package Konscious.Security.Cryptography --version 1.3.0
```

### Add to your project

1. Copy `PasswordHasher.cs` to your project
2. Ensure the Konscious.Security.Cryptography package is installed
3. Add the namespace: `using PasswordHasher;`

## 📖 Usage

### Basic Example

```csharp
using System;
using PasswordHasher;

class Program
{
    static void Main()
    {
        var hasher = new PasswordHasher();

        // Hash a password
        string password = "MySecurePassword123!";
        string hashedPassword = hasher.HashPassword(password);
        Console.WriteLine($"Hashed: {hashedPassword}");

        // Verify a password
        bool isValid = hasher.VerifyPassword(password, hashedPassword);
        Console.WriteLine($"Password is valid: {isValid}");
    }
}
```

## ⚙️ Configuration

### Default Parameters

| Parameter   | Value    | Description                          |
| ----------- | -------- | ------------------------------------ |
| Memory      | 19 MB    | RAM used during hashing              |
| Iterations  | 2        | Number of passes over memory         |
| Parallelism | 1        | Number of parallel threads           |
| Salt Size   | 128 bits | Cryptographically secure random salt |
| Hash Size   | 256 bits | Final hash output size               |

### Security Recommendations

**Modify your parameters**, keep your specific parameter choices (memory, iterations, salt/hash sizes) confidential for better security. Some examples:

- **Low Security (fast)**: Memory: 19MB, Iterations: 2
- **Medium Security (balanced)**: Memory: 64MB, Iterations: 3
- **High Security (slow)**: Memory: 128MB, Iterations: 4

## 🔍 How It Works

1. **Salt Generation**: Creates a unique 128-bit random salt for each password
2. **Key Derivation**: Applies Argon2id algorithm with configured parameters
3. **Storage Format**: Combines salt + hash and encodes as Base64
4. **Verification**: Extracts salt, re-hashes input, and performs constant-time comparison

### Storage Format

```
Base64(Salt[16 bytes] + Hash[32 bytes])
```

Example output:

```
"GqPGkEB6JvwNGkS9MSB8WBhp2XJwR9Xr49TCH7l9aPNQUFSXwEehDmDgMwzCQWZcMH1dIBz0nWQiOZ6YZMfVIg=="
```

## 🛠️ Troubleshooting

### OutOfMemoryException

Reduce the memory parameter:

```csharp
private const int MemorySize = 32768; // 32 MB instead of 64 MB
```

### Performance Issues

- Reduce iterations for development environments
- Consider async hashing for web applications
- Use caching for frequently verified passwords

## 📊 Performance Benchmarks

### Test Environment

- **Device**: LAPTOP-EAGTOJ3F
- **Processor**: 12th Gen Intel(R) Core(TM) i7-1255U @ 1.70 GHz
- **RAM**: 16.0 GB (15.7 GB usable)
- **OS**: Windows 11

### Configuration Used

| Parameter   | Value    |
| ----------- | -------- |
| Memory      | 19 MB    |
| Iterations  | 2        |
| Parallelism | 1        |
| Salt Size   | 128 bits |
| Hash Size   | 256 bits |

### 📈 Performance Results

#### Hashing Performance

| Iteration | Time    | | Iteration | Time    |
|-----------|---------|---|-----------|---------|
| **1**     | 95 ms   | | **6**     | 95 ms   |
| **2**     | 99 ms   | | **7**     | 79 ms   |
| **3**     | 96 ms   | | **8**     | 42 ms   |
| **4**     | 100 ms  | | **9**     | 45 ms   |
| **5**     | 100 ms  | | **10**    | 43 ms   |

#### Verification Performance

| Iteration | Time    | | Iteration | Time    |
|-----------|---------|---|-----------|---------|
| **1**     | 44 ms   | | **6**     | 46 ms   |
| **2**     | 40 ms   | | **7**     | 41 ms   |
| **3**     | 44 ms   | | **8**     | 42 ms   |
| **4**     | 42 ms   | | **9**     | 45 ms   |
| **5**     | 43 ms   | | **10**    | 40 ms   |

#### Password Length Impact

| Password Length          | Hash Time |
| ------------------------ | --------- |
| **Short** (8 chars)      | 44 ms     |
| **Medium** (16 chars)    | 42 ms     |
| **Long** (32 chars)      | 38 ms     |
| **Very Long** (64 chars) | 45 ms     |

### 📊 Summary Statistics

| Metric         | Hashing  | Verification |
| -------------- | -------- | ------------ |
| **Average**    | 71.20 ms | 48.00 ms     |
| **Minimum**    | 38 ms    | 40 ms        |
| **Maximum**    | 105 ms   | 101 ms       |
| **Ops/second** | 14.04    | 20.83        |

## 🔒 Why Argon2id?

- **Winner** of the Password Hashing Competition (2015)
- **OWASP recommended** for password storage
- **Memory-hard** function resistant to GPU/ASIC attacks
- **Side-channel resistant** variant of Argon2
- **Configurable** parameters for different security needs
- **Modern** algorithm designed for current threats

## 📝 Changelog

### v1.0.0

- Initial release
- Argon2id implementation with secure defaults
- Hash and Verify methods
- Comprehensive error handling
- XML documentation

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/enhanced-security`
3. Commit your changes: `git commit -m "Add configurable parameters"`
4. Push to the branch: `git push origin feature/enhanced-security`
5. Submit a Pull Request

## 📚 References

- [Argon2 Specification](https://github.com/P-H-C/phc-winner-argon2/blob/master/argon2-specs.pdf)
- [OWASP Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)
- [RFC 9106 - Argon2](https://datatracker.ietf.org/doc/html/rfc9106)
- [Original Article by ThatSoftwareDude](https://www.thatsoftwaredude.com/content/14030/implementing-argon2id-password-hashing-in-c)

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**diegofercri** — [https://diegofercri.dev](https://diegofercri.dev)

Based on implementation by **ThatSoftwareDude** - [Original Article](https://www.thatsoftwaredude.com/content/14030/implementing-argon2id-password-hashing-in-c)

Enhanced with:

- Comprehensive XML documentation for all methods and parameters
- Optimized memory minimal configuration (19MB) for ultrafast performance following OWASP guidelines
- Detailed inline comments explaining the implementation
- Improved code organization and readability
- Security-focused parameter adjustments
- Complete performance benchmarking and analysis

---

⭐ If you find this helpful, please star the repository!
