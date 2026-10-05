using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;
using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;
using Engineering.Domain.Entities.Advertisements;

namespace Engineering.Application.Abstractions.Data.Advertisements;

public interface IAdvertisementRepository : IBaseRepository<Advertisement>
{
    Task<Advertisement?> GetById(
        long id, CT ct);

    Task<List<Advertisement>?> GetByIds(
        List<long> ids, CT ct);

    Task<bool?> DoesTitleExist(
        long? id,
        string titleFa,
        string titleEn, CT ct);

    Task<GetAdvertisementByIdResponse?> GetAdvertisementById(
        long id, CT ct);

    Task<(List<GetAdvertisementByIdsModel>? Data, int RowCount)> GetAdvertisementByIds(
        List<long> ids,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetFltrAdvertisementModel>? Data, int RowCount)> GetFltrAdvertisement(
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct);
}