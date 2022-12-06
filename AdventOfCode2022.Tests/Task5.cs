using AdventOfCode2022.Task5;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests;

[TestFixture]
public class Task5
{
    const string TestData = "    [D]    \n" +
                            "[N] [C]    \n" +
                            "[Z] [M] [P]\n" +
                            " 1   2   3 \n" +
                            "\n" +
                            "move 1 from 2 to 1\n" +
                            "move 3 from 1 to 3\n" +
                            "move 2 from 2 to 1\n" +
                            "move 1 from 1 to 2\n";

    [Test]
    public void Part5ASample()
    {
        var task = new Task5A();
        var result = task.Solve(TestData.Split('\n'));

        result.Should().Be("CMZ");
    }


    [Test]
    public void Part5BSample()
    {
        var task = new Task5B();
        var result = task.Solve(TestData.Split('\n'));

        result.Should().Be("MCD");
    }
}