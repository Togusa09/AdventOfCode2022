namespace AdventOfCode2022.Task2
{
    public class Task2B : ITask<IEnumerable<string>, int>
    {
        enum Shape
        {
            Paper = 1,
            Scissors = 2,
            Rock = 3,
        }

        enum Outcome
        {
            Loss = 0,
            Draw = 3,
            Win = 6
        }

        Dictionary<char, Shape> ShapeMap = new()
        {
            {'A', Shape.Paper},
            {'B', Shape.Scissors },
            {'C', Shape.Rock },
        };

        Dictionary<char, Outcome> OutcomeMap = new()
        {
            {'X', Outcome.Loss},
            {'Y', Outcome.Draw },
            {'Z', Outcome.Win },
        };


        private Shape GetPlayerShape(Shape opponent, Outcome outcome)
        {
            if (outcome == Outcome.Draw) return opponent;

            return opponent switch
            {
                Shape.Paper => outcome == Outcome.Win ? Shape.Scissors : Shape.Rock,
                Shape.Scissors => outcome == Outcome.Win ? Shape.Rock : Shape.Paper,
                Shape.Rock => outcome == Outcome.Win ? Shape.Paper : Shape.Scissors,
                _ => throw new Exception("Invalid shape or outcome")
            };
        }

        private (Shape opponent, Outcome outcome) ReadFileLine(string line)
        {
            var opponent = ShapeMap[line[0]];
            var outcome = OutcomeMap[line[2]];

            return new(opponent, outcome);
        }

        public int Solve(IEnumerable<string> data)
        {
            var result = data.Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(ReadFileLine)
                .Select(x => new
                {
                    x.opponent,
                    x.outcome,
                    player = GetPlayerShape(x.opponent, x.outcome)
                })
                .Select(x => (int)x.player + (int)x.outcome)
                .Sum();

            return result;
        }
    }
}
