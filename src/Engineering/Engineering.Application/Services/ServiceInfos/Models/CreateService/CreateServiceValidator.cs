namespace Engineering.Application.Services.ServiceInfos.Models.CreateService;

public class CreateServiceInfoValidator : AbstractValidator<CreateServiceInfoRequest>
{
    public CreateServiceInfoValidator()
    {
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoNameIsEmpty);
        RuleFor(oo => oo.ServiceInfoCode).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ServiceInfoErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.ServiceInfoName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.ServiceInfoCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}