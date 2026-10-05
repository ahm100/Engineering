using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.Models;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public partial class ProjectOperationRepository : BaseRepository<EngineeringDBContext, ProjectOperation>, IProjectOperationRepository
{
    private IQueryable<GetsProjectOperationByProjectModel> BuildQueryGetsByProject(
       long projectId,
       long? categoryId,
       long? branchId,
       long? seasonId,
       string? filterData)
    {
        var query = DbSet.AsQueryable();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var newQuery = query.Where(oo =>
                oo.Project!.Id == projectId &&
                (categoryId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) &&
                (branchId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId)) &&
                (seasonId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                oo.IsDeleted != true)

            .Select(x => new GetsProjectOperationByProjectModel
            {
                Id = x.Id,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.Project.Id,
                ProjectName = x.Project.ProjectName,
                Description = x.Description,
                GoodsInProgress = x.GoodsInProgress,
                Status = x.ProjectOperationStatus,
                CostCenterCode = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                MeasurementId = x.UnitOfMeasurementId,
                OperationInfoId = x.OperationInfo.Id,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoMeasurementId = x.OperationInfo.UnitOfMeasurementId,
                Priority = x.Priority,
                ProjectCode = x.Project.ProjectCode,
                Workload = x.Workload,
                TolerancePercentage = x.TolerancePercentage,
                Price = x.Price,
                BasePrice = x.BasePrice,
                ChangedPrice = x.ChangedPrice,
                StartDate = x.ProjectOperationDetails.Where(x => x.StartDate != null).Min(x => x.StartDate),
                EndDate = x.ProjectOperationDetails.Where(x => x.EndDate != null).Max(x => x.EndDate),
                CanAssignContractor = x.ProjectOperationDetails.Any(pod => !pod.IsDeleted),
                CanCreateOperationService = !x.ProjectOperationDetails.Any(pod => !pod.IsDeleted && pod.ProjectOperationDetailContractorServices.Any(service =>
                !service.IsDeleted && service.Type == PODContractorServiceType.ServiceBased)),
                Created = x.Created,
                ///TODO Employers
                //ContractCode = x.EmployerContract.Code,
                //EmployerContractId = x.EmployerContract.Id,
                //HaveEmployerContract = x.EmployerContract != null ? true : false,
                HaveStandard = x.OperationInfo.HaveStandard,
                Urls = x.ProjectOperationDocuments.Select(x => x.Url).ToList(),
                OperationInfoModel = new OperationInfoDataModel()
                {
                    OperationInfoCode = x.OperationInfo.OperationInfoCode,
                    OperationInfoId = x.OperationInfo.Id,
                    OperationInfoName = x.OperationInfo.OperationInfoName,
                },
            });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return newQuery;
    }
    private IQueryable<GetsProjectOperationByProjectModel> BuildQueryGetsFilteredByProject(
        long projectId,
        long? categoryId,
        long? branchId,
        long? seasonId,
        List<long>? contractorIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData)
    {
        var query = DbSet.AsQueryable();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var newQuery = query.Where(oo => oo.Project!.Id == projectId &&
            (contractorIds == null || contractorIds.Count == 0 || oo.ProjectOperationDetails.Any(x => x.ProjectOperationDetailContractorServices.Any(c => c.ContractorId.HasValue && contractorIds.Contains(c.ContractorId.Value)))) &&
            (startDate == null || oo.ProjectOperationDetails.Any(x => x.StartDate.HasValue && x.StartDate.Value.Date >= startDate.Value.Date)) &&
            (endDate == null || oo.ProjectOperationDetails.Any(x => x.EndDate.HasValue && x.EndDate.Value.Date <= endDate.Value.Date)) &&
            (categoryId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) &&
            (branchId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId)) &&
            (seasonId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                oo.IsDeleted != true)

            .Select(x => new GetsProjectOperationByProjectModel
            {
                Id = x.Id,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                Description = x.Description,
                GoodsInProgress = x.GoodsInProgress,
                Status = x.ProjectOperationStatus,
                CostCenterCode = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                MeasurementId = x.UnitOfMeasurementId,
                OperationInfoId = x.OperationInfoId,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoMeasurementId = x.OperationInfo.UnitOfMeasurementId,
                Priority = x.Priority,
                ProjectCode = x.Project.ProjectCode,
                Workload = x.Workload,
                TolerancePercentage = x.TolerancePercentage,
                Price = x.Price,
                BasePrice = x.BasePrice,
                ChangedPrice = x.ChangedPrice,
                StartDate = x.ProjectOperationDetails.Where(x => x.StartDate != null).Min(x => x.StartDate),
                EndDate = x.ProjectOperationDetails.Where(x => x.EndDate != null).Max(x => x.EndDate),
                Created = x.Created,
                ///TODO Employers
                //ContractCode = x.EmployerContract.Code,
                //EmployerContractId = x.EmployerContract.Id,
                //HaveEmployerContract = x.EmployerContract != null ? true : false,
                HaveStandard = x.OperationInfo.HaveStandard,
                Urls = x.ProjectOperationDocuments.Select(x => x.Url).ToList(),
                CanAssignContractor = x.ProjectOperationDetails.Any(pod => !pod.IsDeleted),
                CanCreateOperationService = !x.ProjectOperationDetails.Any(pod => !pod.IsDeleted && pod.ProjectOperationDetailContractorServices.Any(service =>
                !service.IsDeleted && service.Type == PODContractorServiceType.ServiceBased)),
                OperationInfoModel = new OperationInfoDataModel()
                {
                    OperationInfoCode = x.OperationInfo.OperationInfoCode,
                    OperationInfoId = x.OperationInfoId,
                    OperationInfoName = x.OperationInfo.OperationInfoName,
                },
            });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return newQuery;
    }

    private IQueryable<GetsProjectOperationReportingModel> BuildQueryGetsProjectOperationReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<ProjectOperationStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        string? dailyDescription,
        string? filterData)
    {
        var query = DbSet.AsQueryable();

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        query = query.Where(oo => !oo.IsDeleted &&
            (ids == null || ids.Contains(oo.Id)) &&
            (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
            (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
            (operationInfoIds == null || operationInfoIds.Contains(oo.OperationInfo.Id)) &&
            (startDate == null || oo.ProjectOperationDetails.Any(x => x.StartDate.HasValue && x.StartDate.Value.Date >= startDate.Value.Date)) &&
            (endDate == null || oo.ProjectOperationDetails.Any(x => x.EndDate.HasValue && x.EndDate.Value.Date <= endDate.Value.Date)) &&
            (statuses == null || statuses.Contains(oo.ProjectOperationStatus)) &&
            (contractorIds == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(s => s.DailyProjectOperationServices
                .Any(s => s.ContractorId != null && s.ContractorId > 0 && contractorIds.Contains(s.ContractorId.Value))))) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
             string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
            (string.IsNullOrWhiteSpace(description) || oo.ProjectOperationDetails.Any(x => EF.Functions.Like(x.Description, description.MakeLikePattern()))) &&
            (string.IsNullOrWhiteSpace(dailyDescription) || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(z => EF.Functions.Like(z.Description, dailyDescription.MakeLikePattern()))))
            );

        var newQuery = query.Select(x => new GetsProjectOperationReportingModel
        {
            Id = x.Id,
            CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
            CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
            ProjectId = x.Project.Id,
            ProjectName = x.Project.ProjectName,
            Description = x.Description,
            Status = x.ProjectOperationStatus,
            CreatorId = x.CreatorId,
            Created = x.Created,
            CostCenterCode = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
            MeasurementId = x.UnitOfMeasurementId,
            OperationInfoId = x.OperationInfo.Id,
            OperationInfoCode = x.OperationInfo.OperationInfoCode,
            OperationInfoName = x.OperationInfo.OperationInfoName,
            OperationInfoMeasurementId = x.OperationInfo.UnitOfMeasurementId,
            Priority = x.Priority,
            ProjectCode = x.Project.ProjectCode,
            Workload = x.Workload,
            ImplementationAssistantIds = x.ProjectOperationDetails.SelectMany(x => x.UserImplementations.Select(x => x.ImplementationAssistantUserId)).Distinct().ToList(),
            TechnicalAssistantIds = x.ProjectOperationDetails.SelectMany(x => x.UserTechnicals.Select(x => x.TechnicalAssistantUserId)).Distinct().ToList(),
            TolerancePercentage = x.TolerancePercentage,
            Price = x.Price,
            BasePrice = x.BasePrice,
            ChangedPrice = x.ChangedPrice,
            ContractorIds = x.ProjectOperationDetails.SelectMany(x => x.DailyOperations.SelectMany(x => x.DailyProjectOperationServices.Select(s => s.ContractorId))).Distinct().ToList(),
            DailyCreatorsIds = x.ProjectOperationDetails.SelectMany(x => x.DailyOperations.Select(x => x.CreatorId)).Distinct().ToList(),
            DailyAmounts = x.ProjectOperationDetails.SelectMany(x => x.DailyOperations).Select(x => x.FinalAmount).ToList(),
            StartDate = x.ProjectOperationDetails.Where(x => x.StartDate != null).Min(x => x.StartDate),
            EndDate = x.ProjectOperationDetails.Where(x => x.EndDate != null).Max(x => x.EndDate),
            OperationInfoSeasons = x.OperationInfo.OperationInfoSeasons.Select(z => new OperationInfoSeasonModel()
            {
                CategoryId = z.Season.Branch.Category.Id,
                Category = z.Season.Branch.Category.CategoryName,
                BranchId = z.Season.Branch.Id,
                Branch = z.Season.Branch.BranchName,
                SeasonId = z.Season.Id,
                Season = z.Season.SeasonName,
            }).ToList(),
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        return newQuery;
    }

    private IQueryable<GetsProjectOperationEmployerReportingModel> BuildQueryGetsProjectOperationEmployerReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<long>? employerIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData)
    {
        var query = DbSet.AsQueryable();

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        query = query.Where(oo => !oo.IsDeleted &&
            (ids == null || ids.Contains(oo.Id)) &&
            (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
            (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
            (operationInfoIds == null || operationInfoIds.Contains(oo.OperationInfo.Id)) &&
            (contractorIds == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(s =>
                s.DailyProjectOperationServices.Any(s => s.ContractorId != null && s.ContractorId > 0 && contractorIds.Contains(s.ContractorId.Value))))) &&
            (employerIds == null ||
            (oo.Project.EmployerId.HasValue && employerIds.Contains(oo.Project.EmployerId.Value))) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
             string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
            (startDate == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(d => d.StartDate >= startDate))) &&
            (endDate == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(d => d.EndDate <= endDate)))
            );

        var newQuery = query.Select(x => new GetsProjectOperationEmployerReportingModel
        {
            Id = x.Id,
            CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
            CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
            CostCenterCode = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
            ProjectId = x.Project.Id,
            ProjectName = x.Project.ProjectName,
            ProjectCode = x.Project.ProjectCode,
            OperationInfoId = x.OperationInfo.Id,
            OperationInfoCode = x.OperationInfo.OperationInfoCode,
            OperationInfoName = x.OperationInfo.OperationInfoName,
            OperationInfoMeasurementId = x.OperationInfo.UnitOfMeasurementId,
            Workload = x.Workload,
            Created = x.Created,
            StartDate = x.ProjectOperationDetails.Where(x => x.StartDate != null).Min(x => x.StartDate),
            EndDate = x.ProjectOperationDetails.Where(x => x.EndDate != null).Max(x => x.EndDate),
            Description = x.Description,
            DailyProjectOperations = x.ProjectOperationDetails.SelectMany(p =>
                p.DailyOperations.Where(d =>
                    (startDate == null || d.StartDate >= startDate) &&
                    (endDate == null || d.EndDate <= endDate)))
            .Select(z => new DailyProjectOperationModel()
            {
                Id = z.Id,
                ProjectOperationId = x.Id,
                Location = z.ProjectOperationDetail.OperationLocation.PrivateName,
                Length = z.Length,
                Width = z.Width,
                Height = z.Height,
                Weight = z.Weight,
                Number = z.Number,
                Description = z.Description,
            }).ToList(),
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        return newQuery;
    }

    private IQueryable<GetsForEmployerStatusStatementModel> BuildQueryGetsForEmployerStatusStatement(
        long? employerId,
        long? employerStatusStatementId,
        long projectId,
        long costCenterId,
        long? employerContractId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData)
    {
        var query = DbSet.AsQueryable();

        query = query
        .Where(oo =>
            oo.Project.Id == projectId &&

            oo.Workload > 0 &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
            oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
            (startDate == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(oo => oo.StartDate.Date >= startDate.Value.Date))) &&
            (endDate == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(oo => oo.EndDate.Date <= endDate.Value.Date))) &&
            (employerId == null || oo.Project.EmployerId == employerId) &&
            (employerStatusStatementId == null || oo.EmployerStatusStatementProjectOperations.Any(x => x.EmployerStatusStatement.Id == employerStatusStatementId))
            ///TODO Employers 
            ///&& (employerContractId == null || oo.EmployerContract.Id == employerContractId)
            );

        var newQuery = query.Select(item => new GetsForEmployerStatusStatementModel
        {
            Id = item.Id,
            ProjectOperationStatus = item.ProjectOperationStatus,
            OperationInfoId = item.OperationInfo.Id,
            OperationInfoCode = item.OperationInfo.OperationInfoCode,
            OperationInfoName = item.OperationInfo.OperationInfoName,
            UnitOfMeasurementId = item.OperationInfo.UnitOfMeasurementId,

            TotalWorkVolume = item.Workload,
            StatusStatementWorkVolume = item.ProjectOperationDetails.Where(daily => (startDate == null || daily.StartDate >= startDate) && (endDate == null || daily.EndDate <= endDate))
                .Sum(detail => detail.Length * detail.Width * detail.Weight * detail.Height * detail.Number),
            DoneWorkVolume = item.ProjectOperationDetails.SelectMany(
                detail => detail.DailyOperations.Where(daily => (startDate == null || daily.StartDate >= startDate) && (endDate == null || daily.EndDate <= endDate)))
                .Sum(daily => daily.Length * daily.Width * daily.Weight * daily.Height * daily.Number),

            TotalDetailWorkVolume = item.ProjectOperationDetails.Sum(detail => detail.Length * detail.Width * detail.Weight * detail.Height * detail.Number),
            TotalDailyWorkVolume = item.ProjectOperationDetails.SelectMany(detail => detail.DailyOperations).Sum(daily => daily.Length * daily.Width * daily.Weight * daily.Height * daily.Number),

            PriceUnit = item.Price,
            TolerancePercentage = item.TolerancePercentage,
            ///TODO Employers
            //HaveContract = item.EmployerContract != null ? true : false,
            Priority = item.Priority,
            StartDate = item.ProjectOperationDetails.Where(detail => detail.StartDate != null).Min(detail => detail.StartDate),
            EndDate = item.ProjectOperationDetails.Where(detail => detail.EndDate != null).Max(detail => detail.EndDate),
            LastStatusStatement = item.EmployerStatusStatementProjectOperations.Max(po => po.Created),
            Created = item.Created,
            Description = item.Description,
        });

        return newQuery;
    }

    private IQueryable<GetsProjectOperationDailyReportingModel> BuildQueryGetsProjectOperationDailyReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData)
    {
        var query = DbSet.AsQueryable();

        query = query.Where(w =>
                        !w.IsDeleted &&
                        (ids == null || ids.Contains(w.Id)) &&
                        (costCenterId == null || w.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                        (projectIds == null || projectIds.Contains(w.Project.Id)) &&
                        (operationInfoIds == null || operationInfoIds.Contains(w.OperationInfo.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                        (startDate == null || w.ProjectOperationDetails.Any(a => a.DailyOperations.Any(a => a.StartDate >= startDate))) &&
                        (endDate == null || w.ProjectOperationDetails.Any(a => a.DailyOperations.Any(a => a.EndDate <= endDate))));

        var newQuery = query.Select(s => new GetsProjectOperationDailyReportingModel
        {
            Id = s.Id,
            CostCenterId = s.Project.ProjectCostCenters.Any() ? s.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
            CostCenterName = s.Project.ProjectCostCenters.Any() ? s.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
            CostCenterCode = s.Project.ProjectCostCenters.Any() ? s.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
            ProjectId = s.Project.Id,
            ProjectName = s.Project.ProjectName,
            ProjectCode = s.Project.ProjectCode,
            OperationInfoId = s.OperationInfo.Id,
            OperationInfoCode = s.OperationInfo.OperationInfoCode,
            OperationInfoName = s.OperationInfo.OperationInfoName,
            OperationInfoMeasurementId = s.OperationInfo.UnitOfMeasurementId,
            Workload = s.Workload,
            Description = s.Description,
            ProjectOperationDaily = s.ProjectOperationDetails.SelectMany(s =>
                s.DailyOperations.Where(w =>
                    (startDate == null || w.StartDate >= startDate) &&
                    (endDate == null || w.EndDate <= endDate)))
            .Select(s => new ProjectOperationDailyModel()
            {
                Id = s.Id,
                ProjectOperationId = s.Id,
                Location = s.ProjectOperationDetail.OperationLocation.PrivateName,
                Length = s.Length,
                Width = s.Width,
                Height = s.Height,
                Weight = s.Weight,
                Number = s.Number,
                Description = s.Description,
            }).ToList(),
        });

        return newQuery;
    }
}