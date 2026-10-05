namespace Engineering.Application.Services.OperationInfoGroups.Commands.CreateOperationInfoGroup;

public class CreateOperationInfoGroupCommandValidator : AbstractValidator<CreateOperationInfoGroupCommand>
{
    public CreateOperationInfoGroupCommandValidator()
    {
        RuleFor(oo => oo.OperationInfoGroupName).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupNameIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupCode).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationInfoGroupErrors.IsActiveIsEmpty);
    }
}