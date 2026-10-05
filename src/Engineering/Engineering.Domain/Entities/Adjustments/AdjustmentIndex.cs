using Engineering.Domain.Entities.Adjustment.Enums;
using Engineering.Domain.Entities.Contracts.Enums;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Engineering.Domain.Entities.Adjustments;

[Description(AdjustmentCmts.AdjustmentIndex)]
public class AdjustmentIndex : ActivateEntity<AdjustmentIndex>
{
    [Description(AdjustmentCmts.AdjustmentReferenceId)]
    public long AdjustmentReferenceId { get; private set; }

    public AdjustmentReference AdjustmentReference { get; private set; } = null!;
    public long YearId { get; private set; }

    public long? SeasonId { get; private set; }
    public Season? Season { get; private set; }

    public long BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.Title)]
    public string Title { get; private set; } = string.Empty;    

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(AdjustmentCmts.DocumentFile)]
    public string? DocumentFile { get; private set; }

    private readonly List<AdjustmentIndexValue> _values = [];

    public IReadOnlyList<AdjustmentIndexValue> Values =>
        _values.AsReadOnly();

  
    private AdjustmentIndex()
    {
    }

    public AdjustmentIndex(
        long adjustmentReferenceId,
         long yearId,
        Branch branch,
        Season? season,
        string code,
        string title,
        string? description, 
        string? documentFile)
    {
        YearId = yearId;
        SetBranch(branch);
        SetSeason(season);
        SetCode(code);
        SetTitle(title);
        SetDescription(description);
        SetDocumentFile(documentFile);
        AdjustmentReferenceId = adjustmentReferenceId;
        IsActive = true;
    }

    public void Update(
         long? yearId,
        Branch branch,
        Season? season,
        string code,
        string title,
        string? description, string? documentFile)
    {
        if (yearId.HasValue)
            YearId = yearId.Value;
        SetBranch(branch);
        SetSeason(season);
        SetCode(code);
        SetTitle(title);
        SetDocumentFile(documentFile);
        SetDescription(description);
    }

    public void AddValue(AdjustmentIndexValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _values.Add(value);
    }

    public void Activate()
        => IsActive = true;

    public void Deactivate()
        => IsActive = false;

    
    private void SetBranch(Branch value)
    {
        Branch = Guard.Against.Null(value, nameof(value));
        BranchId = value.Id;
    }

    private void SetSeason(Season? value)
    {
        Season = value;
        SeasonId = value?.Id;
    }

    private void SetCode(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 100)
            throw new ArgumentException(
                "Code cannot exceed 100 characters.",
                nameof(value));

        Code = value;
    }

    private void SetTitle(string value)
    {
        value = Guard.Against.NullOrWhiteSpace(value, nameof(value));

        if (value.Length > 250)
            throw new ArgumentException(
                "FaTitle cannot exceed 250 characters.",
                nameof(value));

        Title = value;
    }

    public void SetYearId(long value)
    {
        YearId = Guard.Against.NegativeOrZero(value, nameof(value));
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

    private void SetDocumentFile(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            DocumentFile = null;
            return;
        }

        if (value.Length > 500)
            throw new ArgumentException(
                "DocumentFile cannot exceed 500 characters.",
                nameof(value));

        DocumentFile = value;
    }

    public AdjustmentIndexValue? GetValue(long id)
    {
        return _values.FirstOrDefault(x => x.Id == id);
    }

    public void RemoveValue(long id)
    {
        var value = _values.FirstOrDefault(x => x.Id == id);

        if (value is null)
            return;

        value.SoftDelete();
    }
}
