using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Task11
{
    public class Task11A : ITask<IEnumerable<string>, int>
    {
        enum ActionType
        {
            Throw
        }

        record MonkeyAction(ActionType ActionType, int target);

        record Monkey(int Id, List<int> Item, string[] Operation, int Test, MonkeyAction FalseAction, MonkeyAction TrueAction)
        {
            public static Monkey Parse(IEnumerable<string> val)
            {
                var data = val.ToArray();
                var line1 = data[0].Trim()[7..^1];
                var id = Int16.Parse(line1);
                var line2 = data[1].Replace("Starting items:", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
                var items = line2.Select(x => int.Parse(x.Trim())).ToList();
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


        private int GetVal(string val, int oldVal)
        {
            if (val == "old") return oldVal;
            return int.Parse(val);
        }

        public int Solve(IEnumerable<string> val)
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

            var inspectionCount = data.Select(x => 0).ToArray();

            for (var round = 0; round < 20; round++)
            {
                foreach (var monkey in data)
                {
                    foreach (var item in monkey.Item.ToArray())
                    {
                        var worrylevel = item;

                        var result = 0;
                        var val1 = GetVal(monkey.Operation[2], worrylevel);
                        var val2 = GetVal(monkey.Operation[4], worrylevel);

                        switch (monkey.Operation[3])
                        {
                            case "+":
                                result = val1 + val2;
                                break;
                            case "*":
                                result = val1 * val2;
                                break;
                        }

                        result = result / 3;

                        var test = result % monkey.Test;

                        var newMonkey = test == 0 ? data[monkey.TrueAction.target] : data[monkey.FalseAction.target];
                        newMonkey.Item.Add(result);
                        monkey.Item.Remove(item);
                        inspectionCount[monkey.Id]++;
                    }
                }
            }



            var topTwo = inspectionCount.OrderByDescending(x => x).Take(2).ToArray();
            var endResult = topTwo[0] * topTwo[1];

            return endResult;
        }
    }
}
