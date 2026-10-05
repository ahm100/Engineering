using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAirplane;

public class UpdateAirplaneCommandHandler : ICommandHandler<UpdateAirplaneCommand, TransportationRequest>
{
    private readonly ILogger<UpdateAirplaneCommand> _logger;
    private readonly ITransportationRequestRepository _repository;
    private readonly ITransportationRequestCostCenterRepository _costCenterRepository;
    private readonly ITransportationRequestProjectRepository _projectRepository;
    private readonly ITransportationRequestProjectOperationRepository _projectOperationRepository;
    private readonly ITransportationRequestProjectOperationDetailRepository _projectOperationDetailRepository;

    public UpdateAirplaneCommandHandler(ILogger<UpdateAirplaneCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationRequestCostCenterRepository costCenterRepository,
        ITransportationRequestProjectRepository projectRepository,
        ITransportationRequestProjectOperationRepository projectOperationRepository,
        ITransportationRequestProjectOperationDetailRepository projectOperationDetailRepository)
    {
        _logger = logger;
        _repository = repository;
        _costCenterRepository = costCenterRepository;
        _projectRepository = projectRepository;
        _projectOperationRepository = projectOperationRepository;
        _projectOperationDetailRepository = projectOperationDetailRepository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateAirplaneCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

            entity.SetTransportation(request.Transportation);
            entity.SetTrip(request.Trip);
            entity.SetStartingCityId(request.StartingCityId);
            entity.SetDestinationCityId(request.DestinationCityId);
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetDescription(request.Description);
            entity.SetAccountNumber(request.AccountNumber);
            entity.SetBankId(request.BankId);
            entity.SetCardNumber(request.CardNumber);
            entity.SetCurrencyUnitId(request.CurrencyUnitId);
            entity.SetCompanyId(request.CompanyId);
            entity.SetTransportationCostGroup(request.TransportationCostGroup);
            entity.SetTransportationCostCategory(request.TransportationCostCategory);
            entity.SetPrice(request.FareAmount);
            entity.SetDestinationAddress(request.DestinationAddress);
            entity.SetTicketPayerId(request.TicketPayerId);
            entity.SetPassengerId(request.PassengerId);
            entity.SetPassenger(request.Passenger);
            entity.SetIBAN(request.IBAN);
            entity.SetAccountName(request.AccountName);

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

            if (entity.TransportationRequestCostCenters.Count > 0)
            {
                foreach (var item in entity.TransportationRequestCostCenters)
                {
                    await _costCenterRepository.Remove(item);
                }
            }
            if (entity.TransportationRequestProjects.Count > 0)
            {
                foreach (var item in entity.TransportationRequestProjects)
                {
                    await _projectRepository.Remove(item);
                }
            }
            if (entity.TransportationRequestProjectOperations.Count > 0)
            {
                foreach (var item in entity.TransportationRequestProjectOperations)
                {
                    await _projectOperationRepository.Remove(item);
                }
            }
            if (entity.TransportationRequestProjectOperationDetails.Count > 0)
            {
                foreach (var item in entity.TransportationRequestProjectOperationDetails)
                {
                    await _projectOperationDetailRepository.Remove(item);
                }
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

            if (ValidateTransportationRequestStatus.AllowStatusForResend.Any(x => x == entity.TransportationRequestStatus))
                entity.SetTransportationRequestStatus(TransportationRequestStatus.RequestResended);

            entity.AddHistory();

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