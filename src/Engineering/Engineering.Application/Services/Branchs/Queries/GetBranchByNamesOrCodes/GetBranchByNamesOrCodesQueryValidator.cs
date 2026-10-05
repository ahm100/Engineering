namespace Engineering.Application.Services.Branchs.Queries.GetBranchByNamesOrCodes;

public class GetBranchByNamesOrCodesQueryValidator : AbstractValidator<GetBranchByNamesOrCodesQuery>
{
    public GetBranchByNamesOrCodesQueryValidator()
    {
        RuleForEach(v => v.Names)
            .NotEmpty().WithError(BranchErrors.BranchNameIsEmpty);

        RuleForEach(v => v.Codes)
            .NotEmpty().WithError(BranchErrors.BranchCodeIsEmpty);
    }
}