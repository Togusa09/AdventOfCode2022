namespace AdventOfCode2022.Task1
{
    public class Task1A
    {
        public int Solve(IEnumerable<string> data)
        {
            var currentCalorieCount = 0;
            var carriedCalories = new List<int>();

            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    carriedCalories.Add(currentCalorieCount);
                    currentCalorieCount = 0;
                    continue;
                }

                currentCalorieCount += int.Parse(line);
            }

            return carriedCalories.MaxBy(x => x);
        }
    }
}