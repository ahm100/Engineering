namespace Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;

public class CreateMachineriesGroupValidator : AbstractValidator<CreateMachineriesGroupRequest>
{
    public CreateMachineriesGroupValidator()
    {
        RuleFor(oo => oo.GroupName)
            .IsFullString(MachineriesCmts.GroupName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(oo => oo.GroupCode)
            .IsFullString(MachineriesCmts.GroupCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
