using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations;
using Warehouse.ClientSdks.Services;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeCargoToSecurityConfirm;

public class ChangeCargoToSecurityConfirmCommandHandler : ICommandHandler<ChangeCargoToSecurityConfirmCommand, TransportationCargo>
{
    private readonly ILogger<ChangeCargoToSecurityConfirmCommand> _logger;
    private readonly ITransportationCargoRepository _repository;
    private readonly IPackingService _packingService;

    public ChangeCargoToSecurityConfirmCommandHandler(ILogger<ChangeCargoToSecurityConfirmCommand> logger, ITransportationCargoRepository repository, IPackingService packingService)
    {
        _logger = logger;
        _repository = repository;
        _packingService = packingService;
    }

    public async Task<Result<TransportationCargo?>> Handle(ChangeCargoToSecurityConfirmCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TransportationCargo>(TransportationRequestErrors.CargosNotfound);
            if (entity.IsDeleted == true)
                return Result.Failure<TransportationCargo>(TransportationRequestErrors.IsDeleted);

            if (!entity.TransportationCargoPallets.All(x => x.PackingPallet.PalletPermitStatus == PalletPermitStatus.PermitConfirmed))
                return Result.Failure<TransportationCargo>(TransportationRequestErrors.UnValidPackingStatus);

            if (request.WarehouseStatus == true)
            {
                var pacIds = entity.TransportationCargoPallets.Where(x => x.TransportationCargo.PackingId != null &&
                x.TransportationCargo.PackingId > 0).Select(x => x.TransportationCargo.PackingId).Distinct().ToList();
                if (pacIds != null && pacIds.Count > 0)
                {
                    var updatePac = await _packingService.SetPackingToSentStatus(new(pacIds, request.ManagerDescription), ct);
                    if (updatePac == null || updatePac.IsDone == false)
                        return Result.Failure<TransportationCargo>(TransportationContractorErrors.UpdateStatusFeild);
                }
            }

            entity.SetSecurityConfirm(true);
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationCargo>(SharedErrors.UnknownError);
        }
    }
}