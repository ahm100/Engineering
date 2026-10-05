namespace Engineering.Application.Services.OperationInfos.Commands.UpdateOperationInfo;

public class UpdateOperationInfoCommandValidator : AbstractValidator<UpdateOperationInfoCommand>
{
    public UpdateOperationInfoCommandValidator()
    {
        RuleFor(oo => oo.OperationInfo).NotNull().WithError(OperationInfoErrors.IdIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.UnitOfMeasurementIdIsEmpty);
        RuleFor(oo => oo.OperationInfoName).NotEmpty().WithError(OperationInfoErrors.OperationInfoNameIsEmpty);
        RuleFor(oo => oo.OperationInfoCode).NotEmpty().WithError(OperationInfoErrors.OperationInfoCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationInfoErrors.IsActiveIsEmpty);
    }
}