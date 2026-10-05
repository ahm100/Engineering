using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Abstractions.Data.Transportations;

public interface ITransportationRepository : IBaseRepository<Transportation>
{
    Task<Transportation?> FindByName(string name, long? companyId, CT ct);
    Task<bool> FindTransportationByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);
    Task<Transportation?> FindByCode(string code, long? companyId, CT ct);
    Task<Transportation?> GetById(long id, CT ct);
    Task<Transportation?> GetByType(TransportationType transportationType, CT ct);
    Task<Transportation?> FindForDelete(long id, CT ct);
    Task<string> CodeCreator(long? companyId, CT ct);

    Task<List<Transportation>> GetsTransportationByIds(List<long> ids, CT ct);
    Task<(List<Transportation> Data, int RowCount)> GetsFilteredTransportation(List<long>? ids, string? filterData, string? code, string? name, bool? isPassenger, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<Transportation> Data, int RowCount)> GetsActiveTransportation(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct);
}