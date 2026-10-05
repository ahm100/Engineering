using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.RequestMachineries;

public class RequestMachineryProjectOperationDetail : AuditableEntity<RequestMachineryProjectOperationDetail>
{
    #region Properties

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    #endregion

    public RequestMachineryProjectOperationDetail(ProjectOperationDetail projectOperationDetail,
                                                  RequestMachinery requestMachinery) : this()
    {
        SetProjectOperationDetail(projectOperationDetail);
        SetRequestMachinery(requestMachinery);
    }

    #region Commands

    public void SetData(ProjectOperationDetail projectOperationDetail,
                        RequestMachinery requestMachinery)
    {
        SetProjectOperationDetail(projectOperationDetail);
        SetRequestMachinery(requestMachinery);
    }

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryProjectOperationDetail() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion


}
