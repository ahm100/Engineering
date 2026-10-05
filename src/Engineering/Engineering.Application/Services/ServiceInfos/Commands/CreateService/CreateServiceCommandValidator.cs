namespace Engineering.Application.Services.ServiceInfos.Commands.CreateService;

public class CreateServiceInfoCommandValidator : AbstractValidator<CreateServiceInfoCommand>
{
    public CreateServiceInfoCommandValidator()
    {
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoNameIsEmpty);
        RuleFor(oo => oo.ServiceInfoCode).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().WithError(ServiceInfoErrors.UnitOfMeasurementIdIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ServiceInfoErrors.IsActiveIsEmpty);
    }
}