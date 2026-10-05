
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.ValidatesProjectOperationDetailForScheduling;

public class ValidatesProjectOperationDetailForSchedulingQueryValidator : AbstractValidator<ValidatesProjectOperationDetailForSchedulingQuery>
{
    public ValidatesProjectOperationDetailForSchedulingQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoIds).NotNull().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
        RuleFor(oo => oo.OperationLocationIds).NotNull().WithError(ProjectOperationDetailErrors.OperationLocationIdIsEmpty);
    }
}
