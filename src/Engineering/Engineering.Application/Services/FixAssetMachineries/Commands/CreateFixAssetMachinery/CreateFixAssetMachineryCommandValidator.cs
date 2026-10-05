namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachinery;

public class CreateFixAssetMachineryCommandValidator : AbstractValidator<CreateFixAssetMachineryCommand>
{
    public CreateFixAssetMachineryCommandValidator()
    {
        RuleFor(oo => oo.Machinery).NotEmpty().WithError(FixAssetMachineryErrors.InValidMachinery);
        RuleFor(oo => oo.FixAssetMachineryType).NotEmpty().WithError(FixAssetMachineryErrors.InValidType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum); ;
        RuleFor(oo => oo.IsActive).NotNull().WithError(FixAssetMachineryErrors.InValidIsActive);
    }
}