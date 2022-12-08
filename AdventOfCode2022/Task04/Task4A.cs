using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Task4
{
    public class Task4A : ITask<IEnumerable<string>, int>
    {
        private record Range(int Start, int End)
        {
            public bool Contains(Range otherRange) => otherRange.Start <= Start && otherRange.End >= End;
        }

        private (Range first, Range second) ParseRow(string val)
        {
            var vals = val.Split(',');
            return (ParseRange(vals[0]), ParseRange(vals[1]));
        }

        private Range ParseRange(string val)
        {
            var vals = val.Split('-');
            return new Range(int.Parse(vals[0]), int.Parse(vals[1]));
        }

        public int Solve(IEnumerable<string> data)
        {
            var result = data
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(ParseRow)
                .Count(x => x.first.Contains(x.second) || x.second.Contains(x.first));

            return result;
        }
    }
}
