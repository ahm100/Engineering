namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForValidates;

public class GetProjectOperationDetailForValidatesQueryValidator : AbstractValidator<GetProjectOperationDetailForValidatesQuery>
{
    public GetProjectOperationDetailForValidatesQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
        RuleFor(oo => oo.OperationLocationId).NotNull().WithError(ProjectOperationDetailErrors.OperationLocationIdIsEmpty);
    }
}
