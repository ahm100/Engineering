namespace Engineering.Application.Services.Contracts.Contracts.CreateContractType;

public class CreateContractTypeValidator : AbstractValidator<CreateContractTypeRequest>
{
    public CreateContractTypeValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Kind)
            .IsEnum(GlobalCmts.ContractTypeKind);

        RuleFor(oo => oo.PricingMethod)
            .IsEnum(GlobalCmts.PricingMethod);
    }
}
