namespace Engineering.ClientSdk.Models.Filters;

public struct Int64NullableRange : IEquatable<Int64NullableRange>
{
    public long? From { get; set; }
    public long? To { get; set; }

    public Int64NullableRange()
    {
    }

    public Int64NullableRange(long? from, long? to)
    {
        From = from;
        To = to;
    }

    public Int64NullableRange(string? from, string? to)
    {
        if (from is not null)
        {
            From = long.Parse(from);
        }

        if (to is not null)
        {
            To = long.Parse(to);
        }
    }

    public bool Equals(Int64NullableRange other)
    {
        return From == other.From && To == other.To;
    }

    public override bool Equals(object? obj)
    {
        return obj is Int64NullableRange other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    public static implicit operator Int64NullableRange((long? From, long? To) value) => new(value.From, value.To);
    public static implicit operator Int64NullableRange((long From, long To) value) => new(value.From, value.To);
    public static implicit operator (long? From, long? To)(Int64NullableRange value) => (value.From, value.To);

    public static implicit operator (long From, long To)(Int64NullableRange value) =>
    (
        value.From ?? throw new InvalidOperationException("The 'From' field in the Int64NullableRange is null."),
        value.To ?? throw new InvalidOperationException("The 'To' field in the Int64NullableRange is null.")
    );

    public static implicit operator Int64NullableRange((string? From, string? To) value) => new(value.From, value.To);

    public static implicit operator (string? From, string? To)(Int64NullableRange value) =>
        (value.From.ToString(), value.To.ToString());
}
