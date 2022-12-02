using AdventOfCode2022.Day1;
using AdventOfCode2022.Task1;
using FluentAssertions;
using NUnit.Framework;


namespace AdventOfCode2022.Tests.Task1
{
    [TestFixture]
    public class Task1
    {
        [Test]
        public void Part1ASample()
        {
            var testData =
                "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000\n";

            var task = new Task1A();
            var result = task.Solve(testData.Split('\n'));

            result.Should().Be(24000);
        }


        [Test]
        public void Part1BSample()
        {
            var testData =
                "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000\n";

            var task = new Task1B();
            var result = task.Solve(testData.Split('\n'));

            result.Should().Be(45000);
        }
    }
}
