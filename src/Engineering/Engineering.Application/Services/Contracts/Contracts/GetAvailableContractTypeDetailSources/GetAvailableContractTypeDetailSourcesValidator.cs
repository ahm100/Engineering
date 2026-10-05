namespace Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;

public class GetAvailableContractTypeDetailSourcesValidator
    : AbstractValidator<GetAvailableContractTypeDetailSourcesRequest>
{
    public GetAvailableContractTypeDetailSourcesValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.ContractTypeId)
            .IsPositive(ContractCmts.ContractTypeId);

        RuleFor(oo => oo.ProjectOperationId)
            .IsOptionalPositive(GlobalCmts.ProjectOperationId);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        RuleFor(oo => oo.OrderBy)
            .Must(orderBy =>
                RuleExtensions.HasOnlyValidOrderFields<
                    GetAvailableContractTypeDetailSourcesModel>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}