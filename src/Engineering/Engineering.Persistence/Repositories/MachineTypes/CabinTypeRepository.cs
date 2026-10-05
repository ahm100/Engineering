using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;
using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Persistence.Repositories.MachineTypes;

public class CabinTypeRepository : BaseRepository<EngineeringDBContext, CabinType>, ICabinTypeRepository
{
    public CabinTypeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<CabinType?> GetCabinTypeById(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                w.Id.Equals(id));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCabinTypeByIdResponse?> GetCabinTypeByIdForResponse(
    long id, CT ct)
    {
        var query = DbSet
            .Where(w =>
                !w.IsDeleted &&
                w.Id.Equals(id))
            .Select(w => new GetCabinTypeByIdResponse
            {
                Id = w.Id,
                CabinTypeCode = w.CabinTypeCode,
                CabinTypeTitle = w.CabinTypeName,
                IsActive = w.IsActive
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public async Task<GetCabinTypeByNameResponse?> GetCabinTypeByName(
        string name,
        long? companyId, CT ct)
    {
        var query = DbSet
            .Where(w =>
                w.CabinTypeName.Equals(name) &&
                (companyId == null || w.CompanyId.Equals(companyId)))
            .Select(e => new GetCabinTypeByNameResponse
            {
                Id = e.Id,
                CabinTypeCode = e.CabinTypeCode,
                CabinTypeTitle = e.CabinTypeName,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public Task<bool> GetCabinTypeByNamesOrCodes(
        List<string> names,
        List<int> codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .AnyAsync(a =>
                (companyId == null || a.CompanyId.Equals(companyId)) &&
                names.Contains(a.CabinTypeName) ||
                codes.Contains(a.CabinTypeCode), ct);

        return query;
    }

    public async Task<GetCabinTypeByCodeResponse?> GetCabinTypeByCode(
        int Code,
        long? companyId, CT ct)
    {
        var query = DbSet
            .Where(w =>
                w.CabinTypeCode.Equals(Code) &&
                (companyId == null || w.CompanyId.Equals(companyId)))
            .Select(e => new GetCabinTypeByCodeResponse
            {
                Id = e.Id,
                CabinTypeCode = e.CabinTypeCode,
                CabinTypeName = e.CabinTypeName,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<CabinType>?> GetCabinTypeByCodes(
        List<int> Codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                Codes.Contains(w.CabinTypeCode) &&
                (companyId == null || w.CompanyId.Equals(companyId)));

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<(List<GetsCabinTypeResponseModel> Data, int RowCount)> GetsCabinType(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CabinTypeName, filterData!.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CabinTypeCode.ToString(), filterData.MakeLikePattern()))
            .Select(s => new GetsCabinTypeResponseModel
            {
                Id = s.Id,
                CabinTypeCode = s.CabinTypeCode,
                CabinTypeName = s.CabinTypeName,
                IsActive = s.IsActive,
                CompanyId = s.CompanyId
            });

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);

        query = query.OrderBy(t => t.CabinTypeName);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<CabinType>> GetsCabinTypeByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<GetsActiveCabinTypeModel> Data, int RowCount)> GetsActiveCabinTypes(
        string? filterData,
        int? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.IsActive &&
                (code == null || w.CabinTypeCode.Equals(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CabinTypeCode.ToString(), filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CabinTypeName, filterData.MakeLikePattern())) &&
                (name == null || w.CabinTypeName.Contains(name)))
            .Select(s => new GetsActiveCabinTypeModel
            {
                Id = s.Id,
                CabinTypeCode = s.CabinTypeCode,
                CabinTypeName = s.CabinTypeName
            });
        query = query.OrderBy(o => o.CabinTypeName);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<int> CabinTypeCodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)))
            .Select(s => Convert.ToInt32(s.CabinTypeCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return (int)suggestedCode;
    }
}