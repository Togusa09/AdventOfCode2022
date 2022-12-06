using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Task6
{
    public class Task6B
    {
        public int Solve(string data)
        {
            const int queueSize = 14;
            var queue = new Queue<char>();

            for (var index = 0; index < data.Length; index++)
            {
                var character = data[index];
                queue.Enqueue(character);

                while (queue.Count > queueSize) queue.Dequeue();

                if (queue.Count() == queueSize)
                {
                    var allUnique = queue.All(x => queue.Count(y => x == y) == 1);
                    if (allUnique) return index + 1;
                }
            }

            return 0;
        }
    }
}
