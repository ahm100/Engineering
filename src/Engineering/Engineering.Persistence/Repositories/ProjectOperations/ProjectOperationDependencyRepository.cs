using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public class ProjectOperationDependencyRepository : BaseRepository<EngineeringDBContext, ProjectOperationDependency>, IProjectOperationDependencyRepository
{
    public ProjectOperationDependencyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectOperationDependency?> GetById(
        long Id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == Id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<GetDependencyByPOIdModel>? Data, int RowCount)> GetDependencyByPOId(
        long projectOperationId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x =>
            (projectOperationId == x.PredecessorId || projectOperationId == x.SuccessorId))
            .Select(x => new GetDependencyByPOIdModel
            {
                Id = x.Id,
                PredecessorId = x.PredecessorId,
                PredecessorName = x.Predecessor.OperationInfo.OperationInfoName,
                SuccessorId = x.SuccessorId,
                SuccessorName = x.Successor.OperationInfo.OperationInfoName,
                LagDays = x.LagDays,
                DependencyType = x.DependencyType
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }



    public async Task<List<GetPODependenciesPredecessorModel>?> GetPOPredecessorFullRequirements(
    long projectOperationId,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.SuccessorId == projectOperationId);

        var data = await query
            .Select(x => new GetPODependenciesPredecessorModel
            {
                PredecessorId = x.PredecessorId,
                PredecessorName = x.Predecessor.OperationInfo.OperationInfoName,
                LagDays = x.LagDays,
                StartDate = x.Successor.PlannedStartDate,
                EndDate = x.Successor.PlannedFinishDate,
                DependencyType = x.DependencyType
            })
            .ToListAsync(ct);

        return data;
    }

    public async Task<List<GetPODependenciesSuccessorModel>?> GetPOSuccessorFullRequirements(
    long projectOperationId,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.PredecessorId == projectOperationId);

        var data = await query
            .Select(x => new GetPODependenciesSuccessorModel
            {
                SuccessorId = x.SuccessorId,
                SuccessorName = x.Successor.OperationInfo.OperationInfoName,
                LagDays = x.LagDays,
                StartDate = x.Successor.PlannedStartDate,
                EndDate = x.Successor.PlannedFinishDate,
                DependencyType = x.DependencyType
            })
            .ToListAsync(ct);

        return data;
    }

    public async Task<List<GetSuccessorsByPOIdModel>?> GetPOSuccessorRequirements(
    long projectOperationId,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.PredecessorId == projectOperationId &&
                (x.DependencyType == ProjectOperationDependencyType.FS ||
                 x.DependencyType == ProjectOperationDependencyType.SF));

        var data = await query
            .Select(x => new GetSuccessorsByPOIdModel
            {
                SuccessorId = x.SuccessorId,
                SuccessorName = x.Successor.OperationInfo.OperationInfoName,
                LagDays = x.LagDays,
                StartDate = x.Successor.PlannedStartDate,
                EndDate = x.Successor.PlannedFinishDate,
                DependencyType = x.DependencyType
            })
            .ToListAsync(ct);

        return data;
    }

    public async Task<List<GetPredecessorByPOIdModel>?> GetPOPredecessorRequirements(
    long projectOperationId,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.SuccessorId == projectOperationId &&
                (x.DependencyType == ProjectOperationDependencyType.FS ||
                 x.DependencyType == ProjectOperationDependencyType.SF));

        var data = await query
            .Select(x => new GetPredecessorByPOIdModel
            {
                PredecessorId = x.PredecessorId,
                PredecessorName = x.Predecessor.OperationInfo.OperationInfoName,
                LagDays = x.LagDays,
                StartDate = x.Successor.PlannedStartDate,
                EndDate = x.Successor.PlannedFinishDate,
                DependencyType = x.DependencyType
            })
            .ToListAsync(ct);

        return data;
    }

    public async Task<(List<GetFltrDependencyModel>? Data, int RowCount)> GetFltrDependency(
        List<long>? projectOperationIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x =>
            (String.IsNullOrEmpty(filterData) ||
            EF.Functions.Like(x.Successor.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Successor.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Predecessor.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Predecessor.OperationInfo.OperationInfoName, filterData.MakeLikePattern())) &&
            (projectOperationIds == null ||
            (projectOperationIds.Contains(x.SuccessorId) || projectOperationIds.Contains(x.PredecessorId))))
            .Select(x => new GetFltrDependencyModel
            {
                Id = x.Id,
                PredecessorId = x.PredecessorId,
                PredecessorName = x.Predecessor.OperationInfo.OperationInfoName,
                SuccessorId = x.SuccessorId,
                SuccessorName = x.Successor.OperationInfo.OperationInfoName,
                LagDays = x.LagDays,
                DependencyType = x.DependencyType
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<List<ProjectOperationDependency>?> GetByPOId(
        long POId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Successor)
                .ThenInclude(x => x.OperationInfo)
            .Include(x => x.Predecessor)
                .ThenInclude(x => x.OperationInfo)
            .Where(oo => oo.SuccessorId == POId || oo.PredecessorId == POId);

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<bool> HaveDependency(
        long successorId, long predecessorId, CT ct)
    {
        return await DbSet
            .AnyAsync(x => (x.SuccessorId == successorId && x.PredecessorId == predecessorId) ||
            (x.PredecessorId == successorId && x.SuccessorId == predecessorId), ct);

    }

    public async Task<List<long>> GetAffectedSuccessorIds(
    long operationId,
    CT ct)
    {
        var result = new HashSet<long>();
        var queue = new Queue<long>();

        queue.Enqueue(operationId);


        while (queue.Count > 0)
        {
            var id = queue.Dequeue();

            var successorIds = await DbSet
                .Where(x => x.PredecessorId == id)
                .Select(x => x.SuccessorId)
                .ToListAsync(ct);


            foreach (var successorId in successorIds)
            {
                if (result.Add(successorId))
                {
                    queue.Enqueue(successorId);
                }
            }
        }


        return result.ToList();
    }
}