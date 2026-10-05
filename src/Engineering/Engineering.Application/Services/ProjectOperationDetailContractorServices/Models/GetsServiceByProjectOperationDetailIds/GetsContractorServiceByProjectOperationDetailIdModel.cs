
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;

public record GetsServiceByProjectOperationDetailIdsModel
{
    public long? ProjectOperationDetailId { get; set; }
    public string? PublicName { get; set; }
    public string? PublicCode { get; set; }
    public string? Description { get; set; }
    public DateTime? Created { get; set; }
    public List<GetServiceDataRequestModel>? Services { get; set; }
}
public record GetServiceDataRequestModel
{
    public long? Id { get; set; }
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
    public PODContractorServiceType Type { get; set; }
    public bool HaveRequest { get; set; }
}