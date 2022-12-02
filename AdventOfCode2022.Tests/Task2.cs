using AdventOfCode2022.Day1;
using AdventOfCode2022.Task2;
using FluentAssertions;
using NUnit.Framework;


namespace AdventOfCode2022.Tests
{
    [TestFixture]
    public class Task2
    {
        const string TestData = "A Y\nB X\nC Z\n";

        [Test]
        public void Part1ASample()
        {
            var task = new Task2A();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(15);
        }


        [Test]
        public void Part1BSample()
        {
            var task = new Task2B();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(12);
        }
    }
}
