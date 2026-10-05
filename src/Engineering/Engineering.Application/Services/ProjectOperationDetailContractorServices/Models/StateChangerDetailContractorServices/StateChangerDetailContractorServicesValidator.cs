
namespace Engineering.Application.Services.DetailContractorServices.Models.StateChangerDetailContractorServices;

public class StateChangerDetailContractorServicesValidator : AbstractValidator<StateChangerDetailContractorServicesRequest>
{
    public StateChangerDetailContractorServicesValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
