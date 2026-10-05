using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public partial class DailyProjectOperationServiceRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationService>, IDailyProjectOperationServiceRepository
{
    public DailyProjectOperationServiceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetsDailyProjectOperationServiceModel> Data, int RowCount)> GetsDailyProjectOperationService(
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
        DateTime? fromDate,
        DateTime? toDate,
        ProjectOperationDetailStatus? status,
        string? filterData,
        string? filterServiceInfo,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = BuildQueryGetsDailyProjectOperationService(
            ids, costCenterIds, projectIds, projectOperationIds, projectOperationDetailIds, contractorIds,
            serviceInfoIds, measurUnitIds, startDate, endDate, fromDate, toDate, status, filterData, filterServiceInfo);

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsDailyServiceInfoModel> Data, int RowCount)> GetsDailyServiceInfo(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        var query = DbSet.Where(x => !x.IsDeleted &&
                (costCenterIds == null || x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (x.DailyProjectOperation.Length > 0 || x.DailyProjectOperation.Width > 0 || x.DailyProjectOperation.Height > 0 || x.DailyProjectOperation.Weight > 0 || x.DailyProjectOperation.Number > 0) &&
                (projectIds == null || projectIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.Id)) &&
                (contractorIds == null ||
                (x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value)) ||
                (x.ProjectOperationDetailContractorService.ContractorId.HasValue && contractorIds.Contains(x.ProjectOperationDetailContractorService.ContractorId.Value))) &&
                (ids == null || ids.Contains(x.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern())
                 ))
            .Select(x => new GetsDailyServiceInfoModel
            {
                Id = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                UnitOfMeasurementId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                Volume = x.Volume,
            })
            .GroupBy(x => x.Id)
            .Select(g => g.FirstOrDefault()); ;

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public async Task<List<GetsTotalDailyProjectOperationServiceModel>> GetsTotalDailyProjectOperationService(
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
        DateTime? fromDate,
        DateTime? toDate,
        ProjectOperationDetailStatus? status,
        string? filterData,
        string? filterServiceInfo,
        CT ct)
    {
        var query = BuildQueryGetsTotalDailyProjectOperationService(
            ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            contractorIds,
            serviceInfoIds,
            measurUnitIds,
            startDate,
            endDate,
            fromDate,
            toDate,
            status,
            filterData,
            filterServiceInfo);

        query = query.OrderByDescending(a => a.Created);

        var items = await query.ToListAsync(ct);

        return items;
    }

    public async Task<List<GetDraftedFixDailiesModel>> GetDraftedFixDailies(
        List<long> ids,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x =>
                !x.IsDeleted &&
                !x.DailyProjectOperation.IsDeleted &&
                !x.ProjectOperationDetailContractorService.IsDeleted &&
                ids.Contains(x.ProjectOperationDetailContractorService.Id)
            ).Select(x => new GetDraftedFixDailiesModel
            {
                Id = x.Id,
                DailyId = x.DailyProjectOperation.Id,
                Volume = x.Volume,
                CreatedMiladi = x.Created,
                CreatorId = x.CreatorId,
                Urls = x.DailyProjectOperation.DailyProjectOperationDocuments.Select(x => x.Url).ToList(),

                ProjectOperationId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationDetailId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Id,
                OperationInfoName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectOperationMeasureId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationWorkload = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Workload,

                ProjectOperationDetailContractorServiceId = x.ProjectOperationDetailContractorService.Id,
                ServiceInfoId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoMeasureId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,

                Length = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Length,
                Width = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Width,
                Height = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Height,
                Weight = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Weight,
                Number = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Number,
                PublicName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                Description = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.CreatedMiladi);

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<List<GetDraftedServiceDailiesModel>> GetDraftedServiceDailies(
        List<long> ids,
        DateTime startDate,
        DateTime endDate,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(x =>
                !x.IsDeleted &&
                !x.DailyProjectOperation.IsDeleted &&
                !x.ProjectOperationDetailContractorService.IsDeleted &&
                x.DailyProjectOperation.StartDate.Date >= startDate.Date &&
                x.DailyProjectOperation.EndDate.Date <= endDate.Date &&
                ids.Contains(x.ProjectOperationDetailContractorService.Id)
            ).Select(x => new GetDraftedServiceDailiesModel
            {
                Id = x.Id,
                DailyId = x.DailyProjectOperation.Id,
                WorkDateMiladi = x.DailyProjectOperation.StartDate,
                Volume = x.Volume,
                CreatedMiladi = x.Created,
                CreatorId = x.CreatorId,
                Urls = x.DailyProjectOperation.DailyProjectOperationDocuments.Select(x => x.Url).ToList(),

                ProjectOperationId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationDetailId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Id,
                OperationInfoName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectOperationMeasureId = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationWorkload = x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Workload,

                ProjectOperationDetailContractorServiceId = x.ProjectOperationDetailContractorService.Id,
                ServiceInfoId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoMeasureId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,

                Length = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Length,
                Width = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Width,
                Height = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Height,
                Weight = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Weight,
                Number = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Number,

                ContractorContractDetailId = x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .FirstOrDefault(x => !x.IsDeleted).ContractorContractDetail.Id,

                PublicName = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                Description = x.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.CreatedMiladi);

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<decimal> GetsTotalPriceDailyContractorService(
        long projectId,
        long contractorId,
        DateTime? startDate,
        DateTime? endDate,
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                !x.IsDeleted &&
                !x.DailyProjectOperation.IsDeleted &&
                !x.ProjectOperationDetailContractorService.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.IsDeleted &&
                !x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.IsDeleted &&
                x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(c => !c.IsDeleted) &&
                x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(c => !c.ContractorContractDetail.IsDeleted) &&
                x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(c => !c.ContractorContractDetail.ContractorContract.IsDeleted) &&
                x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(c => !c.ContractorContractDetail.ContractorContract.ContractorContractHeader.IsDeleted) &&

                x.ProjectOperationDetailContractorService.Status == ContractorServiceStatus.Contract &&

                x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(c => c.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed) &&

                x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .Any(c => c.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId == contractorId) &&
                x.ContractorId == contractorId &&

                x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id == projectId &&

                (startDate == null || (x.DailyProjectOperation.StartDate.Date >= startDate.Value.Date)) &&
                (endDate == null || (x.DailyProjectOperation.EndDate.Date <= endDate.Value.Date))
            )
            .Select(x => new TotalPricedModel()
            {

                Id = x.Id,
                CCDId = x.ProjectOperationDetailContractorService.ContractorContractDetailServices.FirstOrDefault(s => !s.IsDeleted).Id,
                Volume = x.Volume,
                UnitPrice = x.ProjectOperationDetailContractorService.ContractorContractDetailServices.FirstOrDefault(s => !s.IsDeleted)
                .ContractorContractDetail.ContractorContractDetailPrices
                .FirstOrDefault(d => d.StartDate.Date <= x.DailyProjectOperation.StartDate && d.EndDate.Date >= x.DailyProjectOperation.StartDate).Price,

                ActiveUnitPrice = x.ProjectOperationDetailContractorService.ContractorContractDetailServices
                    .FirstOrDefault(s => !s.IsDeleted).ContractorContractDetail.ContractorContractDetailPrices
                    .FirstOrDefault(d => d.IsActive).Price
            });

        var items = await query.ToListAsync(ct);

        var total = items.Sum(x => x.Volume * ((x.UnitPrice == null || x.UnitPrice == 0) ? x.ActiveUnitPrice : x.UnitPrice)) ?? 0;

        return total;
    }
}

public class TotalPricedModel
{
    public long Id { get; set; }
    public long CCDId { get; set; }
    public decimal Volume { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? ActiveUnitPrice { get; set; }
}