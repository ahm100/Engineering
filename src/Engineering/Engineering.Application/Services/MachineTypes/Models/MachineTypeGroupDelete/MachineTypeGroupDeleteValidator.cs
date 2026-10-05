
namespace Engineering.Application.Services.MachineTypes.Models.MachineTypeGroupDelete;

public class MachineTypeGroupDeleteValidator : AbstractValidator<MachineTypeGroupDeleteRequest>
{
    public MachineTypeGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
