namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroups;

public class GetMachineriesGroupsValidator : AbstractValidator<GetMachineriesGroupsRequest>
{
    public GetMachineriesGroupsValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
