using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.RequestMachineryStatusStatements;

[Description(RequestMachineryStatusStatementCmts.RequestMachineryStatusStatementDetailProjectOperation)]
public class RequestMachineryStatusStatementDetailProjectOperation : AuditableEntity<RequestMachineryStatusStatementDetailProjectOperation>
{
    #region Properties
    [Description(RequestMachineryStatusStatementCmts.RequestMachineryStatusStatementDetail)]
    public long RequestMachineryStatusStatementDetailId { get; private set; }
    public RequestMachineryStatusStatementDetail RequestMachineryStatusStatementDetail { get; private set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }
    #endregion

    public RequestMachineryStatusStatementDetailProjectOperation(
     RequestMachineryStatusStatementDetail requestMachineryStatusStatementDetail,
     ProjectOperation projectOperation) : this()
    {
        SetProjectOperation(projectOperation);
        SetRequestMachineryStatusStatementDetail(requestMachineryStatusStatementDetail);
    }

    #region Commands

    public void SetData(RequestMachineryStatusStatementDetail requestMachineryStatusStatementDetail,
      ProjectOperation projectOperation)
    {
        SetProjectOperation(projectOperation);
        SetRequestMachineryStatusStatementDetail(requestMachineryStatusStatementDetail);
    }

    public void SetRequestMachineryStatusStatementDetail(RequestMachineryStatusStatementDetail value)
    {
        RequestMachineryStatusStatementDetail = Guard.Against.Null(value, nameof(value));
        RequestMachineryStatusStatementDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryStatusStatementDetailProjectOperation()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    #endregion
}
