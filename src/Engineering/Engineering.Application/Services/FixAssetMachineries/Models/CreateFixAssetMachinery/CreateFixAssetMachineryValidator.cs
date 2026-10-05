
namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachinery;

public class CreateFixAssetMachineryValidator : AbstractValidator<CreateFixAssetMachineryRequest>
{
    public CreateFixAssetMachineryValidator()
    {
        RuleFor(oo => oo.MachineryId).NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachinery)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.FixAssetMachineryType).NotEmpty().WithError(FixAssetMachineryErrors.InValidType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum); ;
        RuleFor(oo => oo.IsActive).NotNull().WithError(FixAssetMachineryErrors.InValidIsActive);
    }
}
