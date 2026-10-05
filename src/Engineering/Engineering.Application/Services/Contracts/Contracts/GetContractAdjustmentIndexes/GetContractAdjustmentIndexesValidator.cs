namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;

public class GetContractAdjustmentIndexesValidator
    : AbstractValidator<GetContractAdjustmentIndexesRequest>
{
    public GetContractAdjustmentIndexesValidator()
    {
        RuleFor(oo => oo.ReferenceId)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}
