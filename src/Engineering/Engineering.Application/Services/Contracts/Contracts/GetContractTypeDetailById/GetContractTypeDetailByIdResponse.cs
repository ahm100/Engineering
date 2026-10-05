using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;

public class GetContractTypeDetailByIdResponse
{
    public long Id { get; set; }

    public long ContractId { get; set; }

    public long ContractTypeId { get; set; }

    public long SourceId { get; set; }
    public string SourceTitle { get; set; } = string.Empty;
    public string? SourceCode { get; set; }

    public ContractTypeKind Kind { get; set; }

    public string KindTitle => Kind.GetEnumDescription();

    public PricingMethod PricingMethod { get; set; }

    public string PricingMethodTitle => PricingMethod.GetEnumDescription();

    public decimal Quantity { get; set; }

    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurementTitle { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? FixedAmount { get; set; }

    public string? TechnicalSpecifications { get; set; }

    public string? ExpectedDeliverables { get; set; }

    public decimal? Duration { get; set; }

    public ContractDurationUnit? DurationUnit { get; set; }

    public GetContractTypeDetailAdjustmentResponse? Adjustment { get; set; }

    public bool IsSubjectToAdjustment { get; set; }

    public decimal? Amount =>
        Engineering.Domain.Entities.Contracts.ContractFinancialMath
            .CalculateContractTypeDetailAmount(
                PricingMethod,
                Quantity,
                UnitPrice,
                FixedAmount,
                Duration);
}
