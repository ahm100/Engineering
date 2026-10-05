namespace Engineering.Application.Services.PublicGroups.Commands.DeletePublicGroup;

public class DeletePublicGroupCommandValidator : AbstractValidator<DeletePublicGroupCommand>
{
    public DeletePublicGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(OperationInfoErrors.NonStandardIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
