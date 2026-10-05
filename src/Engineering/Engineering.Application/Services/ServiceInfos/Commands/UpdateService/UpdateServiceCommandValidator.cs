namespace Engineering.Application.Services.ServiceInfos.Commands.UpdateService;

public class UpdateServiceInfoCommandValidator : AbstractValidator<UpdateServiceInfoCommand>
{
    public UpdateServiceInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoNameIsEmpty);
        RuleFor(oo => oo.ServiceInfoCode).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().WithError(ServiceInfoErrors.UnitOfMeasurementIdIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ServiceInfoErrors.IsActiveIsEmpty);
    }
}