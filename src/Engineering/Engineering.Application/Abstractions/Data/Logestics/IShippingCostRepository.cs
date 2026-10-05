using Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface IShippingCostRepository : IBaseRepository<ShippingCost>
{
    Task<GetShippingCostByIdResponse?> GetShippingCostById(long id, CT ct);

    Task<ShippingCost?> GetShippingCost(long id, CT ct);

    Task<List<ShippingCost>?> GetShippingCostByContractor(long id, CT ct);

    Task<(List<GetsActiveShippingCostResponseModel> Data, int RowCount)> GetAllActiveShippingCosts(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
        List<long>? thirdPartyIds,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsFilteredShippingCostResponseModel> Data, int RowCount)> GetFilteredShippingCosts(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
        List<long>? thirdPartyIds,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ShippingCost>> GetByIds(List<long> ids, CT ct);
}