namespace Engineering.Application.Services.DailyProjectOperations.Models.GetTotalsByProjectOperationDetailId;

public class GetTotalsByProjectOperationDetailIdValidator : AbstractValidator<GetTotalsByProjectOperationDetailIdRequest>
{
    public GetTotalsByProjectOperationDetailIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
    }
}
