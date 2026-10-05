namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;

public class GetMachineriesGroupByNameValidator : AbstractValidator<GetMachineriesGroupByNameRequest>
{
    public GetMachineriesGroupByNameValidator()
    {
        RuleFor(oo => oo.GroupName)
            .IsFullString(MachineriesCmts.GroupName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}
