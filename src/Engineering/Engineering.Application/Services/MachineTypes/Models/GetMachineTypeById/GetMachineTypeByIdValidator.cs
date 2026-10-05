namespace Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;

public class GetMachineTypeByIdValidator : AbstractValidator<GetMachineTypeByIdRequest>
{
    public GetMachineTypeByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}

