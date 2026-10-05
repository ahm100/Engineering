using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractDetailPriceRepository : IBaseRepository<ContractorContractDetailPrice>
{
    Task<GetSuggestedServicePriceResponse> GetSuggestedServicePrice(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
        List<long>? serviceInfoIds,
        string? filterData,
        DateTime? startDate,
        DateTime? endDate,
        decimal? maxPrice,
        decimal? minPrice,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ContractorContractDetailPrice> Data, int RowCount)> GetsFilteredContractorContractDetailOperationPrice(
        long? projectOperationId,
        long? operationInfoId,
        DateTime? startDate,
        DateTime? endDate,
        long? contractorId,
        long? costCenterId,
        long? projectId,
        string? FilterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ContractorContractDetailPrice> Data, int RowCount)> GetsFilteredContractorContractDetailServicePrice(
        long? projectOperationServiceId,
        long? serviceInfoId,
        DateTime? startDate,
        DateTime? endDate,
        long? contractorId,
        long? costCenterId,
        long? projectId,
        string? FilterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsContractorContractDetailPriceModel> Data, int RowCount)> GetsContractorContractDetailPrice(
        long contractorContractHedearId,
        DateTime? stratDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);
}
