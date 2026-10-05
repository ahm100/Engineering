namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public class PriceIndexContractTypeDetailAdjustmentRequestValidator
    : AbstractValidator<PriceIndexContractTypeDetailAdjustmentRequest>
{
    public PriceIndexContractTypeDetailAdjustmentRequestValidator()
    {
        RuleFor(oo => oo.BaseYear)
            .IsPositive(ContractCmts.PriceIndexBaseYear);

        RuleFor(oo => oo.BasePeriod)
            .IsEnum(ContractCmts.PriceIndexBasePeriod);

        RuleFor(oo => oo.ReferenceId)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);

        RuleFor(oo => oo.IndexId)
            .IsPositive(ContractCmts.ContractAdjustmentIndexId);
    }
}
