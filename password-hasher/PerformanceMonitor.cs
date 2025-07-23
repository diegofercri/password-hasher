using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace PasswordHasher
{
    /// <summary>
    /// Provides simple performance monitoring for password hashing operations
    /// </summary>
    public class PerformanceMonitor
    {
        private readonly PasswordHasher _hasher;
        private readonly List<long> _hashingTimes;
        private readonly List<long> _verificationTimes;

        public PerformanceMonitor()
        {
            _hasher = new PasswordHasher();
            _hashingTimes = new List<long>();
            _verificationTimes = new List<long>();
        }

        /// <summary>
        /// Runs a complete performance test suite
        /// </summary>
        public void RunPerformanceTest()
        {
            Console.WriteLine("=== Argon2id Password Hasher Performance Test ===\n");

            // Warm up
            Console.WriteLine("Warming up...");
            _hasher.HashPassword("warmup");

            // Single operation test
            Console.WriteLine("\n1. Single Operation Performance:");
            TestSingleOperation();

            // Multiple operations test
            Console.WriteLine("\n2. Multiple Operations Performance (10 iterations):");
            TestMultipleOperations(10);

            // Different password lengths
            Console.WriteLine("\n3. Performance by Password Length:");
            TestDifferentPasswordLengths();

            // Summary
            Console.WriteLine("\n=== Summary ===");
            PrintSummary();
        }

        /// <summary>
        /// Tests a single hash and verify operation
        /// </summary>
        private void TestSingleOperation()
        {
            string password = "TestPassword123!";

            // Hash
            var hashStopwatch = Stopwatch.StartNew();
            string hash = _hasher.HashPassword(password);
            hashStopwatch.Stop();

            Console.WriteLine($"Hash Time: {hashStopwatch.ElapsedMilliseconds} ms");

            // Verify
            var verifyStopwatch = Stopwatch.StartNew();
            bool isValid = _hasher.VerifyPassword(password, hash);
            verifyStopwatch.Stop();

            Console.WriteLine($"Verify Time: {verifyStopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Verification Result: {(isValid ? "Success" : "Failed")}");

            _hashingTimes.Add(hashStopwatch.ElapsedMilliseconds);
            _verificationTimes.Add(verifyStopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Tests multiple operations to get average performance
        /// </summary>
        private void TestMultipleOperations(int iterations)
        {
            string password = "TestPassword123!";
            var hashes = new List<string>();

            // Hashing
            Console.WriteLine("\nHashing Performance:");
            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = Stopwatch.StartNew();
                string hash = _hasher.HashPassword(password);
                stopwatch.Stop();

                hashes.Add(hash);
                _hashingTimes.Add(stopwatch.ElapsedMilliseconds);

                Console.Write($"Iteration {i + 1}: {stopwatch.ElapsedMilliseconds} ms");
                if (i < iterations - 1) Console.Write(", ");
            }

            // Verification
            Console.WriteLine("\n\nVerification Performance:");
            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = Stopwatch.StartNew();
                bool isValid = _hasher.VerifyPassword(password, hashes[i]);
                stopwatch.Stop();

                _verificationTimes.Add(stopwatch.ElapsedMilliseconds);

                Console.Write($"Iteration {i + 1}: {stopwatch.ElapsedMilliseconds} ms");
                if (i < iterations - 1) Console.Write(", ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Tests performance with different password lengths
        /// </summary>
        private void TestDifferentPasswordLengths()
        {
            var testCases = new Dictionary<string, string>
            {
                { "Short (8 chars)", "Pass123!" },
                { "Medium (16 chars)", "MyPassword123!@#" },
                { "Long (32 chars)", "ThisIsAVeryLongPasswordForTesting123!@#$%^&*()" },
                { "Very Long (64 chars)", "ThisIsAnExtremelyLongPasswordUsedForTestingThePerformanceOfArgon2id!@#$%^&*()123" }
            };

            foreach (var testCase in testCases)
            {
                var stopwatch = Stopwatch.StartNew();
                string hash = _hasher.HashPassword(testCase.Value);
                stopwatch.Stop();

                Console.WriteLine($"{testCase.Key}: {stopwatch.ElapsedMilliseconds} ms");
                _hashingTimes.Add(stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Prints performance summary statistics
        /// </summary>
        private void PrintSummary()
        {
            if (_hashingTimes.Count > 0)
            {
                Console.WriteLine("\nHashing Performance:");
                Console.WriteLine($"- Average: {_hashingTimes.Average():F2} ms");
                Console.WriteLine($"- Min: {_hashingTimes.Min()} ms");
                Console.WriteLine($"- Max: {_hashingTimes.Max()} ms");
                Console.WriteLine($"- Operations per second: {1000.0 / _hashingTimes.Average():F2}");
            }

            if (_verificationTimes.Count > 0)
            {
                Console.WriteLine("\nVerification Performance:");
                Console.WriteLine($"- Average: {_verificationTimes.Average():F2} ms");
                Console.WriteLine($"- Min: {_verificationTimes.Min()} ms");
                Console.WriteLine($"- Max: {_verificationTimes.Max()} ms");
                Console.WriteLine($"- Operations per second: {1000.0 / _verificationTimes.Average():F2}");
            }

            // Security recommendation based on performance
            double avgHashTime = _hashingTimes.Average();
            Console.WriteLine("\nSecurity Analysis:");
            if (avgHashTime < 50)
            {
                Console.WriteLine("Very fast hashing detected. Consider increasing memory/iterations for better security.");
            }
            else if (avgHashTime < 100)
            {
                Console.WriteLine("Good balance between security and performance.");
            }
            else if (avgHashTime < 500)
            {
                Console.WriteLine("Strong security configuration. Suitable for high-security applications.");
            }
            else
            {
                Console.WriteLine("Hashing is quite slow. Consider reducing parameters if performance is critical.");
            }
        }
    }
}