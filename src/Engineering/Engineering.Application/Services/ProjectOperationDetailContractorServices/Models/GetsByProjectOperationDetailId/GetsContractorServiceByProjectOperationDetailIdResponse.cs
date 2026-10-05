
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;

public record GetsContractorServiceByProjectOperationDetailIdResponse(
    List<GetsContractorServiceByProjectOperationDetailIdModel> Data,
    int RowCount
    );

public record GetsContractorServiceByProjectOperationDetailIdModel
{
    public long Id { get; set; }
    public DateTime Created { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public long ServiceInfoId { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public long? UnitOfMeasurementId { get; set; } = 0;
    public string? MeasurementName { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? OrganizationCode { get; set; } = string.Empty;
    public decimal Volume { get; set; }
    public decimal? UsedVolume { get; set; } = 0;
    public decimal? RemaindVolume => Volume - UsedVolume;
    public long? TimeSpantLong { get; set; }
    public string? TimeSpant { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? ProjectServiceDetailId { get; set; }
    public long? ProjectServiceId { get; set; }
    public string? ProjectServiceName { get; set; }
    public long? ProjectServiceUnitOfMeasurementId { get; set; }
    public string? ProjectServiceMeasurementName { get; set; } = string.Empty;
    public bool HaveProjectService => ProjectServiceDetailId is not null ? true : false;
    public ContractorServiceStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public decimal? ProjectServiceVolume { get; set; } = 0;
    public decimal? ProjectServiceDoneVolume { get; set; } = 0;
    public decimal? ProjectServiceRemaindVolume => ProjectServiceVolume - ProjectServiceDoneVolume;
    public bool HaveContractorStatusStatements { get; set; }
    public decimal? ContractorStatusStatementsVolume { get; set; } = 0;
    public PODContractorServiceType Type { get; set; }
}
