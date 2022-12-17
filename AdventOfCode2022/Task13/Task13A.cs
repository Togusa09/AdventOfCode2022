using System.Data;

namespace AdventOfCode2022.Task13
{
    public class Task13A : ITask<IEnumerable<string>, int>
    {
        public enum PacketType { List, Value}

        public struct Packet
        {
            public PacketType PacketType { get;  }

            public Packet[] Packets { get; }
            public Packet(Packet[] packets)
            {
                PacketType = PacketType.List;
                Packets = packets;
                Data = 0;
            }

            public int Data;

            public Packet(int data)
            {
                PacketType = PacketType.Value;
                Data = data;
                Packets = null;
            }


            public static Packet Parse(string s)
            {
                var trimmed = s.Trim();
                if (!trimmed.Contains("["))
                {
                    return new Packet(int.Parse(trimmed));
                }

                if (trimmed[0] == '[')
                {
                    var lastBracket = trimmed.LastIndexOf(']');
                    if (lastBracket < (trimmed.Length - 1)) // Values
                    {
                        var values = trimmed.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToArray();
                        return new Packet(values);
                    }
                    else // Array object
                    {
                        return Parse(trimmed[1..^1]);
                    }
                }

                var vals = trimmed.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToArray();
                return new Packet(vals);
            }
        }

        public int Solve(IEnumerable<string> val)
        {
            var data = val.Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => Packet.Parse(x))
                .Chunk(2)
                .ToArray();
            //    .Select(c =>
            //    new {
            //        First = c[0],
            //        Second = c[0]
            //    })
            //    .ToArray();

            //var total = 0;

            //foreach (var set in data)
            //{

            //}

            //return total;
            return 0;
        }
    }
}
