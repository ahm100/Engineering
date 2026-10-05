namespace Engineering.Application.Services.ServiceInfos.Models.ActiveService;

public class ActiveServiceInfoValidator : AbstractValidator<ActiveServiceInfoRequest>
{
    public ActiveServiceInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}
