
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetTotalsByProjectOperationId;

public class GetTotalsByProjectOperationIdQueryValidator : AbstractValidator<GetTotalsByProjectOperationIdQuery>
{
    public GetTotalsByProjectOperationIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
    }
}
