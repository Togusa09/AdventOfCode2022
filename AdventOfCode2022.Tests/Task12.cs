using AdventOfCode2022.Task12;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests;

internal class Task12
{
    private const string TestData =
        "Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi\n";

    [Test]
    public void PartASample()
    {
        var task = new Task12A();
        var result = task.Solve(TestData.Split('\n'));

        result.Should().Be(31);
    }

    [Test]

    public void PartBSample()
    {
        var task = new Task12B();
        var result = task.Solve(TestData.Split('\n'));

       result.Should().Be(0);
    }
}