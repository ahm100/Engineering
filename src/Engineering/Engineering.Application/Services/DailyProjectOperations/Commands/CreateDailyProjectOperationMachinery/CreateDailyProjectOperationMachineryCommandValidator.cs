namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationMachinery;

public class CreateDailyProjectOperationMachineryCommandValidator : AbstractValidator<CreateDailyProjectOperationMachineryCommand>
{
    public CreateDailyProjectOperationMachineryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().WithError(DailyProjectOperationMachineryErrors.InValidMachineId);
        RuleFor(oo => oo.DailyProjectOperation).NotNull().WithError(DailyProjectOperationMachineryErrors.InValidDailyProjectOperation);
        RuleFor(oo => oo.ConsumableVolumeMachinery).NotNull().WithError(DailyProjectOperationMachineryErrors.InValidConsumableVolumeMachinery);
    }
}
