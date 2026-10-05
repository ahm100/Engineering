using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface ITransportationContractorPersonnelRepository : IBaseRepository<TransportationContractorPersonnel>
{
    Task<GetTransportationContractorPersonnelByIdResponse?> GetTransportationContractorPersonnelById(long id, CT ct);

    Task<TransportationContractorPersonnel?> GetTransportationContractorPersonnel(long id, CT ct);

    Task<(List<GetsActiveTransportationContractorPersonnelResponseModel> Data, int RowCount)> GetAllActiveTransportationContractorPersonnels(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? PersonnelIds,
        List<long>? machineIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsFilteredTransportationContractorPersonnelResponseModel> Data, int RowCount)> GetFilteredTransportationContractorPersonnels(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? PersonnelIds,
        List<long>? machineIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<TransportationContractorPersonnel>> GetByIds(List<long> ids, CT ct);
}