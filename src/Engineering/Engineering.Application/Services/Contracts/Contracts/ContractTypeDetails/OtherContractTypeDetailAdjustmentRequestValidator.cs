namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public class OtherContractTypeDetailAdjustmentRequestValidator
    : AbstractValidator<OtherContractTypeDetailAdjustmentRequest>
{
    public OtherContractTypeDetailAdjustmentRequestValidator()
    {
        RuleFor(oo => oo.Basis)
            .HasMaxLength(ContractCmts.OtherBasis, 250);

        RuleFor(oo => oo.Reference)
            .HasMaxLength(ContractCmts.OtherReference, 250);

        RuleFor(oo => oo.Index)
            .HasMaxLength(ContractCmts.OtherIndex, 250);

        RuleFor(oo => oo.Description)
            .HasMaxLength(GlobalCmts.Description, 1500);
    }
}
