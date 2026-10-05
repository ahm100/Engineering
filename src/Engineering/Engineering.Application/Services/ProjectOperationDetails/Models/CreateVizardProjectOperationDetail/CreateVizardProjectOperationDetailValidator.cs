
namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateVizardProjectOperationDetail;

public class CreateVizardProjectOperationDetailValidator : AbstractValidator<CreateVizardProjectOperationDetailRequest>
{
    public CreateVizardProjectOperationDetailValidator()
    {
        RuleForEach(oo => oo.OperationLocationIds).GreaterThanOrEqualTo(1).NotNull().WithError(ProjectOperationDetailErrors.OperationLocationIdIsEmpty);
        RuleFor(oo => oo.RequestModel).NotEmpty().WithError(ProjectOperationDetailErrors.RequestDataIsEmpty);
    }
}
