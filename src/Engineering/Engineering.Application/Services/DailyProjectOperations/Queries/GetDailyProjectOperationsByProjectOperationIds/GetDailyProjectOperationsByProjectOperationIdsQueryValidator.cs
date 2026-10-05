namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationsByProjectOperationIds;

public class GetDailyProjectOperationsByProjectOperationIdsQueryValidator : AbstractValidator<GetDailyProjectOperationsByProjectOperationIdsQuery>
{
    public GetDailyProjectOperationsByProjectOperationIdsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationIds).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationId);
    }
}
