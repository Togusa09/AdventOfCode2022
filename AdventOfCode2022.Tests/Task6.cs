using AdventOfCode2022.Task6;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    public class Task6
    {
        [Test]
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void Part5ASample(string data, int answer)
        {
            var task = new Task6A();
            var result = task.Solve(data);

            result.Should().Be(answer);
        }


        [Test]
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb",19)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz",23)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg",23)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",26)]

        public void Part5BSample(string data, int answer)
        {
            var task = new Task6B();
            var result = task.Solve(data);

            result.Should().Be(answer);
        }
    }
}
