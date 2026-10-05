using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Abstractions.Data;

public interface IContractorServicesRepository : IBaseRepository<ContractorService>
{
    Task<(List<ContractorService> Data, int RowCount)> GetContractorServicesByContractorId(long contractorId, string? filterData, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<bool> ExistAsync(long serviceInfoId, long contractorId, CT ct);
    Task<List<long>> GetContractorServicesByServiceIds(List<long> ids, CT ct);
}