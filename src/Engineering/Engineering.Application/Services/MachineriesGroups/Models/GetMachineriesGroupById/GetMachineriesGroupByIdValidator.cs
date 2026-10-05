namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupById;

public class GetMachineriesGroupByIdValidator : AbstractValidator<GetMachineriesGroupByIdRequest>
{
    public GetMachineriesGroupByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.MachineriesGroupId);
    }
}
