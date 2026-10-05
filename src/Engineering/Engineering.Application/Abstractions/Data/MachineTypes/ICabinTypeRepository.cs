using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;
using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Abstractions.Data.MachineTypes;

public interface ICabinTypeRepository : IBaseRepository<CabinType>
{
    Task<CabinType?> GetCabinTypeById(
        long id, CT ct);

    Task<GetCabinTypeByIdResponse?> GetCabinTypeByIdForResponse(
    long id, CT ct);

    Task<GetCabinTypeByNameResponse?> GetCabinTypeByName(
        string name,
        long? companyId, CT ct);

    Task<bool> GetCabinTypeByNamesOrCodes(
        List<string> names,
        List<int> codes,
        long? companyId, CT ct);

    Task<GetCabinTypeByCodeResponse?> GetCabinTypeByCode(
            int Code,
            long? companyId, CT ct);

    Task<List<CabinType>?> GetCabinTypeByCodes(
        List<int> Codes,
        long? companyId, CT ct);

    Task<(List<GetsCabinTypeResponseModel> Data, int RowCount)> GetsCabinType(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task<List<CabinType>> GetsCabinTypeByIds(
        List<long> ids, CT ct);

    Task<(List<GetsActiveCabinTypeModel> Data, int RowCount)> GetsActiveCabinTypes(
        string? filterData,
        int? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct);

    Task<int> CabinTypeCodeCreator(
        long? companyId, CT ct);
}