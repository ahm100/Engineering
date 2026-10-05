
namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdatesProjectOperationDetailDate;

public class UpdatesProjectOperationDetailDateValidator : AbstractValidator<UpdatesProjectOperationDetailDateRequest>
{
    public UpdatesProjectOperationDetailDateValidator()
    {
        When(oo => oo.RequestModel != null, () =>
        {
            RuleForEach(oo => oo.RequestModel).NotEmpty().SetValidator(new UpdatesProjectOperationDetailDateRequestModelValidator());
        });
    }
}
