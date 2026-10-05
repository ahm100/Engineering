
namespace Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorCostOver;

public record GetsDraftableContractorCostOverQuery(
    long ContractorId,
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsDraftableContractorCostOverModel>>>;

public record GetsDraftableContractorCostOverModel
{
    public long Id { get; set; }
    public long CostOverId { get; set; }
    public string? CostOverName { get; set; }
    public string? CostOverCode { get; set; }
    public long? ContractorContractId { get; set; }
    public string? ContractorContractType { get; set; } = string.Empty;
    public long? ContractorContractDetailId { get; set; }
    public bool IsContractorContractCostOver => ContractorContractId != null ? true : false;
    public DateTime Created { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long? ProjectOperationUnitOfMeasurementId { get; set; }
    public string? ProjectOperationUnitOfMeasurement { get; set; } = string.Empty;
    public string? ProjectOperationDetail { get; set; } = string.Empty;
    public string? ProjectOperationDetailDesc { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long? ServiceInfoUnitOfMeasurementId { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
}
