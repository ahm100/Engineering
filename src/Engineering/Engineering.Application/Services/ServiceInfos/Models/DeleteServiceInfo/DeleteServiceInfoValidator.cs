namespace Engineering.Application.Services.ServiceInfos.Models.DeleteServiceInfo;

public class DeleteServiceInfoValidator : AbstractValidator<DeleteServiceInfoRequest>
{
    public DeleteServiceInfoValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}
