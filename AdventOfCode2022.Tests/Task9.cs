using AdventOfCode2022.Task09;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests;

internal class Task9
{
    private const string TestData =
        "R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2\n";

    [Test]
    public void PartASample()
    {
        var task = new Task9A();
        var result = task.Solve(TestData.Split('\n'));

        result.Should().Be(13);
    }


    [Test]

    public void PartBSample()
    {
        var task = new Task9B();
        var result = task.Solve(TestData.Split('\n'));

        result.Should().Be(1);
    }

    private const string TestData2 =
        "R 5\nU 8\nL 8\nD 3\nR 17\nD 10\nL 25\nU 20\n";

    [Test]
    public void PartBSample2()
    {
        var task = new Task9B();
        var result = task.Solve(TestData2.Split('\n'));

        result.Should().Be(36);
    }
}