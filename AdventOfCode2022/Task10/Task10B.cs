using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2022.Task10;

public class Task10B : ITask<IEnumerable<string>, string>
{
    enum CommandType
    {
        noop,
        addx
    }

    record Command(CommandType Commandtype, int value)
    {
        public static Command Parse(string s)
        {
            var x = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var type = (CommandType)Enum.Parse(typeof(CommandType), x[0]);
            var value = x.Length > 1 ? int.Parse(x[1]) : 0;

            return new Command(type, value);
        }
    }

    void Record(int timer, int register, List<char> values)
    {
        var rowIndex = (timer % 40) - 1;
        var spriteRange = (min: register - 1, max: register + 1);
        if (rowIndex >= spriteRange.min && rowIndex <= spriteRange.max)
        {
            values.Add('#');
        }
        else
        {
            values.Add('.');
        }
    }

    public string Solve(IEnumerable<string> val)
    {
        var commands = val.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(Command.Parse)
            .ToArray();


        var values = new List<char>();

        var timer = 0;
        var register = 1;
        foreach (var command in commands)
        {
            switch (command.Commandtype)
            {
                case CommandType.noop:
                    timer++;
                    Record(timer, register, values);
                    break;
                case CommandType.addx:
                    for (var i = 0; i < 2; i++)
                    {
                        timer++;
                        Record(timer, register, values);
                    }
                    register += command.value;
                    break;
            }
        }

        var strings = values.Chunk(40).Select(x => new string(x));
        var result = string.Join('\n', strings);
        return result;
    }
}