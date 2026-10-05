
namespace Engineering.Application.Services.MachineTypes.Queries.GetByCode;

public class GetMachineTypeByCodeValidator : AbstractValidator<GetMachineTypeByCodeQuery>
{
    public GetMachineTypeByCodeValidator()
    {
        RuleFor(oo => oo.MachineTypeCode).NotEmpty().WithError(MachineTypeErrors.MachineTypeCodeIsEmpty);
    }
}
