namespace Engineering.ClientSdk.Models.Filters;

public struct DateTimeRange : IEquatable<DateTimeRange>
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public DateTimeRange()
    {
    }

    public DateTimeRange(DateTime? from, DateTime? to)
    {
        From = from;
        To = to;
    }

    public bool Equals(DateTimeRange other)
    {
        return Nullable.Equals(From, other.From) && Nullable.Equals(To, other.To);
    }

    public override bool Equals(object? obj)
    {
        return obj is DateTimeRange other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    public static implicit operator DateTimeRange((DateTime? From, DateTime? To) value) => new(value.From, value.To);
    public static implicit operator DateTimeRange((DateTime From, DateTime To) value) => new(value.From, value.To);
    public static implicit operator (DateTime? From, DateTime? To)(DateTimeRange value) => (value.From, value.To);

    public static implicit operator (DateTime From, DateTime To)(DateTimeRange value) =>
    (
        value.From ?? throw new InvalidOperationException("The 'From' field in the DateTimeRange is null."),
        value.To ?? throw new InvalidOperationException("The 'To' field in the DateTimeRange is null.")
    );
}
