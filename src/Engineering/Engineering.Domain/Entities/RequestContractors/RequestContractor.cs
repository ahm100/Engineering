using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestContractors.Enums;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Domain.Entities.RequestContractors;

[Description(GlobalCmts.RequestContractor)]
public class RequestContractor : AuditableEntity<RequestContractor>
{
    #region Properties

    [Description(GlobalCmts.RequestNumber)]
    public long? RequestNumber { get; private set; }

    [Description(GlobalCmts.Status)]
    public RequestContractorStatus Status { get; private set; } = RequestContractorStatus.New;

    [Description(GlobalCmts.Volume)]
    public decimal Volume { get; private set; }

    [Description(RequestContractorCmts.StatusDescription)]
    public string? StatusDescription { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    [Description(GlobalCmts.ServiceInfo)]
    public long ServiceInfoId { get; private set; }
    public ServiceInfo ServiceInfo { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(RequestContractorCmts.ConfirmedRequestContractorInquiry)]
    public RequestContractorInquiry? ConfirmInquiry => Inquiries.FirstOrDefault(x => x.IsConfirmed == true);

    #endregion

    #region Constructors

    public RequestContractor(
        decimal volume,
        string? description,
        long? companyId,
        ProjectOperationDetail projectOperationDetail,
        ServiceInfo serviceInfo
        ) : this()
    {
        SetVolume(volume);
        SetDescription(description);
        SetCompanyId(companyId);
        SetProjectOperationDetail(projectOperationDetail);
        SetServiceInfo(serviceInfo);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<RequestContractorHistory> _histories;
    public IReadOnlyList<RequestContractorHistory> Histories => _histories;

    private readonly List<RequestContractorInquiry> _inquiries;
    public IReadOnlyList<RequestContractorInquiry> Inquiries => _inquiries;

    private RequestContractor()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _histories = [];
        _inquiries = [];
    }

    #endregion

    #region Commands

    public void SetData(
        decimal volume,
        string? description,
        long? companyId,
        ProjectOperationDetail projectOperationDetail,
        ServiceInfo serviceInfo)
    {
        Volume = Guard.Against.Null(volume, nameof(volume));
        Description = description;
        CompanyId = companyId;
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ServiceInfo = Guard.Against.Null(serviceInfo, nameof(serviceInfo));
    }

    public void AddHistory(string? statusDescription)
    {
        _histories.Add(new RequestContractorHistory(
            Status,
            Volume,
            statusDescription,
            Description,
            this));
    }

    public void ChangeStatus(RequestContractorStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetVolume(decimal value)
    {
        Volume = Guard.Against.Null(value, nameof(value));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetStatusDescription(string? value)
    {
        StatusDescription = value;
    }

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetServiceInfo(ServiceInfo value)
    {
        ServiceInfo = Guard.Against.Null(value, nameof(value));
        ServiceInfoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    #endregion
}
