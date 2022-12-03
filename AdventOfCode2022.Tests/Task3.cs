using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022.Task2;
using AdventOfCode2022.Task3;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    [TestFixture]
    public class Task3
    {
        const string TestData = "vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\nPmmdzqPrVvPwwTWBwg\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\nttgJtRGJQctTZtZT\nCrZsJsPPZsGzwwsLwLmpwMDw\n";

        [Test]
        public void Part3ASample()
        {
            var task = new Task3A();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(157);
        }


        [Test]
        public void Part3BSample()
        {
            var task = new Task3B();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(70);
        }
    }
}
