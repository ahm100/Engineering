namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineryConsumableVolumeById;

public class GetConsumableVolumeMachineryByIdValidator : AbstractValidator<GetConsumableVolumeMachineryByIdRequest>
{
    public GetConsumableVolumeMachineryByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.IdIsEmpty);
    }
}
