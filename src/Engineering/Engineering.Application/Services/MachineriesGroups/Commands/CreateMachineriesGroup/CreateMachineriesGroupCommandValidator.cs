namespace Engineering.Application.Services.MachineriesGroups.Commands.CreateMachineriesGroup;

public class CreateMachineriesGroupCommandValidator : AbstractValidator<CreateMachineriesGroupCommand>
{
    public CreateMachineriesGroupCommandValidator()
    {
        RuleFor(oo => oo.GroupName).NotEmpty().WithError(MachineriesGroupErrors.GroupNameIsEmpty);
        RuleFor(oo => oo.GroupCode).NotEmpty().WithError(MachineriesGroupErrors.GroupCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineryErrors.IsActiveIsEmpty);
    }
}