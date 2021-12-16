namespace AdventOfCode2021;

public class Packet
{
    private readonly long _value;
    private readonly long _typeId;
    private readonly long _version;
    public readonly int Length;
    private readonly IList<Packet> _subPackets;

    public Packet(long value, long typeId, long version, IList<Packet> subPackets, int length)
    {
        _value = value;
        _typeId = typeId;
        _version = version;
        Length = length;
        _subPackets = subPackets ?? new List<Packet>();
    }

    public long GetPacketVersionSum()
    {
        return _subPackets.Select(sp => sp.GetPacketVersionSum()).Sum() + _version;
    }

    public long ComputeExpression()
    {
        return _typeId switch
        {
            0 => _subPackets.Select(sp => sp.ComputeExpression()).Sum(),
            1 => _subPackets.Select(sp => sp.ComputeExpression()).Product(),
            2 => _subPackets.Select(sp => sp.ComputeExpression()).Min(),
            3 => _subPackets.Select(sp => sp.ComputeExpression()).Max(),
            4 => _value,
            5 => _subPackets[0].ComputeExpression() > _subPackets[1].ComputeExpression() ? 1 : 0,
            6 => _subPackets[0].ComputeExpression() < _subPackets[1].ComputeExpression() ? 1 : 0,
            7 => _subPackets[0].ComputeExpression() == _subPackets[1].ComputeExpression() ? 1 : 0,
            _ => throw new Exception($"Invalid TypeId: {_typeId}")
        };
    }
}

public class Day16 : Day, Parts
{
    private readonly char[] _input;

    public Day16()
    {
        _input = LinesStrings[0]
            .ToCharArray()
            .SelectMany(c => Convert
                .ToString(Convert.ToInt64(c.ToString(), 16), 2)
                .PadLeft(4, '0')
                .ToCharArray())
            .ToArray();
    }

    private static long CharArrayToNumber(IEnumerable<char> input)
    {
        return Convert.ToInt64(new string(input.ToArray()), 2);
    }

    private Packet GetPacket(int index = 0)
    {
        var version = CharArrayToNumber(_input.Skip(index).Take(3));
        var typeId = CharArrayToNumber(_input.Skip(index + 3).Take(3));

        if (typeId == 4)
        {
            var i = index + 6;
            var content = _input.Skip(i).Take(5);
            var isLast = content.First() == '0';
            var value = content.Skip(1);

            i += 5;

            while (!isLast)
            {
                content = _input.Skip(i).Take(5);
                isLast = content.First() == '0';
                value = value.Concat(content.Skip(1));
                i += 5;
            }

            var numericalValue = CharArrayToNumber(value);

            return new Packet(numericalValue, typeId, version, Array.Empty<Packet>(), i - index);
        }
        else
        {
            var i = index + 6;
            var lengthTypeId = _input.Skip(i).First();
            i += 1;

            if (lengthTypeId == '0')
            {
                var length = 0;
                var maxLength = CharArrayToNumber(_input.Skip(i).Take(15));
                i += 15;

                var subPackets = new List<Packet>();

                while (length < maxLength)
                {
                    var newPacket = GetPacket(i);
                    i += newPacket.Length;
                    length += newPacket.Length;

                    subPackets.Add(newPacket);
                }

                return new Packet(0, typeId, version, subPackets, i - index);
            }
            else
            {
                var subPacketCount = CharArrayToNumber(_input.Skip(i).Take(11));
                i += 11;

                var subPackets = new List<Packet>();

                for (var _ = 0; _ < subPacketCount; _++)
                {
                    var newPacket = GetPacket(i);
                    i += newPacket.Length;

                    subPackets.Add(newPacket);
                }

                return new Packet(0, typeId, version, subPackets, i - index);
            }
        }
    }


    public object Part1()
    {
        return GetPacket().GetPacketVersionSum();
    }

    public object Part2()
    {
        return  GetPacket().ComputeExpression();
    }
}