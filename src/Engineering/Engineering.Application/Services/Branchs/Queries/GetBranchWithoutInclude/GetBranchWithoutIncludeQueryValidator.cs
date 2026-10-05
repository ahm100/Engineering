namespace Engineering.Application.Services.Branchs.Queries.GetBranchWithoutInclude;

public class GetBranchWithoutIncludeQueryValidator : AbstractValidator<GetBranchWithoutIncludeQuery>
{
    public GetBranchWithoutIncludeQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(BranchErrors.BranchWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}