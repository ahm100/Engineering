using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;
using Engineering.Domain.Entities.GoodsManager;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IGoodsManagerAssignmentRepository
    : IBaseRepository<GoodsManagerAssignment>
{
    Task<GoodsManagerAssignment?> GetById(
        long id,
        CT ct);

    Task<List<GetFilteredGoodsManagerAssignmentModel>?> GetManagerById(
        long id,
        CT ct);

    Task<bool> FindHaveCategory(
        long productId, CT ct);

    Task<List<GetFilteredGoodsManagerAssignmentModel>> GetFiltered(
        List<long>? ids,
        long? organizationId,
        long? productId,
        CT ct);

    Task<bool> ExistsDuplicate(
        long organizationId,
        long productId,
        long? excludeId,
        CT ct);

    Task<List<GoodsManagerAssignment>> GetActiveByProductId(
        long productId,
        CT ct);

    Task<List<GoodsManagerAssignment>> GetByOrganizationId(
        long organizationId,
        CT ct);

    Task<List<long>?> GetAllManagerGoods(
        List<long> organizationIds,
        CT ct);
}