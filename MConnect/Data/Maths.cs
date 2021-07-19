using System;
using System.Collections.Generic;

namespace MConnect.Data
{
    public static class Maths
    {
        public static List<int> Fibo(int n = 10)
        {
            var fiboResult = new List<int>();
            var fiboArray = new List<int> {1, 1};
            // const int n = 10;

            for (var count = 0; count < n; count++)
            {
                var prev = fiboArray[^1];
                var prev2 = fiboArray[^2];
                fiboArray.Add(prev + prev2);
            }

            foreach (var item in fiboArray)
            {
                fiboResult.Add(item);
                Console.Write($"{item}, ");
            }

            Console.WriteLine("");
            return fiboResult;
        }

        public static long Factorial(int n)
        {
            // Test for invalid input.
            if (n is < 0 or > 20)
            {
                return -1;
            }

            // Calculate the factorial iteratively rather than recursively.
            long factResult = 1;
            for (var i = 1; i <= n; i++)
            {
                factResult *= i;
            }

            Console.WriteLine("\nTest Program - Factorial");
            Console.WriteLine($"Factorial of {n} is {factResult}");
            return factResult;
        }
    }
}