namespace Engineering.Application.Services.OperationInfoServices.Commands.CreateOperationInfoService;

public class CreateOperationInfoServiceCommandValidator : AbstractValidator<CreateOperationInfoServiceCommand>
{
    public CreateOperationInfoServiceCommandValidator()
    {
        RuleFor(oo => oo.OperationInfo).NotEmpty().WithError(OperationInfoServiceErrors.OperationInfoIsEmpty);
    }
}