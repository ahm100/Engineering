using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;

public record GetsContractorContractServiceReportResponse(
    GetsContractorContractServiceReportTotalModel OtherData,
    List<GetsContractorContractServiceReportModel> Data,
    int RowCount
    );

public record GetsContractorContractServiceReportModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; } = string.Empty;
    public ProjectOperationStatus? ProjectOperationStatus { get; set; }
    public string? ProjectOperationStatusDescription => ProjectOperationStatus?.GetEnumDescription();
    public long? UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public ProjectOperationDetailStatus? ProjectOperationDetailStatus { get; set; }
    public string? ProjectOperationDetailStatusDescription => ProjectOperationDetailStatus?.GetEnumDescription();
    public DateTime? ProjectOperationDetailStartDate { get; set; }
    public string? ProjectOperationDetailStartDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailStartDate);
    public DateTime? ProjectOperationDetailEndDate { get; set; }
    public string? ProjectOperationDetailEndDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailEndDate);
    public DateTime? ProjectOperationDetailCreateDate { get; set; }
    public string? ProjectOperationDetailCreateDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailCreateDate);
    public long? OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long? ServiceInfoMeasureId { get; set; }
    public string? ServiceInfoMeasure { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public string? CreatorNickname { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public decimal? Price { get; set; } = 0;
    public decimal? TotalPrice { get; set; } = 0;
    public string? ContractorContractCode { get; set; }
    public ContractorContractStatus? ContractorContractStatus { get; set; }
    public string? ContractorContractStatusDescription => ContractorContractStatus?.GetEnumDescription();
    public string? ContractorContractType { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public string? Description { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
}

public record GetsContractorContractServiceReportTotalModel
{
    public decimal TotalServiceVolume { get; set; } = 0;
    public decimal Price { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0;
}