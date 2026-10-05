using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.SubProjects.Models;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.Histories;
using System.Reflection;

namespace Engineering.Persistence.Repositories.Projects;

public class SubProjectRepository : BaseRepository<EngineeringDBContext, SubProject>, ISubProjectRepository
{
    private readonly EngineeringDBContext _context;

    public SubProjectRepository(
        EngineeringDBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SubProject?> GetSubProjectById(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
            .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<SubProject?> GetSubProjectByCode(
        string code, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
            .Where(oo => oo.SubProjectCode == code);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<SubProjectDetails?> GetSubProjectDetailsById(
        long id, CT ct)
    {
        var query = DetailsQuery()
            .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<SubProjectDetails?> GetSubProjectDetailsByCode(
        string code, CT ct)
    {
        var query = DetailsQuery()
            .Where(oo => oo.Code == code);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<SubProjectDetails> Data, int RowCount)> GetSubProjects(
        long projectId,
        string? filterData,
        SubProjectStatus? status,
        SubProjectType? type,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DetailsQuery()
            .Where(oo => oo.ProjectId == projectId);

        if (status.HasValue)
            query = query.Where(oo => oo.Status == status);

        if (type.HasValue)
            query = query.Where(oo => oo.Type == type);

        if (!string.IsNullOrWhiteSpace(filterData))
            query = query.Where(oo =>
                oo.Code.Contains(filterData) ||
                oo.Name.Contains(filterData));

        query = query.OrderByDescending(oo => oo.SequenceNumber);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<long> AllocateSequence(
        long projectId, CT ct)
    {
        const int maxRetryCount = 3;

        for (var retryCount = 0; retryCount < maxRetryCount; retryCount++)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(ct);
            var sequences = _context.Set<SubProjectSequence>();
            var affectedRows = await sequences
                .Where(oo => oo.ProjectId == projectId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(oo => oo.LastSequenceNumber,
                        oo => oo.LastSequenceNumber + 1), ct);

            if (affectedRows == 0)
            {
                var sequence = new SubProjectSequence(projectId, 1);
                await sequences.AddAsync(sequence, ct);

                try
                {
                    await _context.SaveChangesAsync(ct);
                }
                catch (DbUpdateException) when (retryCount < maxRetryCount - 1)
                {
                    await transaction.RollbackAsync(ct);
                    _context.Entry(sequence).State = EntityState.Detached;
                    continue;
                }
            }

            var lastSequenceNumber = await sequences
                .Where(oo => oo.ProjectId == projectId)
                .Select(oo => oo.LastSequenceNumber)
                .SingleAsync(ct);

            await transaction.CommitAsync(ct);
            return lastSequenceNumber;
        }

        throw new DbUpdateException("Could not allocate the next SubProject sequence number.");
    }

    public async Task<bool> HasAccess(
        long projectId,
        long userId,
        List<long> thirdPartyIds,
        CT ct)
    {
        return await _context.Set<Project>()
            .AnyAsync(oo => oo.Id == projectId &&
                (oo.CreatorId == userId ||
                 (oo.ProjectManager.HasValue && thirdPartyIds.Contains(oo.ProjectManager.Value)) ||
                 (oo.PlanningAssistant.HasValue && thirdPartyIds.Contains(oo.PlanningAssistant.Value)) ||
                 (oo.SupervisorEngineer.HasValue && thirdPartyIds.Contains(oo.SupervisorEngineer.Value)) ||
                 (oo.Advisor.HasValue && thirdPartyIds.Contains(oo.Advisor.Value)) ||
                 oo.ProjectThirdParties.Any(item =>
                     thirdPartyIds.Contains(item.AuthorizedThirdPartyId))), ct);
    }

    public async Task<bool> HasDependencies(long id, CT ct)
    {
        var dependencies = _context.Model
            .GetEntityTypes()
            .Where(oo => oo.ClrType != typeof(SubProjectHistory))
            .SelectMany(oo => oo.GetForeignKeys()
                .Where(item => item.PrincipalEntityType.ClrType == typeof(SubProject))
                .Select(item => new
                {
                    oo.ClrType,
                    ForeignKeyProperty = item.Properties.SingleOrDefault()?.Name,
                    HasIsDeletedProperty = oo.FindProperty(nameof(SubProject.IsDeleted)) is not null
                }))
            .Where(oo => oo.ForeignKeyProperty is not null)
            .ToList();

        var method = GetType().GetMethod(
            nameof(HasDependency),
            BindingFlags.Instance | BindingFlags.NonPublic)!;

        foreach (var dependency in dependencies)
        {
            var genericMethod = method.MakeGenericMethod(dependency.ClrType);
            var task = (Task<bool>)genericMethod.Invoke(this,
                [id, dependency.ForeignKeyProperty!, dependency.HasIsDeletedProperty, ct])!;

            if (await task)
                return true;
        }

        return false;
    }

    public async Task<bool> ProjectHasSubProjects(
        long projectId, CT ct)
    {
        return await DbSet.AnyAsync(oo => oo.ProjectId == projectId, ct);
    }

    public async Task<List<long>> GetAllowedManagerIds(long projectId, CT ct)
    {
        var project = await _context.Set<Project>()
            .Where(oo => oo.Id == projectId)
            .Select(oo => new
            {
                oo.ProjectManager,
                oo.PlanningAssistant,
                oo.SupervisorEngineer,
                oo.Advisor,
                ThirdParties = oo.ProjectThirdParties
                    .Select(item => item.AuthorizedThirdPartyId)
                    .ToList()
            }).FirstOrDefaultAsync(ct);

        if (project is null)
            return [];

        return new long?[]
            {
                project.ProjectManager,
                project.PlanningAssistant,
                project.SupervisorEngineer,
                project.Advisor
            }
            .Where(oo => oo.HasValue)
            .Select(oo => oo!.Value)
            .Concat(project.ThirdParties)
            .Distinct()
            .ToList();
    }

    public async Task AddHistory(
        SubProjectHistory history, CT ct)
    {
        await _context.Set<SubProjectHistory>().AddAsync(history, ct);
    }

    private IQueryable<SubProjectDetails> DetailsQuery()
    {
        return DbSet.Select(oo => new SubProjectDetails
        {
            Id = oo.Id,
            ProjectId = oo.ProjectId,
            Code = oo.SubProjectCode,
            SequenceNumber = oo.SequenceNumber,
            Name = oo.Name,
            Type = oo.Type,
            Description = oo.Description,
            ManagerId = oo.ManagerId,
            StartDate = oo.StartDate,
            EndDate = oo.EndDate,
            Status = oo.Status,
            Created = oo.Created,
            CreatorId = oo.CreatorId,
            Updated = oo.Updated,
            UpdaterId = oo.UpdaterId,
            ParentProject = new ParentProjectDetails
            {
                Id = oo.Project.Id,
                Code = oo.Project.ProjectCode,
                Name = oo.Project.ProjectName,
                EmployerId = oo.Project.EmployerId,
                CompanyId = oo.Project.CompanyId,
                OrganizationId = oo.Project.OrganizationId,
                CityId = oo.Project.CityId
            }
        });
    }

    private Task<bool> HasDependency<TEntity>(
        long id,
        string foreignKeyProperty,
        bool hasIsDeletedProperty,
        CT ct) where TEntity : class
    {
        var query = _context.Set<TEntity>()
            .Where(oo => EF.Property<long>(oo, foreignKeyProperty) == id);

        if (hasIsDeletedProperty)
            query = query.Where(oo => !EF.Property<bool>(oo, nameof(SubProject.IsDeleted)));

        return query.AnyAsync(ct);
    }
}
