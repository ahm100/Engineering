namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerDocUrl)]
public class EmployerDocUrl : ActivateEntity<EmployerDocUrl>
{
    [Description(GlobalCmts.URL)]
    public string URL { get; private set; }

    [Description(EContractCmts.EmployerDoc)]
    public long EmployerDocId { get; set; }
    public EmployerDoc EmployerDoc { get; set; }

    public EmployerDocUrl(
        EmployerDoc doc,
        string uRL) : this()
    {
        SetEmployerDoc(doc);
        SetURL(uRL);
    }

    private void SetEmployerDoc(EmployerDoc value)
    {
        EmployerDoc = Guard.Against.Null(value, nameof(value));
        EmployerDocId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetURL(string value)
    {
        URL = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private EmployerDocUrl()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
