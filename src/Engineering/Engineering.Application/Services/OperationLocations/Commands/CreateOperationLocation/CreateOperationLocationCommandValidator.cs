namespace Engineering.Application.Services.OperationLocations.Commands.CreateOperationLocation;

public class CreateOperationLocationCommandValidator : AbstractValidator<CreateOperationLocationCommand>
{
    public CreateOperationLocationCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotNull().WithError(OperationLocationErrors.CostCenterIsEmpty);
        RuleFor(oo => oo.PublicName).NotEmpty().WithError(OperationLocationErrors.PublicNameIsEmpty);
        RuleFor(oo => oo.PrivateName).NotEmpty().WithError(OperationLocationErrors.PrivateNameIsEmpty);
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
        RuleFor(oo => oo.PublicCode).NotEmpty().WithError(OperationLocationErrors.PublicCodeIsEmpty);
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(OperationLocationErrors.PriorityIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationLocationErrors.IsActiveIsEmpty);
    }
}