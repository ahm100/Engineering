using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportLoadWeight;

public class UpdateTransportLoadWeightCommandHandler : ICommandHandler<UpdateTransportLoadWeightCommand, TransportationRequest>
{
    private readonly ILogger<UpdateTransportLoadWeightCommand> _logger;
    private readonly ITransportationRequestRepository _repository;
    private readonly ITransportationCargoPalletRepository _cargoPalletRepository;

    public UpdateTransportLoadWeightCommandHandler(
        ILogger<UpdateTransportLoadWeightCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationCargoPalletRepository cargoPalletRepository)
    {
        _logger = logger;
        _repository = repository;
        _cargoPalletRepository = cargoPalletRepository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateTransportLoadWeightCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;

            entity.SetLoadWeight(request.LoadWeight);

            var insurancePrice = 0m;
            var servicePrice = 0m;
            var priceWeight = 0m;
            var totalTransferPrice = 0m;
            var taxPrice = 0m;

            if (entity.TransportationContractor!.Type == TransportationContractorCalculateType.Weight)
            {
                GetPrices(entity, entity.TransportationCargoPallets.ToList(), entity.LoadWeight ?? 0, ref insurancePrice,
                    ref servicePrice, ref priceWeight, ref totalTransferPrice, ref taxPrice);

                entity.SetPrice(totalTransferPrice);
                var detail = entity.TransportationRequestDetails.FirstOrDefault();
                detail?.Update(detail.GlobalFreightNumber, null, taxPrice, totalTransferPrice, servicePrice, null, insurancePrice, totalTransferPrice,
                    entity.TransportationCargoPallets.Sum(x => x.Price ?? 0), detail.OutofRange, null, entity);
            }

            entity.AddHistory();
            await _repository.Update(entity);

            if (request.UpdatePallets == false)
            {
                foreach (var item in entity.TransportationCargoPallets)
                {
                    if (entity.TransportationContractor!.Type == TransportationContractorCalculateType.Weight)
                    {
                        GetPrices(entity, [item], item.Weight ?? 0, ref insurancePrice,
                                    ref servicePrice, ref priceWeight, ref totalTransferPrice, ref taxPrice);

                        item.SetTransferPrice(totalTransferPrice);
                    }

                    await _cargoPalletRepository.Update(item);
                }
            }

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }

    private static void GetPrices(
        TransportationRequest entity,
        List<TransportationCargoPallet>? pallets,
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
            var productPrice = pallets!.Sum(x => x.Price ?? 0);
            if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
            {
                insurancePrice = insuranceData.FixedPrice;
            }
            else if (productPrice > insuranceData?.MaxProductPrice)
            {
                insurancePrice = (productPrice / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
            }

            servicePrice = entity.TransportationContractor.ServicePrice ?? 0;

            priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(),
                pallets!.Count == 1 ? pallets.Sum(x => x.Weight ?? 0) : weight) ?? 0;

            totalTransferPrice = insurancePrice + (priceWeight) + ((priceWeight) *
                (entity.TransportationContractor.PercentageValue ?? 0) / 100) + servicePrice;

            taxPrice = ((priceWeight) * (entity.TransportationContractor.TaxPercent ?? 1)) / 100;
        }
    }
}