using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MConnect.Data.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            _testOutputHelper.WriteLine("Test Program - Fibo Series");
            var fiboDefault = Maths.Fibo();
            _testOutputHelper.WriteLine($"\nFibo default-10: {fiboDefault}");
            var fibo20 = Maths.Fibo(20);
            _testOutputHelper.WriteLine($"\nFibo-20: {fibo20}");
            var res = Maths.Factorial(10);
            _testOutputHelper.WriteLine($"\nFactorial result: {res}");
            var fiboArray = Maths.Fibo(20);
            var fibos = fiboArray.Where(r => r % 2 == 0).ToList();
            _testOutputHelper.WriteLine($"\nEven Fibos: {fibos.Count()} ");
            foreach (var fibo in fibos)
            {
                _testOutputHelper.WriteLine($"{fibo}");
            }
        }
    }
}