using AdventOfCode2022.Task08;
using AdventOfCode2022.Task7;
using NUnit.Framework;
using FluentAssertions;

namespace AdventOfCode2022.Tests
{
    internal class Task8
    {
        private const string TestData =
            "30373\n25512\n65332\n33549\n35390\n";

        [Test]
        public void Part8ASample()
        {
            var task = new Task8A();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(21);
        }


        [Test]

        public void Part8BSample()
        {
            var task = new Task8B();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(8);
            result.Should().Be(8);
        }
    }
}
