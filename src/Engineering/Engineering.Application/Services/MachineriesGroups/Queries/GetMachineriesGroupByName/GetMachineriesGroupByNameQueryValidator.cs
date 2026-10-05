namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByName;

public class GetMachineriesGroupByNameQueryValidator : AbstractValidator<GetMachineriesGroupByNameQuery>
{
    public GetMachineriesGroupByNameQueryValidator()
    {
        RuleFor(oo => oo.GroupName).NotEmpty().WithError(MachineriesGroupErrors.GroupNameIsEmpty);
    }
}