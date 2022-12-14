
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
                return Start;
            }

            if (a == 'E')
            {
                EndPoint = new Point(x, y);
                return End;
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

            return adjacentPoints;
        }

        private int GetDistanceToEnd(int[][] map, MapPoint point, Point[] previous)
        {
            if (point.Point == EndPoint) return 0;

            var adjacentPoints = GetAdjacentPoints(map, point);


            if (adjacentPoints.Any(p => p.Point == EndPoint) && point.Height == 25)
            {
                return 1;
            }

            var distances = adjacentPoints
                .Where(x => !previous.Contains(x.Point))
                .Where(x => x.Height - point.Height == 0 || x.Height - point.Height == 1)
                .Select(x => GetDistanceToEnd(map, x, previous.Union(new[] {x.Point}).ToArray()))
                .Where(x => x != int.MaxValue)
                .ToArray();

            if (!distances.Any()) return int.MaxValue;

            var minDistance = distances.Min();

            return minDistance + 1;
        }

        private Point StartPoint;
        private Point EndPoint;

        private record Run(int StepCount);

        public int Solve(IEnumerable<string> val)
        {
            var map = val
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select((row, y) => row.ToCharArray()
                    .Select((col, x) => ParseHeight(col, x, y)).ToArray())
                .ToArray();

        

            var visitCount = new Dictionary<Point, int>();

            //var testPoint = new Point(4, 1);
            var testPoint = StartPoint;
            var height = map[testPoint.Y][testPoint.X];

            for (var y = 0; y < map.Length; y++)
            for (var x = 0; x < map[y].Length; x++)
            {
                visitCount[new Point(x, y)] = 0;
            }


            //var test = GetDistanceToEnd(map, new MapPoint(testPoint, height), new[] {StartPoint});

            
            //var currentPosition = new MapPoint(testPoint, height);

            var random = new Random();
            var runs = new List<Run>();


            for (var i = 0; i < 1000; i++)
            {
                var visitedSquares = new List<Point>();
                var moveCount = 0;
                var currentPosition = new MapPoint(testPoint, height);

                var map2 = val
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select((row, y) => row.ToCharArray())
                    .ToArray();

                while (currentPosition.Point != EndPoint)
                {

                    visitedSquares.Add(currentPosition.Point);
                    map2[currentPosition.Point.Y][currentPosition.Point.X] = '*';

                    var adjacentCells = GetAdjacentPoints(map, currentPosition)
                        .Where(x => currentPosition.Height + 1 >= x.Height)
                        .OrderByDescending(x => x.Height * 100 )
                        .ThenBy(x => Math.Abs(x.Point.X - EndPoint.X) + Math.Abs(x.Point.Y - EndPoint.Y))
                        //.Where(x => !visitedSquares.Contains(x.Point))
                        .ToArray();

                    //var adjacent = GetAdjacentPoints(map, currentPosition)
                    //    //.Where(x => !visitedSquares.Contains(x.Point))
                    //    .Where(x => currentPosition.Height + 1 >= x.Height)
                    //    .Select(x => new
                    //    {
                    //        Distance = Math.Abs(x.Point.X - EndPoint.X) + Math.Abs(x.Point.Y - EndPoint.Y),
                    //        VisitCount = visitCount[x.Point],
                    //        P = x
                    //    })
                    //    //.OrderBy(x => visitedSquares.Contains(x.P.Point))
                    //    //.ThenBy(x => x.VisitCount)
                    //    .OrderBy(x => x.Distance)
                    //    .ToArray();

                    //                    if (!adjacentCells.Any())
                    //                      break;

                    if (currentPosition.Height == 2)
                    {
                        adjacentCells = adjacentCells.Where(x => x.Height != 0).ToArray();
                    }

                    //var completeMap2 = map2.Select(x => new string(x)).ToArray();
                    var unvisited = adjacentCells.Where(x => !visitedSquares.Contains(x.Point)).ToArray();
                    if (unvisited.Any())
                    {
                        currentPosition = unvisited.First();
                    }
                    else if (!adjacentCells.Any())
                    {
                        break;
                    }
                    else
                    {
                        var dir = random.Next() % adjacentCells.Length;
                        
                        currentPosition = adjacentCells[dir];
                    }



                    //var dir = random.Next() % adjacentCells.Length;
                    //var adjacent = adjacentCells[dir];

                    
                    visitCount[currentPosition.Point]++;

                    //var adjacent = GetAdjacentPoints(map, currentPosition)
                    //    .Where(x => !visitedSquares.Contains(x.Point))
                    //    .Where(x =>  x.Height - currentPosition.Height == 1)
                    //    .ToArray();

                    //if (!adjacent.Any())
                    //{
                    //    adjacent = GetAdjacentPoints(map, currentPosition)
                    //        .Where(x => !visitedSquares.Contains(x.Point))
                    //        .Where(x => x.Height - currentPosition.Height == 0 || x.Height - currentPosition.Height == -1)
                    //        .ToArray();
                    //}

                    moveCount++;

                    if (moveCount > 100000) break;
                }

                //if (currentPosition.Point != EndPoint)
                var completeMap = map2.Select(x => new string(x)).ToArray();

                runs.Add(new Run(moveCount));
            }

            return 0;
        }
    }
}
