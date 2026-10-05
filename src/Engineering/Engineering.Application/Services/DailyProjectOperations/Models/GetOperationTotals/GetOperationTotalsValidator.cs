namespace Engineering.Application.Services.DailyProjectOperations.Models.GetOperationTotals;

public class GetOperationTotalsValidator : AbstractValidator<GetOperationTotalsRequest>
{
    public GetOperationTotalsValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationId);
    }
}
