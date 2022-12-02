namespace AdventOfCode2022.Day1
{
    public class Task1B
    {
        public int Solve(IEnumerable<string> data)
        {
            //var index = 0;
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

            return carriedCalories.OrderByDescending(x => x).Take(3).Sum();
        }
    }
}