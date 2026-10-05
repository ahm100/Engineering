namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractType;

public class UpdateContractTypeValidator : AbstractValidator<UpdateContractTypeRequest>
{
    public UpdateContractTypeValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.Kind)
            .IsEnum(GlobalCmts.ContractTypeKind);

        RuleFor(oo => oo.PricingMethod)
            .IsEnum(GlobalCmts.PricingMethod);
    }
}
