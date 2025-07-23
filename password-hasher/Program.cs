using System;

namespace PasswordHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Argon2id Password Hasher ===");
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Demo (Basic functionality)");
            Console.WriteLine("2. Performance Test");
            Console.WriteLine("3. Exit");
            Console.Write("\nChoice: ");

            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    RunDemo();
                    break;
                case "2":
                    RunPerformanceTest();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void RunDemo()
        {
            var hasher = new PasswordHasher();

            Console.WriteLine("\n=== Password Hasher Demo ===");

            // Demo with predefined password
            string password = "MySecurePassword123!";
            Console.WriteLine($"Original password: {password}");

            // Hash the password
            Console.WriteLine("\nHashing password...");
            string hashedPassword = hasher.HashPassword(password);
            Console.WriteLine($"Hashed: {hashedPassword}");
            Console.WriteLine($"Hash length: {hashedPassword.Length} characters");

            // Verify correct password
            Console.WriteLine("\nVerifying correct password...");
            bool isValid = hasher.VerifyPassword(password, hashedPassword);
            Console.WriteLine($"Verification (correct): {(isValid ? "Success" : "Failed")}");

            // Verify incorrect password
            Console.WriteLine("\nVerifying incorrect password...");
            bool isInvalid = hasher.VerifyPassword("WrongPassword", hashedPassword);
            Console.WriteLine($"Verification (incorrect): {(isInvalid ? "Success (This should be false!)" : "Failed (Expected)")}");

            // Interactive demo
            Console.WriteLine("\n--- Try Your Own Password ---");
            Console.Write("Enter a password to hash: ");
            string userPassword = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(userPassword))
            {
                Console.WriteLine("\nHashing your password...");
                string userHash = hasher.HashPassword(userPassword);
                Console.WriteLine($"Your hash: {userHash}");

                Console.Write("\nEnter password to verify: ");
                string verifyPassword = Console.ReadLine() ?? string.Empty;

                bool userValid = hasher.VerifyPassword(verifyPassword, userHash);
                Console.WriteLine($"Verification: {(userValid ? "Password matches!" : "Password does not match!")}");
            }
        }

        static void RunPerformanceTest()
        {
            var monitor = new PerformanceMonitor();
            monitor.RunPerformanceTest();
        }
    }
}