namespace Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;

public class UpdateTransportPalletLoadWeightRequestValidator : AbstractValidator<UpdateTransportPalletLoadWeightRequest>
{
    public UpdateTransportPalletLoadWeightRequestValidator()
    {
        When(x => x.TransportationRequestId != null, () =>
        {
            RuleFor(oo => oo.TransportationRequestId!.Value).IsPositive(TransportationRequestWarehouseComment.TransportationRequestId);
        });

        When(x => x.TransportPallets.Count > 0, () =>
        {
            RuleForEach(oo => oo.TransportPallets)
            .NotNull()
            .NotEmpty()
            .WithError(TransportationRequestErrors.PalletIdsIsEmpty);

            RuleForEach(oo => oo.TransportPallets)
            .SetValidator(new TransportPalletWeightsModelValidator());
        });
    }
}

public class TransportPalletWeightsModelValidator : AbstractValidator<TransportPalletWeightsModel>
{
    public TransportPalletWeightsModelValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(TransportationRequestWarehouseComment.TransportationCargoPallet);

        RuleFor(oo => oo.Weight)
            .IsRequiredDecimal(TransportationRequestWarehouseComment.Weight);
    }
}