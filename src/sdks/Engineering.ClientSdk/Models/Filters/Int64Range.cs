namespace Engineering.ClientSdk.Models.Filters;

public struct Int64Range : IEquatable<Int64Range>
{
    public long From { get; set; }
    public long To { get; set; }

    public Int64Range()
    {
    }

    public Int64Range(long from, long to)
    {
        From = from;
        To = to;
    }

    public Int64Range(string from, string to)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        From = long.Parse(from);
        To = long.Parse(to);
    }

    public bool Equals(Int64Range other)
    {
        return From == other.From && To == other.To;
    }

    public override bool Equals(object? obj)
    {
        return obj is Int64Range other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    public static implicit operator Int64Range((long From, long To) value) => new(value.From, value.To);
    public static implicit operator (long? From, long? To)(Int64Range value) => (value.From, value.To);
    public static implicit operator (long From, long To)(Int64Range value) => (value.From, value.To);

    public static implicit operator Int64Range((string From, string To) value) => new(value.From, value.To);

    public static implicit operator (string From, string To)(Int64Range value) =>
        (value.From.ToString(), value.To.ToString());
}
