namespace AdventOfCode2022
{
    public interface ITask<TIn, out TOut>
    {
        TOut Solve(TIn val);
    }
}
