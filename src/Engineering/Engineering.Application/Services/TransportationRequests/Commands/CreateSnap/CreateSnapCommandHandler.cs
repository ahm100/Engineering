using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateSnap;

public class CreateSnapCommandHandler : ICommandHandler<CreateSnapCommand, TransportationRequest?>
{
    private readonly ILogger<CreateSnapCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public CreateSnapCommandHandler(ILogger<CreateSnapCommand> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(CreateSnapCommand request, CT ct)
    {
        try
        {
            var requestNumber = await _repository.RequestNumberCreator(true, request.CompanyId, ct);

            var entity = new TransportationRequest(request.Transportation, request.Trip, request.TransportationCostGroup,
                  request.TransportationCostCategory, request.StartingCityId, request.DestinationCityId,
                  request.StartDate, request.EndDate, request.Description, request.DriverId, request.DriverName, request.PhoneNumber,
                  request.CarSpecifications, request.NumberPlates, request.CurrencyUnitId, request.CompanyId, request.SnapRequester,
                  request.SecondDestinationCityId, request.FareAmount, request.StopRate, request.DestinationAddress, request.SecondDestinationAddress,
                  request.PersonalPayment, request.StartingCityAddress, request.ReturnToStart, request.RecipientName, requestNumber, true);
            var result = await _repository.Create(entity, ct);

            if (request.IsPassenger == true)
            {
                entity.SetBillOfLading(null);
                entity.SetPostageDate(null);
                entity.SetReceivedDate(null);
                entity.SetBillOfLadingImage(null);
                entity.SetDelivererName(null);
                entity.SetLoadWeight(null);
            }

            if (request.CostCenters is not null && request.CostCenters?.Count > 0)
                foreach (var item in request.CostCenters)
                    entity.AddTransportationRequestCostCenter(new TransportationRequestCostCenter(entity, item));

            if (request.Projects is not null && request.Projects?.Count > 0)
                foreach (var item in request.Projects)
                    entity.AddTransportationRequestProject(new TransportationRequestProject(entity, item));

            entity.AddHistory();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest?>(SharedErrors.UnknownError);
        }
    }
}