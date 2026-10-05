namespace Engineering.Domain.Entities.Advertisements;

[Description(AdvertisementCmts.Advertisement)]
public class Advertisement : ActivateEntity<Advertisement, long>
{
    [Description(AdvertisementCmts.TitleFa)]
    public string TitleFa { get; private set; } = string.Empty;
    [Description(AdvertisementCmts.TitleEn)]
    public string TitleEn { get; private set; } = string.Empty;

    [Description(AdvertisementCmts.DescriptionFa)]
    public string DescriptionFa { get; private set; } = string.Empty;
    [Description(AdvertisementCmts.DescriptionEn)]
    public string DescriptionEn { get; private set; } = string.Empty;

    [Description(AdvertisementCmts.TechnicalCode)]
    public string TechnicalCode { get; private set; } = string.Empty;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    public Advertisement(
        string titleFa,
        string titleEn,
        string descriptionFa,
        string descriptionEn,
        string technicalCode,
        List<string>? urls,
        long? companyId,
        bool isActive) : this()
    {
        SetTitleFa(titleFa);
        SetTitleEn(titleEn);
        SetDescriptionFa(descriptionFa);
        SetDescriptionEn(descriptionEn);
        SetTechnicalCode(technicalCode);
        SetCompanyId(companyId);
        AddDocuments(urls);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(
        string titleFa,
        string titleEn,
        string descriptionFa,
        string descriptionEn,
        string technicalCode,
        List<string>? urls,
        bool? isActive)
    {
        SetTitleFa(titleFa);
        SetTitleEn(titleEn);
        SetDescriptionFa(descriptionFa);
        SetDescriptionEn(descriptionEn);
        SetTechnicalCode(technicalCode);
        AddDocuments(urls);
        if (isActive is not null && isActive.Value)
            SetActive();
        else if (isActive is not null && !isActive.Value)
            SetDeactivate();
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetTitleFa(string value)
    {
        TitleFa = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetTitleEn(string value)
    {
        TitleEn = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetDescriptionFa(string value)
    {
        DescriptionFa = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetDescriptionEn(string value)
    {
        DescriptionEn = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetTechnicalCode(string value)
    {
        TechnicalCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            _advertisementDocument.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _advertisementDocument.Add(AdvertisementDocument.Create(url, this));
        }
        else
            _advertisementDocument.ForEach(c => c.SetIsDeleted());
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<AdvertisementDocument> _advertisementDocument;
    public IReadOnlyList<AdvertisementDocument> AdvertisementDocuments => _advertisementDocument;

    private Advertisement()
    {
        _advertisementDocument = [];
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}