using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.AddTransportationRequestBill;

public class AddTransportationRequestBillCommandHandler : ICommandHandler<AddTransportationRequestBillCommand, TransportationRequest>
{
    private readonly ILogger<AddTransportationRequestBillCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public AddTransportationRequestBillCommandHandler(
        ILogger<AddTransportationRequestBillCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationRequestWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(AddTransportationRequestBillCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

            entity.AddBills(request.DocumentUrls);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}