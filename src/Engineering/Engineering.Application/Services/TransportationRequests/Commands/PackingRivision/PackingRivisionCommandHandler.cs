using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;
using Warehouse.ClientSdks.Services;

namespace Engineering.Application.Services.TransportationRequests.Commands.PackingRivision;

public class PackingRivisionCommandHandler : ICommandHandler<PackingRivisionCommand, bool?>
{
    private readonly ILogger<PackingRivisionCommand> _logger;
    private readonly ITransportationCargoPalletRepository _repository;
    private readonly ITransportationCargoRepository _cargorepository;
    private readonly IPackingService _packingService;
    private readonly ITransportationRequestRepository _transportationRequestRepository;

    public PackingRivisionCommandHandler(
        ILogger<PackingRivisionCommand> logger,
        ITransportationCargoPalletRepository repository,
        IPackingService packingService,
        ITransportationCargoRepository cargorepository,
        ITransportationRequestRepository transportationRequestRepository)
    {
        _logger = logger;
        _repository = repository;
        _packingService = packingService;
        _cargorepository = cargorepository;
        _transportationRequestRepository = transportationRequestRepository;
    }

    public async Task<Result<bool?>> Handle(PackingRivisionCommand request, CT ct)
    {
        try
        {
            var packingIds = new List<long>();
            if (request.Ids is not null && request.Ids.Count > 0)
            {
                var entities = await _repository.GetTransportationPallets(request.Ids, ct);
                if (entities == null || !entities.Any())
                    return Result.Failure<bool?>(TransportationRequestErrors.CargosPalletWithNotfound);

                if (entities.Any())
                {
                    (bool flowControl, Result<bool?> value) = await UpdatePallets(packingIds, entities);
                    if (!flowControl)
                        return Result.Failure<bool?>(TransportationRequestErrors.CargosPalletTransport);

                    packingIds.AddRange(entities.Listed(x => x.TransportationCargo.PackingId));
                }
            }
            else if (request.PackingIds is not null && request.PackingIds.Count > 0)
            {
                var entities = await _repository.GetTransportationPalletsByPackingIds(request.PackingIds, ct);
                if (entities is not null && entities.Count > 0)
                {
                    (bool flowControl, Result<bool?> value) = await UpdatePallets(packingIds, entities);
                    if (!flowControl)
                        return Result.Failure<bool?>(TransportationRequestErrors.CargosPalletTransport);

                    packingIds.AddRange(entities.Listed(x => x.TransportationCargo.PackingId));
                }
                else
                {
                    packingIds.AddRange(request.PackingIds);
                }
            }

            if (packingIds.Any())
            {
                var updateResult = await _packingService.PackingRivision(new(packingIds.Distinct().ToList(), request.Description), ct);
                if (updateResult == null || !updateResult.IsDone)
                    return Result.Failure<bool?>(TransportationContractorErrors.UpdateStatusFeild);
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }

    private async Task<(bool flowControl, Result<bool?> value)> UpdatePallets(List<long> packingIds, List<TransportationCargoPallet> entities)
    {
        if (entities.Any(x => x.TransportationRequest is not null && x.TransportationRequestId != null))
            return (flowControl: false, value: Result.Failure<bool?>(TransportationRequestErrors.CargosPalletHaveTransport));

        foreach (var pallet in entities)
        {
            // حذف پالت
            pallet.SoftDelete();
            await _repository.Update(pallet);
            pallet.TransportationCargo!.SoftDelete();
            await _cargorepository.Update(pallet.TransportationCargo);
        }

        return (flowControl: true, value: null);
    }
}