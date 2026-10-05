namespace Engineering.Domain.Entities.Advertisements;

[Description(GlobalCmts.Document)]
public class AdvertisementDocument : AuditableEntity<AdvertisementDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(RGSCmts.RequestGoodsSupplyDetail)]
    public long AdvertisementId { get; private set; }
    public Advertisement Advertisement { get; private set; }

    public AdvertisementDocument(string url,
        Advertisement advertisement) : this()
    {
        SetUrl(url);
        SetAdvertisement(advertisement);
    }

    public static AdvertisementDocument Create(string url, Advertisement advertisement)
    {
        return new AdvertisementDocument(url, advertisement);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetAdvertisement(Advertisement value)
    {
        Advertisement = Guard.Against.Null(value, nameof(value));
        AdvertisementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AdvertisementDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}