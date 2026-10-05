using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.TransportationContractorMachines;

public class TransportationContractorMachineRepository : BaseRepository<EngineeringDBContext, TransportationContractorMachine>, ITransportationContractorMachineRepository
{
    public TransportationContractorMachineRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetTransportationContractorMachineByIdResponse?> GetTransportationContractorMachineById(long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetTransportationContractorMachineByIdResponse
            {
                Color = x.Color,
                ContractorId = x.TransportationContractor.Id,
                Id = x.Id,
                Created = x.Created,
                CompanyName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                FirstName = x.TransportationContractor.ThirdParty.FirstName,
                IdentityNo = x.TransportationContractor.ThirdParty.IdentityNo,
                IsActive = x.IsActive,
                IsIndivisual = x.TransportationContractor.ThirdParty.IsIndividual,
                LastName = x.TransportationContractor.ThirdParty.LastName,
                LegalId = x.TransportationContractor.ThirdParty.Legal.Id,
                ThirdPartyId = x.TransportationContractor.ThirdParty.Id,
                RegisterationNo = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                Vin = x.Vin,
                PhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                NumberPlate = x.NumberPlate,
                MachineTypeId = x.MachineType.Id,
                MachineTypeCode = x.MachineType.MachineTypeCode,
                MachineTypeName = x.MachineType.MachineTypeTitle,
                Personnels = x.ContractorPersonnelMachines.Select(z => new GetContractorPersonnelMachineResponseModel()
                {
                    FullName = z.TransportationContractorPersonnel.ThirdParty.FirstName + " " + z.TransportationContractorPersonnel.ThirdParty.LastName,
                    Id = z.Id,
                    ThirdPartyId = z.Id,
                    PhoneNumber = z.TransportationContractorPersonnel.ThirdParty.DefaultPhoneNo,
                    ContractorPersonnelId = z.TransportationContractorPersonnel.Id
                }).ToList()
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<TransportationContractorMachine?> GetTransportationContractorMachine(long id, CT ct)
    {
        var query = await DbSet
            .Include(x => x.TransportationContractor)
            .Include(x => x.MachineType)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return query;
    }

    public async Task<(List<GetsActiveTransportationContractorMachineResponseModel> Data, int RowCount)> GetAllActiveTransportationContractorMachines(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x =>
            x.IsActive &&
            (ids == null || ids.Contains(x.Id)) &&
            (contractorIds == null || contractorIds.Contains(x.TransportationContractor.Id)) &&
            (machineTypeIds == null || machineTypeIds.Contains(x.MachineType.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.NumberPlate, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Vin, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Color, filterData.MakeLikePattern())))
            .Select(x => new GetsActiveTransportationContractorMachineResponseModel()
            {
                Color = x.Color,
                ContractorId = x.TransportationContractor.Id,
                Id = x.Id,
                Created = x.Created,
                CompanyName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                FirstName = x.TransportationContractor.ThirdParty.FirstName,
                IdentityNo = x.TransportationContractor.ThirdParty.IdentityNo,
                IsActive = x.IsActive,
                IsIndivisual = x.TransportationContractor.ThirdParty.IsIndividual,
                LastName = x.TransportationContractor.ThirdParty.LastName,
                LegalId = x.TransportationContractor.ThirdParty.Legal.Id,
                ThirdPartyId = x.TransportationContractor.ThirdParty.Id,
                RegisterationNo = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                Vin = x.Vin,
                PhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                NumberPlate = x.NumberPlate,
                MachineTypeId = x.MachineType.Id,
                MachineTypeCode = x.MachineType.MachineTypeCode,
                MachineTypeName = x.MachineType.MachineTypeTitle,
                Personnels = x.ContractorPersonnelMachines.Select(z => new GetContractorPersonnelMachineResponseModel()
                {
                    FullName = z.TransportationContractorPersonnel.ThirdParty.FirstName + " " + z.TransportationContractorPersonnel.ThirdParty.LastName,
                    Id = z.Id,
                    ThirdPartyId = z.Id,
                    PhoneNumber = z.TransportationContractorPersonnel.ThirdParty.DefaultPhoneNo,
                    ContractorPersonnelId = z.TransportationContractorPersonnel.Id
                }).ToList()
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<GetsFilteredTransportationContractorMachineResponseModel> Data, int RowCount)> GetFilteredTransportationContractorMachines(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? machineTypeIds,
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
            (machineTypeIds == null || machineTypeIds.Contains(x.MachineType.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.NumberPlate, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Vin, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Color, filterData.MakeLikePattern())))
            .Select(x => new GetsFilteredTransportationContractorMachineResponseModel()
            {
                Color = x.Color,
                ContractorId = x.TransportationContractor.Id,
                Id = x.Id,
                Created = x.Created,
                CompanyName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                FirstName = x.TransportationContractor.ThirdParty.FirstName,
                IdentityNo = x.TransportationContractor.ThirdParty.IdentityNo,
                IsActive = x.IsActive,
                IsIndivisual = x.TransportationContractor.ThirdParty.IsIndividual,
                LastName = x.TransportationContractor.ThirdParty.LastName,
                LegalId = x.TransportationContractor.ThirdParty.Legal.Id,
                ThirdPartyId = x.TransportationContractor.ThirdParty.Id,
                RegisterationNo = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                Vin = x.Vin,
                PhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                NumberPlate = x.NumberPlate,
                MachineTypeId = x.MachineType.Id,
                MachineTypeCode = x.MachineType.MachineTypeCode,
                MachineTypeName = x.MachineType.MachineTypeTitle,
                Personnels = x.ContractorPersonnelMachines.Select(z => new GetContractorPersonnelMachineResponseModel()
                {
                    FullName = z.TransportationContractorPersonnel.ThirdParty.FirstName + " " + z.TransportationContractorPersonnel.ThirdParty.LastName,
                    Id = z.Id,
                    ThirdPartyId = z.Id,
                    PhoneNumber = z.TransportationContractorPersonnel.ThirdParty.DefaultPhoneNo,
                    ContractorPersonnelId = z.TransportationContractorPersonnel.Id
                }).ToList()
            });
        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<TransportationContractorMachine>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.TransportationContractor)
            .Include(x => x.MachineType)
            .Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }

    public Task<bool> IsDuplicateMachine(long? id, string numberPlate, string? vin, CT ct)
    {
        return DbSet.AnyAsync(x =>
            x.NumberPlate == numberPlate &&
            (vin == null || (!string.IsNullOrEmpty(x.Vin) && x.Vin == vin)) &&
            (id == null || x.Id != id), ct);
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

}