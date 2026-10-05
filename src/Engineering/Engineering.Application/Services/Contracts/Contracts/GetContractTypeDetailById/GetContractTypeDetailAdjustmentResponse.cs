using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;

public class GetContractTypeDetailAdjustmentResponse
{
    public long Id { get; set; }

    public ContractTypeDetailAdjustmentType Type { get; set; }

    public string TypeDesc => Type.GetEnumDescription();

    public int? BaseYear { get; set; }

    public ContractAdjustmentPeriod? BasePeriod { get; set; }

    public string? BasePeriodDesc => BasePeriod?.GetEnumDescription();

    public string? Reference { get; set; }

    public string? Index { get; set; }

    public DateTime? BaseDate { get; set; }

    public decimal? BaseRate { get; set; }

    public long? CurrencyId { get; set; }

    public string? CurrencyTitle { get; set; }

    public ContractAdjustmentCurrencyReferenceType? ReferenceType { get; set; }

    public string? ReferenceTypeDesc => ReferenceType?.GetEnumDescription();

    public string? CustomReference { get; set; }

    public string? Basis { get; set; }

    public string? Description { get; set; }

    public long? PriceIndexReferenceId { get; set; }

    public string? PriceIndexReferenceFaTitle { get; set; }

    public string? PriceIndexReferenceEnTitle { get; set; }

    public long? PriceIndexId { get; set; }

    public string? PriceIndexFaTitle { get; set; }

    public string? PriceIndexEnTitle { get; set; }
}
