using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.RequestMachineryStatusStatements;

[Description(RequestMachineryStatusStatementCmts.RequestMachineryStatusStatementDetailProjectOperationDetail)]
public class RequestMachineryStatusStatementDetailProjectOperationDetail : AuditableEntity<RequestMachineryStatusStatementDetailProjectOperationDetail>
{
    #region Properties

    [Description(RequestMachineryStatusStatementCmts.RequestMachineryStatusStatementDetail)]
    public long RequestMachineryStatusStatementDetailId { get; private set; }
    public RequestMachineryStatusStatementDetail RequestMachineryStatusStatementDetail { get; private set; }

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    #endregion

    public RequestMachineryStatusStatementDetailProjectOperationDetail(
     RequestMachineryStatusStatementDetail requestMachineryStatusStatementDetail,
     ProjectOperationDetail projectOperationDetail) : this()
    {
        SetRequestMachineryStatusStatementDetail(requestMachineryStatusStatementDetail);
        SetProjectOperationDetail(projectOperationDetail);
    }

    #region Commands

    public void SetData(
      RequestMachineryStatusStatementDetail requestMachineryStatusStatementDetail,
      ProjectOperationDetail projectOperationDetail)
    {
        SetRequestMachineryStatusStatementDetail(requestMachineryStatusStatementDetail);
        SetProjectOperationDetail(projectOperationDetail);
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

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryStatusStatementDetailProjectOperationDetail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    #endregion

}
