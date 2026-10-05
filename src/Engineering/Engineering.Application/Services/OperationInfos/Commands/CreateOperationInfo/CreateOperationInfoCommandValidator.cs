namespace Engineering.Application.Services.OperationInfos.Commands.CreateOperationInfo;

public class CreateOperationInfoCommandValidator : AbstractValidator<CreateOperationInfoCommand>
{
    public CreateOperationInfoCommandValidator()
    {
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.UnitOfMeasurementIdIsEmpty);
        RuleFor(oo => oo.OperationInfoName).NotEmpty().WithError(OperationInfoErrors.OperationInfoNameIsEmpty);
        RuleFor(oo => oo.OperationInfoCode).NotEmpty().WithError(OperationInfoErrors.OperationInfoCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationInfoErrors.IsActiveIsEmpty);
    }
}