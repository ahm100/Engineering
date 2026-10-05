namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetTotalsByProjectOperationDetailId;

public class GetTotalsByProjectOperationDetailIdQueryValidator : AbstractValidator<GetTotalsByProjectOperationDetailIdQuery>
{
    public GetTotalsByProjectOperationDetailIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
    }
}
