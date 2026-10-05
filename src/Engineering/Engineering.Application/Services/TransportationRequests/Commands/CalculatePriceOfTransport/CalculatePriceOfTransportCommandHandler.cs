using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;
using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationRequests.Commands.CalculatePriceOfTransport;

public class CalculatePriceOfTransportCommandHandler : ICommandHandler<CalculatePriceOfTransportCommand, CalculatePriceOfTransportResponse?>
{
    private readonly ILogger<CalculatePriceOfTransportCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public CalculatePriceOfTransportCommandHandler(
        ILogger<CalculatePriceOfTransportCommand> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CalculatePriceOfTransportResponse?>> Handle(CalculatePriceOfTransportCommand request, CT ct)
    {
        try
        {
            decimal productPrice = 0;
            decimal insurancePrice = 0;
            decimal servicePrice = 0;
            decimal? priceWeight = 0;
            decimal totalTransferPrice = 0;
            decimal taxPrice = 0;
            var entity = request.TransportationRequest;
            var contractor = request.TransportationRequest.TransportationContractor;
            var palletData = request.TransportationRequest.TransportationCargoPallets.ToList();

            if (entity.TransportationContractor!.Type == TransportationContractorCalculateType.Weight)
            {
                var insuranceData = contractor!.TransportationContractorInsurances.LastOrDefault();
                productPrice = palletData!.Sum(x => x.Price ?? 0);
                if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
                {
                    insurancePrice = insuranceData.FixedPrice;
                }
                else if (productPrice > insuranceData?.MaxProductPrice)
                {
                    insurancePrice = (productPrice / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
                }

                servicePrice = contractor.ServicePrice ?? 0;
                priceWeight = Calculator.CalculateTransportPriceWeight(request.PriceWeights!.ToArray(), palletData!.Sum(x => x.Weight) ?? 0);
                totalTransferPrice = insurancePrice + (priceWeight ?? 0m) + ((priceWeight ?? 0m) *
                    (contractor.PercentageValue ?? 0) / 100) + servicePrice;
                taxPrice = ((priceWeight ?? 0m) * (contractor.TaxPercent ?? 1)) / 100;
            }

            return new CalculatePriceOfTransportResponse(entity.Id, productPrice, insurancePrice, servicePrice, priceWeight ?? 0m, totalTransferPrice, taxPrice);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<CalculatePriceOfTransportResponse?>(SharedErrors.UnknownError);
        }
    }
}