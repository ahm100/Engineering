namespace Engineering.Application.Services.OperationInfoGroups.Commands.UpdateOperationInfoGroup;

public class UpdateOperationInfoGroupCommandValidator : AbstractValidator<UpdateOperationInfoGroupCommand>
{
    public UpdateOperationInfoGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupName).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupNameIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupCode).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationInfoGroupErrors.IsActiveIsEmpty);
    }
}