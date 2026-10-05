using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateCostCenterRequests;

public class UpdateCostCenterRequestsCommandHandler : ICommandHandler<UpdateCostCenterRequestsCommand, TransportationRequest>
{
    private readonly ILogger<UpdateCostCenterRequestsCommand> _logger;
    private readonly ITransportationRequestRepository _repository;
    private readonly ITransportationRequestCostCenterRepository _costCenterRepository;

    public UpdateCostCenterRequestsCommandHandler(
        ILogger<UpdateCostCenterRequestsCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationRequestCostCenterRepository costCenterRepository)
    {
        _logger = logger;
        _repository = repository;
        _costCenterRepository = costCenterRepository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateCostCenterRequestsCommand request, CT ct)
    {
        try
        {
            var entities = request.CostCenters;
            if (entities is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnValidCostCenters);

            foreach (var item in entities)
            {
                item.SetTransportRequest(request.TransportationRequest);
                await _costCenterRepository.Update(item);
            }

            return request.TransportationRequest;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}