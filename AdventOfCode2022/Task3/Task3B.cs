namespace AdventOfCode2022.Task3;

public class Task3B : ITask<IEnumerable<string>, int>
{
    public int GetPriority(char val)
    {
        if (char.IsLower(val)) return (int)val - (int)'a' + 1;
        return (int)val - (int)'A' + 27;
    }

    public int Solve(IEnumerable<string> data)
    {
        //var dataArray = data.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        //var result = 0;
        //for (var i = 0; i < dataArray.Length; i += 3)
        //{
        //    var common = dataArray[i].Intersect(dataArray[i + 1]).Intersect(dataArray[i + 2]);
        //    result += GetPriority(common.Single());
        //}

        var result = data.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.AsEnumerable())
            .Chunk(3)
            .Select(x => x.Aggregate((a, b) => a.Intersect(b)))
            .Sum(x => GetPriority(x.Single()));


        return result;
    }
}