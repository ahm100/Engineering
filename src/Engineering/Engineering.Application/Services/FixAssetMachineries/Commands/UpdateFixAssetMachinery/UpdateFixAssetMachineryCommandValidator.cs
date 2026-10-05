namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachinery;

public class UpdateFixAssetMachineryCommandValidator : AbstractValidator<UpdateFixAssetMachineryCommand>
{
    public UpdateFixAssetMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.Machinery).NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachinery);
        RuleFor(oo => oo.FixAssetMachineryType).NotEmpty().WithError(FixAssetMachineryErrors.InValidType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);
        RuleFor(oo => oo.IsActive).NotNull().WithError(FixAssetMachineryErrors.InValidIsActive);
    }
}