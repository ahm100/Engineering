
namespace Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;

public class CreateOperationInfoServiceValidator : AbstractValidator<CreateOperationInfoServiceRequest>
{
    public CreateOperationInfoServiceValidator()
    {
        RuleFor(oo => oo.OperationInfoIds).NotNull().WithError(OperationInfoServiceErrors.OperationInfoIsEmpty);
    }
}
