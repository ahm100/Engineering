namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByCode;

public class GetMachineriesGroupByCodeQueryValidator : AbstractValidator<GetMachineriesGroupByCodeQuery>
{
    public GetMachineriesGroupByCodeQueryValidator()
    {
        RuleFor(oo => oo.GroupCode).NotEmpty().WithError(MachineriesGroupErrors.GroupCodeIsEmpty);
    }
}