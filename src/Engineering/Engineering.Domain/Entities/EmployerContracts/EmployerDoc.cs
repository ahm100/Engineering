namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerDoc)]
public class EmployerDoc : ActivateEntity<EmployerDoc, long>
{
    [Description(EContractCmts.EDocumentType)]
    public EDocumentType Type { get; private set; } = EDocumentType.Other;
    [Description(EContractCmts.RegistrationDate)]
    public DateTime RegistrationDate { get; private set; } = DateTime.Now;
    [Description(GlobalCmts.Version)]
    public decimal? Version { get; private set; }
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(EContractCmts.EmployerContract)]
    public long EmployerContractId { get; set; }
    public EmployerContract EmployerContract { get; set; }

    public EmployerDoc(
        EmployerContract contract,
        EDocumentType type,
        decimal version,
        List<string> uRLs,
        string? description,
        DateTime? registration,
        bool isActive) : this()
    {
        SetEmployerContract(contract);
        SetType(type);
        SetVersion(version);
        SetRegistrationDate(registration);
        SetDescription(description);
        AddURLs(uRLs);

        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    private void SetEmployerContract(EmployerContract value)
    {
        EmployerContract = Guard.Against.Null(value, nameof(value));
        EmployerContractId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void AddURLs(List<string> values)
    {
        foreach (var value in values)
            _employerDocUrls.Add(new(this, value));
    }

    public void Update(
        EDocumentType type,
        decimal version,
        List<string> uRLs,
        string? description,
        DateTime? registration,
        bool isActive)
    {
        SetType(type);
        SetVersion(version);
        SetRegistrationDate(registration);
        SetDescription(description);
        ModifyURLs(uRLs);

        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    private void ModifyURLs(List<string> values)
    {
        if (_employerDocUrls.Count > 0)
            foreach (var value in _employerDocUrls)
                value.SoftDelete();

        foreach (var value in values)
            _employerDocUrls.Add(new(this, value));
    }

    private void SetType(EDocumentType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    private void SetDescription(string? value)
    {
        Description = value ?? "";
    }

    private void SetVersion(decimal? value)
    {
        Version = value;
    }

    private void SetRegistrationDate(DateTime? value)
    {
        RegistrationDate = value == null ? DateTime.Now : value.Value;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(EContractCmts.EmployerDoc)]
    private List<EmployerDocUrl> _employerDocUrls;
    public IReadOnlyList<EmployerDocUrl> EmployerDocUrls => _employerDocUrls;
    private EmployerDoc()
    {
        _employerDocUrls = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
