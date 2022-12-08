namespace AdventOfCode2022.Task08
{
    public class Task8A : ITask<IEnumerable<string>, int>
    {
        private record Result(int X, int Y, int Value, bool Visible);

        public int Solve(IEnumerable<string> val)
        {
            var data = val.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var parsedData = data.Select(x => x.ToCharArray().Select(y => (int)char.GetNumericValue(y)).ToArray()).ToArray();

            var visibility = parsedData.Select((row, y) =>
                {
                    var visible = row.Select((t, x) =>
                    {
                        if (x == 0 || x == (row.Length - 1)) return new Result(x, y, t, true);
                        if (y == 0 || y == (data.Length - 1)) return new Result(x, y, t, true);

                        var treeVisible = row[..x].All(o => o < t)
                                          || row[(x + 1)..].All(o => o < t)
                                          || parsedData[..y].All(o => o[x] < t)
                                          || parsedData[(y + 1)..].All(o => o[x] < t);

                        return new Result(x, y, t, treeVisible);
                    }).ToArray();

                    return visible;
                })
                .ToArray();

            var visibleCount = visibility.Sum(y => y.Count(x => x.Visible));
            return visibleCount;
        }
    }
}
