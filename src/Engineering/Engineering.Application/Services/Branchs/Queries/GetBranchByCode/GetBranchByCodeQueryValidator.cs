namespace Engineering.Application.Services.Branchs.Queries.GetBranchByCode;

public class GetBranchByCodeQueryValidator : AbstractValidator<GetBranchByCodeQuery>
{
    public GetBranchByCodeQueryValidator()
    {
        RuleFor(v => v.BranchCode)
            .NotEmpty().WithError(BranchErrors.BranchCodeIsEmpty);
    }
}