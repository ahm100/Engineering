
namespace Engineering.Application.Services.OperationInfoServices.Commands.DeleteOperationInfoService;

public class DeleteOperationInfoServiceCommandValidator : AbstractValidator<DeleteOperationInfoServiceCommand>
{
    public DeleteOperationInfoServiceCommandValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoServiceErrors.OperationInfoIsEmpty);
    }
}