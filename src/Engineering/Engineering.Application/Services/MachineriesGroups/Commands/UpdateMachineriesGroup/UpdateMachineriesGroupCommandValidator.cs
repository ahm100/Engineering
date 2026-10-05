namespace Engineering.Application.Services.MachineriesGroups.Commands.UpdateMachineriesGroup;

public class UpdateMachineriesGroupCommandValidator : AbstractValidator<UpdateMachineriesGroupCommand>
{
    public UpdateMachineriesGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineriesGroupErrors.IdIsEmpty);
        RuleFor(oo => oo.GroupName).NotEmpty().WithError(MachineriesGroupErrors.GroupNameIsEmpty);
        RuleFor(oo => oo.GroupCode).NotEmpty().WithError(MachineriesGroupErrors.GroupCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineryErrors.IsActiveIsEmpty);
    }
}