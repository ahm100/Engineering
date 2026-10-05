
namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetProjectOperationSchedulings;

public class GetProjectOperationSchedulingsQueryValidator : AbstractValidator<GetProjectOperationSchedulingsQuery>
{
    public GetProjectOperationSchedulingsQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
