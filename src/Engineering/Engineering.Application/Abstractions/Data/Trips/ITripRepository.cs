using Engineering.Domain.Entities.Trips;

namespace Engineering.Application.Abstractions.Data.Trips;

public interface ITripRepository : IBaseRepository<Trip>
{
    Task<Trip?> FindByName(string name, long? companyId, CT ct);
    Task<bool> FindTripByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);
    Task<Trip?> FindByCode(string code, long? companyId, CT ct);
    Task<List<Trip>?> GetByCodes(List<string> codes, long? companyId, CT ct);
    Task<Trip?> GetById(long id, CT ct);
    Task<Trip?> FindForDelete(long id, CT ct);
    Task<string> CodeCreator(long? companyId, CT ct);

    Task<List<Trip>> GetsTripByIds(List<long> ids, CT ct);
    Task<(List<Trip> Data, int RowCount)> GetsFilteredTrip(List<long>? ids, string? filterData, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<Trip> Data, int RowCount)> GetsActiveTrip(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct);
}
