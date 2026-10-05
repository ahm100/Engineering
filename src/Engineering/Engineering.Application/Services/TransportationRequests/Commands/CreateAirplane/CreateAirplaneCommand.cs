using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Trips;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateAirplane;

public record CreateAirplaneCommand(
    Trip Trip,
    Transportation Transportation,
    bool? IsPassenger,
    long StartingCityId,
    long DestinationCityId,
    DateTime StartDate,
    DateTime EndDate,
    string? Description,
    List<CostCenter> CostCenters,
    List<Project>? Projects,
    List<ProjectOperation>? ProjectOperations,
    List<ProjectOperationDetail>? ProjectOperationDetails,
    string? AccountName,
    string? AccountNumber,
    long? BankId,
    long? TicketPayerId,
    string? CardNumber,
    long? CurrencyUnitId,
    long? CompanyId,
    long? TransportationCostGroup,
    long? TransportationCostCategory,
    decimal? FareAmount,
    string? DestinationAddress,
    long? PassengerId,
    string? Passenger,
    string? IBAN
    ) : ICommand<TransportationRequest?>;
