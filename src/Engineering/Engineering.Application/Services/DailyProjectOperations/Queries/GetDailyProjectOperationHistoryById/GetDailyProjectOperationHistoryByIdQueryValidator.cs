namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationHistoryById;

public class GetDailyProjectOperationHistoryByIdQueryValidator : AbstractValidator<GetDailyProjectOperationHistoryByIdQuery>
{
    public GetDailyProjectOperationHistoryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}