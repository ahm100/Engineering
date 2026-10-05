namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetFilteredTotalOfConsumebleMachineries;

public class GetFilteredTotalOfConsumebleMachineriesValidator : AbstractValidator<GetFilteredTotalOfConsumebleMachineriesRequest>
{
    public GetFilteredTotalOfConsumebleMachineriesValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ConsumableVolumeMachineryErrors.ProjectIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(ConsumableVolumeMachineryErrors.CostCenterIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.MachineryId).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
