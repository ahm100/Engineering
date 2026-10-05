namespace Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;

public class GetContractRegistrationGridValidator : AbstractValidator<GetContractRegistrationGridRequest>
{
    public GetContractRegistrationGridValidator()
    {
        RuleFor(x => x.ContractNumber).IsOptionalPositive(ContractCmts.ContractNumber);
        RuleFor(x => x.ContractNumberFrom).IsOptionalPositive(ContractCmts.ContractNumber);
        RuleFor(x => x.ContractNumberTo).IsOptionalPositive(ContractCmts.ContractNumber);
        RuleFor(x => x.Status).IsNullableEnum(GlobalCmts.Status);
        RuleFor(x => x.FilterData).MaximumLength(250);
        RuleFor(x => x.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(x => x.PageSize).PageSizeZero(GlobalCmts.PageSize);
        RuleFor(x => x.OrderBy).Must(x => RuleExtensions.HasOnlyValidOrderFields<GetContractRegistrationGridModel>(x))
            .WithMessage("Invalid OrderBy field.");
        RuleFor(x => x).Must(x => !x.ContractNumber.HasValue ||
            (!x.ContractNumberFrom.HasValue && !x.ContractNumberTo.HasValue))
            .WithError(ContractErrors.ContractRegistrationGridFilterInvalid);
        RuleFor(x => x).Must(x => !x.ContractNumberFrom.HasValue || !x.ContractNumberTo.HasValue ||
            x.ContractNumberFrom.Value <= x.ContractNumberTo.Value)
            .WithError(ContractErrors.ContractRegistrationGridFilterInvalid);
    }
}
