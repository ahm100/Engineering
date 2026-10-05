
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdWithoutInclude;

public class GetProjectOperationDetailByIdWithoutIncludeQueryValidator : AbstractValidator<GetProjectOperationDetailByIdWithoutIncludeQuery>
{
    public GetProjectOperationDetailByIdWithoutIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
