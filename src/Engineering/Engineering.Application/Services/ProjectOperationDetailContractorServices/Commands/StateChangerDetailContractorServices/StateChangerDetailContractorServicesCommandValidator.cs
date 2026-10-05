namespace Engineering.Application.Services.DetailContractorServices.Commands.StateChangerDetailContractorServices;

public class StateChangerDetailContractorServicesCommandValidator : AbstractValidator<StateChangerDetailContractorServicesCommand>
{
    public StateChangerDetailContractorServicesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
