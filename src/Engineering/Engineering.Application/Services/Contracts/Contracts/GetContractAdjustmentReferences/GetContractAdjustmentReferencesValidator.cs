namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;

public class GetContractAdjustmentReferencesValidator
    : AbstractValidator<GetContractAdjustmentReferencesRequest>
{
    public GetContractAdjustmentReferencesValidator()
    {
        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}
