namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertConsumableVolumeById;

public class GetConsumableVolumeExpertByIdValidator : AbstractValidator<GetConsumableVolumeExpertByIdRequest>
{
    public GetConsumableVolumeExpertByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
    }
}
