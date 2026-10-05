namespace Engineering.Domain.Entities.Synonyms.MetaData.Currencies;

public class ViewCurrency : ActivateEntity<ViewCurrency>
{
    public long CountryId { get; }
    public bool IsDefault { get; private set; }
    public int NumberOfDecimals { get; private set; }

    public string Iso { get; private set; }
    public string Name { get; private set; }
    public string? Symbol { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ViewCurrency() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}