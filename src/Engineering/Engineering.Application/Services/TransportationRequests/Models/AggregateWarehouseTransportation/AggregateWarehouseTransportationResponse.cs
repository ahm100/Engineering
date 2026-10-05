
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;

public record AggregateWarehouseTransportationResponse(
    bool IsDone
    );

public record ValidateRequestOfAggragateRequestModel(
    List<TransportationCargo>? Cargos,
    TransportationContractor? Contractor,
    List<MachineType>? MachineTypes,
    List<TransportationCargoPallet>? PalletsData,
    List<ViewPacking>? PackingsData,
    List<ViewThirdParty>? Drivers,
    List<long>? CargoIds,
    List<long>? MachineTypeIds,
    List<long>? PalletIds,
    long? ContractorId);