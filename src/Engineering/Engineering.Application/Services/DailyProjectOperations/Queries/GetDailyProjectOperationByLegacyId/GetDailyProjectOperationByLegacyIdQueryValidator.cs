namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationByLegacyId;

public class GetDailyProjectOperationByLegacyIdQueryValidator : AbstractValidator<GetDailyProjectOperationByLegacyIdQuery>
{
    public GetDailyProjectOperationByLegacyIdQueryValidator()
    {
        RuleFor(oo => oo.LegacyId).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
