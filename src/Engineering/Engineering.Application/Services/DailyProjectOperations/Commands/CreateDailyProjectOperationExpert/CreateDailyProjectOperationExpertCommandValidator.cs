namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationExpert;

public class CreateDailyProjectOperationExpertCommandValidator : AbstractValidator<CreateDailyProjectOperationExpertCommand>
{
    public CreateDailyProjectOperationExpertCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(DailyProjectOperationExpertErrors.InValidThirdPartyId);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(DailyProjectOperationExpertErrors.InValidFinalValue);
        RuleFor(oo => oo.DailyProjectOperation).NotNull().WithError(DailyProjectOperationExpertErrors.InValidDailyProjectOperation);
        RuleFor(oo => oo.ConsumableVolumeExpert).NotNull().WithError(DailyProjectOperationExpertErrors.InValidConsumableVolumeExpert);
    }
}
