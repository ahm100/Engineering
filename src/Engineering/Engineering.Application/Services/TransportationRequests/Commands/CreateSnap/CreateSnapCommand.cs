using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Trips;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateSnap;

public record CreateSnapCommand(
    Trip Trip,
    Transportation Transportation,
    long? StartingCityId,
    long? DestinationCityId,
    DateTime StartDate,
    DateTime EndDate,
    string? Description,
    List<CostCenter> CostCenters,
    List<Project>? Projects,
    long? DriverId,
    string? DriverName,
    string? PhoneNumber,
    string? CarSpecifications,
    string? NumberPlates,
    long? CurrencyUnitId,
    bool IsPassenger,
    long? CompanyId,
    long? TransportationCostGroup,
    long? TransportationCostCategory,
    long? SnapRequester,
    long? SecondDestinationCityId,
    decimal? FareAmount,
    int? StopRate,
    string DestinationAddress,
    string? SecondDestinationAddress,
    bool? PersonalPayment,
    string StartingCityAddress,
    bool? ReturnToStart,
    string? RecipientName
    ) : ICommand<TransportationRequest?>;
