namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetOperationTotals;

public class GetOperationTotalsQueryValidator : AbstractValidator<GetOperationTotalsQuery>
{
    public GetOperationTotalsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationId);
    }
}
