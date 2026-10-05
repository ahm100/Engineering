namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationById;

public class GetDailyProjectOperationByIdQueryValidator : AbstractValidator<GetDailyProjectOperationByIdQuery>
{
    public GetDailyProjectOperationByIdQueryValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
