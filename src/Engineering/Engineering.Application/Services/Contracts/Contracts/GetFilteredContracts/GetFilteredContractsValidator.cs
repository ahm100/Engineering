namespace Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;

public class GetFilteredContractsValidator : AbstractValidator<GetFilteredContractsRequest>
{
    public GetFilteredContractsValidator()
    {
        RuleFor(oo => oo.ContractNumber)
            .IsOptionalPositive(ContractCmts.ContractNumber);

        RuleFor(oo => oo.FaTitle)
            .MaximumLength(250)
            .WithError(GlobalErrors.RequiredMaxLength(GlobalCmts.FaTitle, 250));

        RuleFor(oo => oo.EnTitle)
            .MaximumLength(250)
            .WithError(GlobalErrors.RequiredMaxLength(GlobalCmts.EnTitle, 250));

        RuleFor(oo => oo.ProjectId)
            .IsOptionalPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.ContractPartyId)
            .IsOptionalPositive(ContractCmts.ContractPartyId);

        RuleFor(oo => oo.Status)
            .IsNullableEnum(GlobalCmts.Status);

        RuleFor(oo => oo.DurationUnit)
            .IsNullableEnum(ContractCmts.DurationUnit);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        RuleFor(oo => oo.OrderBy)
            .Must(orderBy => RuleExtensions.HasOnlyValidOrderFields<GetFilteredContractsModel>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}