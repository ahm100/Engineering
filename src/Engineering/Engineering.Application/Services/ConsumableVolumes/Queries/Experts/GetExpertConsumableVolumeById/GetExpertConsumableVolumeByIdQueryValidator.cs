namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;

public class GetConsumableVolumeExpertByIdQueryValidator : AbstractValidator<GetConsumableVolumeExpertByIdQuery>
{
    public GetConsumableVolumeExpertByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
    }
}
