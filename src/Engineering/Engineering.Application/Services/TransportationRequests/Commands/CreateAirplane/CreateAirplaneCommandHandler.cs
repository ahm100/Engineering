using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateAirplane;

public class CreateAirplaneCommandHandler : ICommandHandler<CreateAirplaneCommand, TransportationRequest?>
{
    private readonly ILogger<CreateAirplaneCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public CreateAirplaneCommandHandler(ILogger<CreateAirplaneCommand> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(CreateAirplaneCommand request, CT ct)
    {
        try
        {
            //fix
            var requestNumber = await _repository.RequestNumberCreator(false, request.CompanyId, ct);

            var entity = new TransportationRequest(request.Transportation, request.Trip, request.TransportationCostGroup,
                request.TransportationCostCategory, request.StartingCityId, request.DestinationCityId, request.StartDate,
                request.EndDate, request.Description, request.AccountName, request.AccountNumber, request.CardNumber, request.BankId,
                request.CurrencyUnitId, request.CompanyId, request.Passenger, request.PassengerId, request.TicketPayerId, request.FareAmount,
                request.DestinationAddress, request.IBAN, requestNumber, false);
            var result = await _repository.Create(entity, ct);

            if (request.IsPassenger == true)
            {
                entity.SetBillOfLading(null);
                entity.SetPostageDate(null);
                entity.SetReceivedDate(null);
                entity.SetBillOfLadingImage(null);
                entity.SetDelivererName(null);
                entity.SetRecipientName(null);
                entity.SetLoadWeight(null);
            }

            if (request.CostCenters is not null && request.CostCenters?.Count > 0)
                foreach (var item in request.CostCenters)
                    entity.AddTransportationRequestCostCenter(new TransportationRequestCostCenter(entity, item));

            if (request.Projects is not null && request.Projects?.Count > 0)
                foreach (var item in request.Projects)
                    entity.AddTransportationRequestProject(new TransportationRequestProject(entity, item));

            if (request.ProjectOperations is not null && request.ProjectOperations?.Count > 0)
                foreach (var item in request.ProjectOperations)
                    entity.AddTransportationRequestProjectOperation(new TransportationRequestProjectOperation(entity, item));

            if (request.ProjectOperationDetails is not null && request.ProjectOperationDetails?.Count > 0)
                foreach (var item in request.ProjectOperationDetails)
                    entity.AddTransportationRequestProjectOperationDetail(new TransportationRequestProjectOperationDetail(entity, item));

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