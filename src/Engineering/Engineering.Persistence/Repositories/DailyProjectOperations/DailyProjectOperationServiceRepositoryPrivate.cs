using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public partial class DailyProjectOperationServiceRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationService>, IDailyProjectOperationServiceRepository
{
    private IQueryable<GetsDailyProjectOperationServiceModel> BuildQueryGetsDailyProjectOperationService(
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
   string? filterServiceInfo)
    {
        var query = DbSet.AsQueryable();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var newQuery = query
            .Where(x => !x.IsDeleted &&
                (costCenterIds == null || x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (x.DailyProjectOperation.Length > 0 || x.DailyProjectOperation.Width > 0 || x.DailyProjectOperation.Height > 0 || x.DailyProjectOperation.Weight > 0 || x.DailyProjectOperation.Number > 0) &&
                (projectIds == null || projectIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.Id)) &&
                (contractorIds == null ||
                (x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value)) ||
                (x.ProjectOperationDetailContractorService.ContractorId.HasValue && contractorIds.Contains(x.ProjectOperationDetailContractorService.ContractorId.Value))) &&
                (startDate == null || x.DailyProjectOperation.StartDate.Date >= startDate.Value.Date) &&
                (endDate == null || x.DailyProjectOperation.EndDate.Date <= endDate.Value.Date) &&
                (fromDate == null || x.DailyProjectOperation.Created.Date >= fromDate.Value.Date) &&
                (toDate == null || x.DailyProjectOperation.Created.Date <= toDate.Value.Date) &&
                (status == null || x.DailyProjectOperation.Status == status) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id)) &&
                (measurUnitIds == null || measurUnitIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId)) &&
                (ids == null || ids.Contains(x.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.DailyProjectOperation.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.DailyProjectOperation.ProjectOperationDetail.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterServiceInfo) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterServiceInfo.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode, filterServiceInfo.MakeLikePattern())))

            .Select(x => new GetsDailyProjectOperationServiceModel
            {
                Id = x.Id,
                CostCenterId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectOperationId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id,
                ProjectOperationName = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationStatus = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.ProjectOperationStatus,
                UnitOfMeasurementId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
                ProjectOperationDetailId = x.DailyProjectOperation.ProjectOperationDetail.Id,
                ProjectOperationDetailFinalAmount = x.DailyProjectOperation.ProjectOperationDetail.FinalAmount,
                ProjectOperationDetailDescription = x.DailyProjectOperation.ProjectOperationDetail.Description,
                ProjectOperationDetailStatus = x.DailyProjectOperation.ProjectOperationDetail.Status!.Value,
                ProjectOperationDetailStartDate = x.DailyProjectOperation.ProjectOperationDetail.StartDate,
                ProjectOperationDetailEndDate = x.DailyProjectOperation.ProjectOperationDetail.EndDate,
                ProjectOperationDetailCreateDate = x.DailyProjectOperation.ProjectOperationDetail.Created,
                OperationLocationId = x.DailyProjectOperation.ProjectOperationDetail.OperationLocation.Id,
                PrivateName = x.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = x.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = x.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PublicCode,
                Description = x.DailyProjectOperation.Description,
                Status = x.DailyProjectOperation.Status,
                Type = x.DailyProjectOperation.Type,
                StartDate = x.DailyProjectOperation.StartDate,
                EndDate = x.DailyProjectOperation.EndDate,
                DeductionAmounts = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList(),
                CreatorId = x.CreatorId,
                Created = x.Created,
                ContractorId = x.ContractorId,
                DailyProjectOperationFinalAmount = x.DailyProjectOperation.FinalAmount,
                ProjectOperationDetailContractorId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailContractorServices.FirstOrDefault(z => z.Id == x.ProjectOperationDetailContractorService.Id).ContractorId,
                ProjectOperationDetailVolume = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailContractorServices.FirstOrDefault(z => z.Id == x.ProjectOperationDetailContractorService.Id).Volume,
                ProjectOperationDetailServiceInfoId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailContractorServices.FirstOrDefault(z => z.Id == x.ProjectOperationDetailContractorService.Id).OperationInfoService.ServiceInfo.Id,
                ProjectOperationDetailServiceInfoMeasureId = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailContractorServices.FirstOrDefault(z => z.Id == x.ProjectOperationDetailContractorService.Id).OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                ProjectOperationDetailServiceInfoName = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailContractorServices.FirstOrDefault(z => z.Id == x.ProjectOperationDetailContractorService.Id).OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoMeasureId = x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                Volume = x.Volume,
                DailyProjectOperationId = x.DailyProjectOperation.Id,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return newQuery;
    }

    private IQueryable<GetsTotalDailyProjectOperationServiceModel> BuildQueryGetsTotalDailyProjectOperationService(
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
    string? filterServiceInfo)
    {
        var query = DbSet.AsQueryable();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var newQuery = query
            .Where(x => !x.IsDeleted &&
                (costCenterIds == null || x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (x.DailyProjectOperation.Length > 0 || x.DailyProjectOperation.Width > 0 || x.DailyProjectOperation.Height > 0 || x.DailyProjectOperation.Weight > 0 || x.DailyProjectOperation.Number > 0) &&
                (projectIds == null || projectIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.DailyProjectOperation.ProjectOperationDetail.Id)) &&

                (contractorIds == null || (x.ContractorId.HasValue && contractorIds.Contains(x.ContractorId.Value))) &&

                (startDate == null || x.DailyProjectOperation.StartDate.Date >= startDate.Value.Date) &&
                (endDate == null || x.DailyProjectOperation.EndDate.Date <= endDate.Value.Date) &&
                (fromDate == null || x.DailyProjectOperation.Created.Date >= fromDate.Value.Date) &&
                (toDate == null || x.DailyProjectOperation.Created.Date <= toDate.Value.Date) &&
                (status == null || x.DailyProjectOperation.Status == status) &&
                (serviceInfoIds == null || serviceInfoIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id)) &&
                (measurUnitIds == null || measurUnitIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId)) &&
                (ids == null || ids.Contains(x.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                 EF.Functions.Like(x.DailyProjectOperation.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.DailyProjectOperation.ProjectOperationDetail.Description, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterServiceInfo) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterServiceInfo.MakeLikePattern()) ||
                 EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode, filterServiceInfo.MakeLikePattern())))

            .Select(x => new GetsTotalDailyProjectOperationServiceModel
            {
                ProjectOperationDetailId = x.DailyProjectOperation.ProjectOperationDetail.Id,
                ProjectOperationDetailVolume = x.DailyProjectOperation.ProjectOperationDetail.ProjectOperationDetailContractorServices.FirstOrDefault(z => z.Id == x.ProjectOperationDetailContractorService.Id).Volume,
                Volume = x.Volume,
                Created = x.Created,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return newQuery;
    }
}
