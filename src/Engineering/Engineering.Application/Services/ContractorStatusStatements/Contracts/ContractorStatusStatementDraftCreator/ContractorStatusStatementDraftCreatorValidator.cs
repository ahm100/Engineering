
namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;

public class ContractorStatusStatementDraftCreatorValidator : AbstractValidator<ContractorStatusStatementDraftCreatorRequest>
{
    public ContractorStatusStatementDraftCreatorValidator()
    {
        RuleFor(oo => oo.ContractorId)
            .NotNull().WithError(CSSErrors.InValidContractor)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(CSSErrors.InValidProjectId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.StartDate)
            .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);

        RuleFor(oo => oo.StartDate.Date)
            .LessThanOrEqualTo(oo => oo.EndDate.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

        RuleFor(oo => oo.EndDate.Date)
            .GreaterThanOrEqualTo(oo => oo.StartDate.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);

    }
}
