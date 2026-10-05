namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractAdjustmentReference)]
public class ContractAdjustmentReference
    : AuditableEntity<ContractAdjustmentReference, long>
{
    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.FaTitle)]
    public string FaTitle { get; private set; } = string.Empty;

    [Description(GlobalCmts.EnTitle)]
    public string EnTitle { get; private set; } = string.Empty;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    private readonly List<ContractAdjustmentIndex> _indexes = [];

    public IReadOnlyList<ContractAdjustmentIndex> Indexes =>
        _indexes.AsReadOnly();

    private ContractAdjustmentReference()
    {
    }

    public ContractAdjustmentReference(
        string code,
        string faTitle,
        string enTitle,
        string? description)
    {
        SetCode(code);
        SetFaTitle(faTitle);
        SetEnTitle(enTitle);
        SetDescription(description);
        IsActive = true;
    }

    public void Update(
        string code,
        string faTitle,
        string enTitle,
        string? description)
    {
        SetCode(code);
        SetFaTitle(faTitle);
        SetEnTitle(enTitle);
        SetDescription(description);
    }

    public void Activate()
        => IsActive = true;

    public void Deactivate()
        => IsActive = false;

    public ContractAdjustmentIndex AddIndex(
        string code,
        string faTitle,
        string enTitle,
        string? description)
    {
        var index = new ContractAdjustmentIndex(
            this,
            code,
            faTitle,
            enTitle,
            description);

        _indexes.Add(index);

        return index;
    }

    public void UpdateIndex(
        long id,
        string code,
        string faTitle,
        string enTitle,
        string? description)
    {
        var index = _indexes.First(oo => oo.Id == id);

        index.Update(
            code,
            faTitle,
            enTitle,
            description);
    }

    public void ActivateIndex(long id)
        => _indexes.First(oo => oo.Id == id).Activate();

    public void DeactivateIndex(long id)
        => _indexes.First(oo => oo.Id == id).Deactivate();

    private void SetCode(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 100)
            throw new ArgumentException(
                "Code cannot exceed 100 characters.",
                nameof(value));

        Code = value;
    }

    private void SetFaTitle(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 250)
            throw new ArgumentException(
                "FaTitle cannot exceed 250 characters.",
                nameof(value));

        FaTitle = value;
    }

    private void SetEnTitle(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 250)
            throw new ArgumentException(
                "EnTitle cannot exceed 250 characters.",
                nameof(value));

        EnTitle = value;
    }

    private void SetDescription(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Description = null;
            return;
        }

        if (value.Length > 1500)
            throw new ArgumentException(
                "Description cannot exceed 1500 characters.",
                nameof(value));

        Description = value;
    }
}
