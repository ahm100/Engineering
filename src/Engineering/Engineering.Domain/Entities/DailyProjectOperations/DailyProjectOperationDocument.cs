namespace Engineering.Domain.Entities.DailyProjectOperations;

[Description(DailyProjectOperationCmts.DailyProjectOperationDocuments)]
public class DailyProjectOperationDocument : AuditableEntity<DailyProjectOperationDocument>
{

    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public DailyProjectOperation DailyProjectOperation { get; private set; }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DailyProjectOperationDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public DailyProjectOperationDocument(string url,
                                         DailyProjectOperation dailyProjectOperation) : this()
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        DailyProjectOperation = Guard.Against.Null(dailyProjectOperation, nameof(dailyProjectOperation));
    }

    #endregion

    #region Commands

    /// <summary>
    /// حذف
    /// </summary>
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion
}
