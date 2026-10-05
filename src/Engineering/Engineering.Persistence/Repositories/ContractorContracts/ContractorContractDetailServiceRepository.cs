using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractDetailServiceRepository : BaseRepository<EngineeringDBContext, ContractorContractDetailService>, IContractorContractDetailServiceRepository
{
    public ContractorContractDetailServiceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractorContractDetailService?> GetContractorContractDetailServiceById(long id, CT ct)
    {
        var query = DbSet

            .Include(x => x.ContractorContractDetail)
                .ThenInclude(c => c.ContractorContractDetailServices)
            .Include(c => c.ProjectOperationDetailContractorService.DailyOperationServices)
                .ThenInclude(c => c.ContractorStatusStatementServiceDailies)

            .Include(c => c.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService.ServiceInfo)

            .Where(c => c.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<(List<GetsContractorContractServiceReportModel> Data, int RowCount)> GetsContractorContractServiceReport(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? measurUnitIds,
        ContractorContractType? contractTypeId,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? fromCreated,
        DateTime? toCreated,
        ProjectOperationDetailStatus? status,
        ContractorContractStatus? contractStatus,
        string? filterData,
        string? filterDescription,
        string? filterServiceInfo,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x =>
                !x.IsDeleted &&
                !x.ContractorContractDetail.IsDeleted &&
                !x.ContractorContractDetail.ContractorContract.IsDeleted &&
                !x.ContractorContractDetail.ContractorContract.ContractorContractHeader.IsDeleted &&
                !x.ProjectOperationDetailContractorService.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.IsDeleted &&
                (contractTypeId == null || x.ContractorContractDetail.ContractorContract.ContractorContractType == contractTypeId) &&
                (costCenterIds == null || x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.ProjectOperationDetailContractorService.ProjectOperationDetail.Id)) &&
                (contractorIds == null ||
                (contractorIds.Contains(x.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId)) ||
                (x.ProjectOperationDetailContractorService.ContractorId.HasValue && contractorIds.Contains(x.ProjectOperationDetailContractorService.ContractorId.Value))) &&
                (startDate == null || (x.ProjectOperationDetailContractorService.ProjectOperationDetail.StartDate.HasValue &&
                                       x.ProjectOperationDetailContractorService.ProjectOperationDetail.StartDate.Value.Date >= startDate.Value.Date)) &&
                (endDate == null || (x.ProjectOperationDetailContractorService.ProjectOperationDetail.EndDate.HasValue &&
                                     x.ProjectOperationDetailContractorService.ProjectOperationDetail.EndDate.Value.Date <= endDate.Value.Date)) &&
                (fromDate == null || x.ContractorContractDetail.ContractorContract.StartDate.Date >= fromDate.Value.Date) &&
                (toDate == null || x.ContractorContractDetail.ContractorContract.EndDate.Date <= toDate.Value.Date) &&
                (fromCreated == null || x.ContractorContractDetail.ContractorContract.Created.Date >= fromCreated.Value.Date) &&
                (toCreated == null || x.ContractorContractDetail.ContractorContract.Created.Date <= toCreated.Value.Date) &&
                (status == null || x.ProjectOperationDetailContractorService.ProjectOperationDetail.Status == status) &&
                (contractStatus == null || x.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status == contractStatus) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id)) &&
                (measurUnitIds == null || measurUnitIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId)) &&
                (ids == null || ids.Contains(x.Id)) &&
                (string.IsNullOrWhiteSpace(filterDescription) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.ProjectOperationDetail.Description, filterDescription.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.ProjectOperationDetail.Code, filterDescription.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterServiceInfo) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterServiceInfo.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode, filterServiceInfo.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.ContractorContractDetail.ContractorContract.ContractorContractHeader.Id.ToString(), filterData.MakeLikePattern())))

            .Select(x => new GetsContractorContractServiceReportModel
            {
                Id = x.Id,
                CostCenterId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectOperationId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationStatus = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.ProjectOperationStatus,
                UnitOfMeasurementId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationDetailId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Id,
                ProjectOperationDetailDescription = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
                ProjectOperationDetailStatus = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Status!.Value,
                ProjectOperationDetailStartDate = x.ProjectOperationDetailContractorService.ProjectOperationDetail.StartDate,
                ProjectOperationDetailEndDate = x.ProjectOperationDetailContractorService.ProjectOperationDetail.EndDate,
                ProjectOperationDetailCreateDate = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Created,
                OperationLocationId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.Id,
                PrivateName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                Description = x.ContractorContractDetail.ContractorContract.Description,
                ContractorContractStatus = x.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status,
                StartDate = x.ContractorContractDetail.ContractorContract.StartDate,
                EndDate = x.ContractorContractDetail.ContractorContract.EndDate,
                CreatorId = x.ContractorContractDetail.ContractorContract.CreatorId,
                Created = x.ContractorContractDetail.ContractorContract.Created,
                ContractorId = x.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId,
                ServiceInfoId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoMeasureId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                Volume = x.ProjectOperationDetailContractorService.Volume,
                ContractorContractCode = x.ContractorContractDetail.ContractorContract.ContractorContractHeader.Id.ToString(),
                ContractorContractType = x.ContractorContractDetail.ContractorContract.ContractorContractType.GetEnumDescription(),
                Price = x.ContractorContractDetail.ContractorContractDetailPrices.FirstOrDefault(z => z.IsActive).Price,
                CurrencyId = x.ContractorContractDetail.ContractorContract.ContractorContractHeader.CurrencyId,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<GetConfirmedCCDailyServicesModel> Data, int RowCount)> GetConfirmedCCDailyServices(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? measurUnitIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string? filterServiceInfo,
        string[]? orderBy,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x =>
                !x.IsDeleted &&
                !x.ContractorContractDetail.IsDeleted &&
                !x.ContractorContractDetail.ContractorContract.IsDeleted &&
                !x.ContractorContractDetail.ContractorContract.ContractorContractHeader.IsDeleted &&
                !x.ProjectOperationDetailContractorService.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.IsDeleted &&
                x.ContractorContractDetail.ContractorContract.ContractorContractType == ContractorContractType.Service &&
                x.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&

                (ids == null || ids.Contains(x.Id)) &&

                (costCenterIds == null || x.ProjectOperationDetailContractorService
                    .ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(x.ProjectOperationDetailContractorService
                    .ProjectOperationDetail.ProjectOperation.Project.Id)) &&

                (projectOperationIds == null || projectOperationIds.Contains(x.ProjectOperationDetailContractorService
                    .ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.ProjectOperationDetailContractorService
                    .ProjectOperationDetail.Id)) &&

                (contractorIds == null || (contractorIds.Contains(x.ContractorContractDetail.ContractorContract
                    .ContractorContractHeader.ContractorId))) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(x.ProjectOperationDetailContractorService
                    .OperationInfoService.ServiceInfo.Id)) &&
                (measurUnitIds == null || measurUnitIds.Contains(x.ProjectOperationDetailContractorService
                    .OperationInfoService.ServiceInfo.UnitOfMeasurementId)) &&

                (startDate == null || (x.ProjectOperationDetailContractorService.ProjectOperationDetail.StartDate.HasValue &&
                    x.ProjectOperationDetailContractorService.ProjectOperationDetail.StartDate.Value.Date >= startDate.Value.Date)) &&
                (endDate == null || (x.ProjectOperationDetailContractorService.ProjectOperationDetail.EndDate.HasValue &&
                    x.ProjectOperationDetailContractorService.ProjectOperationDetail.EndDate.Value.Date <= endDate.Value.Date)) &&

                (string.IsNullOrWhiteSpace(filterServiceInfo) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo
                    .ServiceInfoName, filterServiceInfo.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo
                    .ServiceInfoCode, filterServiceInfo.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.ContractorContractDetail.ContractorContract.ContractorContractHeader
                 .Id.ToString(), filterData.MakeLikePattern())))

            .SelectMany(x => x.ProjectOperationDetailContractorService.DailyOperationServices.Select(item => new GetConfirmedCCDailyServicesModel
            {
                Id = item.Id,
                ContractorContractCode = x.ContractorContractDetail.ContractorContract.ContractorContractHeader.Id.ToString(),
                ContractorContractDetailId = x.ContractorContractDetail.Id,
                ContractorContractDetailVolume = x.ContractorContractDetail.WorkLoad,
                ContractorContractDetailPrice = x.ContractorContractDetail.TotalAmount,
                Volume = item.Volume,
                TotalServiceVolume = item.ProjectOperationDetailContractorService.Volume,
                UnitPrice = x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .FirstOrDefault(x => !x.IsDeleted).ContractorContractDetail.ContractorContractDetailPrices
                    .Any(d => d.StartDate.Date >= item.DailyProjectOperation.StartDate.Date &&
                                d.EndDate.Date <= item.DailyProjectOperation.StartDate.Date) ?

                    x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .FirstOrDefault(x => !x.IsDeleted).ContractorContractDetail.ContractorContractDetailPrices
                    .FirstOrDefault(d => d.StartDate.Date >= item.DailyProjectOperation.StartDate.Date &&
                                            d.EndDate.Date <= item.DailyProjectOperation.StartDate.Date).Price :

                     x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .FirstOrDefault(x => !x.IsDeleted).ContractorContractDetail.ContractorContractDetailPrices
                    .FirstOrDefault(d => d.IsActive).Price,
                ServiceInfoId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoMeasureId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,

                ContractorId = x.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId,

                CostCenterId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectOperationId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationMeasureId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,

                ProjectOperationDetailId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Id,
                ProjectOperationDetailDescription = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
                PrivateName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                Length = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Length,
                Width = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Width,
                Height = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Height,
                Weight = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Weight,
                Number = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Number,

                StartDateMiladi = x.ContractorContractDetail.ContractorContract.StartDate,
                EndDateMiladi = x.ContractorContractDetail.ContractorContract.EndDate,

                CreatorId = item.CreatorId,
                Created = item.Created,

                Urls = item.DailyProjectOperation.DailyProjectOperationDocuments
                    .Select(x => x.Url).ToList(),

            }));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}
