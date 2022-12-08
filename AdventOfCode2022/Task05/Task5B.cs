namespace AdventOfCode2022.Task5
{
    public class Task5B : ITask<IEnumerable<string>, string>
    {
        private record Command(int Quantity, int Source, int Target);

        private Stack<char> ParseContainers(string[] rows, int column)
        {
            var index = ((column - 1) * 4) + 1;
            var containers = rows.Select(x => x[index])
                .Where(x => !char.IsWhiteSpace(x))
                .Reverse()
                .ToArray();
            var containerStack = new Stack<char>(containers);
            return containerStack;
        }

        private Command ParseCommand(string command)
        {
            var values = command.Replace("move", "")
                .Replace("from", "")
                .Replace("to", "")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            return new Command(values[0], values[1], values[2]);
        }

        public string Solve(IEnumerable<string> data)
        {
            var cargoData = data
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.Contains("move"))
                .ToArray();
            var columns = cargoData.Last()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
            var containerStacks = columns
                .Select(x => ParseContainers(cargoData.Take(cargoData.Length - 1).ToArray(), x))
                .ToArray();

            var commands = data
                .Where(x => x.Contains("move"))
                .Select(ParseCommand)
                .ToArray();

            foreach (var command in commands)
            {
                var craneStack = new Stack<char>();
                for (var i = 0; i < command.Quantity; i++)
                {
                    var container = containerStacks[command.Source - 1].Pop();
                    craneStack.Push(container);
                }

                for (var i = 0; i < command.Quantity; i++)
                {
                    var container = craneStack.Pop();
                    containerStacks[command.Target - 1].Push(container);
                }
            }

            var resultChars = containerStacks.Select(x => x.Peek()).ToArray();
            return new string(resultChars);

            //"[Z] [M] [P]\n" +
            //    " 1   2   3 \n" +
            //    "\n" +
            //    "move 1 from 2 to 1\n" +
            //[F] [L] [H] [R] [Z] [J] [J] [D] [D]

            //1, 
        }
    }
}