namespace Engineering.Application.Services.ProjectOperations.Models.GetsPrioritizeProjectOperation;

public class GetsPrioritizeProjectOperationValidator : AbstractValidator<GetsPrioritizeProjectOperationRequest>
{
    public GetsPrioritizeProjectOperationValidator()
    {
        RuleFor(oo => oo.Priority).GreaterThan(1).WithError(ProjectOperationErrors.PriorityCanNot1);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
