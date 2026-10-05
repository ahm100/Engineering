namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;

public class GetDailyProjectOperationHistoryByIdValidator : AbstractValidator<GetDailyProjectOperationHistoryByIdRequest>
{
    public GetDailyProjectOperationHistoryByIdValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
