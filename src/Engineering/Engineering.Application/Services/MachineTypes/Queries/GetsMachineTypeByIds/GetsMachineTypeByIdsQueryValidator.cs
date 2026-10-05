
namespace Engineering.Application.Services.MachineTypes.Queries.GetsMachineTypeByIds;

public class GetsMachineTypeByIdsQueryValidator : AbstractValidator<GetsMachineTypeByIdsQuery>
{
    public GetsMachineTypeByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
