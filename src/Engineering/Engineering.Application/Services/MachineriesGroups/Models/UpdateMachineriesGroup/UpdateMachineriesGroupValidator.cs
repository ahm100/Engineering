namespace Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;

public class UpdateMachineriesGroupValidator : AbstractValidator<UpdateMachineriesGroupRequest>
{
    public UpdateMachineriesGroupValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.MachineriesGroupId);
        RuleFor(oo => oo.GroupName)
            .IsFullString(MachineriesCmts.GroupName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.GroupCode)
            .IsFullString(MachineriesCmts.GroupCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
