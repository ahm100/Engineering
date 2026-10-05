namespace Engineering.Application.Services.CostCenters.Commands.UpdateCostCenter;

public class UpdateCostCenterCommandValidator : AbstractValidator<UpdateCostCenterCommand>
{
    public UpdateCostCenterCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.CostCenterType).NotEmpty().WithError(CostCenterErrors.CostCenterTypeIdIsEmpty);
        RuleFor(oo => oo.CostCenterName).NotEmpty().WithError(CostCenterErrors.CostCenterNameIsEmpty);
        RuleFor(oo => oo.CostCenterCode).NotEmpty().WithError(CostCenterErrors.CostCenterCodeIsEmpty);
        RuleFor(oo => oo.CityId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(CostCenterErrors.CityIdIsEmpty);
        RuleFor(oo => oo.Address).NotEmpty().WithError(CostCenterErrors.AddressIsEmpty);
        RuleFor(oo => oo.WeatherState).NotNull().WithError(CostCenterErrors.WeatherStateIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(CostCenterErrors.IsActiveIsEmpty);
    }
}