namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public class GetDailyProjectOperationByIdValidator : AbstractValidator<GetDailyProjectOperationByIdRequest>
{
    public GetDailyProjectOperationByIdValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
