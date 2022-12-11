using AdventOfCode2022.Task09;
using AdventOfCode2022.Task10;
using AdventOfCode2022.Task11;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Tests;

internal class Task11
{
    private const string TestData =
        "Monkey 0:\n  Starting items: 79, 98\n  Operation: new = old * 19\n  Test: divisible by 23\n    If true: throw to monkey 2\n    If false: throw to monkey 3\n\nMonkey 1:\n  Starting items: 54, 65, 75, 74\n  Operation: new = old + 6\n  Test: divisible by 19\n    If true: throw to monkey 2\n    If false: throw to monkey 0\n\nMonkey 2:\n  Starting items: 79, 60, 97\n  Operation: new = old * old\n  Test: divisible by 13\n    If true: throw to monkey 1\n    If false: throw to monkey 3\n\nMonkey 3:\n  Starting items: 74\n  Operation: new = old + 3\n  Test: divisible by 17\n    If true: throw to monkey 0\n    If false: throw to monkey 1\n";

    [Test]
    public void PartASample()
    {
        var task = new Task11A();
        var result = task.Solve(TestData.Split('\n'));

        result.Should().Be(10605);
    }

    [Test]

    public void PartBSample()
    {
        var task = new Task11B();
        var result = task.Solve(TestData.Split('\n'));

       result.Should().Be(2713310158);
    }
}