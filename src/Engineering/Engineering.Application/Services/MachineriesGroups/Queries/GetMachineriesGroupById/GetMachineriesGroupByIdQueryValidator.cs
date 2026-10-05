namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupById;

public class GetMachineriesGroupByIdQueryValidator : AbstractValidator<GetMachineriesGroupByIdQuery>
{
    public GetMachineriesGroupByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MachineriesGroupErrors.IdIsEmpty);
    }
}