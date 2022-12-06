namespace AdventOfCode2022.Task6
{
    public class Task6A
    {
        public int Solve(string data)
        {
            const int queueSize = 4;
            var queue = new Queue<char>(queueSize);

            for (var index = 0; index < data.Length; index++)
            {
                queue.Enqueue(data[index]);
                while (queue.Count > queueSize) queue.Dequeue();
                if (queue.Distinct().Count() == queueSize) return index + 1;;
            }

            return 0;
        }
    }
}
