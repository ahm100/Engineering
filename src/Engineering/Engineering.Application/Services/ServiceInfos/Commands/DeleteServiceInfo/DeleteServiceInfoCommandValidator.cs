
namespace Engineering.Application.Services.ServiceInfos.Commands.DeleteServiceInfo;

public class DeleteServiceInfoCommandValidator : AbstractValidator<DeleteServiceInfoCommand>
{
    public DeleteServiceInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}
