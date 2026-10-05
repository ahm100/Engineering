namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;

public class GetContractTypeDetailsModel
{
    public long Id { get; set; }

    public long SourceId { get; set; }
    public string SourceTitle { get; set; } = string.Empty;
    public string? SourceCode { get; set; }

    public decimal Quantity { get; set; }

    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurementTitle { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? FixedAmount { get; set; }

    public string? TechnicalSpecifications { get; set; }

    public string? ExpectedDeliverables { get; set; }

    public decimal? Duration { get; set; }

    public Engineering.Domain.Entities.Contracts.Enums.ContractDurationUnit?
        DurationUnit
    { get; set; }

    public Engineering.Domain.Entities.Contracts.Enums.ContractTypeDetailAdjustmentType?
        AdjustmentType
    { get; set; }

    public string? AdjustmentTypeDesc => AdjustmentType?.GetEnumDescription();

    public bool IsSubjectToAdjustment { get; set; }

    public decimal? Amount { get; set; }
}
