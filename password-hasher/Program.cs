using System;

namespace PasswordHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            var hasher = new PasswordHasher();

            // Demo
            Console.WriteLine("=== Password Hasher Demo ===");

            string password = "MySecurePassword123!";
            Console.WriteLine($"Original password: {password}");

            // Hash the password
            string hashedPassword = hasher.HashPassword(password);
            Console.WriteLine($"Hashed: {hashedPassword}");

            // Verify correct password
            bool isValid = hasher.VerifyPassword(password, hashedPassword);
            Console.WriteLine($"Verification (correct): {isValid}");

            // Verify incorrect password
            bool isInvalid = hasher.VerifyPassword("WrongPassword", hashedPassword);
            Console.WriteLine($"Verification (incorrect): {isInvalid}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}