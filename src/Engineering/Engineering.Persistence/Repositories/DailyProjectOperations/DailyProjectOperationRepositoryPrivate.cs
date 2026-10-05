using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public partial class DailyProjectOperationRepository : BaseRepository<EngineeringDBContext, DailyProjectOperation>, IDailyProjectOperationRepository
{
    private IQueryable<GetsDetailedDailyProjectOperationModel> BuildQueryGetsDetailedDailyProjectOperation(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? creatorIds,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        List<ProjectOperationDetailStatus>? projectOperationDetailStatus,
        List<ProjectOperationDetailStatus>? status,
        string? filterData)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query

            .Where(x => !x.IsDeleted && !x.ProjectOperationDetail.IsDeleted && !x.ProjectOperationDetail.ProjectOperation.IsDeleted && !x.ProjectOperationDetail.OperationLocation.IsDeleted &&
                (costCenterIds == null || x.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (x.Length > 0 && x.Width > 0 && x.Height > 0 && x.Weight > 0 && x.Number > 0) &&
                (projectIds == null || projectIds.Contains(x.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                (creatorIds == null || creatorIds.Contains(x.CreatorId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.ProjectOperationDetail.Id)) &&
                (contractorIds == null || x.DailyProjectOperationServices.Any(cs => cs.ContractorId.HasValue && contractorIds.Contains(cs.ContractorId.Value))) &&
                (serviceInfoIds == null || x.DailyProjectOperationServices.Any(cs => serviceInfoIds.Contains(cs.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id))) &&
                (startDate == null || x.StartDate.Date >= startDate.Value.Date) &&
                (endDate == null || x.StartDate.Date <= endDate.Value.Date) &&
                (fromDate == null || x.Created.Date >= fromDate.Value.Date) &&
                (toDate == null || x.Created.Date <= toDate.Value.Date) &&
                (projectOperationDetailStatus == null || (x.ProjectOperationDetail.Status.HasValue && projectOperationDetailStatus.Contains(x.ProjectOperationDetail.Status.Value))) &&
                (status == null || status.Contains(x.Status)) &&
                (ids == null || ids.Contains(x.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.ProjectOperationDetail.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())))

            .Select(x => new GetsDetailedDailyProjectOperationModel
            {
                Id = x.Id,
                Type = x.Type,
                CostCenterId = x.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = x.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = x.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectOperationId = x.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationName = x.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationStatus = x.ProjectOperationDetail.ProjectOperation.ProjectOperationStatus,
                UnitOfMeasurementId = x.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationDetailId = x.ProjectOperationDetail.Id,
                ProjectOperationDetailCode = x.ProjectOperationDetail.Code,
                ProjectOperationDetailLength = x.ProjectOperationDetail.Length,
                ProjectOperationDetailWidth = x.ProjectOperationDetail.Width,
                ProjectOperationDetailHeight = x.ProjectOperationDetail.Height,
                ProjectOperationDetailWeight = x.ProjectOperationDetail.Weight,
                ProjectOperationDetailNumber = x.ProjectOperationDetail.Number,
                ProjectOperationDetailFinalAmount = x.ProjectOperationDetail.FinalAmount,
                ProjectOperationDetailDescription = x.ProjectOperationDetail.Description,
                ProjectOperationDetailStatus = x.ProjectOperationDetail.Status!.Value,
                ProjectOperationDetailStartDate = x.ProjectOperationDetail.StartDate,
                ProjectOperationDetailEndDate = x.ProjectOperationDetail.EndDate,
                ProjectOperationDetailCreateDate = x.ProjectOperationDetail.Created,
                OperationLocationId = x.ProjectOperationDetail.OperationLocation.Id,
                PrivateName = x.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = x.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = x.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.ProjectOperationDetail.OperationLocation.PublicCode,
                Length = x.Length,
                Width = x.Width,
                Height = x.Height,
                Weight = x.Weight,
                Number = x.Number,
                Description = x.Description,
                Status = x.Status,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                ContractorIds = x.DailyProjectOperationServices.Select(x => x.ContractorId).ToList(),
                DeductionAmounts = x.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList(),
                CreatorId = x.CreatorId,
                Created = x.Created,
                services = x.DailyProjectOperationServices.Where(x => !x.IsDeleted).Select(z => new DetailedServiceInfoModels()
                {
                    Id = z.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                    ServiceInfoName = z.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                    ServiceInfoMeasureId = z.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                }).ToList(),
                HaveDocument = x.DailyProjectOperationDocuments.Any() && x.DailyProjectOperationDocuments.Count > 0 ? true : false,
            });

        return newQuery;
    }

    private IQueryable<GetDetailedDailyProjectOperationTotalsModel> BuildQueryGetsDetailedDailyProjectOperationTotals(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? contractorIds,
        List<long>? serviceInfoIds,
        List<long>? creatorIds,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        List<ProjectOperationDetailStatus>? status,
        List<ProjectOperationDetailStatus>? projectOperationDetailStatus,
        string? filterData)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query

            .Where(x => !x.IsDeleted &&
                (costCenterIds == null || x.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (x.Length > 0 || x.Width > 0 || x.Height > 0 || x.Weight > 0 || x.Number > 0) &&
                (creatorIds == null || creatorIds.Contains(x.CreatorId)) &&
                (projectIds == null || projectIds.Contains(x.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.ProjectOperationDetail.Id)) &&
                (contractorIds == null || x.DailyProjectOperationServices.Any(cs => cs.ContractorId.HasValue && contractorIds.Contains(cs.ContractorId.Value))) &&
                (serviceInfoIds == null || x.DailyProjectOperationServices.Any(cs => serviceInfoIds.Contains(cs.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id))) &&
                (startDate == null || x.StartDate.Date >= startDate.Value.Date) &&
                (endDate == null || x.StartDate.Date <= endDate.Value.Date) &&
                (fromDate == null || x.Created.Date >= fromDate.Value.Date) &&
                (toDate == null || x.Created.Date <= toDate.Value.Date) &&
                (status == null || status.Contains(x.Status)) &&
                (projectOperationDetailStatus == null || (x.ProjectOperationDetail.Status.HasValue && projectOperationDetailStatus.Contains(x.ProjectOperationDetail.Status.Value))) &&
                (ids == null || ids.Contains(x.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.ProjectOperationDetail.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())))

            .Select(x => new GetDetailedDailyProjectOperationTotalsModel
            {
                Id = x.Id,
                ProjectOperationId = x.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationWorkload = x.ProjectOperationDetail.ProjectOperation.Workload,
                ProjectOperationDetailId = x.ProjectOperationDetail.Id,
                ProjectOperationDetailLength = x.ProjectOperationDetail.Length,
                ProjectOperationDetailWidth = x.ProjectOperationDetail.Width,
                ProjectOperationDetailHeight = x.ProjectOperationDetail.Height,
                ProjectOperationDetailWeight = x.ProjectOperationDetail.Weight,
                ProjectOperationDetailNumber = x.ProjectOperationDetail.Number,
                ProjectOperationDetailFinalAmount = x.ProjectOperationDetail.FinalAmount,
                Length = x.Length,
                Width = x.Width,
                Height = x.Height,
                Weight = x.Weight,
                Number = x.Number,
                DeductionAmounts = x.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList(),
            });

        return newQuery;
    }

    private IQueryable<GetsDailyProjectOperationDocumentResponse> BuildQueryGetsDailyProjectOperationDocument(
        long dailyProjectOperationId)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query

            .Where(x =>
                x.Id == dailyProjectOperationId)

            .Select(x => new GetsDailyProjectOperationDocumentResponse
            {
                Urls = x.DailyProjectOperationDocuments.Select(x => x.Url).ToList(),
            });

        return newQuery;
    }

}
