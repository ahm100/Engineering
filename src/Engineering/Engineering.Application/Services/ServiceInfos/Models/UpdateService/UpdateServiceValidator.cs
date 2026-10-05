namespace Engineering.Application.Services.ServiceInfos.Models.UpdateService;

public class UpdateServiceInfoValidator : AbstractValidator<UpdateServiceInfoRequest>
{
    public UpdateServiceInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoNameIsEmpty);
        RuleFor(oo => oo.ServiceInfoCode).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
        RuleFor(oo => oo.MeasurementData.Id).NotNull().WithError(ServiceInfoErrors.UnitOfMeasurementIdIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ServiceInfoErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.ServiceInfoName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.ServiceInfoCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
