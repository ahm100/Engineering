using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewPackingRepository : IBaseRepository<ViewPacking>
{
    Task<ViewPacking?> GetPacking(
         long id,
         CT ct);

    Task<List<ViewPacking>> GetPackings(
        List<long> ids, CT ct);

    Task<ViewPacking?> GetPackingNumber(
            long requestNumber,
            CT ct);
    Task<bool> GetPackingByLegacyId(
            long legacyId, CT ct);
    Task<ViewPacking?> DeletePacking(
            long id, CT ct);
    Task<ViewPacking?> DeletePackingByCommercPackId(
            long commercPackId, CT ct);
    Task<ViewPacking?> GetByNumber(
            long number, CT ct);
    Task<ViewPacking?> GetByNumberForAfterWarehouseConfirm(
            long number, CT ct);
    Task<List<ViewPacking>?> GetByNumbers(
            List<long> numbers, CT ct);
    Task<List<ViewPacking>?> GetByIds(
            List<long> ids, CT ct);

    Task<List<ViewPacking>?> GetByIdsWithInclude(
        List<long> ids, CT ct);
}