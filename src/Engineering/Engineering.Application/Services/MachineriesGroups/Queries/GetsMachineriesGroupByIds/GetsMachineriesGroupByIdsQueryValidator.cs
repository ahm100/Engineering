
namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByIds;

public class GetsMachineriesGroupByIdsQueryValidator : AbstractValidator<GetsMachineriesGroupByIdsQuery>
{
    public GetsMachineriesGroupByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
