
namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdatesProjectOperationDetailDate;

public class UpdatesProjectOperationDetailDateRequestModelValidator : AbstractValidator<UpdatesProjectOperationDetailDateRequestModel>
{
    public UpdatesProjectOperationDetailDateRequestModelValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDetailErrors.IdIsEmpty);

        When(oo => oo.StartDate != null || oo.EndDate != null, () =>
        {
            RuleFor(oo => oo.StartDate).NotEmpty().WithError(GlobalErrors.StartDateIsNull);
            RuleFor(oo => oo.EndDate).NotEmpty().WithError(GlobalErrors.EndDateIsNull);
            RuleFor(oo => oo.StartDate!.Value.Date).LessThanOrEqualTo(oo => oo.EndDate!.Value.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);
            RuleFor(oo => oo.EndDate!.Value.Date).GreaterThanOrEqualTo(oo => oo.StartDate!.Value.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);
        });
    }
}
