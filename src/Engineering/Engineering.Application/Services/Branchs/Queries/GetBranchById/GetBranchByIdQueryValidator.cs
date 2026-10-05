namespace Engineering.Application.Services.Branchs.Queries.GetBranchById;

public class GetBranchByIdQueryValidator : AbstractValidator<GetBranchByIdQuery>
{
    public GetBranchByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(BranchErrors.BranchWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}