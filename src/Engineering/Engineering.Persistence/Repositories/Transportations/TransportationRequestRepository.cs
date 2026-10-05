using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalSnapPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Extensions;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestRepository : BaseRepository<EngineeringDBContext, TransportationRequest>, ITransportationRequestRepository
{
    public TransportationRequestRepository(EngineeringDBContext context) : base(context)
    {
    }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8629 // Nullable value type may be null.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8603 // Possible null reference return.

    public async Task<long> RequestNumberCreator(bool? isCredit, long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x =>
            (isCredit == null || x.IsCredit == isCredit) &&
            (companyId == null || x.CompanyId == companyId) &&
            (x.RequestNumber != null && x.RequestNumber > 0))
            .Select(x => (long)x.RequestNumber!).ToListAsync(ct);

        long suggestedRequestedNumber = 1;
        if (query is not null && query.Any())
            suggestedRequestedNumber = query.Max() + 1;

        return suggestedRequestedNumber;
    }

    public async Task<TransportationRequest?> GetByIdNoInclude(
        long id, CT ct)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<TransportationRequest?> GetById(long id, CT ct)
    {
        var query = DbSet
            .AsSplitQuery()
                    .Include(t => t.TransportationRequestDetails)
                    .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.ShippingCost)
                    .Include(t => t.TransportationContractor)
                        .ThenInclude(t => t.PriceWeights)
                    .Include(t => t.TransportationContractor)
                        .ThenInclude(t => t.TransportationContractorInsurances)
                    .Include(t => t.TransportationContractor)
                        .ThenInclude(t => t.ShippingCosts)
                    .Include(t => t.Transportation)
                    .Include(t => t.Trip)
                    .Include(t => t.TransportationRequestDocuments)
                    .Include(t => t.MachineType)
                    .Include(t => t.CostCenter)
                    .Include(t => t.Project)
                        .ThenInclude(t => t.ProjectCostCenters)
                            .ThenInclude(t => t.CostCenter)
                    .Include(t => t.BillOfLading)
                    .Include(t => t.TransportationRequestProjectOperations)
                        .ThenInclude(t => t.ProjectOperation)
                            .ThenInclude(t => t.OperationInfo)
                    .Include(t => t.TransportationRequestProjectOperationDetails)
                        .ThenInclude(t => t.ProjectOperationDetail)
                            .ThenInclude(t => t.OperationLocation)
                    .Include(t => t.TransportationRequestProjects)
                        .ThenInclude(t => t.Project)
                    .Include(t => t.TransportationRequestCostCenters)
                        .ThenInclude(t => t.CostCenter)
                    .Include(t => t.Season.Branch.Category)

                    .Where(t => t.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<TransportationRequest?> GetLogesticById(long id, CT ct)
    {
        var query = DbSet
                   .Include(t => t.TransportationRequestDetails)
                   .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.TransportationRequestWarehouses)
                            .ThenInclude(x => x.PackingProduct)
                   .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.TransportationRequestWarehouses)
                            .ThenInclude(x => x.PackingSourceAddress)
                   .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.TransportationRequestWarehouses)
                            .ThenInclude(x => x.PackingDestinationAddress)
                   .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.TransportationCargo.Packing)
                   .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.TransportationCargo.Packing)
                   .Include(t => t.TransportationCargoPallets)
                    .ThenInclude(t => t.TransportationCargo)
                   .Include(t => t.TransportationContractor)
                       .ThenInclude(t => t.PriceWeights)
                   .Include(t => t.TransportationContractor)
                       .ThenInclude(t => t.TransportationContractorInsurances)
                   .Include(t => t.TransportationContractor)
                       .ThenInclude(t => t.ShippingCosts)
                   .Include(t => t.MachineType)

                    .Where(t => t.Id == id)
                    .AsNoTracking();

        var item = await query.FirstOrDefaultAsync(ct);

        if (item == null)
            return null;

        var packingids = item.TransportationCargoPallets.Listed(x => x.TransportationCargo.PackingId);

        var packingAddress = DbContext.Set<ViewPackingAddress>()
            .Where(x => x.PackingId != null && packingids.Contains(x.PackingId.Value));

        foreach (var item1 in item.TransportationCargoPallets.Listed(x => x.TransportationCargo.Packing))
        {
            item1.PackingAddress = packingAddress.Where(x => x.PackingId == item1.Id).ToList();
        }

        return item;
    }
    public async Task<TransportationRequest?> GetByIdlessInclude(long id, CT ct)
    {
        var query = DbSet
                    .Include(t => t.TransportationRequestDetails)
                    .Include(t => t.TransportationCargoPallets)
                        .ThenInclude(t => t.ShippingCost)
                    .Include(t => t.TransportationContractor)
                        .ThenInclude(t => t.TransportationContractorInsurances)
                    .Include(t => t.Transportation)
                    .Include(t => t.TransportationRequestDocuments)
                    .Include(t => t.MachineType)
                    .Include(t => t.CostCenter)
                    .Include(t => t.Project)
                        .ThenInclude(t => t.ProjectCostCenters)
                            .ThenInclude(t => t.CostCenter)
                    .Include(t => t.BillOfLading)
                    .Include(t => t.Season.Branch.Category)

                    .Where(t => t.Id == id)
                    .AsNoTracking();

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }
    public async Task<TransportationRequest?> GetByIdIncludeLess(long id, CT ct)
    {
        var query = DbSet
            .Include(t => t.TransportationContractor)
                .ThenInclude(x => x.TransportationContractorInsurances)
            .Include(t => t.TransportationContractor)
                .ThenInclude(x => x.PriceWeights)
            .Include(t => t.Transportation)
            .Include(t => t.TransportationRequestDocuments)
            .Include(t => t.MachineType)
            .Include(t => t.TransportationRequestDetails)
            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(t => t.TransportationCargo)
                  .ThenInclude(t => t.Packing)
            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(t => t.TransportationCargo)
            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(t => t.PackingPallet)
            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(t => t.TransportationRequestWarehouses)
                    .ThenInclude(t => t.PackingProduct.DestinationPackingAddress)
            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(t => t.TransportationRequestWarehouses)
                    .ThenInclude(t => t.PackingProduct.SourcePackingAddress)

             .Where(t => t.Id == id)
             .AsNoTracking();

        var packingIds = query.SelectMany(x => x.TransportationCargoPallets).Listed(x => x.TransportationCargo.PackingId);
        var addresses = await DbContext.Set<ViewPackingAddress>()
            .Where(x => x.PackingId != null && packingIds.Contains(x.PackingId.Value)).ToListAsync(ct);

        var item = await query.FirstOrDefaultAsync(ct);

        foreach (var pallet in item.TransportationCargoPallets)
            pallet.TransportationCargo.Packing.PackingAddress = addresses.Where(x => x.PackingId == pallet.TransportationCargo.PackingId).ToList();

        return item;
    }

    public async Task<GetTransportationRequestByIdResponse?> GetByIdWithoutInclude(long id, CT ct)
    {
        var query = DbSet
             .Where(t => t.Id == id)
             .Select(x => new GetTransportationRequestByIdResponse()
             {
                 Id = x.Id,
                 RequestNumber = x.RequestNumber,
                 CostCenterNames = string.Join(", ", x.TransportationRequestCostCenters.Select(z => z.CostCenter.CostCenterName) ?? null),
                 ProjectNames = string.Join(", ", x.TransportationRequestProjects.Select(z => z.Project.ProjectName) ?? null),
                 TransportationId = x.Transportation.Id,
                 TransportationName = x.Transportation.TransportationName,
                 IsPassenger = x.Transportation.IsPassenger,
                 TripId = x.Trip.Id,
                 TripName = x.Trip.TripName,
                 MachineTypeId = x.MachineType != null ? x.MachineType.Id : null,
                 MachineTypeName = x.MachineType != null ? x.MachineType.MachineTypeTitle : null,
                 BillOfLadingId = x.BillOfLading != null ? x.BillOfLading.Id : null,
                 BillOfLadingName = x.BillOfLading != null ? x.BillOfLading.BillOfLadingName : null,
                 RequestById = x.CreatorId,
                 StartingCityId = x.StartingCityId,
                 DestinationCityId = x.DestinationCityId,
                 ImageLink = x.ImageLink,
                 StartDate = TimeCalculator.DatePiker(x.StartDate)!,
                 EndDate = TimeCalculator.DatePiker(x.EndDate)!,
                 Description = x.Description,
                 DriverId = x.DriverId,
                 PostageDate = TimeCalculator.DatePiker(x.PostageDate),
                 ReceivedDate = TimeCalculator.DatePiker(x.ReceivedDate),
                 BillOfLadingImage = x.BillOfLadingImage,
                 DelivererName = x.DelivererName,
                 RecipientName = x.RecipientName,
                 FreightNumber = x.FreightNumber,
                 LoadWeight = x.LoadWeight,
                 CarSpecifications = x.CarSpecifications,
                 NumberPlates = x.NumberPlates,
                 AccountNumber = x.AccountNumber,
                 BankId = x.BankId,
                 CardNumber = x.CardNumber,
                 AccountName = x.AccountName,
                 IBAN = x.IBAN,
                 Price = x.Price,
                 CurrencyUnitId = x.CurrencyUnitId,
                 AccountDescription = x.AccountDescription,
                 CarID = x.CarID,
                 StatusData = new((int)x.TransportationRequestStatus, x.TransportationRequestStatus.GetEnumDescription()),
                 ManagerDescription = x.ManagerDescription,
                 ConfrimUserId = x.ConfirmUserId,
                 ConfrimDate = x.ConfirmDate,
                 StartingCityAddress = x.StartingCityAddress,
                 DestinationAddress = x.DestinationAddress,
                 SeasonId = x.Season != null ? x.Season.Id : null,
                 Season = x.Season != null ? x.Season.SeasonName : null,
                 BranchId = x.Season != null && x.Season.Branch != null ? x.Season.Branch.Id : null,
                 Branch = x.Season != null && x.Season.Branch != null ? x.Season.Branch.BranchName : null,
                 CategoryId = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.Id : null,
                 Category = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.CategoryName : null,
                 PaymentType = x.TransportationPaymentType,
                 PaymentDate = x.PaymentDate,
                 CompanyId = x.CompanyId,
                 CostCenters = x.TransportationRequestCostCenters.Select(z => new GetTransportationRequestCostCenter(z.Id, z.CostCenter.Id, z.CostCenter.CostCenterName)).ToList(),
                 Projects = x.TransportationRequestProjects.Select(z => new GetTransportationRequestProject(z.Id, z.Project.Id, z.Project.ProjectName)).ToList(),
                 ProjectOperations = x.TransportationRequestProjectOperations.Select(z => new GetTransportationRequestProjectOperation(z.Id, z.ProjectOperation.Id, z.ProjectOperation.OperationInfo.OperationInfoName)).ToList(),
                 ProjectOperationDetails = x.TransportationRequestProjectOperationDetails.Select(z => new GetTransportationRequestProjectOperationDetail(z.Id, z.ProjectOperationDetail.Id, z.ProjectOperationDetail.OperationLocation.PublicName)).ToList(),
                 Created = x.Created,
                 Documents = x.TransportationRequestDocuments.Select(z => new GetTransportationRequestDocumentByIdDocumentModel()
                 {
                     Id = z.Id,
                     Url = z.Url,
                 }).ToList(),
                 TicketPayerId = x.TicketPayerId,
                 DriverName = x.DriverName,
                 TransportationCostCategoryId = x.CostCategoryId,
                 TransportationCostGroupId = x.CostGroupId,
                 SecondDestinationCityId = x.SecondDestinationCityId,
                 PhoneNumber = x.PhoneNumber,
                 TransportationContractorId = x.TransportationContractor.Id,
                 Title = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                 Address = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                 AddressId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                 CityId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                 City = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                 PostalCode = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.PostalCode).FirstOrDefault(),
                 ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                 ContractorPhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                 MainName = $"{x.TransportationContractor.ThirdParty.Legal.CompanyName}-{x.TransportationContractor.ThirdParty.FirstName} {x.TransportationContractor.ThirdParty.LastName}",
                 Warehouses = x.TransportationCargoPallets.Select(z => new GetRequestWarehousesByIdModel()
                 {
                     Id = z.Id,
                     PackingId = z.TransportationCargo.Packing.Id,
                     PackingNumber = z.TransportationCargo.PackingNumber,
                     Price = z.Price,
                     ShippingCostId = z.ShippingCostId,
                     ShippingCostPrice = z.ShippingCost.Price,
                     ThirdPartyId = z.TransportationCargo.ThirdPartyId,
                     ThirdParty = $"{z.TransportationCargo.ThirdParty.FirstName} {z.TransportationCargo.ThirdParty.LastName}",
                     ThirdPartyName = z.TransportationCargo.ThirdPartyName,
                     WarehouseId = z.PackingDestinationAddress.WarehouseId,
                     WarehouseName = z.PackingDestinationAddress.Warehouse.Name,
                     WarehouseCode = z.PackingDestinationAddress.Warehouse.Code
                 }).ToList()
             });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetAirplaneByIdResponse?> GetAirPlaneByIdWithoutInclude(long id, CT ct)
    {
        var query = DbSet
             .Where(t => t.Id == id)
             .Select(x => new GetAirplaneByIdResponse()
             {
                 Id = x.Id,
                 RequestNumber = x.RequestNumber,
                 CostCenterNames = string.Join(", ", x.TransportationRequestCostCenters.Select(z => z.CostCenter.CostCenterName) ?? null),
                 ProjectNames = string.Join(", ", x.TransportationRequestProjects.Select(z => z.Project.ProjectName) ?? null),
                 TransportationId = x.Transportation.Id,
                 TransportationName = x.Transportation.TransportationName,
                 TripId = x.Trip.Id,
                 TripName = x.Trip.TripName,
                 CreatorId = x.CreatorId,
                 StartingCityId = x.StartingCityId,
                 DestinationCityId = x.DestinationCityId,
                 StartDate = TimeCalculator.DatePiker(x.StartDate)!,
                 EndDate = TimeCalculator.DatePiker(x.EndDate)!,
                 Description = x.Description,
                 AccountNumber = x.AccountNumber,
                 BankId = x.BankId,
                 CardNumber = x.CardNumber,
                 AccountName = x.AccountName,
                 Iban = x.IBAN,
                 FareAmount = x.Price,
                 CurrencyUnitId = x.CurrencyUnitId,
                 StatusData = new((int)x.TransportationRequestStatus, x.TransportationRequestStatus.GetEnumDescription()),
                 ManagerDescription = x.ManagerDescription,
                 ConfrimUserId = x.ConfirmUserId,
                 ConfrimDate = x.ConfirmDate,
                 DestinationAddress = x.DestinationAddress,
                 SeasonId = x.Season != null ? x.Season.Id : null,
                 Season = x.Season != null ? x.Season.SeasonName : null,
                 BranchId = x.Season != null && x.Season.Branch != null ? x.Season.Branch.Id : null,
                 Branch = x.Season != null && x.Season.Branch != null ? x.Season.Branch.BranchName : null,
                 CategoryId = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.Id : null,
                 Category = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.CategoryName : null,
                 PaymentType = x.TransportationPaymentType,
                 PaymentDate = x.PaymentDate,
                 CompanyId = x.CompanyId,
                 CostCenters = x.TransportationRequestCostCenters.Select(z => new GetTransportationRequestCostCenter(z.Id, z.CostCenter.Id, z.CostCenter.CostCenterName)).ToList(),
                 Projects = x.TransportationRequestProjects.Select(z => new GetTransportationRequestProject(z.Id, z.Project.Id, z.Project.ProjectName)).ToList(),
                 ProjectOperations = x.TransportationRequestProjectOperations.Select(z => new GetAirplaneProjectOperation(z.Id, z.ProjectOperation.Id, z.ProjectOperation.OperationInfo.OperationInfoName)).ToList(),
                 ProjectOperationDetails = x.TransportationRequestProjectOperationDetails.Select(z => new GetAirplaneProjectOperationDetail(z.Id, z.ProjectOperationDetail.Id, z.ProjectOperationDetail.OperationLocation.PublicName)).ToList(),
                 Created = x.Created,
                 Documents = x.TransportationRequestDocuments.Select(z => new GetAirplaneDocumentByIdDocumentModel()
                 {
                     Id = z.Id,
                     Url = z.Url,
                 }).ToList(),
                 TicketPayerId = x.TicketPayerId,
                 TransportationCostCategoryId = x.CostCategoryId,
                 TransportationCostGroupId = x.CostGroupId,
                 EndDateShamsi = TimeCalculator.ConvertToShamsi(x.EndDate),
                 StartDateShamsi = TimeCalculator.ConvertToShamsi(x.StartDate),
                 EndTime = x.EndDate.Value.TimeOfDay,
                 StartTime = x.StartDate.Value.TimeOfDay,
                 PassengerId = x.PassengerId,
                 Passenger = x.Passenger,
                 TransportationRequestStatus = x.TransportationRequestStatus,
                 TransportationType = x.Transportation.TransportationType,
             });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetSnapByIdResponse?> GetSnapByIdWithoutInclude(long id, CT ct)
    {
        var query = DbSet
             .Where(t => t.Id == id)
             .Select(x => new GetSnapByIdResponse()
             {
                 Id = x.Id,
                 RequestNumber = x.RequestNumber,
                 CostCenterNames = string.Join(", ", x.TransportationRequestCostCenters.Select(z => z.CostCenter.CostCenterName) ?? null),
                 ProjectNames = string.Join(", ", x.TransportationRequestProjects.Select(z => z.Project.ProjectName) ?? null),
                 TransportationId = x.Transportation.Id,
                 TransportationName = x.Transportation.TransportationName,
                 TripId = x.Trip.Id,
                 TripName = x.Trip.TripName,
                 RequestById = x.CreatorId,
                 StartingCityId = x.StartingCityId,
                 DestinationCityId = x.DestinationCityId,
                 StartDate = TimeCalculator.DatePiker(x.StartDate)!,
                 EndDate = TimeCalculator.DatePiker(x.EndDate)!,
                 Description = x.Description,
                 FareAmount = x.Price,
                 CurrencyUnitId = x.CurrencyUnitId,
                 StatusData = new((int)x.TransportationRequestStatus, x.TransportationRequestStatus.GetEnumDescription()),
                 ManagerDescription = x.ManagerDescription,
                 ConfrimUserId = x.ConfirmUserId,
                 ConfrimDate = x.ConfirmDate,
                 DestinationAddress = x.DestinationAddress,
                 SeasonId = x.Season != null ? x.Season.Id : null,
                 Season = x.Season != null ? x.Season.SeasonName : null,
                 BranchId = x.Season != null && x.Season.Branch != null ? x.Season.Branch.Id : null,
                 Branch = x.Season != null && x.Season.Branch != null ? x.Season.Branch.BranchName : null,
                 CategoryId = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.Id : null,
                 Category = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.CategoryName : null,
                 PaymentType = x.TransportationPaymentType,
                 PaymentDate = x.PaymentDate,
                 CompanyId = x.CompanyId,
                 CostCenters = x.TransportationRequestCostCenters.Select(z => new GetTransportationRequestCostCenter(z.Id, z.CostCenter.Id, z.CostCenter.CostCenterName)).ToList(),
                 Projects = x.TransportationRequestProjects.Select(z => new GetTransportationRequestProject(z.Id, z.Project.Id, z.Project.ProjectName)).ToList(),
                 ProjectOperations = x.TransportationRequestProjectOperations.Select(z => new GetSnapProjectOperation(z.Id, z.ProjectOperation.Id, z.ProjectOperation.OperationInfo.OperationInfoName)).ToList(),
                 ProjectOperationDetails = x.TransportationRequestProjectOperationDetails.Select(z => new GetSnapProjectOperationDetail(z.Id, z.ProjectOperationDetail.Id, z.ProjectOperationDetail.OperationLocation.PublicName)).ToList(),
                 Created = x.Created,
                 Documents = x.TransportationRequestDocuments.Select(z => new GetSnapDocumentByIdDocumentModel()
                 {
                     Id = z.Id,
                     Url = z.Url,
                 }).ToList(),
                 TransportationCostCategoryId = x.CostCategoryId,
                 TransportationCostGroupId = x.CostGroupId,
                 EndDateShamsi = TimeCalculator.ConvertToShamsi(x.EndDate),
                 StartDateShamsi = TimeCalculator.ConvertToShamsi(x.StartDate),
                 EndTime = x.EndDate.Value.TimeOfDay,
                 StartTime = x.StartDate.Value.TimeOfDay,
                 TransportationRequestStatus = x.TransportationRequestStatus,
                 DriverId = x.DriverId,
                 DriverName = x.DriverName,
                 CarSpecifications = x.CarSpecifications,
                 IsPassenger = x.Transportation.IsPassenger,
                 NumberPlates = x.NumberPlates,
                 PersonalPayment = x.PersonalPayment,
                 PhoneNumber = x.PhoneNumber,
                 RecipientName = x.RecipientName,
                 ReturnToStart = x.ReturnToStart,
                 SecondDestinationAddress = x.SecondDestinationAddress,
                 SecondDestinationCityId = x.SecondDestinationCityId,
                 SnapRequester = x.SnapRequester,
                 StartingCityAddress = x.StartingCityAddress,
                 StopRate = x.StopRate,
             });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetsTransportationRequestHistoryResponse> GetsTransportationRequestHistory(
        long id,
        CT ct)
    {
        var query = DbSet
             .Where(t => t.Id == id)
             .Select(t => new GetsTransportationRequestHistoryResponse()
             {
                 Id = t.Id,
                 RequestNumber = t.RequestNumber,
                 MachineTypeId = t.MachineType.Id,
                 MachineTypeName = t.MachineType.MachineTypeTitle,
                 TransportationId = t.Transportation.Id,
                 TransportationName = t.Transportation.TransportationName,
                 TripId = t.Trip.Id,
                 TripName = t.Trip.TripName,
                 Data = t.TransportationRequestHistories.Select(h => new GetsTransportationRequestHistoryResponseModel()
                 {
                     AccountDescription = h.AccountDescription,
                     AccountName = h.AccountName,
                     AccountNumber = h.AccountNumber,
                     BankId = h.BankId,
                     CardNumber = h.CardNumber,
                     ConfirmDate = h.ConfirmDate,
                     ConfirmUserId = h.ConfirmUserId,
                     Created = h.Created,
                     CreatorId = h.CreatorId,
                     CurrencyId = h.CurrencyUnitId,
                     Description = h.Description,
                     DriverId = h.DriverId,
                     DriverName = h.DriverName,
                     EndDate = h.EndDate,
                     StartDate = h.StartDate,
                     Id = h.Id,
                     IBAN = h.IBAN,
                     Price = h.Price,
                     ManagerDescription = h.ManagerDescription,
                     PaymentOrderId = h.PaymentOrderId,
                     PaymentDate = h.PaymentDate,
                     TransportationRequestStatus = h.TransportationRequestStatus,
                     RequestNumber = h.TransportationRequest.RequestNumber,
                     TransportationPaymentType = h.TransportationPaymentType,
                 }).OrderByDescending(h => h.Created).ToList()
             });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<TransportationRequest?> GetByIdForPayment(long id, CT ct)
    {
        var query = DbSet
            .Include(t => t.TransportationRequestDocuments)
            .Include(t => t.MachineType)
            .Include(t => t.BillOfLading)
            .Include(t => t.TransportationRequestProjects)
                .ThenInclude(t => t.Project.ProjectCostCenters)
                    .ThenInclude(t => t.CostCenter)
            .Include(t => t.TransportationRequestCostCenters)
                .ThenInclude(t => t.CostCenter.Projects)
            .Include(t => t.Season.Branch.Category)

             .Where(t => t.Id == id)
             .AsNoTracking();

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<List<TransportationRequest>?> GetByIdsForPayment(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(t => t.TransportationRequestDocuments)
            .Include(t => t.MachineType)
            .Include(t => t.BillOfLading)
            .Include(t => t.TransportationRequestProjects)
                .ThenInclude(t => t.Project.ProjectCostCenters)
                    .ThenInclude(t => t.CostCenter)
            .Include(t => t.TransportationRequestCostCenters)
                .ThenInclude(t => t.CostCenter.Projects)
            .Include(t => t.Season.Branch.Category)

             .Where(t => ids.Contains(t.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<TransportationRequest?> GetByIdForChangeStatus(long id, CT ct)
    {
        var query = DbSet

            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(x => x.PackingPallet)

            .Include(t => t.TransportationCargoPallets)
                .ThenInclude(x => x.TransportationCargo.Packing)
            .Include(t => t.Transportation)
            .Include(t => t.Trip)
            .Include(t => t.TransportationRequestDocuments)
            .Include(t => t.MachineType)
            .Include(t => t.CostCenter)
            .Include(t => t.Project)
                .ThenInclude(t => t.ProjectCostCenters)
                    .ThenInclude(t => t.CostCenter)
            .Include(t => t.BillOfLading)
            .Include(t => t.TransportationRequestProjects)
                .ThenInclude(t => t.Project)
            .Include(t => t.TransportationRequestCostCenters)
                .ThenInclude(t => t.CostCenter)
            .Include(t => t.Season.Branch.Category)

             .Where(t => t.Id == id)
             .AsNoTracking();

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<TransportationRequest?> FindForDelete(long id, CT ct)
    {
        var query = DbSet

             .Where(t => t.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<TransportationRequest?> GetSnapsWithRefrenceId(long refrenceId, CT ct)
    {
        var query = DbSet
             .Where(t => t.Id == refrenceId);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<GetsFilteredTransportationRequestResponseModel> Data, int RowCount)> GetsFilteredTransportationRequest(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        long? tripId,
        long? billOfLadingId,
        long? transportationId,
        long? requestById,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        long? requestNumber,
        decimal? fromPrice,
        decimal? toPrice,
        string? driverName,
        List<long>? driverIds,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(t => t.TransportationContractor == null &&
                (projectIds == null || t.TransportationRequestProjects.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || t.TransportationRequestCostCenters.Any(x => costCenterIds.Contains(x.CostCenter.Id))) &&
                (projectOperationIds == null || t.TransportationRequestProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (projectOperationDetailIds == null || t.TransportationRequestProjectOperationDetails.Any(p => projectOperationDetailIds.Contains(p.ProjectOperationDetail.Id))) &&
                (transportationRequestStatus == null || t.TransportationRequestStatus == transportationRequestStatus) &&
                (costGroupIds == null || (t.CostGroupId.HasValue && costGroupIds.Contains(t.CostGroupId.Value))) &&
                (costCategoryIds == null || (t.CostCategoryId.HasValue && costCategoryIds.Contains(t.CostCategoryId.Value))) &&
                (tripId == null || t.Trip.Id == tripId) &&
                (paymentType == null || t.TransportationPaymentType == paymentType) &&
                (billOfLadingId == null || t.BillOfLading.Id == billOfLadingId) &&
                (transportationId == null || t.Transportation.Id == transportationId) &&
                (requestById == null || t.CreatorId == requestById) &&
                (startDate == null || t.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || t.EndDate.Value.Date <= endDate.Value.Date) &&
                (fromDate == null || (t.PaymentDate.HasValue && t.PaymentDate.Value.Date >= fromDate.Value.Date)) &&
                (toDate == null || (t.PaymentDate.HasValue && t.PaymentDate.Value.Date <= toDate.Value.Date)) &&
                (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&
                (requestNumber == null || t.RequestNumber == requestNumber) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.RequestNumber.ToString(), filterData.MakeLikePattern())) &&
                (driverIds == null || driverIds.Count == 0 || (t.DriverId != null && driverIds.Contains(t.DriverId.Value))) &&
                (fromPrice == null || (t.Price != null && t.Price >= fromPrice)) &&
                (toPrice == null || (t.Price != null && t.Price <= toPrice)) &&
                (driverName == null || string.IsNullOrEmpty(driverName) ||
                EF.Functions.Like(t.DriverName.ToString(), driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.FirstName, driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.LastName.ToString(), driverName.MakeLikePattern())) &&
                t.IsCredit == false)
            .Select(x => new GetsFilteredTransportationRequestResponseModel()
            {
                Id = x.Id,
                RequestNumber = x.RequestNumber,
                CostCenterNames = string.Join(", ", x.TransportationRequestCostCenters.Select(z => z.CostCenter.CostCenterName) ?? null),
                ProjectNames = string.Join(", ", x.TransportationRequestProjects.Select(z => z.Project.ProjectName) ?? null),
                TransportationId = x.Transportation.Id,
                TransportationName = x.Transportation.TransportationName,
                IsPassenger = x.Transportation.IsPassenger,
                TripId = x.Trip.Id,
                TripName = x.Trip.TripName,
                BillOfLadingId = x.BillOfLading != null ? x.BillOfLading.Id : null,
                BillOfLadingName = x.BillOfLading != null ? x.BillOfLading.BillOfLadingName : null,
                RequestById = x.CreatorId,
                StartingCityId = x.StartingCityId,
                DestinationCityId = x.DestinationCityId,
                StartDate = TimeCalculator.ConvertToShamsi(x.StartDate)!,
                EndDate = TimeCalculator.ConvertToShamsi(x.EndDate)!,
                Description = x.Description,
                DriverId = x.DriverId,
                CardNumber = x.CardNumber,
                AccountName = x.AccountName,
                IBAN = x.IBAN,
                Price = x.Price,
                CurrencyUnitId = x.CurrencyUnitId,
                CarID = x.CarID,
                ManagerDescription = x.ManagerDescription,
                StartingCityAddress = x.StartingCityAddress,
                DestinationAddress = x.DestinationAddress,
                PaymentType = x.TransportationPaymentType,
                PaymentDate = x.PaymentDate,
                CompanyId = x.CompanyId,
                CostCenters = x.TransportationRequestCostCenters.Select(z => new GetTransportationRequestCostCenter(z.Id, z.CostCenter.Id, z.CostCenter.CostCenterName)).ToList(),
                Projects = x.TransportationRequestProjects.Select(z => new GetTransportationRequestProject(z.Id, z.Project.Id, z.Project.ProjectName)).ToList(),
                ProjectOperations = x.TransportationRequestProjectOperations.Select(z => new GetTransportationRequestProjectOperation(z.Id, z.ProjectOperation.Id, z.ProjectOperation.OperationInfo.OperationInfoName)).ToList(),
                ProjectOperationDetails = x.TransportationRequestProjectOperationDetails.Select(z => new GetTransportationRequestProjectOperationDetail(z.Id, z.ProjectOperationDetail.Id, z.ProjectOperationDetail.OperationLocation.PublicName)).ToList(),
                Created = x.Created,
                TicketPayerId = x.TicketPayerId,
                DriverName = x.DriverName,
                TransportationCostCategoryId = x.CostCategoryId,
                TransportationCostGroupId = x.CostGroupId,
                ConfirmDate = x.ConfirmDate,
                ConfirmUserId = x.ConfirmUserId,
                TransportationRequestStatus = x.TransportationRequestStatus,
                TransportationContractorId = x.TransportationContractor.Id,
                Title = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Title).FirstOrDefault(),
                Address = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.AddressText).FirstOrDefault(),
                AddressId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.Id).FirstOrDefault(),
                CityId = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Id).FirstOrDefault(),
                City = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.City.Name).FirstOrDefault(),
                PostalCode = x.TransportationContractor.ThirdParty.Addresses.Select(x => x.PostalCode).FirstOrDefault(),
                ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                ContractorPhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                MainName = $"{x.TransportationContractor.ThirdParty.Legal.CompanyName}-{x.TransportationContractor.ThirdParty.FirstName} {x.TransportationContractor.ThirdParty.LastName}",
                Warehouses = x.TransportationCargoPallets.Select(z => new GetFilteredRequestWarehousesModel()
                {
                    Id = z.Id,
                    PackingId = z.TransportationCargo.Packing.Id,
                    PackingNumber = z.TransportationCargo.PackingNumber,
                    Price = z.Price,
                    ShippingCostId = z.ShippingCostId,
                    ShippingCostPrice = z.ShippingCost.Price,
                    ThirdPartyId = z.TransportationCargo.ThirdPartyId,
                    ThirdParty = $"{z.TransportationCargo.ThirdParty.FirstName} {z.TransportationCargo.ThirdParty.LastName}",
                    ThirdPartyName = z.TransportationCargo.ThirdPartyName,
                    WarehouseId = z.PackingDestinationAddress.WarehouseId,
                    WarehouseName = z.PackingDestinationAddress.Warehouse.Name,
                    WarehouseCode = z.PackingDestinationAddress.Warehouse.Code
                }).ToList()
            });

        query = query.OrderByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsFilteredSnapResponseModel> Data, int RowCount)> GetsFilteredSnap(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         string[]? orderBy,
         int pageIndex,
         int pageSize, CT ct)
    {
        var query = DbSet
            .Where(t => t.TransportationContractor == null &&
                t.Transportation.TransportationType == TransportationType.SnappPassenger &&
                t.IsCredit == true &&
                (tripIds == null || tripIds.Contains(t.Trip.Id)) &&
                (transportationIds == null || transportationIds.Contains(t.Transportation.Id)) &&
                (projectIds == null || t.TransportationRequestProjects.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || t.TransportationRequestCostCenters.Any(x => costCenterIds.Contains(x.CostCenter.Id))) &&
                (projectOperationIds == null || t.TransportationRequestProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (projectOperationDetailIds == null || t.TransportationRequestProjectOperationDetails.Any(p => projectOperationDetailIds.Contains(p.ProjectOperationDetail.Id))) &&
                (transportationRequestStatus == null || t.TransportationRequestStatus == transportationRequestStatus) &&
                (costGroupIds == null || (t.CostGroupId.HasValue && costGroupIds.Contains(t.CostGroupId.Value))) &&
                (costCategoryIds == null || (t.CostCategoryId.HasValue && costCategoryIds.Contains(t.CostCategoryId.Value))) &&
                (startDate == null || t.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || t.EndDate.Value.Date <= endDate.Value.Date) &&
                (fromCreateDate == null || t.Created.Date >= fromCreateDate.Value.Date) &&
                (toCreateDate == null || t.Created.Date <= toCreateDate.Value.Date) &&
                (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&
                (passengerIds == null || passengerIds.Count == 0 || (t.SnapRequester.HasValue && passengerIds.Contains(t.SnapRequester.Value))) &&
                (requestNumber == null || t.RequestNumber == requestNumber) &&
                (paymentType == null || t.TransportationPaymentType == paymentType) &&
                (requestById == null || t.CreatorId == requestById) &&
                (fromPrice == null || (t.Price != null && t.Price >= fromPrice)) &&
                (toPrice == null || (t.Price != null && t.Price <= toPrice)) &&
                (driverName == null || string.IsNullOrEmpty(driverName) ||
                EF.Functions.Like(t.DriverName.ToString(), driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.FirstName, driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.LastName.ToString(), driverName.MakeLikePattern())) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.RequestNumber.ToString(), filterData.MakeLikePattern()))
                 )
            .Select(x => new GetsFilteredSnapResponseModel()
            {
                Id = x.Id,
                RequestNumber = x.RequestNumber,
                CostCenterNames = string.Join(", ", x.TransportationRequestCostCenters.Select(z => z.CostCenter.CostCenterName) ?? null),
                ProjectNames = string.Join(", ", x.TransportationRequestProjects.Select(z => z.Project.ProjectName) ?? null),
                TransportationId = x.Transportation.Id,
                TransportationName = x.Transportation.TransportationName,
                IsPassenger = x.Transportation.IsPassenger,
                TripId = x.Trip.Id,
                TripName = x.Trip.TripName,
                CreatorId = x.CreatorId,
                StartingCityId = x.StartingCityId,
                DestinationCityId = x.DestinationCityId,
                StartDate = x.StartDate!,
                EndDate = x.EndDate!,
                Description = x.Description,
                DriverId = x.DriverId,
                FareAmount = x.Price,
                CurrencyUnitId = x.CurrencyUnitId,
                ManagerDescription = x.ManagerDescription,
                StartingCityAddress = x.StartingCityAddress,
                DestinationAddress = x.DestinationAddress,
                PaymentType = x.TransportationPaymentType,
                PaymentDate = x.PaymentDate,
                CompanyId = x.CompanyId,
                CostCenters = x.TransportationRequestCostCenters.Select(z => new GetTransportationRequestCostCenter(z.Id, z.CostCenter.Id, z.CostCenter.CostCenterName)).ToList(),
                Projects = x.TransportationRequestProjects.Select(z => new GetTransportationRequestProject(z.Id, z.Project.Id, z.Project.ProjectName)).ToList(),
                ProjectOperations = x.TransportationRequestProjectOperations.Select(z => new GetSnapProjectOperation(z.Id, z.ProjectOperation.Id, z.ProjectOperation.OperationInfo.OperationInfoName)).ToList(),
                ProjectOperationDetails = x.TransportationRequestProjectOperationDetails.Select(z => new GetSnapProjectOperationDetail(z.Id, z.ProjectOperationDetail.Id, z.ProjectOperationDetail.OperationLocation.PublicName)).ToList(),
                Created = x.Created,
                DriverName = x.DriverName,
                TransportationCostCategoryId = x.CostCategoryId,
                TransportationCostGroupId = x.CostGroupId,
                ConfrimDate = x.ConfirmDate,
                ConfrimUserId = x.ConfirmUserId,
                TransportationRequestStatus = x.TransportationRequestStatus,
                CarSpecifications = x.CarSpecifications,
                Documents = x.TransportationRequestDocuments.Select(x => new GetSnapDocumentByIdDocumentModel()
                {
                    Id = x.Id,
                    Url = x.Url,
                }).ToList(),
                EndDateShamsi = TimeCalculator.ConvertToShamsi(x.EndDate),
                StartDateShamsi = TimeCalculator.ConvertToShamsi(x.StartDate),
                NumberPlates = x.NumberPlates,
                PersonalPayment = x.PersonalPayment,
                PhoneNumber = x.PhoneNumber,
                RecipientName = x.RecipientName,
                ReturnToStart = x.ReturnToStart,
                SecondDestinationAddress = x.SecondDestinationAddress,
                StopRate = x.StopRate,
                SecondDestinationCityId = x.SecondDestinationCityId,
                SnapRequester = x.SnapRequester,
                StatusData = new((int)x.TransportationRequestStatus, x.TransportationRequestStatus.GetEnumDescription()),
            });

        query = query.OrderByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsFilteredAirplaneResponseModel> Data, int RowCount)> GetsFilteredAirplane(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         string[]? orderBy,
         int pageIndex,
         int pageSize,
         CT ct)
    {
        var query = DbSet

            .Where(t => t.TransportationContractor == null &&
                t.Transportation.TransportationType == TransportationType.Airplane &&
                t.IsCredit == false &&
                (tripIds == null || tripIds.Contains(t.Trip.Id)) &&
                (transportationIds == null || transportationIds.Contains(t.Transportation.Id)) &&
                (projectIds == null || t.TransportationRequestProjects.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || t.TransportationRequestCostCenters.Any(x => costCenterIds.Contains(x.CostCenter.Id))) &&
                (projectOperationIds == null || t.TransportationRequestProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (projectOperationDetailIds == null || t.TransportationRequestProjectOperationDetails.Any(p => projectOperationDetailIds.Contains(p.ProjectOperationDetail.Id))) &&
                (transportationRequestStatus == null || t.TransportationRequestStatus == transportationRequestStatus) &&
                (costGroupIds == null || (t.CostGroupId.HasValue && costGroupIds.Contains(t.CostGroupId.Value))) &&
                (costCategoryIds == null || (t.CostCategoryId.HasValue && costCategoryIds.Contains(t.CostCategoryId.Value))) &&
                (startDate == null || t.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || t.EndDate.Value.Date <= endDate.Value.Date) &&
                (fromCreateDate == null || t.Created.Date >= fromCreateDate.Value.Date) &&
                (toCreateDate == null || t.Created.Date <= toCreateDate.Value.Date) &&
                (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&
                (passengerIds == null || passengerIds.Count == 0 || passengerIds.Contains(t.PassengerId.Value)) &&
                (paymentType == null || t.TransportationPaymentType == paymentType) &&
                (requestNumber == null || t.RequestNumber == requestNumber) &&
                (requestById == null || t.CreatorId == requestById) &&
                (fromPrice == null || (t.Price != null && t.Price >= fromPrice)) &&
                (toPrice == null || (t.Price != null && t.Price <= toPrice)) &&
                (driverName == null || string.IsNullOrEmpty(driverName) ||
                EF.Functions.Like(t.DriverName.ToString(), driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.FirstName, driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.LastName.ToString(), driverName.MakeLikePattern())) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.RequestNumber.ToString(), filterData.MakeLikePattern()))
                 ).Select(x => new GetsFilteredAirplaneResponseModel()
                 {
                     Id = x.Id,
                     RequestNumber = x.RequestNumber,
                     CostCenterNames = string.Join(", ", x.TransportationRequestCostCenters.Select(z => z.CostCenter.CostCenterName) ?? null),
                     ProjectNames = string.Join(", ", x.TransportationRequestProjects.Select(z => z.Project.ProjectName) ?? null),
                     TransportationId = x.Transportation.Id,
                     TransportationName = x.Transportation.TransportationName,
                     TripId = x.Trip.Id,
                     TripName = x.Trip.TripName,
                     CreatorId = x.CreatorId,
                     StartingCityId = x.StartingCityId,
                     DestinationCityId = x.DestinationCityId,
                     StartDate = x.StartDate,
                     EndDate = x.EndDate,
                     Description = x.Description,
                     AccountNumber = x.AccountNumber,
                     BankId = x.BankId,
                     CardNumber = x.CardNumber,
                     AccountName = x.AccountName,
                     Iban = x.IBAN,
                     FareAmount = x.Price,
                     CurrencyUnitId = x.CurrencyUnitId,
                     StatusData = new((int)x.TransportationRequestStatus, x.TransportationRequestStatus.GetEnumDescription()),
                     ManagerDescription = x.ManagerDescription,
                     ConfrimUserId = x.ConfirmUserId,
                     ConfrimDate = x.ConfirmDate,
                     DestinationAddress = x.DestinationAddress,
                     SeasonId = x.Season != null ? x.Season.Id : null,
                     Season = x.Season != null ? x.Season.SeasonName : null,
                     BranchId = x.Season != null && x.Season.Branch != null ? x.Season.Branch.Id : null,
                     Branch = x.Season != null && x.Season.Branch != null ? x.Season.Branch.BranchName : null,
                     CategoryId = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.Id : null,
                     Category = x.Season != null && x.Season.Branch != null && x.Season.Branch.Category != null ? x.Season.Branch.Category.CategoryName : null,
                     PaymentType = x.TransportationPaymentType,
                     PaymentDate = x.PaymentDate,
                     CompanyId = x.CompanyId,
                     CostCenters = x.TransportationRequestCostCenters.Select(z => new GetTransportationRequestCostCenter(z.Id, z.CostCenter.Id, z.CostCenter.CostCenterName)).ToList(),
                     Projects = x.TransportationRequestProjects.Select(z => new GetTransportationRequestProject(z.Id, z.Project.Id, z.Project.ProjectName)).ToList(),
                     ProjectOperations = x.TransportationRequestProjectOperations.Select(z => new GetAirplaneProjectOperation(z.Id, z.ProjectOperation.Id, z.ProjectOperation.OperationInfo.OperationInfoName)).ToList(),
                     ProjectOperationDetails = x.TransportationRequestProjectOperationDetails.Select(z => new GetAirplaneProjectOperationDetail(z.Id, z.ProjectOperationDetail.Id, z.ProjectOperationDetail.OperationLocation.PublicName)).ToList(),
                     Created = x.Created,
                     Documents = x.TransportationRequestDocuments.Select(z => new GetAirplaneDocumentByIdDocumentModel()
                     {
                         Id = z.Id,
                         Url = z.Url,
                     }).ToList(),
                     TicketPayerId = x.TicketPayerId,
                     TransportationCostCategoryId = x.CostCategoryId,
                     TransportationCostGroupId = x.CostGroupId,
                     EndDateShamsi = TimeCalculator.ConvertToShamsi(x.EndDate),
                     StartDateShamsi = TimeCalculator.ConvertToShamsi(x.StartDate),
                     PassengerId = x.PassengerId,
                     Passenger = x.Passenger,
                     TransportationRequestStatus = x.TransportationRequestStatus,
                     TransportationType = x.Transportation.TransportationType,
                 });

        query = query.OrderByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<TransportationRequest>> GetsByRequestIdAsync(
        long requestById,
        DateTime startDate,
        DateTime endDate,
        TransportationRequestStatus status,
        CT ct)
    {
        var query = DbSet.Include(t => t.Transportation)
                         .Include(t => t.Trip)
                         .Include(t => t.TransportationRequestProjectOperations)
                            .ThenInclude(t => t.ProjectOperation)
                                .ThenInclude(t => t.OperationInfo)

                         .Where(t => t.IsCredit == false &&
                                     t.CreatorId == requestById &&
                                     t.StartDate.Value.Date >= startDate.Date &&
                                     t.EndDate.Value.Date <= endDate.Date &&
                                     t.TransportationRequestStatus == status);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredRequester(CT ct)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted)
            .Select(c => c.CreatorId).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }


    public async Task<GetsTotalTransportationRequestPriceResponse> GetsTotalTransportationRequestPrice(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        long? tripId,
        long? billOfLadingId,
        long? transportationId,
        long? requestById,
        DateTime? startDate,
        DateTime? endDate,
        long? requestNumber,
        decimal? fromPrice,
        decimal? toPrice,
        string? driverName,
        List<long>? driverIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var data = new GetsTotalTransportationRequestPriceResponse();
        var query = await DbSet
            .Where(t => t.TransportationContractor == null &&
                (projectIds == null || t.TransportationRequestProjects.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || t.TransportationRequestCostCenters.Any(x => costCenterIds.Contains(x.CostCenter.Id))) &&
                (projectOperationIds == null || t.TransportationRequestProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (projectOperationDetailIds == null || t.TransportationRequestProjectOperationDetails.Any(p => projectOperationDetailIds.Contains(p.ProjectOperationDetail.Id))) &&
                (transportationRequestStatus == null || t.TransportationRequestStatus == transportationRequestStatus) &&
                (costGroupIds == null || (t.CostGroupId.HasValue && costGroupIds.Contains(t.CostGroupId.Value))) &&
                (costCategoryIds == null || (t.CostCategoryId.HasValue && costCategoryIds.Contains(t.CostCategoryId.Value))) &&
                (tripId == null || t.Trip.Id == tripId) &&
                (paymentType == null || t.TransportationPaymentType == paymentType) &&
                (billOfLadingId == null || t.BillOfLading.Id == billOfLadingId) &&
                (transportationId == null || t.Transportation.Id == transportationId) &&
                (requestById == null || t.CreatorId == requestById) &&
                (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&
                (startDate == null || t.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || t.EndDate.Value.Date <= endDate.Value.Date) &&
                (fromPrice == null || (t.Price != null && t.Price >= fromPrice)) &&
                (toPrice == null || (t.Price != null && t.Price <= toPrice)) &&
                (driverIds == null || driverIds.Count == 0 || (t.DriverId != null && driverIds.Contains(t.DriverId.Value))) &&
                (driverName == null || string.IsNullOrEmpty(driverName) ||
                EF.Functions.Like(t.DriverName.ToString(), driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.FirstName, driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.LastName.ToString(), driverName.MakeLikePattern())) &&
                (requestNumber == null || t.Id == requestNumber) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.Id.ToString(), filterData.MakeLikePattern())) &&
                t.IsCredit == false
                 )
            .Select(x => x.Price).Where(x => x != null && x > 0).ToListAsync(ct);

        data.TotalPrice = query?.Sum(x => x) ?? 0;
        return data;
    }

    public async Task<GetsTotalSnapPriceResponse> GetsTotalSnapPrice(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         int pageIndex,
         int pageSize,
         CT ct)
    {
        var data = new GetsTotalSnapPriceResponse();
        var query = await DbSet

            .Where(t => t.TransportationContractor == null &&
                t.Transportation.TransportationType == TransportationType.SnappPassenger &&
                t.IsCredit == true &&
                (tripIds == null || tripIds.Contains(t.Trip.Id)) &&
                (transportationIds == null || transportationIds.Contains(t.Transportation.Id)) &&
                (projectIds == null || t.TransportationRequestProjects.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || t.TransportationRequestCostCenters.Any(x => costCenterIds.Contains(x.CostCenter.Id))) &&
                (projectOperationIds == null || t.TransportationRequestProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (projectOperationDetailIds == null || t.TransportationRequestProjectOperationDetails.Any(p => projectOperationDetailIds.Contains(p.ProjectOperationDetail.Id))) &&
                (transportationRequestStatus == null || t.TransportationRequestStatus == transportationRequestStatus) &&
                (costGroupIds == null || (t.CostGroupId.HasValue && costGroupIds.Contains(t.CostGroupId.Value))) &&
                (costCategoryIds == null || (t.CostCategoryId.HasValue && costCategoryIds.Contains(t.CostCategoryId.Value))) &&
                (startDate == null || t.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || t.EndDate.Value.Date <= endDate.Value.Date) &&
                (paymentType == null || t.TransportationPaymentType == paymentType) &&
                (fromCreateDate == null || t.Created.Date >= fromCreateDate.Value.Date) &&
                (toCreateDate == null || t.Created.Date <= toCreateDate.Value.Date) &&
                (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&
                (passengerIds == null || passengerIds.Count == 0 || (t.SnapRequester.HasValue && passengerIds.Contains(t.SnapRequester.Value))) &&
                (requestNumber == null || t.Id == requestNumber) &&
                (requestById == null || t.CreatorId == requestById) &&
                (fromPrice == null || (t.Price != null && t.Price >= fromPrice)) &&
                (toPrice == null || (t.Price != null && t.Price <= toPrice)) &&
                (driverName == null || string.IsNullOrEmpty(driverName) ||
                EF.Functions.Like(t.DriverName.ToString(), driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.FirstName, driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.LastName.ToString(), driverName.MakeLikePattern())) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.Id.ToString(), filterData.MakeLikePattern()))
                 )
            .Select(x => x.Price).Where(x => x != null && x > 0).ToListAsync(ct);

        data.TotalPrice = query?.Sum(x => x) ?? 0;
        return data;
    }

    public async Task<GetsTotalAirplanePriceResponse> GetsTotalAirplanePrice(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         int pageIndex,
         int pageSize,
         CT ct)
    {
        var data = new GetsTotalAirplanePriceResponse();
        var query = await DbSet
            .Where(t => t.TransportationContractor == null &&
                t.Transportation.TransportationType == TransportationType.Airplane &&
                t.IsCredit == false &&
                (tripIds == null || tripIds.Contains(t.Trip.Id)) &&
                (transportationIds == null || transportationIds.Contains(t.Transportation.Id)) &&
                (projectIds == null || t.TransportationRequestProjects.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || t.TransportationRequestCostCenters.Any(x => costCenterIds.Contains(x.CostCenter.Id))) &&
                (projectOperationIds == null || t.TransportationRequestProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (projectOperationDetailIds == null || t.TransportationRequestProjectOperationDetails.Any(p => projectOperationDetailIds.Contains(p.ProjectOperationDetail.Id))) &&
                (transportationRequestStatus == null || t.TransportationRequestStatus == transportationRequestStatus) &&
                (costGroupIds == null || (t.CostGroupId.HasValue && costGroupIds.Contains(t.CostGroupId.Value))) &&
                (costCategoryIds == null || (t.CostCategoryId.HasValue && costCategoryIds.Contains(t.CostCategoryId.Value))) &&
                (startDate == null || t.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || t.EndDate.Value.Date <= endDate.Value.Date) &&
                (paymentType == null || t.TransportationPaymentType == paymentType) &&
                (fromCreateDate == null || t.Created.Date >= fromCreateDate.Value.Date) &&
                (toCreateDate == null || t.Created.Date <= toCreateDate.Value.Date) &&
                (ids == null || ids.Count == 0 || ids.Contains(t.Id)) &&
                (passengerIds == null || passengerIds.Count == 0 || (t.PassengerId.HasValue && passengerIds.Contains(t.PassengerId.Value))) &&
                (requestNumber == null || t.Id == requestNumber) &&
                (requestById == null || t.CreatorId == requestById) &&
                (fromPrice == null || (t.Price != null && t.Price >= fromPrice)) &&
                (toPrice == null || (t.Price != null && t.Price <= toPrice)) &&
                (driverName == null || string.IsNullOrEmpty(driverName) ||
                EF.Functions.Like(t.DriverName.ToString(), driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.FirstName, driverName.MakeLikePattern()) ||
                EF.Functions.Like(t.Driver.LastName.ToString(), driverName.MakeLikePattern())) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.Id.ToString(), filterData.MakeLikePattern()))
                 )
            .Select(x => x.Price).Where(x => x != null && x > 0).ToListAsync(ct);

        data.TotalPrice = query?.Sum(x => x) ?? 0;
        return data;
    }

    public async Task<List<TransportationRequest>?> GetByIds(List<long> ids, CT ct)
    {
        return await DbSet
            .Include(x => x.TransportationContractor)
                .ThenInclude(x => x.TransportationContractorInsurances)
            .Include(x => x.TransportationContractor)
            .Include(x => x.MachineType)
            .Include(x => x.TransportationCargoPallets)
            .Include(x => x.TransportationRequestCostCenters)
                .ThenInclude(x => x.CostCenter)
            .Include(x => x.TransportationRequestProjects)
                .ThenInclude(x => x.Project)
            .Where(z => ids.Contains(z.Id))
            .ToListAsync(ct);
    }

    public async Task<int> GetCountOfYearRequest(CT ct)
    {
        return await DbSet
        .Where(x => x.Created.Year == DateTime.UtcNow.Year)
        .CountAsync(ct);
    }

    public async Task<string?> GetLastFreightNumber(long contractorId, CT ct)
    {
        var query = DbSet
            .Include(x => x.TransportationContractor)
        .Where(x => x.TransportationContractorId == contractorId && !string.IsNullOrEmpty(x.FreightNumber));

        query = query.OrderByDescending(x => x.Created);

        return await query.Select(x => x.FreightNumber).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetsWarehouseTransportationResponseModel> Data, int RowCount)> GetsWarehouseTransportation(
        List<long>? ids,
        long? contractorId,
        List<TransportationRequestStatus>? transportationRequestStatus,
        long? transportationId,
        long? requestById,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
           .Where(t => t.TransportationContractor != null &&
                t.IsCredit == false &&
                (ids == null || ids.Contains(t.Id)) &&
                (contractorId == null || t.TransportationContractor.Id == contractorId) &&
                (transportationId == null || t.Transportation.Id == transportationId) &&
                (transportationRequestStatus == null || transportationRequestStatus.Contains(t.TransportationRequestStatus)) &&
                (fromDate == null || t.PostageDate.Value.Date >= fromDate.Value.Date) &&
                (toDate == null || t.PostageDate.Value.Date <= toDate.Value.Date) &&
                (requestById == null || t.CreatorId == requestById) &&
                (string.IsNullOrEmpty(filterData) ||
                 EF.Functions.Like(t.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
                 t.TransportationCargoPallets.Any(w => EF.Functions.Like(w.TransportationCargo.Packing.RequestNumber.ToString(), filterData.MakeLikePattern()))))
           .Select(x => new GetsWarehouseTransportationResponseModel()
           {
               Id = x.Id,
               RequestNumber = x.RequestNumber,
               TransportationId = x.Transportation.Id,
               TransportationName = x.Transportation.TransportationName,
               ContractorPhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
               CostCenters = x.TransportationRequestCostCenters.Select(z => new GetWarehouseCostCentersModel
               {
                   TransportationCostCenterId = z.Id,
                   CostCenterId = z.CostCenter.Id,
                   CostCenterCode = z.CostCenter.CostCenterCode,
                   CostCenterName = z.CostCenter.CostCenterName,
               }).ToList(),
               Projects = x.TransportationRequestProjects.Select(z => new GetWarehouseProjectsModel
               {
                   TransportationProjectId = z.Id,
                   ProjectId = z.Project.Id,
                   ProjectCode = z.Project.ProjectCode,
                   ProjectName = z.Project.ProjectName,
               }).ToList(),
               Created = x.Created,
               CreatorId = x.CreatorId,
               Description = x.Description,
               DestinationAddress = x.DestinationAddress,
               DestinationCityId = x.DestinationCityId,
               DestinationCityName = x.DestinationCity.Name,
               DriverId = x.DriverId,
               DriverName = x.Driver.FirstName + " " + x.Driver.LastName,
               FreightNumber = x.FreightNumber,
               MachineType = x.MachineType.MachineTypeTitle,
               MachineTypeId = x.MachineType.Id,
               MainName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
               NumberPlate = x.NumberPlates,
               StartingCityAddress = x.StartingCityAddress,
               StartingCityId = x.StartingCityId,
               StartingCityName = x.StartingCity.Name,
               TransportationContractorId = x.TransportationContractor.Id,
               Status = x.TransportationRequestStatus,
               ThirdPartyId = x.TransportationContractor.ThirdPartyId,
               TransferPrice = x.Price,
               IsAggregate = x.IsAggregate,
               PostageDate = x.PostageDate,
               CertificateNumber = x.CertificateNumber,
               DeliveryMethod = x.DeliveryMethod,
               LoadWeight = x.LoadWeight,
               Volume = x.Volume,
               DeliveryType = x.DeliveryType,
               PackingData = x.TransportationCargoPallets.Where(x => x.Quantity > 0).Select(z => new GetTransportWarehouseModel()
               {
                   Id = z.Id,
                   RequestNumber = x.RequestNumber,
                   PackingId = z.TransportationCargo.PackingId,
                   Channel = null,
                   DeliveryDate = z.TransportationCargo.Packing.DeliveryDate,
                   ExitInvoice = null,
                   PalletNumber = z.PalletNumber,
                   PackingNumber = z.TransportationCargo.PackingNumber,
                   Price = z.Price,
                   Quantity = z.Quantity,

               }).ToList(),
           });

        var packingIds = query.SelectMany(x => x.PackingData).Select(x => x.PackingId).ToList();
        var pallets = await DbContext.Set<ViewPackingPallet>()
            .Where(pp => packingIds.Contains(pp.PackingId))
            .Select(pp => new { pp.PackingId, pp.Number, pp.PackagingSpec.Title })
            .ToListAsync(ct);

        foreach (var item in query)
        {
            foreach (var packingItem in item.PackingData)
            {
                if (packingItem.PackingId.HasValue)
                {
                    packingItem.PackagingSpec = pallets
                        .FirstOrDefault(p => p.PackingId == packingItem.PackingId && p.Number == packingItem.PalletNumber)?.Title;
                }
            }
        }

        query = query.OrderByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsAggregateWarehouseTransportationResponseModel> Data, int RowCount)> GetsAggregateWarehouseTransportation(
        List<long>? ids,
        List<long>? productIds,
        long? contractorId,
        List<TransportationRequestStatus>? transportationRequestStatus,
        long? transportationId,
        long? machineTypeId,
        long? requestById,
        DateTime? fromDate,
        DateTime? toDate,
        string? freightNumber,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
           .Where(t => t.TransportationContractor != null &&
                t.IsCredit == false &&
                (ids == null || ids.Contains(t.Id)) &&
                (productIds == null || t.TransportationCargoPallets.Any(x => x.TransportationRequestWarehouses
                .Any(z => productIds.Contains(z.PackingProduct.ProductId)))) &&
                (contractorId == null || t.TransportationContractor.Id == contractorId) &&
                (machineTypeId == null || t.MachineType.Id == machineTypeId) &&
                (transportationId == null || t.Transportation.Id == transportationId) &&
                (transportationRequestStatus == null || transportationRequestStatus.Contains(t.TransportationRequestStatus)) &&
                (fromDate == null || t.PostageDate.Value.Date >= fromDate.Value.Date) &&
                (toDate == null || t.PostageDate.Value.Date <= toDate.Value.Date) &&
                (requestById == null || t.CreatorId == requestById) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(t.RequestNumber.ToString(), filterData.MakeLikePattern())) &&
                (freightNumber == null || string.IsNullOrEmpty(freightNumber) || EF.Functions.Like(t.FreightNumber, freightNumber.MakeLikePattern()))
                ).Select(x => new GetsAggregateWarehouseTransportationResponseModel()
                {
                    Id = x.Id,
                    RequestNumber = x.RequestNumber,
                    ContractorPhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
                    Created = x.Created,
                    Volume = x.Volume,
                    CreatorId = x.CreatorId,
                    Description = x.Description,
                    DestinationAddress = x.DestinationAddress,
                    DestinationCityId = x.DestinationCityId,
                    DestinationCityName = x.DestinationCity.Name,
                    DriverId = x.DriverId,
                    DriverFullName = x.Driver != null ? x.Driver.FirstName + " " + x.Driver.LastName : null,
                    Driver = x.DriverName,
                    FreightNumber = x.FreightNumber,
                    MachineType = x.MachineType.MachineTypeTitle,
                    MachineTypeId = x.MachineType.Id,
                    MainName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
                    NumberPlate = x.NumberPlates,
                    StartingCityAddress = x.StartingCityAddress,
                    StartingCityId = x.StartingCityId,
                    StartingCityName = x.StartingCity.Name,
                    TransportationContractorId = x.TransportationContractor.Id,
                    Status = x.TransportationRequestStatus,
                    ThirdPartyId = x.TransportationContractor.ThirdPartyId,
                    TransferPrice = x.Price,
                    PostageDate = x.PostageDate,
                    CalculateType = x.TransportationContractor.Type,
                    CertificateNumber = x.CertificateNumber,
                    DeliveryMethod = x.DeliveryMethod,
                    LoadWeight = x.LoadWeight,
                    DeliveryType = x.DeliveryType,
                    GearBoxNumber = string.Empty,
                    MainAddress = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().AddressText,
                    MainCode = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
                    DriverPhoneNumber = x.Driver.DefaultPhoneNo,
                    CarSpec = x.CarSpecifications,
                    Documents = x.TransportationRequestDocuments.Select(z => new GetFilteredAggregateDocuments()
                    {
                        Id = z.Id,
                        IsBill = z.IsBill,
                        Url = z.Url,
                    }).ToList(),
                    ExtraInfo = x.TransportationRequestDetails.OrderByDescending(x => x.Id).Select(z => new GetAggregateTransportWarehouseDetailModel
                    {
                        ClassifiedFreightNumber = z.ClassifiedFreightNumber,
                        GlobalFreightNumber = z.GlobalFreightNumber,
                        Id = z.Id,
                        InsuranceNumber = z.InsuranceNumber,
                        InsurancePrice = z.InsurancePrice,
                        OrderNumber = z.OrderNumber,
                        OutofRange = z.OutofRange,
                        ProductTotalPrice = z.ProductTotalPrice,
                        ServicePrice = z.ServicePrice,
                        ShippingCost = z.ShippingCost,
                        Tax = z.Tax,
                        TransferPrice = z.TransferPrice,
                    }).FirstOrDefault(),
                });

        query = query.OrderByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<GetsAggregateWarehouseTransportationByIdResponse> GetsAggregateWarehouseTransportationById(
        long id,
        CT ct)
    {
        var query = DbSet
           .Where(t => t.TransportationContractor != null && t.Id == id)
           .Select(x => new GetsAggregateWarehouseTransportationByIdResponse()
           {
               Id = x.Id,
               RequestNumber = x.RequestNumber,
               ContractorPhoneNumber = x.TransportationContractor.ThirdParty.DefaultPhoneNo,
               Created = x.Created,
               Volume = x.Volume,
               CreatorId = x.CreatorId,
               Description = x.Description,
               DestinationAddress = x.DestinationAddress,
               DestinationCityId = x.DestinationCityId,
               DestinationCityName = x.DestinationCity.Name,
               DriverId = x.DriverId,
               DriverFullName = x.Driver != null ? x.Driver.FirstName + " " + x.Driver.LastName : null,
               Driver = x.DriverName,
               FreightNumber = x.FreightNumber,
               MachineType = x.MachineType.MachineTypeTitle,
               MachineTypeId = x.MachineType.Id,
               MainName = x.TransportationContractor.ThirdParty.Legal.CompanyName,
               NumberPlate = x.NumberPlates,
               StartingCityAddress = x.StartingCityAddress,
               StartingCityId = x.StartingCityId,
               StartingCityName = x.StartingCity.Name,
               TransportationContractorId = x.TransportationContractor.Id,
               Status = x.TransportationRequestStatus,
               ThirdPartyId = x.TransportationContractor.ThirdPartyId,
               TransferPrice = x.Price,
               PostageDate = x.PostageDate,
               CalculateType = x.TransportationContractor.Type,
               CertificateNumber = x.CertificateNumber,
               DeliveryMethod = x.DeliveryMethod,
               LoadWeight = x.LoadWeight,
               DeliveryType = x.DeliveryType,
               GearBoxNumber = string.Empty,
               MainAddress = x.TransportationContractor.ThirdParty.Addresses.FirstOrDefault().AddressText,
               MainCode = x.TransportationContractor.ThirdParty.Legal.RegistrationNo,
               DriverPhoneNumber = x.Driver.DefaultPhoneNo,
               CarSpec = x.CarSpecifications,
               Documents = x.TransportationRequestDocuments.Select(z => new GetByIdAggregateDocuments()
               {
                   Id = z.Id,
                   IsBill = z.IsBill,
                   Url = z.Url,
               }).ToList(),
               ExtraInfo = x.TransportationRequestDetails.OrderByDescending(x => x.Id).Select(z => new GetAggregateTransportWarehouseDetailByIdModel
               {
                   ClassifiedFreightNumber = z.ClassifiedFreightNumber,
                   GlobalFreightNumber = z.GlobalFreightNumber,
                   Id = z.Id,
                   InsuranceNumber = z.InsuranceNumber,
                   InsurancePrice = z.InsurancePrice,
                   OrderNumber = z.OrderNumber,
                   OutofRange = z.OutofRange,
                   ProductTotalPrice = z.ProductTotalPrice,
                   ServicePrice = z.ServicePrice,
                   ShippingCost = z.ShippingCost,
                   Tax = z.Tax,
                   TransferPrice = z.TransferPrice,
               }).FirstOrDefault(),
           });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<TransportationRequest?> GetByPackingId(long id, CT ct)
    {
        var query = DbSet
            .Include(t => t.TransportationContractor)
                .ThenInclude(x => x.ThirdParty)
            .Include(t => t.Transportation)
            .Include(t => t.TransportationRequestDocuments)
            .Include(t => t.MachineType)
            .Include(t => t.TransportationRequestDetails)
            .Include(t => t.TransportationCargoPallets)
            .Where(t => !t.IsDeleted &&
            t.TransportationCargoPallets.Any(x => x.TransportationCargo.PackingId == id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
}
