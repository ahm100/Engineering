
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdNoIncluding;

public class GetProjectOperationDetailByIdNoIncludingQueryValidator : AbstractValidator<GetProjectOperationDetailByIdNoIncludingQuery>
{
    public GetProjectOperationDetailByIdNoIncludingQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
