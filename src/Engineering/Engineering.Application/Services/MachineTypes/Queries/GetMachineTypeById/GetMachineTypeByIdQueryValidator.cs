
namespace Engineering.Application.Services.MachineTypes.Queries.GetMachineTypeById;

public class GetMachineTypeByIdQueryValidator : AbstractValidator<GetMachineTypeByIdQuery>
{
    public GetMachineTypeByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MachineTypeErrors.IdIsEmpty);
    }
}