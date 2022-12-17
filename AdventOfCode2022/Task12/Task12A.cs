
using AdventOfCode2022.Task09;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022.Task12
{
    public class Task12A : ITask<IEnumerable<string>, int>
    {
        record Point(int X, int Y)
        {
            public static Point operator +(Point a) => a;
            public static Point operator -(Point a) => new Point(-a.X, -a.Y);

            public static Point operator +(Point a, Point b)
                => new Point(a.X + b.X, a.Y + b.Y);

            public static Point operator -(Point a, Point b)
                => a + (-b);
        }

        record MapPoint(Point Point, int Height);

        private const int Start = -1;
        private const int End = 26;

        public int ParseHeight(char a, int x, int y)
        {
            if (a == 'S')
            {
                StartPoint = new Point(x, y);
                return (int)'a' - (int)'a';
            }

            if (a == 'E')
            {
                EndPoint = new Point(x, y);
                return (int)'a' - (int)'z';
            }

            return (int)a - (int)'a';
        }

        private IEnumerable<MapPoint> GetAdjacentPoints(int[][] map, MapPoint mapPoint)
        {
            var point = mapPoint.Point;
            List<MapPoint> adjacentPoints = new List<MapPoint>();
            if (point.X > 0)
            {
                var adjacentPoint = point with { X = point.X - 1 };
                var height = map[adjacentPoint.Y][adjacentPoint.X];
                adjacentPoints.Add(new MapPoint(adjacentPoint, height));
            }

            if (point.X < map[0].Length - 1)
            {
                var adjacentPoint = point with { X = point.X + 1 };
                var height = map[adjacentPoint.Y][adjacentPoint.X];
                adjacentPoints.Add(new MapPoint(adjacentPoint, height));
            }

            if (point.Y > 0)
            {
                var adjacentPoint = point with { Y = point.Y - 1 };
                var height = map[adjacentPoint.Y][adjacentPoint.X];
                adjacentPoints.Add(new MapPoint(adjacentPoint, height));
            }

            if (point.Y < map.Length - 1)
            {
                var adjacentPoint = point with { Y = point.Y + 1 };
                var height = map[adjacentPoint.Y][adjacentPoint.X];
                adjacentPoints.Add(new MapPoint(adjacentPoint, height));
            }

            return adjacentPoints.Where(x => (mapPoint.Height + 1 >= x.Height));
        }

        private Point StartPoint;
        private Point EndPoint;
        
        private MapPoint GetMapPoint(int[][] map, Point point)
        {
            var height = map[point.Y][point.X];
            return new MapPoint(point, height);
        }

        public int Solve(IEnumerable<string> val)
        {
            

            var map = val
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select((row, y) => row.ToCharArray()
                    .Select((col, x) => ParseHeight(col, x, y)).ToArray())
                .ToArray();

        

            var stepCount = new Dictionary<Point, int>();

            var testPoint = StartPoint;
            var height = map[testPoint.Y][testPoint.X];

            for (var y = 0; y < map.Length; y++)
            for (var x = 0; x < map[y].Length; x++)
            {
                stepCount[new Point(x, y)] = 0;
            }
            var moveCount = 0;
            var startPosition = new MapPoint(testPoint, height);

            var map2 = val
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select((row, y) => row.ToCharArray())
                .ToArray();

            var visitedSquares = new HashSet<Point>();
            stepCount[startPosition.Point] = 0;
            visitedSquares.Add(startPosition.Point);

            var step = 0;

            //Console.ResetColor();
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.SetCursorPosition(0, 0);
            //var completeMap = map2.Select(x => new string(x)).ToArray();
            //foreach (var line in completeMap)
            //{
            //    Console.WriteLine(line);
            //}
            //Console.WriteLine();
            //Console.ReadLine();
            var result = 0;

            while (!visitedSquares.Contains(EndPoint))
            {
                step++;

                var allAdjacentCells = visitedSquares.SelectMany(x => GetAdjacentPoints(map, GetMapPoint(map, x)))
                    .Where(x => !visitedSquares.Contains(x.Point))
                    .ToArray();

                if (!allAdjacentCells.Any()) break;

                foreach (var cell in allAdjacentCells)
                {
                    stepCount[cell.Point] = step;
                    map2[cell.Point.Y][cell.Point.X] = step.ToString()[^1];

                    visitedSquares.Add(cell.Point);

                    if (cell.Point.X == EndPoint.X && cell.Point.Y == EndPoint.Y) result = step;

                    //Console.ForegroundColor = (ConsoleColor)((cell.Height) % 14 + 1);

                    //Console.SetCursorPosition(cell.Point.X, cell.Point.Y);
                    //Console.Write(step.ToString()[^1]);
                }

                //Thread.Sleep(50);
            }

            return result;
        }
    }
}
