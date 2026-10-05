namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByCodes;

public class GetsMachineriesGroupByCodesQueryValidator : AbstractValidator<GetsMachineriesGroupByCodesQuery>
{
    public GetsMachineriesGroupByCodesQueryValidator()
    {
        RuleFor(oo => oo.Codes).NotEmpty().WithError(MachineriesGroupErrors.GroupCodeIsEmpty);
    }
}
