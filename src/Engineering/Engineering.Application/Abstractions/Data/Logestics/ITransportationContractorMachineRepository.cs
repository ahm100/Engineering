using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface ITransportationContractorMachineRepository : IBaseRepository<TransportationContractorMachine>
{
    Task<GetTransportationContractorMachineByIdResponse?> GetTransportationContractorMachineById(long id, CT ct);

    Task<TransportationContractorMachine?> GetTransportationContractorMachine(long id, CT ct);

    Task<bool> IsDuplicateMachine(long? id, string numberPlate, string? vin, CT ct);

    Task<(List<GetsActiveTransportationContractorMachineResponseModel> Data, int RowCount)> GetAllActiveTransportationContractorMachines(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsFilteredTransportationContractorMachineResponseModel> Data, int RowCount)> GetFilteredTransportationContractorMachines(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<TransportationContractorMachine>> GetByIds(List<long> ids, CT ct);
}