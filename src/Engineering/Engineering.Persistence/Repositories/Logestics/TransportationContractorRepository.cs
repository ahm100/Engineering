using Engineering.Application.Extensions;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.TransportationContractors;

public class TransportationContractorRepository : BaseRepository<EngineeringDBContext, TransportationContractor>, ITransportationContractorRepository
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public TransportationContractorRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetTransportationContractorByIdResponse?> GetTransportationContractorById(long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetTransportationContractorByIdResponse
            {
                Id = x.Id,
                Created = x.Created,
                EndOfContract = x.EndOfContract,
                StartOfContract = x.StartOfContract,
                IsActive = x.IsActive,
                LegacyId = x.LegacyId,
                ThirdPartyId = x.ThirdParty.Id,
                IsIndivisual = x.ThirdParty.IsIndividual,
                Address = x.ThirdParty.Addresses.Select(a => a.AddressText).FirstOrDefault(),
                AddressId = x.ThirdParty.Addresses.Select(a => a.Id).FirstOrDefault(),
                City = x.ThirdParty.Addresses.Select(a => a.City.Name).FirstOrDefault(),
                CityId = x.ThirdParty.Addresses.Select(a => a.City.Id).FirstOrDefault(),
                Title = x.ThirdParty.Addresses.Select(a => a.Title).FirstOrDefault(),
                CompanyName = x.ThirdParty.Legal.CompanyName ?? x.Title,
                ContractorTitle = x.Title ?? x.ThirdParty.Legal.CompanyName,
                Description = x.ThirdParty.Description,
                FirstName = x.ThirdParty.FirstName,
                LastName = x.ThirdParty.LastName,
                IdentityNo = x.ThirdParty.IdentityNo,
                LegalId = x.ThirdParty.Legal.Id,
                PhoneNumber = x.ThirdParty.DefaultPhoneNo,
                PostalCode = x.ThirdParty.Addresses.Select(a => a.PostalCode).FirstOrDefault(),
                RegisterationNo = x.ThirdParty.Legal.RegistrationNo,
                DeliveryMethod = TransportationContractor.YieldDeliveryMethods(x.DeliveryMethod).ToArray(),
                DeliveryType = TransportationContractor.YieldDeliveryTypes(x.DeliveryType).ToArray(),
                TransportationContractorDocumentUrls = x.ContractorDocuments
                    .Select(z => z.DocumentUrl)
                    .ToList(),
                Type = x.Type,
                FirstPrefix = x.FirstPrefix,
                SecondPrefix = x.SecondPrefix,
                FixedNumber = x.FixedNumber,
                PercentageValue = x.PercentageValue,
                PriceWeights = x.PriceWeights.Select(z => new GetContractorPriceWeightByIdModel()
                {
                    Id = z.Id,
                    IsFixed = z.IsFixed,
                    Price = z.Price,
                    UntilWeight = z.UntilWeight,
                }).ToList(),
                ServicePrice = x.ServicePrice,
                TaxPercent = x.TaxPercent,
                Insurances = x.TransportationContractorInsurances.Select(z => new GetContractorInsuranceByIdModel()
                {
                    Addition = z.Addition,
                    Division = z.Division,
                    FixedPrice = z.FixedPrice,
                    Id = z.Id,
                    MaxProductPrice = z.MaxProductPrice,
                    MinProductPrice = z.MinProductPrice,
                    Multiplication = z.Multiplication,
                    Subtraction = z.Subtraction
                }).ToList(),
                Managers = x.ContractorManagers.Select(z => new GetcontractorManagerModel()
                {
                    Id = x.Id,
                    FirstName = x.ThirdParty.FirstName,
                    LastName = x.ThirdParty.LastName,
                    ThirdPartyId = x.ThirdPartyId,
                    OrganizationCode = x.ThirdParty.OrganizationCode,
                    PhoneNumber = x.ThirdParty.DefaultPhoneNo
                }).ToList()
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<TransportationContractor?> GetTransportationContractor(long id, CT ct)
    {
        var query = await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return query;
    }

    public async Task<TransportationContractor?> GetTransportationContractorForUpdate(long id, CT ct)
    {
        var query = await DbSet
            .Include(x => x.ContractorDocuments.Where(t => t.TransportationContractorId == id))
            .Include(x => x.PriceWeights.Where(t => t.TransportationContractorId == id))
                .ThenInclude(x => x.PriceWeightHistories)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return query;
    }

    public async Task<TransportationContractor?> GetTransportationContractorForImport(long id, CT ct)
    {
        var query = await DbSet
            .Include(x => x.PriceWeights.Where(t => t.TransportationContractorId == id))
                .ThenInclude(x => x.PriceWeightHistories)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return query;
    }

    public async Task<(List<GetsActiveTransportationContractorResponseModel> Data, int RowCount)> GetAllActiveTransportationContractors(
        List<long>? ids,
        List<long>? thirdPartyIds,
        DateTime? startDate,
        DateTime? endDate,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<TransportationContractorCalculateType>? types,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        IQueryable<TransportationContractor> query = DbSet;

        if (ids != null)
        {
            query = query.Where(x => ids.Contains(x.Id));
        }

        if (thirdPartyIds != null)
        {
            query = query.Where(x => x.ThirdPartyId != null && thirdPartyIds.Contains(x.ThirdPartyId.Value));
        }

        if (startDate != null)
        {
            query = query.Where(x => x.StartOfContract != null && x.StartOfContract.Value.Date >= startDate.Value.Date);
        }

        if (endDate != null)
        {
            query = query.Where(x => x.EndOfContract != null && x.EndOfContract.Value.Date <= endDate.Value.Date);
        }

        if (deliveryTypes != null)
        {
            var typesCondition = TransportationContractor.CreateDeliveryTypeCondition(deliveryTypes);
            query = query.Where(typesCondition);
        }

        if (deliveryMethods != null)
        {
            var typesCondition = TransportationContractor.CreateDeliveryMethodCondition(deliveryMethods);
            query = query.Where(typesCondition);
        }

        if (types != null)
        {
            query = query.Where(x => types.Contains(x.Type));
        }

        if (!string.IsNullOrEmpty(filterData))
        {
            query = query.Where(x =>
            EF.Functions.Like(x.ThirdParty.Legal.CompanyName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.FirstName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.FirstName + " " + x.ThirdParty.LastName, filterData.MakeLikePattern()));
        }

        query = query.Where(x => x.IsActive);

        var result = await query
            .OrderByDescending(a => a.Created)
            .Select(x => new GetsActiveTransportationContractorResponseModel()
            {
                Id = x.Id,
                Created = x.Created,
                EndOfContract = x.EndOfContract,
                StartOfContract = x.StartOfContract,
                IsActive = x.IsActive,
                LegacyId = x.LegacyId,
                ThirdPartyId = x.ThirdParty.Id,
                IsIndivisual = x.ThirdParty.IsIndividual,
                Address = x.ThirdParty.Addresses.Select(a => a.AddressText).FirstOrDefault(),
                AddressId = x.ThirdParty.Addresses.Select(a => a.Id).FirstOrDefault(),
                City = x.ThirdParty.Addresses.Select(a => a.City.Name).FirstOrDefault(),
                CityId = x.ThirdParty.Addresses.Select(a => a.City.Id).FirstOrDefault(),
                Title = x.ThirdParty.Addresses.Select(a => a.Title).FirstOrDefault(),
                CompanyName = x.ThirdParty.Legal.CompanyName ?? x.Title,
                ContractorTitle = x.Title ?? x.ThirdParty.Legal.CompanyName,
                Description = x.ThirdParty.Description,
                FirstName = x.ThirdParty.FirstName,
                LastName = x.ThirdParty.LastName,
                IdentityNo = x.ThirdParty.IdentityNo,
                LegalId = x.ThirdParty.Legal.Id,
                PhoneNumber = x.ThirdParty.DefaultPhoneNo,
                PostalCode = x.ThirdParty.Addresses.Select(a => a.PostalCode).FirstOrDefault(),
                RegisterationNo = x.ThirdParty.Legal.RegistrationNo,
                DeliveryMethod = TransportationContractor.YieldDeliveryMethods(x.DeliveryMethod).ToArray(),
                DeliveryType = TransportationContractor.YieldDeliveryTypes(x.DeliveryType).ToArray(),
                TransportationContractorDocumentUrls = x.ContractorDocuments
                    .Select(z => z.DocumentUrl)
                    .ToList(),

                Type = x.Type,
                //Personnels = x.ContractorPersonnels
                //    .Select(z => new GetsActivecontractorPersonnelModel
                //    {
                //        Id = z.Id,
                //        ThirdPartyId = z.ThirdParty.Id,
                //        IsIndivisual = x.ThirdParty.IsIndividual,
                //        AddressId = z.ThirdParty.Addresses.Select(a => a.Id).FirstOrDefault(),
                //        Title = z.ThirdParty.Addresses.Select(a => a.Title).FirstOrDefault(),
                //        Address = z.ThirdParty.Addresses.Select(a => a.AddressText).FirstOrDefault(),
                //        CityId = z.ThirdParty.Addresses.Select(a => a.City.Id).FirstOrDefault(),
                //        City = z.ThirdParty.Addresses.Select(a => a.City.Name).FirstOrDefault(),
                //        Description = z.ThirdParty.Description,
                //        FirstName = z.ThirdParty.FirstName,
                //        IdentityNo = z.ThirdParty.IdentityNo,
                //        LastName = z.ThirdParty.LastName,
                //        PhoneNumber = z.ThirdParty.DefaultPhoneNo,
                //    })
                //    .ToList(),
                FirstPrefix = x.FirstPrefix,
                SecondPrefix = x.SecondPrefix,
                FixedNumber = x.FixedNumber,
                PercentageValue = x.PercentageValue,
                PriceWeights = x.PriceWeights.Select(z => new GetActivecontractorPriceWeightModel()
                {
                    Id = z.Id,
                    IsFixed = z.IsFixed,
                    Price = z.Price,
                    UntilWeight = z.UntilWeight,
                }).ToList(),
                ServicePrice = x.ServicePrice,
                TaxPercent = x.TaxPercent,
                Insurances = x.TransportationContractorInsurances.Select(z => new GetActiveContractorInsuranceModel()
                {
                    Addition = z.Addition,
                    Division = z.Division,
                    FixedPrice = z.FixedPrice,
                    Id = z.Id,
                    MaxProductPrice = z.MaxProductPrice,
                    MinProductPrice = z.MinProductPrice,
                    Multiplication = z.Multiplication,
                    Subtraction = z.Subtraction
                }).ToList(),
                Managers = x.ContractorManagers.Select(z => new GetsActivecontractorManagerModel()
                {
                    Id = x.Id,
                    FirstName = x.ThirdParty.FirstName,
                    LastName = x.ThirdParty.LastName,
                    ThirdPartyId = x.ThirdPartyId,
                    OrganizationCode = x.ThirdParty.OrganizationCode,
                    PhoneNumber = x.ThirdParty.DefaultPhoneNo
                }).ToList()
            })
            .ToPagedResultSet(pageIndex, pageSize, ct);

        return (result.Data, result.TotalCount);
    }

    public async Task<(List<GetsFilteredTransportationContractorResponseModel> Data, int RowCount)> GetFilteredTransportationContractors(
        List<long>? ids,
        List<long>? thirdPartyIds,
        DateTime? startDate,
        DateTime? endDate,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<TransportationContractorCalculateType>? types,
        PackingShippingType? packingShippingType,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        IQueryable<TransportationContractor> query = DbSet;

        if (ids != null)
        {
            query = query.Where(x => ids.Contains(x.Id));
        }

        if (thirdPartyIds != null)
        {
            query = query.Where(x => x.ThirdPartyId != null && thirdPartyIds.Contains(x.ThirdPartyId.Value));
        }

        if (startDate != null)
        {
            query = query.Where(x => x.StartOfContract != null && x.StartOfContract.Value.Date >= startDate.Value.Date);
        }

        if (endDate != null)
        {
            query = query.Where(x => x.EndOfContract != null && x.EndOfContract.Value.Date <= endDate.Value.Date);
        }

        if (deliveryTypes != null)
        {
            var typesCondition = TransportationContractor.CreateDeliveryTypeCondition(deliveryTypes);
            query = query.Where(typesCondition);
        }

        if (deliveryMethods != null)
        {
            var typesCondition = TransportationContractor.CreateDeliveryMethodCondition(deliveryMethods);
            query = query.Where(typesCondition);
        }

        if (types != null)
        {
            query = query.Where(x => types.Contains(x.Type));
        }

        if (isActive != null)
        {
            query = query.Where(x => x.IsActive == isActive);
        }

        if (!string.IsNullOrEmpty(filterData))
        {
            query = query.Where(x =>
            EF.Functions.Like(x.ThirdParty.Legal.CompanyName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.FirstName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.FirstName + " " + x.ThirdParty.LastName, filterData.MakeLikePattern()));
        }

        if (packingShippingType != null)
        {
            if (packingShippingType == PackingShippingType.Agency)
            {
                query = query.Where(x => x.ThirdPartyId == null && !string.IsNullOrEmpty(x.Title));
            }
            else if (packingShippingType == PackingShippingType.Contracting)
            {
                query = query.Where(x => x.ThirdPartyId != null);
            }
        }

        var result = await query
            .OrderByDescending(a => a.Created)
            .Select(x => new GetsFilteredTransportationContractorResponseModel()
            {
                Id = x.Id,
                Created = x.Created,
                EndOfContract = x.EndOfContract,
                StartOfContract = x.StartOfContract,
                IsActive = x.IsActive,
                LegacyId = x.LegacyId,
                ThirdPartyId = x.ThirdParty.Id,
                IsIndivisual = x.ThirdParty.IsIndividual,
                Address = x.ThirdParty.Addresses.Select(a => a.AddressText).FirstOrDefault(),
                AddressId = x.ThirdParty.Addresses.Select(a => a.Id).FirstOrDefault(),
                City = x.ThirdParty.Addresses.Select(a => a.City.Name).FirstOrDefault(),
                CityId = x.ThirdParty.Addresses.Select(a => a.City.Id).FirstOrDefault(),
                Title = x.ThirdParty.Addresses.Select(a => a.Title).FirstOrDefault(),
                CompanyName = x.ThirdParty.Legal.CompanyName ?? x.Title,
                ContractorTitle = x.Title ?? x.ThirdParty.Legal.CompanyName,
                Description = x.ThirdParty.Description,
                FirstName = x.ThirdParty.FirstName,
                LastName = x.ThirdParty.LastName,
                IdentityNo = x.ThirdParty.IdentityNo,
                LegalId = x.ThirdParty.Legal.Id,
                PhoneNumber = x.ThirdParty.DefaultPhoneNo,
                PostalCode = x.ThirdParty.Addresses.Select(a => a.PostalCode).FirstOrDefault(),
                RegisterationNo = x.ThirdParty.Legal.RegistrationNo,
                DeliveryMethod = TransportationContractor.YieldDeliveryMethods(x.DeliveryMethod).ToArray(),
                DeliveryType = TransportationContractor.YieldDeliveryTypes(x.DeliveryType).ToArray(),
                TransportationContractorDocumentUrls = x.ContractorDocuments
                    .Select(z => z.DocumentUrl)
                    .ToList(),

                Type = x.Type,
                FirstPrefix = x.FirstPrefix,
                SecondPrefix = x.SecondPrefix,
                FixedNumber = x.FixedNumber,
                PercentageValue = x.PercentageValue,
                PriceWeights = x.PriceWeights.Select(z => new GetFilteredcontractorPriceWeightModel()
                {
                    Id = z.Id,
                    IsFixed = z.IsFixed,
                    Price = z.Price,
                    UntilWeight = z.UntilWeight,
                }).ToList(),
                ServicePrice = x.ServicePrice,
                TaxPercent = x.TaxPercent,
                Insurances = x.TransportationContractorInsurances.Select(z => new GetFilteredContractorInsuranceModel()
                {
                    Addition = z.Addition,
                    Division = z.Division,
                    FixedPrice = z.FixedPrice,
                    Id = z.Id,
                    MaxProductPrice = z.MaxProductPrice,
                    MinProductPrice = z.MinProductPrice,
                    Multiplication = z.Multiplication,
                    Subtraction = z.Subtraction
                }).ToList(),
                Managers = x.ContractorManagers.Select(z => new GetsFilteredcontractorManagerModel()
                {
                    Id = x.Id,
                    FirstName = x.ThirdParty.FirstName,
                    LastName = x.ThirdParty.LastName,
                    ThirdPartyId = x.ThirdPartyId,
                    OrganizationCode = x.ThirdParty.OrganizationCode,
                    PhoneNumber = x.ThirdParty.DefaultPhoneNo
                }).ToList()
            })
            .ToPagedResultSet(pageIndex, pageSize, ct);

        return (result.Data, result.TotalCount);
    }

    public async Task<List<TransportationContractor>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.ContractorPersonnels.Where(z => ids.Contains(z.TransportationContractorId)))
            .Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }

    public async Task<List<TransportationContractor>> GetByIdsIncludeLess(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.ThirdParty.Legal)
            .Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }

#pragma warning restore CS8602 // Dereference of a possibly null reference.
}