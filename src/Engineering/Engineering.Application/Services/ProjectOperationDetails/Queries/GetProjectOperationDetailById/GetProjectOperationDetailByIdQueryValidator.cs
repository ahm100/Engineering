
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailById;

public class GetProjectOperationDetailByIdQueryValidator : AbstractValidator<GetProjectOperationDetailByIdQuery>
{
    public GetProjectOperationDetailByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
