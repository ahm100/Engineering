namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationByLegacyId;

public class GetDailyProjectOperationByLegacyIdValidator : AbstractValidator<GetDailyProjectOperationByLegacyIdRequest>
{
    public GetDailyProjectOperationByLegacyIdValidator()
    {
        RuleFor(oo => oo.LegacyId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
