namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetConsumableVolumeMachineryById;

public class GetConsumableVolumeMachineryByIdQueryValidator : AbstractValidator<GetConsumableVolumeMachineryByIdQuery>
{
    public GetConsumableVolumeMachineryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeMachineryErrors.IdIsEmpty);
    }
}
