using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewThirdPartyRepository(EngineeringDBContext context, IUserInfoService userInfoService) :
    BaseRepository<EngineeringDBContext, ViewThirdParty>(context), IViewThirdPartyRepository
{
    public async Task<ViewThirdParty?> GetById(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(x => x.Legal)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<ViewThirdParty?> GetDefaultPhoneNoAndAddressById(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(x => x.Addresses.Where(y => y.IsDefault == true))
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<ViewThirdParty?> GetByDefaultPhoneNo(
        string defaultPhoneNo,
        CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.DefaultPhoneNo == defaultPhoneNo, ct);
    }

    public async Task<ViewThirdParty?> GetByIdWithSpecialRelation(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(i => i.Legal)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<ViewThirdParty?> GetByIdWithSkills(
        long id,
        CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<ViewThirdParty?> GetThirdPartyWithRelationsById(
        long id,
        CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<ViewThirdParty?> GetThirdPartyWithAllRelationsById(
        long id,
        CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesByIds(
        List<long> ids,
        bool? ignoreQuery,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Legal)
            .Include(x => x.Addresses.Where(y => y.IsActive && !y.IsDeleted))
                .ThenInclude(x => x.City)
            .Where(x => ids.Contains(x.Id));
        query = query.OrderByDescending(x => x.IsActive ? 1 : 0).ThenByDescending(x => x.Created);
        var count = ignoreQuery == true
            ? await query.IgnoreQueryFilters().CountAsync(ct)
            : await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = ignoreQuery == true
            ? await query.IgnoreQueryFilters().ToListAsync(ct)
            : await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<GetsTransportationThirdPartyModel> Data, int RowCount)> GetsTransportationThirdParty(
        List<long>? ids,
        string? filterData,
        int pageIndex,
        int pageSize,
        bool? ignoreQuery,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x =>
            (ids == null || ids.Contains(x.Id)) &&
            ((string.IsNullOrWhiteSpace(filterData) ||
               string.Concat(x.FirstName, " ", x.LastName).Contains(filterData) ||
               EF.Functions.Like(x.Nickname, filterData.MakeLikePattern()) ||
               EF.Functions.Like(x.Legal.CompanyName, filterData.MakeLikePattern()))
            ))
            .Select(z => new GetsTransportationThirdPartyModel()
            {
                Created = z.Created,
                DefaultPhoneNo = z.DefaultPhoneNo,
                Description = z.Description,
                FirstName = z.FirstName,
                FullName = $"{z.FirstName} {z.LastName}",
                Id = z.Id,
                IsActive = z.IsActive,
                IdentityNo = z.IdentityNo,
                IsIndividual = z.IsIndividual,
                LastName = z.LastName,
                Legal = new TransportationLegalModel(z.Legal.Id, z.Legal.CompanyName, z.Legal.RegistrationNo),
                UserId = z.UserId,
                PreferentialReferenceCode = z.PreferentialReferenceCode,
                Nickname = z.Nickname,
                Address = z.Addresses.Where(x => !x.IsDeleted).Select(x => new TransportationAddressModel(x.Id, x.Title, x.AddressText, x.City.Id, x.City.Name, x.PostalCode)).FirstOrDefault(),
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(x => x.Created);

        var count = ignoreQuery == true
            ? await query.IgnoreQueryFilters().CountAsync(ct)
            : await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = ignoreQuery == true
            ? await query.IgnoreQueryFilters().ToListAsync(ct)
            : await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesByIdsIncludeCompany(
        List<long> ids,
        bool? ignoreQuery,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => ids.Contains(x.Id));
        query = query.OrderByDescending(x => x.IsActive ? 1 : 0).ThenByDescending(x => x.Created);
        var count = ignoreQuery == true
            ? await query.IgnoreQueryFilters().CountAsync(ct)
            : await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = ignoreQuery == true
            ? await query.IgnoreQueryFilters().ToListAsync(ct)
            : await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesWithSelectedSkillIds(
        List<long> ids,
        List<long> skillIds,
        string? filterData,
        CT ct)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted
                        && (ids.Contains(x.Id))
                        && (string.IsNullOrWhiteSpace(filterData) ||
                            string.Concat(x.FirstName, " ", x.LastName).Contains(filterData)));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0).ThenByDescending(x => x.Created);
        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetFilteredThirdParties(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        bool? isIndividual,
        List<long>? skillIds,
        List<long>? guildTypeIds,
        string? defaultPhoneNo,
        string? iBAN,
        string[]? orderBy,
        Guid[]? preferentialReferenceCodes,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                !x.IsDeleted
                && (isActive == null || x.IsActive == isActive)
                && (isIndividual == null || x.IsIndividual == isIndividual)
                && (ids == null || !ids.Any() || ids.Contains(x.Id))
                && (preferentialReferenceCodes == null || !preferentialReferenceCodes.Any() || preferentialReferenceCodes.Contains(x.PreferentialReferenceCode.Value))
                && (string.IsNullOrWhiteSpace(defaultPhoneNo) || EF.Functions.Like(x.DefaultPhoneNo!, defaultPhoneNo.MakeLikePattern()))
                && (string.IsNullOrWhiteSpace(filterData)
                    || (x.IsIndividual == false && EF.Functions.Like(x.Legal!.CompanyName, filterData.MakeLikePattern()))
                    || EF.Functions.Like(x.DefaultPhoneNo!, filterData.MakeLikePattern())
                    || EF.Functions.Like(x.NationalCode!, filterData.MakeLikePattern())
                    || EF.Functions.Like(string.Concat(x.FirstName, " ", x.LastName), filterData.MakeLikePattern())
                    || EF.Functions.Like(x.OrganizationCode!, filterData.MakeLikePattern())));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0).ThenByDescending(x => x.Created);
        var count = await query.CountAsync(ct);
        if (orderBy?.Length > 0)
        {
            query = query.SortBy(orderBy);
        }
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetFilteredThirdPartiesForSnap(
        List<string>? organizationCodes,
        List<string>? defaultPhoneNumbers,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                !x.IsDeleted
                && ((organizationCodes == null || (x.OrganizationCode != null && organizationCodes.Contains(x.OrganizationCode)))
                || (defaultPhoneNumbers == null || defaultPhoneNumbers.Contains(x.DefaultPhoneNo)))
                && (string.IsNullOrWhiteSpace(filterData)
                    || (x.IsIndividual == false && EF.Functions.Like(x.Legal!.CompanyName, filterData.MakeLikePattern()))
                    || EF.Functions.Like(x.DefaultPhoneNo!, filterData.MakeLikePattern())
                    || EF.Functions.Like(string.Concat(x.FirstName, " ", x.LastName), filterData.MakeLikePattern())
                    || EF.Functions.Like(x.OrganizationCode!, filterData.MakeLikePattern())));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0).ThenByDescending(x => x.Created);
        var count = await query.CountAsync(ct);
        if (orderBy?.Length > 0)
        {
            query = query.SortBy(orderBy);
        }
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesBySkillCode(
        long? ViewThirdPartyId,
        string? fullName,
        string? organizationCode,
        int skillCode,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        bool? isUserNullable,
        CT ct)
    {

        var query = DbSet.Include(x => x.Legal)
            .Where(x =>
                !x.IsDeleted
                && (isUserNullable == null || isUserNullable == true || (isUserNullable == false && x.UserId != null))
                && (isActive == null || x.IsActive == isActive)
                && (ViewThirdPartyId == null || x.Id == ViewThirdPartyId)
                && (string.IsNullOrWhiteSpace(fullName) ||
                    string.Concat(x.FirstName, " ", x.LastName).Contains(fullName))
                && (string.IsNullOrWhiteSpace(organizationCode) || x.OrganizationCode!.Contains(organizationCode))
                && (string.IsNullOrWhiteSpace(filterData)
                    || string.Concat(x.FirstName, " ", x.LastName).Contains(filterData)
                    || EF.Functions.Like(x.Nickname, filterData.MakeLikePattern())
                    || x.OrganizationCode!.Contains(filterData)
                    || x.DefaultPhoneNo.Contains(filterData)
                    || (x.IsIndividual == false && EF.Functions.Like(x.Legal!.CompanyName, filterData.MakeLikePattern()))));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0).ThenByDescending(x => x.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<ViewThirdParty>> GetByIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet.Where(oo => ids.Contains(oo.Id) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<ViewThirdParty>> GetByCodes(
        List<string> codes,
        CT ct)
    {
        var query = DbSet.Where(oo => !oo.IsDeleted && !string.IsNullOrEmpty(oo.OrganizationCode) && codes.Contains(oo.OrganizationCode));

        return await query.ToListAsync(ct);
    }

    public async Task<List<ViewThirdParty>> GetByUserIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet.Where(oo => oo.UserId != null && ids.Contains(oo.UserId.Value) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<ViewThirdParty>> GetByNationalCodes(
        List<string> nationalCodes,
        CT ct)
    {
        var query = DbSet.Where(oo => nationalCodes.Contains(oo.NationalCode) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<ViewThirdParty>> GetByDefaultPhoneNos(
        List<string> defaultPhoneNos,
        CT ct)
    {
        var query = DbSet.Where(oo => defaultPhoneNos.Contains(oo.DefaultPhoneNo) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<string> CodeCreator(
        CancellationToken ct)
    {
        var query = await DbSet
            .Where(ViewThirdParty => EF.Functions.IsNumeric(ViewThirdParty.OrganizationCode!))
            .Select(x => Convert.ToInt64(x.OrganizationCode))
            .DefaultIfEmpty()
            .MaxAsync(ct);
        return (query + 1).ToString();
    }

    public async Task<(List<ViewThirdParty> Data, int RowCount)> GetFltrThirdParties(
        List<long> ids,
        string? filterData,
        int pageIndex,
        int pageSize,
        CancellationToken ct)
    {
        var query = DbSet.Where(x => ids.Contains(x.Id) &&
        (string.IsNullOrWhiteSpace(filterData) ||
        string.Concat(x.FirstName, " ", x.LastName).Contains(filterData)));

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }
}