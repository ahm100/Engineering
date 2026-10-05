
namespace Engineering.Application.Services.MachineTypes.Queries.GetByName;

public class GetMachineTypeByNameQueryValidator : AbstractValidator<GetMachineTypeByNameQuery>
{
    public GetMachineTypeByNameQueryValidator()
    {
        RuleFor(oo => oo.MachineTypeTitle).NotEmpty().WithError(MachineTypeErrors.MachineTypeTitleIsEmpty);
    }
}
