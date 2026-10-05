using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectOperationWbsRepository : BaseRepository<EngineeringDBContext, ProjectOperationWbs>, IProjectOperationWbsRepository
{
    public ProjectOperationWbsRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectOperationWbs?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<GetProjectOperationWbsByIdResponse?> GetProjectOperationWbsById(
        long id, CT ct)
    {
        return await DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetProjectOperationWbsByIdResponse
            {
                Id = x.Id,
                ProjectWbsId = x.ProjectWbsId,
                TitleFa = x.ProjectWbs.TitleFa,
                TitleEn = x.ProjectWbs.TitleEn,
                Code = x.ProjectWbs.Code,
                DescriptionFa = x.ProjectWbs.DescriptionFa,
                DescriptionEn = x.ProjectWbs.DescriptionEn,
                ProjectOperationId = x.ProjectOperationId,
                ProjectOperationDescription = x.ProjectOperation.Description,
                OperationInfoId = x.ProjectOperation.OperationInfoId,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetPOWbsByPOIdModel>? Data, int RowCount)> GetPOWbsByPOId(
        long projectOperationId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.ProjectOperationId == projectOperationId)
            .Select(x => new GetPOWbsByPOIdModel
            {
                Id = x.Id,
                ProjectWbsId = x.ProjectWbsId,
                TitleFa = x.ProjectWbs.TitleFa,
                TitleEn = x.ProjectWbs.TitleEn,
                Code = x.ProjectWbs.Code,
                DescriptionFa = x.ProjectWbs.DescriptionFa,
                DescriptionEn = x.ProjectWbs.DescriptionEn,
                ProjectOperationId = x.ProjectOperationId,
                ProjectOperationDescription = x.ProjectOperation.Description,
                OperationInfoId = x.ProjectOperation.OperationInfoId,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<(List<GetDetailPOWbsByProjectWbsIdModel>? Data, int RowCount)> GetDetailPOWbsByProjectWbsId(
        long projectWbsId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.ProjectWbsId == projectWbsId)
            .Select(x => new GetDetailPOWbsByProjectWbsIdModel
            {
                Id = x.Id,
                ProjectWbsId = x.ProjectWbsId,
                TitleFa = x.ProjectWbs.TitleFa,
                TitleEn = x.ProjectWbs.TitleEn,
                Code = x.ProjectWbs.Code,
                DescriptionFa = x.ProjectWbs.DescriptionFa,
                DescriptionEn = x.ProjectWbs.DescriptionEn,
                ProjectOperationId = x.ProjectOperationId,
                ProjectOperationDescription = x.ProjectOperation.Description,
                OperationInfoId = x.ProjectOperation.OperationInfoId,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                PlannedStartDate = x.ProjectOperation.PlannedStartDate,
                PlannedFinishDate = x.ProjectOperation.PlannedFinishDate,

                TotalDailyVolume = x.ProjectOperation.ProjectOperationDetails
                    .SelectMany(d => d.DailyOperations)
                    .Sum(d => d.Width * d.Weight * d.Height * d.Number * d.Length),

                TotalWorkload = x.ProjectOperation.Workload,
                Status = x.ProjectOperation.ProjectOperationStatus == ProjectOperationStatus.NotStarted
                ? ProjectOperationWbsStatus.NotStarted :
                x.ProjectOperation.ProjectOperationStatus == ProjectOperationStatus.Doing &&
                x.ProjectOperation.PlannedFinishDate != null &&
                EF.Functions.DateDiffDay(x.ProjectOperation.PlannedFinishDate.Value, DateTime.Now) > 0
                ? ProjectOperationWbsStatus.Delayed

                : x.ProjectOperation.ProjectOperationStatus == ProjectOperationStatus.Doing &&
                x.ProjectOperation.PlannedFinishDate != null &&
                EF.Functions.DateDiffDay(DateTime.Now, x.ProjectOperation.PlannedFinishDate.Value) >= 0 &&
                EF.Functions.DateDiffDay(DateTime.Now, x.ProjectOperation.PlannedFinishDate.Value) <= 3
                ? ProjectOperationWbsStatus.Critical

                : ProjectOperationWbsStatus.Safe,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive,
                pOWbsPredecessors = x.ProjectOperation.PredecessorProjectOperationDependencies.Select(y => new POWbsPredecessorDependencyModel
                {
                    SuccessorId = y.SuccessorId,
                    Successor = y.Successor.OperationInfo.OperationInfoName,
                    DependencyType = y.DependencyType
                }).ToList(),
                pOWbsSuccessors = x.ProjectOperation.SuccessorProjectOperationDependencies.Select(y => new POWbsSuccessorDependencyModel
                {
                    PredecessorId = y.PredecessorId,
                    Predecessor = y.Predecessor.OperationInfo.OperationInfoName,
                    DependencyType = y.DependencyType
                }).ToList()
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<(List<GetFltrPOWbsModel>? Data, int RowCount)> GetFltrPOWbs(
        long? projectId,
        List<long>? projectOperationIds,
        List<long>? projectWbsIds,
        List<long>? projectOperationWbsIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x =>
        (projectId == null || projectId == x.ProjectWbs.ProjectId) &&
        (projectOperationWbsIds == null || projectOperationWbsIds.Contains(x.Id)) &&
        (projectOperationIds == null || projectOperationIds.Contains(x.ProjectOperationId)) &&
        (projectWbsIds == null || projectWbsIds.Contains(x.ProjectWbsId)) &&
        (string.IsNullOrEmpty(filterData) ||
        EF.Functions.Like(x.ProjectWbs.Code, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.ProjectWbs.TitleFa, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())))
            .Select(x => new GetFltrPOWbsModel
            {
                Id = x.Id,
                ProjectWbsId = x.ProjectWbsId,
                TitleFa = x.ProjectWbs.TitleFa,
                TitleEn = x.ProjectWbs.TitleEn,
                Code = x.ProjectWbs.Code,
                DescriptionFa = x.ProjectWbs.DescriptionFa,
                DescriptionEn = x.ProjectWbs.DescriptionEn,
                ProjectOperationId = x.ProjectOperationId,
                ProjectOperationDescription = x.ProjectOperation.Description,
                OperationInfoId = x.ProjectOperation.OperationInfoId,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<(List<GetPOWbsByProjectWbsIdModel>? Data, int RowCount)> GetPOWbsByProjectWbsId(
        long projectWbsId,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x =>
        projectWbsId == x.ProjectWbsId &&
        (string.IsNullOrEmpty(filterData) ||
        EF.Functions.Like(x.ProjectWbs.Code, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.ProjectWbs.TitleFa, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern())))
            .Select(x => new GetPOWbsByProjectWbsIdModel
            {
                Id = x.Id,
                ProjectOperationId = x.ProjectOperationId,
                ProjectOperationDescription = x.ProjectOperation.Description,
                OperationInfoId = x.ProjectOperation.OperationInfoId,
                OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }
}