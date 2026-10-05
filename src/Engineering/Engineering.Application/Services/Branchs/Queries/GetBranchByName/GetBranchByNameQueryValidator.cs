namespace Engineering.Application.Services.Branchs.Queries.GetBranchByName;

public class GetBranchByNameQueryValidator : AbstractValidator<GetBranchByNameQuery>
{
    public GetBranchByNameQueryValidator()
    {
        RuleFor(v => v.BranchName)
            .NotEmpty().WithError(BranchErrors.BranchNameIsEmpty);
    }
}