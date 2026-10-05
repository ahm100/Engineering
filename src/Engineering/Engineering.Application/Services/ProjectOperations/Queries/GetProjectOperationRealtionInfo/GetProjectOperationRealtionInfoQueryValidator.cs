namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationRealtionInfo;

public class GetProjectOperationRealtionInfoQueryValidator : AbstractValidator<GetProjectOperationRealtionInfoQuery>
{
    public GetProjectOperationRealtionInfoQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
