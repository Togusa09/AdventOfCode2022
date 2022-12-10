using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Task10
{
    public class Task10A : ITask<IEnumerable<string>, int> 
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

        void Record(int timer, int register, List<int> values)
        {
            var indexesToRead = new int[] { 20, 60, 100, 140, 180, 220 };
            if (!indexesToRead.Contains(timer)) return;

            values.Add(timer * register);
        }

        public int Solve(IEnumerable<string> val)
        {
            var commands = val.Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(Command.Parse)
                .ToArray();

            
            var values = new List<int>();

            var timer = 0;
            var register = 1;
            foreach(var command in commands)
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

            var result = values.Sum();
            return result;
        }
    }
}
