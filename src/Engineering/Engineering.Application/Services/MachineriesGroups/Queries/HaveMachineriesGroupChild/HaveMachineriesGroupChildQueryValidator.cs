namespace Engineering.Application.Services.MachineriesGroups.Queries.HaveMachineriesGroupChild;

public class HaveMachineriesGroupChildQueryValidator : AbstractValidator<HaveMachineriesGroupChildQuery>
{
    public HaveMachineriesGroupChildQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MachineriesGroupErrors.IdIsEmpty);
    }
}