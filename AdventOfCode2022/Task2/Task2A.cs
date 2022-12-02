namespace AdventOfCode2022.Task2
{
    public class Task2A
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

        Dictionary<char, Shape> ShapeMaps = new()
        {
            {'A', Shape.Paper},
            {'B', Shape.Scissors },
            {'C', Shape.Rock },
            {'X', Shape.Paper},
            {'Y', Shape.Scissors },
            {'Z', Shape.Rock }
        };


        private Outcome ComputeOutcome(Shape player, Shape opponent)
        {
            if (player == opponent) return Outcome.Draw;

            return player switch
            {
                Shape.Paper => (opponent == Shape.Scissors) ? Outcome.Loss : Outcome.Win,
                Shape.Scissors => (opponent == Shape.Rock) ? Outcome.Loss : Outcome.Win,
                Shape.Rock => (opponent == Shape.Paper) ? Outcome.Loss : Outcome.Win,
                _ => throw new ArgumentException("Invalid shape")
            };
        }

        private int ComputeScore(string line)
        {
            var opponent = ShapeMaps[line[0]];
            var player = ShapeMaps[line[2]];

            var playerResult = (int)ComputeOutcome(player, opponent) + (int)player;
            return playerResult;
        }

        public int Solve(IEnumerable<string> data)
        {
            var result = data.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => ComputeScore(x)).ToArray();

            var playerScore = result.Sum();
            return playerScore;
        }
    }
}
