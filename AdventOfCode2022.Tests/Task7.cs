using AdventOfCode2022.Task7;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests
{
    public class Task7
    {
        private const string TestData =
            "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst\n$ cd e\n$ ls\n584 i\n$ cd ..\n$ cd ..\n$ cd d\n$ ls\n4060174 j\n8033020 d.log\n5626152 d.ext\n7214296 k\n";

        [Test]
        public void Part7ASample()
        {
            var task = new Task7A();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(95437);
        }


        [Test]

        public void Part7BSample()
        {
            var task = new Task7B();
            var result = task.Solve(TestData.Split('\n'));

            result.Should().Be(24933642);
        }
    }
}
