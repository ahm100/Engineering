using Engineering.Domain.Entities.Synonyms.MetaData.Addresses;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewAddressRepository : IBaseRepository<ViewAddress>
{
    Task<(List<ViewAddress> Data, int RowCount)> GetActiveAddresses(int pageIndex, int pageSize,
        CT ct);

    Task<(List<ViewAddress> Data, int RowCount)> GetActiveAddressesByThirdPartyId(long thirdPartyId, bool? isActive, bool? isDefault,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ViewAddress> Data, int RowCount)> GetFilteredAddresses(long? thirdPartyId, string? fullName,
        string? organizationCode, string? filterData, bool? isActive, string[]? orderBy, int pageIndex, int pageSize,
        CT ct);

    Task<ViewAddress?> GetDefaultAddressByThirdPartyId(long requestThirdPartyId, long? addressId,
        CT ct);

    Task<(List<ViewAddress> Data, int RowCount)> GetAllActiveAddresses(string? filterData, int pageIndex,
        int pageSize, CT ct);

    Task<(List<ViewAddress> Data, int RowCount)> GetAddressesByIds(List<long> ids, bool? ignoreQuery, int pageIndex, int pageSize,
        CT ct);

    Task<ViewAddress?> GetById(long id, CT ct);

    Task<bool> IsDuplicateAddress(long? id, long thirdPartyId, string title,
        CT ct);

    Task<List<ViewAddress>> GetByIds(List<long> ids, CT ct);

    Task<List<long>> GetThirdPartyIdsByProvinceAndCity(
        long? provinceId,
        long? cityId,
        CT ct);
}