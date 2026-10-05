namespace Engineering.Application.Services.CostCenters.Commands.CreateCostCenter;

public class CreateCostCenterCommandValidator : AbstractValidator<CreateCostCenterCommand>
{
    public CreateCostCenterCommandValidator()
    {
        RuleFor(oo => oo.CostCenterType).NotEmpty().WithError(CostCenterErrors.CostCenterTypeIdIsEmpty);
        RuleFor(oo => oo.CostCenterName).NotEmpty().WithError(CostCenterErrors.CostCenterNameIsEmpty);
        RuleFor(oo => oo.CostCenterCode).NotEmpty().WithError(CostCenterErrors.CostCenterCodeIsEmpty);
        RuleFor(oo => oo.CityId).NotNull().WithError(CostCenterErrors.CityIdIsEmpty);
        RuleFor(oo => oo.Address).NotEmpty().WithError(CostCenterErrors.AddressIsEmpty);
        RuleFor(oo => oo.WeatherState).NotNull().WithError(CostCenterErrors.WeatherStateIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(CostCenterErrors.IsActiveIsEmpty);
    }
}