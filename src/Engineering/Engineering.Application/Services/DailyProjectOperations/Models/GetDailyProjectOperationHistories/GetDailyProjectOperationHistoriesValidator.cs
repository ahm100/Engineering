namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;

public class GetDailyProjectOperationHistoriesValidator : AbstractValidator<GetDailyProjectOperationHistoriesRequest>
{
    public GetDailyProjectOperationHistoriesValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
    }
}
