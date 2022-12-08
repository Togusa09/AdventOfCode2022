namespace AdventOfCode2022.Task08;

public class Task8B : ITask<IEnumerable<string>, int>
{
    private record Result(int X, int Y, int Value, int ScenicScore);

    public int Solve(IEnumerable<string> val)
    {
        var data = val.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var parsedData = data.Select(x => x.ToCharArray().Select(y => (int)char.GetNumericValue(y)).ToArray()).ToArray();

        var visibility = parsedData.Select((row, y) =>
            {

                var score = row.Select((t, x) =>
                {
                    // Scenic score is a multiplication of view range and trees on edge will have view range in one direction of 0
                    if (x == 0 || x == (row.Length - 1)) return new Result(x, y, t, 0);
                    if (y == 0 || y == (data.Length - 1)) return new Result(x, y, t, 0);

                    var left = row[..x].Reverse().ToArray();
                    var leftUntilBlocked = left.TakeWhile(o => o < t).ToArray();

                    var right = row[(x + 1)..].ToArray();
                    var rightUntilBlocked = right.TakeWhile(o => o < t).ToArray();

                    var up =  parsedData[..y].Select(o => o[x]).Reverse().ToArray();
                    var upUntilBlocked = up.TakeWhile(o => o < t).ToArray();

                    var down = parsedData[(y + 1)..].Select(o => o[x]).ToArray();
                    var downUntilBlocked = down.TakeWhile(o => o < t).ToArray();

                    var leftScenic = leftUntilBlocked.Length + (left.Length > leftUntilBlocked.Length ? 1 : 0);
                    var rightScenic = rightUntilBlocked.Length + (right.Length > rightUntilBlocked.Length ? 1 : 0);
                    var upScenic = upUntilBlocked.Length + (up.Length > upUntilBlocked.Length ? 1 : 0);
                    var downScenic = downUntilBlocked.Length + (down.Length > downUntilBlocked.Length ? 1 : 0);

                    return new Result(x, y, t, leftScenic * rightScenic * upScenic * downScenic);
                }).ToArray();

                return score;
            })
            .ToArray();

        var visibleCount = visibility.Max(y => y.Max(x => x.ScenicScore));
        return visibleCount;
    }
}