using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public partial class ProjectOperationDetailRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetail>, IProjectOperationDetailRepository
{
    private IQueryable<GetsProjectOperationDetailDocumentResponse> BuildQueryGetsProjectOperationDetailDocument(
        long projectOperationDetailId)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query

            .Where(x =>
                x.Id == projectOperationDetailId)

            .Select(x => new GetsProjectOperationDetailDocumentResponse
            {
                Urls = x.ProjectOperationDetailDocuments.Select(x => x.Url).ToList(),
            });

        return newQuery;
    }

    private IQueryable<ProjectOperationDetailsModel> BuildQueryGetsByProjectOperationId(
        List<long>? ids,
        long projectOperationId,
        string? privateName,
        string? privateCode,
        string? filterData,
        long? employerId,
        ProjectOperationDetailStatus? status,
        List<long>? contractorIds,
        DateTime? createDate,
        DateTime? startDate,
        DateTime? endDate,
        List<long>? serviceInfoIds,
        List<long>? implementationAssistantIds,
        List<long>? technicalAssistantIds,
        long? creatorId)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)

            .AsQueryable();
#pragma warning disable CS8629 // Nullable value type may be null.
        var newQuery = query

           .Where(oo =>
                oo.OperationLocation!.IsDeleted == false &&
                oo.ProjectOperation!.Id == projectOperationId &&
                oo.IsDeleted == false &&
                (ids == null || ids.Contains(oo.Id)) &&
                (creatorId == null || oo.CreatorId == creatorId) &&
                (status == null || oo.Status == status) &&
                (createDate == null || oo.Created.Date == createDate.Value.Date) &&
                (startDate == null || oo.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || oo.EndDate.Value.Date <= endDate.Value.Date) &&
                (employerId == null || oo.ProjectOperation.Project.EmployerId.Equals(employerId)) &&
                (string.IsNullOrWhiteSpace(privateName) || EF.Functions.Like(oo.OperationLocation.PrivateName, privateName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(privateCode) || EF.Functions.Like(oo.OperationLocation.PrivateCode, privateCode.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLocation.PublicName, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLocation.PublicCode, filterData.MakeLikePattern())) &&
                (contractorIds == null || oo.ProjectOperationDetailContractorServices.Any(x => x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value))) &&
                (serviceInfoIds == null || oo.ProjectOperationDetailContractorServices.Any(x => serviceInfoIds.Contains(x.OperationInfoService.ServiceInfo.Id))) &&
                (implementationAssistantIds == null || oo.UserImplementations.Any(x => implementationAssistantIds.Contains(x.ImplementationAssistantUserId))) &&
                (technicalAssistantIds == null || oo.UserTechnicals.Any(x => technicalAssistantIds.Contains(x.TechnicalAssistantUserId)))
                )

            .Select(x => new ProjectOperationDetailsModel
            {
                Id = x.Id,
                CostCenterId = x.ProjectOperation.Project.ProjectCostCenters.Any() ? x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.ProjectOperation.Project.ProjectCostCenters.Any() ? x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.ProjectOperation.Project.Id,
                ProjectName = x.ProjectOperation.Project.ProjectName,
                ProjectOperationId = x.ProjectOperation.Id,
                OperationLocationId = x.OperationLocation.Id,
                PrivateName = x.OperationLocation.PrivateName,
                PrivateCode = x.OperationLocation.PrivateCode,
                PublicName = x.OperationLocation.PublicName,
                PublicCode = x.OperationLocation.PublicCode,
                Length = x.Length,
                Width = x.Width,
                Height = x.Height,
                Weight = x.Weight,
                Number = x.Number,
                Description = x.Description,
                Code = x.Code,
                Day = x.Day,
                FinalAmount = x.FinalAmount,
                Hour = x.Hour,
                OperationInfoId = x.ProjectOperation.OperationInfo.Id,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                Priority = x.Priority,
                CompanyId = x.CompanyId,
                CreateDate = x.Created,
                CreatorId = x.CreatorId,
                UpdaterId = x.UpdaterId,
                DailyOperationNumber = x.DailyOperations.Count(x => x.Length != 0 && x.Width != 0 && x.Height != 0 && x.Weight != 0 && x.Number != 0),
                SupplyNumber = x.ConsumableVolumeProducts.SelectMany(x => x.RequestGoodsSupplyDetails).Count(),
                HaveDaily = x.DailyOperations.Any() ? true : false,
                HaveProduct = x.ConsumableVolumeProducts.Any() ? true : false,
                HeightChangeable = x.HeightChangeable,
                HaveSupply = x.ConsumableVolumeProducts.Any(x => x.RequestGoodsSupplyDetails != null && x.RequestGoodsSupplyDetails.Count > 0) ? true : false,
                LengthChangeable = x.LengthChangeable,
                ModifyDate = x.Updated,
                NumberChangeable = x.NumberChangeable,
                StatusModel = new((int)x.Status!, x.Status.GetEnumDescription()),
                WidthChangeable = x.WidthChangeable,
                WeightChangeable = x.WeightChangeable,
                DailyAmounts = x.DailyOperations.Select(x => x.FinalAmount).ToList(),
                DeductionAmounts = x.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList(),
                ContractorIds = x.ProjectOperationDetailContractorServices.Select(x => x.ContractorId).Distinct().ToList(),
                PlannerUserIds = x.UserPlaners.Select(x => x.UserPlanerId).ToList(),
                TechnicalAssistantUserIds = x.UserTechnicals.Select(x => x.TechnicalAssistantUserId).ToList(),
                ImplementationAssistantUserIds = x.UserImplementations.Select(x => x.ImplementationAssistantUserId).ToList(),
                ProjectManagerId = x.ProjectOperation.Project.ProjectManager,
                WorkLoad = x.ProjectOperation.Workload,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
            });
