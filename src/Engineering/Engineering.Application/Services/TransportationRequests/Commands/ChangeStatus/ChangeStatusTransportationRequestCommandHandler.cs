using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations.Enums;
using Warehouse.ClientSdks.Services;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatus;

public class ChangeStatusTransportationRequestCommandHandler : ICommandHandler<ChangeStatusTransportationRequestCommand, TransportationRequest>
{
    private readonly ILogger<ChangeStatusTransportationRequestCommand> _logger;
    private readonly ITransportationRequestRepository _repository;
    private readonly ITransportationCargoRepository _cargoRepository;
    private readonly IPackingService _packingService;

    public ChangeStatusTransportationRequestCommandHandler(ILogger<ChangeStatusTransportationRequestCommand> logger, ITransportationRequestRepository repository, IPackingService packingService, ITransportationCargoRepository cargoRepository)
    {
        _logger = logger;
        _repository = repository;
        _packingService = packingService;
        _cargoRepository = cargoRepository;
    }

    public async Task<Result<TransportationRequest?>> Handle(ChangeStatusTransportationRequestCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdForChangeStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.IsDeleted);

            if (request.WarehouseStatus == true)
            {
                if (request.Status == TransportationRequestStatus.SendDone)
                {
                    if (!(ValidateTransportationRequestStatus.AllowStatusForSendDone.Any(x => x == entity.TransportationRequestStatus)))
                        return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnValidStatus);
                }

                if (request.Status == TransportationRequestStatus.SecurityConfirm)
                {
                    if (!entity.TransportationCargoPallets.All(x =>
                        x.PackingPallet.PalletPermitStatus == PalletPermitStatus.PermitConfirmed))
                        return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnValidPackingStatus);

                    if (!(ValidateTransportationRequestStatus.AllowStatusForSecurityConfirm.Any(x => x == entity.TransportationRequestStatus)))
                        return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnValidStatus);

                    var pacIds = entity.TransportationCargoPallets.Where(x => x.TransportationCargo.PackingId != null &&
                    x.TransportationCargo.PackingId > 0).Select(x => x.TransportationCargo.PackingId).Distinct().ToList();
                    if (pacIds != null && pacIds.Count > 0)
                    {
                        var updatePac = await _packingService.SetPackingToSentStatus(new(pacIds, request.ManagerDescription), ct);
                        if (updatePac == null || updatePac.IsDone == false)
                            return Result.Failure<TransportationRequest>(TransportationContractorErrors.UpdateStatusFeild);
                    }

                    var cargos = entity.TransportationCargoPallets.Select(x => x.TransportationCargo).Distinct().ToList();
                    foreach (var cargo in cargos)
                    {
                        cargo.SetSecurityConfirm(true);
                        await _cargoRepository.Update(cargo);
                    }
                }
            }

            entity.SetTransportationRequestStatus(request.Status);
            entity.SetManagerDescription(request.ManagerDescription);
            entity.AddHistory();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}