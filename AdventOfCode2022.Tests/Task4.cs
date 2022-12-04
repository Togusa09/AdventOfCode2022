using AdventOfCode2022.Task4;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    public class Task4
    {
        const string TestData = "2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8\n";

        [Test]
        public void Part4ASample()
        {
            var task = new Task4A();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(2);
        }


        [Test]
        public void Part4BSample()
        {
            var task = new Task4B();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(4);
        }
    }
}
