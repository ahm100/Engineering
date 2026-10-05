namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCodes;

public class GetsBranchByCodesQueryValidator : AbstractValidator<GetsBranchByCodesQuery>
{
    public GetsBranchByCodesQueryValidator()
    {
        RuleFor(v => v.Codes)
            .NotEmpty().WithError(BranchErrors.BranchCodeIsEmpty);
    }
}