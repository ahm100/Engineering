using Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.ShippingCosts;

public class ShippingCostRepository : BaseRepository<EngineeringDBContext, ShippingCost>, IShippingCostRepository
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public ShippingCostRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetShippingCostByIdResponse?> GetShippingCostById(long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetShippingCostByIdResponse
            {
                IsActive = true,
                TransportationContractorId = x.TransportationContractor.Id,
                Count = x.Count,
                Created = x.Created,
                MachinTypeId = x.MachineType.Id,
                Id = x.Id,
                Title = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                Address = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                AddressId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                CityId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                City = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                PostalCode = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.PostalCode).FirstOrDefault(),
                Description = x.Description,
                LegacyId = x.LegacyId,
                LoadWeight = x.LoadWeight,
                MachinTypeCode = x.MachineType.MachineTypeCode,
                MachinTypeName = $"{x.MachineType.MachineTypeTitle} {x.MachineType.CabinType.CabinTypeName} وزن {x.MachineType.FromWeight} تا {x.MachineType.UntilWeight}",
                ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                Tax = x.Tax,
                SourceCityId = x.SourceCity.Id,
                SourceCity = x.SourceCity.Name,
                DestinationCityId = x.DestinationCity.Id,
                DestinationCity = x.DestinationCity.Name,
                Price = x.Price,
                PhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                MainName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                RegionId = x.Region.Id,
                Region = x.Region.Name,
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                ShippingThirdParty = $"{x.ThirdParty.FirstName} {x.ThirdParty.LastName}",
                ShippingThirdPartyId = x.ThirdPartyId,
                ThirdPartyCompanyId = x.ThirdPartyCompanyId,
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ShippingCost?> GetShippingCost(long id, CT ct)
    {
        var query = await DbSet
            .Include(x => x.TransportationContractor)
            .Include(x => x.MachineType)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return query;
    }

    public async Task<(List<GetsActiveShippingCostResponseModel> Data, int RowCount)> GetAllActiveShippingCosts(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
        List<long>? thirdPartyIds,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x =>
            !x.IsDeleted &&
            !x.TransportationContractor.IsDeleted &&
            !x.MachineType.IsDeleted &&
            x.IsActive &&
            (ids == null || ids.Contains(x.Id)) &&
            (contractorIds == null || contractorIds.Contains(x.TransportationContractor.Id)) &&
            (thirdPartyIds == null || (x.ThirdPartyId.HasValue && thirdPartyIds.Contains(x.ThirdPartyId.Value))) &&
            (machineTypeIds == null || machineTypeIds.Contains(x.MachineType.Id)) &&
            (fromDate == null || x.Created.Date >= fromDate.Value.Date) &&
            (toDate == null || x.Created.Date <= toDate.Value.Date) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.TransportationContractor.ThirdParty.Legal.CompanyName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Description, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.TransportationContractor.ThirdParty.FirstName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.TransportationContractor.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.TransportationContractor.ThirdParty.FirstName + " " + x.TransportationContractor.ThirdParty.LastName, filterData.MakeLikePattern()))
            )
            .Select(x => new GetsActiveShippingCostResponseModel()
            {
                IsActive = true,
                TransportationContractorId = x.TransportationContractor.Id,
                Count = x.Count,
                Created = x.Created,
                MachinTypeId = x.MachineType.Id,
                Id = x.Id,
                Title = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                Address = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                AddressId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                CityId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                City = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                PostalCode = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.PostalCode).FirstOrDefault(),
                Description = x.Description,
                LegacyId = x.LegacyId,
                LoadWeight = x.LoadWeight,
                MachinTypeCode = x.MachineType.MachineTypeCode,
                MachinTypeName = $"{x.MachineType.MachineTypeTitle} {x.MachineType.CabinType.CabinTypeName} وزن {x.MachineType.FromWeight} تا {x.MachineType.UntilWeight}",
                ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                Tax = x.Tax,
                SourceCityId = x.SourceCity.Id,
                SourceCity = x.SourceCity.Name,
                DestinationCityId = x.DestinationCity.Id,
                DestinationCity = x.DestinationCity.Name,
                Price = x.Price,
                PhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                MainName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                RegionId = x.Region.Id,
                Region = x.Region.Name,
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                ShippingThirdParty = $"{x.ThirdParty.FirstName} {x.ThirdParty.LastName}",
                ShippingThirdPartyId = x.ThirdPartyId,
                ThirdPartyCompanyId = x.ThirdPartyCompanyId,
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<GetsFilteredShippingCostResponseModel> Data, int RowCount)> GetFilteredShippingCosts(
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
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                    !x.IsDeleted &&
                    !x.TransportationContractor.IsDeleted &&
                    !x.MachineType.IsDeleted &&
                    (isActive == null || x.IsActive == isActive) &&
                    (ids == null || ids.Contains(x.Id)) &&
                    (contractorIds == null || contractorIds.Contains(x.TransportationContractor.Id)) &&
                    (machineTypeIds == null || machineTypeIds.Contains(x.MachineType.Id)) &&
                    (thirdPartyIds == null || (x.ThirdPartyId.HasValue && thirdPartyIds.Contains(x.ThirdPartyId.Value))) &&
                    (fromDate == null || x.Created.Date >= fromDate.Value.Date) &&
                    (toDate == null || x.Created.Date <= toDate.Value.Date) &&
                    (string.IsNullOrWhiteSpace(filterData) ||
                    EF.Functions.Like(x.TransportationContractor.ThirdParty.Legal.CompanyName, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.Description, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.TransportationContractor.ThirdParty.FirstName, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.TransportationContractor.ThirdParty.LastName, filterData.MakeLikePattern()) ||
                    EF.Functions.Like(x.TransportationContractor.ThirdParty.FirstName +
                    " " + x.TransportationContractor.ThirdParty.LastName, filterData.MakeLikePattern()))
                    )
                    .Select(x => new GetsFilteredShippingCostResponseModel()
                    {
                        IsActive = x.IsActive,
                        TransportationContractorId = x.TransportationContractor.Id,
                        Count = x.Count,
                        Created = x.Created,
                        MachinTypeId = x.MachineType.Id,
                        Id = x.Id,
                        Title = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().Title,
                        Address = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().AddressText,
                        AddressId = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().Id,
                        CityId = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().City.Id,
                        City = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().City.Name,
                        PostalCode = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().PostalCode,
                        Description = x.Description,
                        LegacyId = x.LegacyId,
                        LoadWeight = x.LoadWeight,
                        MachinTypeCode = x.MachineType.MachineTypeCode,
                        MachinTypeName = $"{x.MachineType.MachineTypeTitle} {x.MachineType.CabinType.CabinTypeName} وزن {x.MachineType.FromWeight} تا {x.MachineType.UntilWeight}",
                        ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                        Tax = x.Tax,
                        SourceCityId = x.SourceCity.Id,
                        SourceCity = x.SourceCity.Name,
                        DestinationCityId = x.DestinationCity.Id,
                        DestinationCity = x.DestinationCity.Name,
                        Price = x.Price,
                        PhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                        MainName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        RegionId = x.Region.Id,
                        Region = x.Region.Name,
                        Longitude = x.Longitude,
                        Latitude = x.Latitude,
                        ShippingThirdParty = $"{x.ThirdParty.FirstName} {x.ThirdParty.LastName}",
                        ShippingThirdPartyId = x.ThirdPartyId,
                        ThirdPartyCompanyId = x.ThirdPartyCompanyId,
                    });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<ShippingCost>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.TransportationContractor)
            .Include(x => x.MachineType)
            .Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }

    public async Task<List<ShippingCost>?> GetShippingCostByContractor(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.MachineType)
            .Where(oo => oo.TransportationContractorId == id);

        return await query.ToListAsync(ct);
    }

#pragma warning restore CS8602 // Dereference of a possibly null reference.
}