namespace Engineering.Application.Services.ServiceInfos.Models.InactiveService;

public class InactiveServiceInfoValidator : AbstractValidator<InactiveServiceInfoRequest>
{
    public InactiveServiceInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}
