using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportPalletLoadWeight;

public class UpdateTransportPalletLoadWeightCommandHandler : ICommandHandler<UpdateTransportPalletLoadWeightCommand, List<TransportationCargoPallet>>
{
    private readonly ILogger<UpdateTransportPalletLoadWeightCommand> _logger;
    private readonly ITransportationCargoPalletRepository _cargoPalletRepository;

    public UpdateTransportPalletLoadWeightCommandHandler(
        ILogger<UpdateTransportPalletLoadWeightCommand> logger,
        ITransportationCargoPalletRepository cargoPalletRepository)
    {
        _logger = logger;
        _cargoPalletRepository = cargoPalletRepository;
    }

    public async Task<Result<List<TransportationCargoPallet>?>> Handle(UpdateTransportPalletLoadWeightCommand request, CT ct)
    {
        try
        {
            List<TransportationCargoPallet>? entities = [];
            if (request.Pallets != null && request.Pallets.Count > 0)
            {
                var palletIds = request.Pallets.Listed(x => x.Id);
                entities = await _cargoPalletRepository.GetByIds(palletIds, ct);
                if (entities == null || entities.Count != palletIds.Count)
                    return Result.Failure<List<TransportationCargoPallet>>(TransportationRequestErrors.CargosPalletWithNotfound);

                foreach (var entity in entities)
                {
                    var req = request.Pallets.FirstOrDefault(x => x.Id == entity.Id);
                    if (entity.Weight == req!.Weight)
                        continue;

                    entity.SetWeight(req.Weight);

                    if (entity.TransportationContractor is not null &&
                        entity.TransportationContractor.Type == TransportationContractorCalculateType.Weight)
                    {
                        decimal insurancePrice = 0m;
                        decimal servicePrice = 0m;
                        decimal priceWeight = 0m;
                        decimal totalTransferPrice = 0m;
                        decimal taxPrice = 0m;
                        GetPrices(entity, req.Weight, ref insurancePrice,
                            ref servicePrice, ref priceWeight, ref totalTransferPrice, ref taxPrice);

                        entity.SetTransferPrice(totalTransferPrice);
                        entity.SetShippingCost(null);
                    }

                    await _cargoPalletRepository.Update(entity);
                }

            }
            return entities;
        }

        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<TransportationCargoPallet>>(SharedErrors.UnknownError);
        }
    }

    private static void GetPrices(
        TransportationCargoPallet entity,
        decimal weight,
        ref decimal insurancePrice,
        ref decimal servicePrice,
        ref decimal priceWeight,
        ref decimal totalTransferPrice,
        ref decimal taxPrice)
    {
        if (entity.TransportationContractor?.Type == TransportationContractorCalculateType.Weight)
        {
            var priceWeights = entity.TransportationContractor.PriceWeights.ToList();
            var insuranceData = entity.TransportationContractor.TransportationContractorInsurances.OrderByDescending(x => x.Id).FirstOrDefault();
            var productPrice = entity.Price;

            if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
            {
                insurancePrice = insuranceData.FixedPrice;
            }
            else if (productPrice > insuranceData?.MaxProductPrice)
            {
                insurancePrice = ((productPrice ?? 1) / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
            }

            servicePrice = entity.TransportationContractor.ServicePrice ?? 0;

            priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(), weight) ?? 0;

            totalTransferPrice = insurancePrice + (priceWeight) + ((priceWeight) *
                (entity.TransportationContractor.PercentageValue ?? 0) / 100) + servicePrice;

            taxPrice = ((priceWeight) * (entity.TransportationContractor.TaxPercent ?? 1)) / 100;
        }
    }
}