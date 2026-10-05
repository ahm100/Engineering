
namespace Engineering.Domain.Entities.EmployerStatusStatements;
/// <summary>
/// مستندات پیوست قرارداد پیمانکار
/// </summary>
public class EmployerStatusStatementDocument : AuditableEntity<EmployerStatusStatementDocument>
{
    /// <summary>
    /// لینک
    /// </summary>
    public string Url { get; private set; } = string.Empty;

    /// <summary>
    /// قرارداد پیمانکار
    /// </summary>
    public EmployerStatusStatement EmployerStatusStatement { get; private set; }

    public EmployerStatusStatementDocument(string url, EmployerStatusStatement employerStatusStatement)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        EmployerStatusStatement = Guard.Against.Null(employerStatusStatement, nameof(employerStatusStatement));
    }

    public static EmployerStatusStatementDocument Create(string url, EmployerStatusStatement employerStatusStatement)
    {
        return new EmployerStatusStatementDocument(url, employerStatusStatement);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
