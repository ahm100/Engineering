using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportPalletLoadWeight;

public class UpdateTransportPalletLoadWeightCommandValidator : AbstractValidator<UpdateTransportPalletLoadWeightCommand>
{
    public UpdateTransportPalletLoadWeightCommandValidator()
    {
        When(x => x.Pallets.Count > 0, () =>
        {
            RuleForEach(oo => oo.Pallets)
                .NotNull()
                .NotEmpty()
                .WithError(TransportationRequestErrors.PalletIdsIsEmpty);

            RuleForEach(oo => oo.Pallets).SetValidator(new TransportPalletWeightsModelValidator());
        });
    }
}