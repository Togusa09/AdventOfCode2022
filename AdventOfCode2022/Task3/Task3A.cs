using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Task3
{
    public class Task3A
    {
        public int GetPriority(char val)
        {
            if (char.IsLower(val)) return (int)val - (int)'a' + 1;
            return (int)val - (int)'A' + 27;
        }

        public int Solve(IEnumerable<string> data)
        {
            var result = data
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x =>
                    new
                    {
                        firstCompartment = x.Substring(0, x.Length / 2).ToCharArray(),
                        secondCompartment = x.Substring(x.Length / 2).ToCharArray()
                    }
                ).Select(x => x.firstCompartment.Intersect(x.secondCompartment).First())
                    .Sum(GetPriority);

            return result;
        }
    }
}
