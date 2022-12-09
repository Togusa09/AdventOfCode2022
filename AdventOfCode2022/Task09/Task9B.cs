using System.Net.Http.Headers;

namespace AdventOfCode2022.Task09;

public class Task9B : ITask<IEnumerable<string>, int>
{
    record struct Point(int X, int Y)
    {
        public static Point operator +(Point a) => a;
        public static Point operator -(Point a) => new Point(-a.X, -a.Y);

        public static Point operator +(Point a, Point b)
            => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b)
            => a + (-b);
    }

    enum Direction
    {
        U,
        D,
        L,
        R
    }

    record Command(Direction Direction, int Distance)
    {
        public static Command Parse(string command)
        {
            var t = command.Split(" ");
            var dir = (Direction)Enum.Parse(typeof(Direction), t[0]);
            var len = int.Parse(t[1]);
            return new Command(dir, len);
        }
    };

    private Point Chase(Point headPosition, Point tailPosition)
    {
        var distance = headPosition - tailPosition;
        if (tailPosition.X == headPosition.X && Math.Abs(distance.Y) > 1) // Same row
        {
            var direction = distance.Y / Math.Abs(distance.Y);
            tailPosition += new Point(0, direction);
        }
        else if (tailPosition.Y == headPosition.Y && Math.Abs(distance.X) > 1) // Same column
        {
            var direction = distance.X / Math.Abs(distance.X);
            tailPosition += new Point(direction, 0);
        }
        else if (Math.Abs(distance.X) > 1 || Math.Abs(distance.Y) > 1) // Diagonal
        {
            var directionX = distance.X / Math.Abs(distance.X);
            var directionY = distance.Y / Math.Abs(distance.Y);

            tailPosition += new Point(directionX, directionY);
        }

        return tailPosition;
    }

    public int Solve(IEnumerable<string> val)
    {
        var data = val
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(Command.Parse)
            .ToArray();

        var tailHistory = new List<Point>();

        var body = new Point[10];
        for (var i = 0; i < 10; i++) body[i] = new Point(0, 0);

        foreach (var command in data)
        {

            for (var i = 0; i < command.Distance; i++)
            {
                switch (command.Direction)
                {
                    case Direction.U:
                        body[0] += new Point(0, 1);
                        break;
                    case Direction.D:
                        body[0] += new Point(0, -1);
                        break;
                    case Direction.L:
                        body[0] += new Point(-1, 0);
                        break;
                    case Direction.R:
                        body[0] += new Point(1, 0);
                        break;
                }

                for (var segment = 1; segment < body.Length; segment++)
                {
                    body[segment] = Chase(body[segment - 1], body[segment]);
                }

                tailHistory.Add(body[^1]);
            }
        }

        var count = tailHistory.Distinct().Count();
        return count;
    }
}