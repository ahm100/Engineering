
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailValidator;

public class GetProjectOperationDetailValidatorQueryValidator : AbstractValidator<GetProjectOperationDetailValidatorQuery>
{
    public GetProjectOperationDetailValidatorQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
