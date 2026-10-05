using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.TransportationContractorPersonnels;

public class TransportationContractorPersonnelRepository : BaseRepository<EngineeringDBContext, TransportationContractorPersonnel>, ITransportationContractorPersonnelRepository
{
    public TransportationContractorPersonnelRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetTransportationContractorPersonnelByIdResponse?> GetTransportationContractorPersonnelById(long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetTransportationContractorPersonnelByIdResponse
            {
                CompanyName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                ContractorId = x.TransportationContractor.Id,
                Created = x.Created,
                Id = x.Id,
                IsActive = x.IsActive,
                IsIndivisual = x.TransportationContractor.ThirdParty.IsIndividual,
                LegalId = x.TransportationContractor.ThirdParty.Legal.Id,
                ThirdPartyId = x.TransportationContractor.ThirdParty.Id,
                RegisterationNo = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                PersonnelTitle = x.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                PersonnelAddressId = x.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                PersonnelAddress = x.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                PersonnelCityId = x.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                PersonnelCity = x.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                PersonnelThirdPartyId = x.ThirdParty.Id,
                PersonnelPhoneNumber = x.ThirdParty.DefaultPhoneNo,
                PersonnelLegacyId = x.ThirdParty.LegacyId,
                PersonnelLastName = x.ThirdParty.LastName,
                PersonnelFirstName = x.ThirdParty.FirstName,
                PersonnelIdentityNo = x.ThirdParty.IdentityNo,
                PersonnelDescription = x.ThirdParty.Description,
                CertificateNumber = x.CertificateNumber,
                Machines = x.ContractorPersonnelMachines.Select(z => new GetContractorMachinePersonnelResponseModel()
                {
                    Id = z.Id,
                    Color = z.TransportationContractorMachine.Color,
                    ContractorMachineId = z.TransportationContractorMachine.Id,
                    Vin = z.TransportationContractorMachine.Vin,
                    MachineTypeCode = z.TransportationContractorMachine.MachineType.MachineTypeCode,
                    MachineTypeName = z.TransportationContractorMachine.MachineType.MachineTypeTitle,
                    MachineTypeId = z.TransportationContractorMachine.MachineType.Id,
                    NumberPlate = z.TransportationContractorMachine.NumberPlate,
                }).ToList()
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<TransportationContractorPersonnel?> GetTransportationContractorPersonnel(long id, CT ct)
    {
        var query = await DbSet
            .Include(x => x.TransportationContractor)
            .Include(x => x.ContractorPersonnelMachines)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return query;
    }

    public async Task<(List<GetsActiveTransportationContractorPersonnelResponseModel> Data, int RowCount)> GetAllActiveTransportationContractorPersonnels(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? PersonnelIds,
        List<long>? machineIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x =>
            x.IsActive &&
            (ids == null || ids.Contains(x.Id)) &&
            (contractorIds == null || contractorIds.Contains(x.TransportationContractor.Id)) &&
            (PersonnelIds == null || PersonnelIds.Contains(x.ThirdParty.Id)) &&
            (machineIds == null || x.ContractorPersonnelMachines.Any(z => machineIds.Contains(z.TransportationContractorMachine.Id))) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.ThirdParty.FirstName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.FirstName + " " + x.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.IdentityNo, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.DefaultPhoneNo, filterData.MakeLikePattern())))
            .Select(x => new GetsActiveTransportationContractorPersonnelResponseModel()
            {
                CompanyName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                ContractorId = x.TransportationContractor.Id,
                Created = x.Created,
                Id = x.Id,
                IsActive = x.IsActive,
                IsIndivisual = x.TransportationContractor.ThirdParty.IsIndividual,
                LegalId = x.TransportationContractor.ThirdParty.Legal.Id,
                ThirdPartyId = x.TransportationContractor.ThirdParty.Id,
                RegisterationNo = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                PersonnelTitle = x.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                PersonnelAddressId = x.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                PersonnelAddress = x.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                PersonnelCityId = x.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                PersonnelCity = x.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                PersonnelThirdPartyId = x.ThirdParty.Id,
                PersonnelPhoneNumber = x.ThirdParty.DefaultPhoneNo,
                PersonnelLegacyId = x.ThirdParty.LegacyId,
                PersonnelLastName = x.ThirdParty.LastName,
                PersonnelFirstName = x.ThirdParty.FirstName,
                PersonnelIdentityNo = x.ThirdParty.IdentityNo,
                PersonnelDescription = x.ThirdParty.Description,
                CertificateNumber = x.CertificateNumber,
                Machines = x.ContractorPersonnelMachines.Select(z => new GetContractorMachinePersonnelResponseModel()
                {
                    Id = z.Id,
                    Color = z.TransportationContractorMachine.Color,
                    ContractorMachineId = z.TransportationContractorMachine.Id,
                    Vin = z.TransportationContractorMachine.Vin,
                    MachineTypeCode = z.TransportationContractorMachine.MachineType.MachineTypeCode,
                    MachineTypeName = z.TransportationContractorMachine.MachineType.MachineTypeTitle,
                    MachineTypeId = z.TransportationContractorMachine.MachineType.Id,
                    NumberPlate = z.TransportationContractorMachine.NumberPlate,
                }).ToList()
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<GetsFilteredTransportationContractorPersonnelResponseModel> Data, int RowCount)> GetFilteredTransportationContractorPersonnels(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? PersonnelIds,
        List<long>? machineIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x =>
            (isActive == null || x.IsActive == isActive) &&
            (ids == null || ids.Contains(x.Id)) &&
            (contractorIds == null || contractorIds.Contains(x.TransportationContractor.Id)) &&
            (PersonnelIds == null || PersonnelIds.Contains(x.ThirdParty.Id)) &&
            (machineIds == null || x.ContractorPersonnelMachines.Any(z => machineIds.Contains(z.TransportationContractorMachine.MachineTypeId))) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.ThirdParty.FirstName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.FirstName + " " + x.ThirdParty.LastName, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.IdentityNo, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.ThirdParty.DefaultPhoneNo, filterData.MakeLikePattern())))
            .Select(x => new GetsFilteredTransportationContractorPersonnelResponseModel()
            {
                CompanyName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                ContractorId = x.TransportationContractor.Id,
                Created = x.Created,
                Id = x.Id,
                IsActive = x.IsActive,
                IsIndivisual = x.TransportationContractor.ThirdParty.IsIndividual,
                LegalId = x.TransportationContractor.ThirdParty.Legal.Id,
                ThirdPartyId = x.TransportationContractor.ThirdParty.Id,
                RegisterationNo = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                PersonnelTitle = x.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                PersonnelAddressId = x.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                PersonnelAddress = x.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                PersonnelCityId = x.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                PersonnelCity = x.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                PersonnelThirdPartyId = x.ThirdParty.Id,
                PersonnelPhoneNumber = x.ThirdParty.DefaultPhoneNo,
                PersonnelLegacyId = x.ThirdParty.LegacyId,
                PersonnelLastName = x.ThirdParty.LastName,
                PersonnelFirstName = x.ThirdParty.FirstName,
                PersonnelIdentityNo = x.ThirdParty.IdentityNo,
                PersonnelDescription = x.ThirdParty.Description,
                CertificateNumber = x.CertificateNumber,
                Machines = x.ContractorPersonnelMachines.Select(z => new GetContractorMachinePersonnelResponseModel()
                {
                    Id = z.Id,
                    Color = z.TransportationContractorMachine.Color,
                    ContractorMachineId = z.TransportationContractorMachine.Id,
                    Vin = z.TransportationContractorMachine.Vin,
                    MachineTypeCode = z.TransportationContractorMachine.MachineType.MachineTypeCode,
                    MachineTypeName = z.TransportationContractorMachine.MachineType.MachineTypeTitle,
                    MachineTypeId = z.TransportationContractorMachine.MachineType.Id,
                    NumberPlate = z.TransportationContractorMachine.NumberPlate,
                }).ToList()
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<TransportationContractorPersonnel>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.TransportationContractor)
            .Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

}