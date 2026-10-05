namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetFilteredTotalOfConsumebleMachineries;

public class GetFilteredTotalOfConsumebleMachineriesQueryValidator : AbstractValidator<GetFilteredTotalOfConsumebleMachineriesQuery>
{
    public GetFilteredTotalOfConsumebleMachineriesQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ConsumableVolumeMachineryErrors.ProjectIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(ConsumableVolumeMachineryErrors.CostCenterIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.MachineryId).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