#pragma warning restore CS8629 // Nullable value type may be null.

        return newQuery;
    }

    private IQueryable<GetsProjectOperationDetailReportingModel> BuildQueryGetsProjectOperationDetailReporting(
        List<long>? ids,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? createFrom,
        DateTime? createTo,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? projectOperationIds,
        List<long>? contractorIds,
        List<ProjectOperationDetailStatus>? statuses,
        string? locationFilterData,
        string? descriptionFilterData,
        string? dailyDescription,
        string? filterData)
    {
        var query = DbSet.AsQueryable();

#pragma warning disable CS8629 // Nullable value type may be null.
        var newQuery = query
            .Where(p => !p.IsDeleted &&
                (startDate == null || p.StartDate >= startDate) &&
                (endDate == null || p.EndDate <= endDate) &&
                (createFrom == null || p.Created >= createFrom) &&
                (createTo == null || p.Created <= createTo) &&
                (costCenterId == null || p.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectIds == null || projectIds.Count == 0 || projectIds.Contains(p.ProjectOperation.Project.Id)) &&
                (operationInfoIds == null || operationInfoIds.Count == 0 || operationInfoIds.Contains(p.ProjectOperation.OperationInfo.Id)) &&
                (projectOperationIds == null || projectOperationIds.Count == 0 || projectOperationIds.Contains(p.ProjectOperation.Id)) &&
                (contractorIds == null || p.ProjectOperationDetailContractorServices.Any(x => x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value))) &&
                (statuses == null || statuses.Count == 0 || statuses.Contains(p.Status.Value)) &&
                (ids == null || ids.Count == 0 || ids.Contains(p.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(p.Description, filterData.MakeLikePattern()) ||
                EF.Functions.Like(p.OperationLocation.PublicName, filterData.MakeLikePattern()) ||
                EF.Functions.Like(p.OperationLocation.PublicCode, filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(dailyDescription) || p.DailyOperations.Any(x => EF.Functions.Like(x.Description, dailyDescription.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(descriptionFilterData) || EF.Functions.Like(p.Description, descriptionFilterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(locationFilterData) || EF.Functions.Like(p.OperationLocation.PublicName, locationFilterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(locationFilterData) || EF.Functions.Like(p.OperationLocation.PublicCode!, locationFilterData.MakeLikePattern())))

            .Select(x => new GetsProjectOperationDetailReportingModel
            {
                Id = x.Id,
                CostCenterId = x.ProjectOperation.Project.ProjectCostCenters.Any() ? x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.ProjectOperation.Project.ProjectCostCenters.Any() ? x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.ProjectOperation.Project.Id,
                ProjectName = x.ProjectOperation.Project.ProjectName,
                ProjectOperationId = x.ProjectOperation.Id,
                OperationLocationId = x.OperationLocation.Id,
                PrivateName = x.OperationLocation.PrivateName,
                PrivateCode = x.OperationLocation.PrivateCode,
                PublicName = x.OperationLocation.PublicName,
                PublicCode = x.OperationLocation.PublicCode,
                Length = x.Length,
                Width = x.Width,
                Height = x.Height,
                Weight = x.Weight,
                Number = x.Number,
                Description = x.Description,
                Status = x.Status,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                CreatorId = x.CreatorId,
                Created = x.Created,
                Code = x.Code,
                CostCenterCode = x.ProjectOperation.Project.ProjectCostCenters.Any() ? x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                Day = x.Day,
                DailyAmounts = x.DailyOperations.Select(x => x.FinalAmount).ToList(),
                DeductionAmounts = x.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList(),
                UpdaterId = x.UpdaterId,
                FinalAmount = x.FinalAmount,
                Hour = x.Hour,
                MeasurementId = x.ProjectOperation.UnitOfMeasurementId,
                Updated = x.Updated,
                OperationInfoId = x.ProjectOperation.OperationInfo.Id,
                OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                Priority = x.Priority,
                ProjectCode = x.ProjectOperation.Project.ProjectCode,
                ContractorIds = x.ProjectOperationDetailContractorServices.Select(x => x.ContractorId).Distinct().ToList(),
                ProjectOperationWorkLoad = x.ProjectOperation.Workload
            });
#pragma warning restore CS8629 // Nullable value type may be null.

        return newQuery;
    }
}
