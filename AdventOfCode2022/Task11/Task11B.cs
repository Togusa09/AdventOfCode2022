namespace AdventOfCode2022.Task11;

public class Task11B : ITask<IEnumerable<string>, long>
{
    enum ActionType
    {
        Throw
    }

    record MonkeyAction(ActionType ActionType, int target);

    record Monkey(int Id, List<long> Item, string[] Operation, int Test, MonkeyAction FalseAction, MonkeyAction TrueAction)
    {
        public static Monkey Parse(IEnumerable<string> val)
        {
            var data = val.ToArray();
            var line1 = data[0].Trim()[7..^1];
            var id = Int16.Parse(line1);
            var line2 = data[1].Replace("Starting items:", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
            var items = line2.Select(x => long.Parse(x.Trim())).ToList();
            var line3 = data[2].Replace("Operation: ", "").Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            var line4 = data[3].Replace("Test: divisible by ", "").Trim();
            var test = int.Parse(line4);
            //var line5 = data[4].Replace("If true: throw to monkey", "").Trim();
            //var line6 = data[5].Replace("If false: throw to monkey", "").Trim();
            var trueAction = new MonkeyAction(ActionType.Throw, int.Parse(data[4][^1..^0]));
            var falseAction = new MonkeyAction(ActionType.Throw, int.Parse(data[5][^1..^0]));

            return new Monkey(id, items, line3, test, falseAction, trueAction);
        }
    }


    private long GetVal(string val, long oldVal)
    {
        if (val == "old") return oldVal;
        return int.Parse(val);
    }

    public long Solve(IEnumerable<string> val)
    {
        /*
         * Monkey 0:
            Starting items: 79, 98
            Operation: new = old * 19
            Test: divisible by 23
            If true: throw to monkey 2
            If false: throw to monkey 3
         */

        var data = val.Where(x => !string.IsNullOrWhiteSpace(x))
            .Chunk(6)
            .Select(Monkey.Parse)
            .ToArray();

        var inspectionCount = data.Select(x => 0L).ToArray();

        var multiple = data.Select(x => x.Test).Aggregate(1, (a, b) => a * b);

        var interestingRounds = new int[] { 1, 10,11,12,13,14,15, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };
        var roundHistory = new Dictionary<int, long[]>();

        for (var round = 0; round < 10000; round++)
        {
            foreach (var monkey in data)
            {
                foreach (var item in monkey.Item.ToArray())
                {
                    var result = 0L;
                    var val1 = GetVal(monkey.Operation[2], item);
                    var val2 = GetVal(monkey.Operation[4], item);

                    switch (monkey.Operation[3])
                    {
                        case "+":
                            result = val1 + val2;
                            break;
                        case "*":
                            result = val1 * val2;
                            break;
                    }


                    var test = (result % monkey.Test) == 0;

                    var newMonkey = test ? data[monkey.TrueAction.target] : data[monkey.FalseAction.target];

                    // result = test ? result / monkey.Test : result;
                    result = result % multiple;
                    //result = result / 10000;
                    //result = result / 9699690 + result % 9699690;
                    //result = result / monkey.Test + result % monkey.Test;
                    //result = result / newMonkey.Test + result % newMonkey.Test;

                    //result = test ? result % monkey.Test : result;
                    //result = result % 9699690;
                    //result = result / 9699690;
                    //result = result % 21;
                    //result = result / 21;


                    newMonkey.Item.Add(result);
                    monkey.Item.Remove(item);
                    inspectionCount[monkey.Id]++;
                }
            }

            if (interestingRounds.Contains(round + 1))
            {
                roundHistory[round + 1] = inspectionCount.ToArray();
            }
        }

        var topTwo = inspectionCount.OrderByDescending(x => x).Take(2).ToArray();
        var endResult = topTwo[0] * topTwo[1];

        return endResult;
    }
}