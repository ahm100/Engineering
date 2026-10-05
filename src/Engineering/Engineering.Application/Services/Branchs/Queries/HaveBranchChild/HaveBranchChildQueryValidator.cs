namespace Engineering.Application.Services.Branchs.Queries.HaveBranchChild;

public class HaveBranchChildQueryValidator : AbstractValidator<HaveBranchChildQuery>
{
    public HaveBranchChildQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(BranchErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}