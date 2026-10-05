using Engineering.Domain.Entities.BillOfLadings;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Trips;

using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.Update;

public record UpdateTransportationRequestCommand(
    long Id,
    Trip Trip,
    Transportation Transportation,
    MachineType MachineType,
    long StartingCityId,
    long DestinationCityId,
    DateTime StartDate,
    DateTime EndDate,
    string? ImageLink,
    string? Description,
    List<CostCenter> CostCenters,
    List<Project>? Projects,
    List<ProjectOperation>? ProjectOperations,
    List<ProjectOperationDetail>? ProjectOperationDetails,
    long? DriverId,
    string? DriverName,
    BillOfLading? BillOfLading,
    DateTime? PostageDate,
    DateTime? ReceivedDate,
    string? DelivererName,
    string? RecipientName,
    string? FreightNumber,
    decimal? LoadWeight,
    string? PhoneNumber,
    string? CarSpecifications,
    string? NumberPlates,
    string? BillOfLadingImage,
    string? AccountNumber,
    long? BankId,
    string? CardNumber,
    string? AccountName,
    string? IBAN,
    decimal? Price,
    long? CurrencyUnitId,
    string? AccountDescription,
    string? CarID,
    bool IsPassenger,
    long? TransportationCostGroupId,
    long? TransportationCostCategoryId,
    string? StartingCityAddress,
    string? DestinationAddress,
    long? CompanyId,
    TransportationContractor? TransportationContractor
    ) : ICommand<TransportationRequest>;