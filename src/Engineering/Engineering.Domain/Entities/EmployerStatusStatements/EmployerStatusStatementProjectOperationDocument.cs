
namespace Engineering.Domain.Entities.EmployerStatusStatements;
/// <summary>
/// مستندات پیوست قرارداد پیمانکار
/// </summary>
public class EmployerStatusStatementProjectOperationDocument : AuditableEntity<EmployerStatusStatementProjectOperationDocument>
{
    /// <summary>
    /// لینک
    /// </summary>
    public string Url { get; private set; } = string.Empty;

    /// <summary>
    /// قرارداد پیمانکار
    /// </summary>
    public EmployerStatusStatementProjectOperation EmployerStatusStatementProjectOperation { get; private set; }

    public EmployerStatusStatementProjectOperationDocument(string url, EmployerStatusStatementProjectOperation employerStatusStatementProjectOperation)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        EmployerStatusStatementProjectOperation = Guard.Against.Null(employerStatusStatementProjectOperation, nameof(employerStatusStatementProjectOperation));
    }

    public static EmployerStatusStatementProjectOperationDocument Create(string url, EmployerStatusStatementProjectOperation employerStatusStatementProjectOperation)
    {
        return new EmployerStatusStatementProjectOperationDocument(url, employerStatusStatementProjectOperation);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementProjectOperationDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
