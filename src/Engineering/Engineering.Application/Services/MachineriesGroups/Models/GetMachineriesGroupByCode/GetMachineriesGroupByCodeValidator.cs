namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByCode;

public class GetMachineriesGroupByCodeValidator : AbstractValidator<GetMachineriesGroupByCodeRequest>
{
    public GetMachineriesGroupByCodeValidator()
    {
        RuleFor(oo => oo.GroupCode)
            .IsFullString(MachineriesCmts.GroupCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
