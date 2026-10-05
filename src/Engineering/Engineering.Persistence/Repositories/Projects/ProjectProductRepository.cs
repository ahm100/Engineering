using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProductsByProjectId;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectProductRepository : BaseRepository<EngineeringDBContext, ProjectProduct>, IProjectProductRepository
{
    public ProjectProductRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectProduct?> GetProjectProductById(
        long id, CT ct)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<ProjectProduct?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.RequestGoodsSupplyTypeDetails)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<List<GetProjectProductModel>?> GetPPByProjectId(
        long projectId,
        ProjectProductType type,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Project)

            .Where(p =>
                p.ProjectId == projectId &&
                p.ProjectProductType == type &&
                !p.IsDeleted
                )
            .Select(item => new GetProjectProductModel()
            {
                Id = item.Id,
                ProductGroupId = item.ProductGroupId ?? item.ProductCategoryId!.Value,
                RequestQuantity = item.RequestQuantity,
                RemainingQuantity = item.RemainingQuantity,
                CompletedQuantity = item.CompletedQuantity,
                InProgressQuantity = item.InProgressQuantity,
                DefaultManagerSet = item.DefaultManagerSet,
                TolerancePercentage = item.TolerancePercentage,
                IsActive = item.IsActive,
            });

        return await query.ToListAsync(ct);
    }

    public async Task<List<long>?> GetUsedPPGroupByProjectId(
        long projectId,
        CT ct)
    {
        return await DbSet.Where(p =>
            p.ProjectId == projectId &&
            p.ProjectProductType == ProjectProductType.ProductGroup &&
            !p.IsDeleted).Select(x => x.ProductGroupId!.Value).ToListAsync(ct);
    }

    public async Task<List<long>?> GetUsedPPCategoryByProjectId(
        long projectId,
        CT ct)
    {
        return await DbSet.Where(p =>
            p.ProjectId == projectId &&
            p.ProjectProductType == ProjectProductType.Category &&
            !p.IsDeleted).Select(x => x.ProductCategoryId!.Value).ToListAsync(ct);
    }

    public async Task<List<ProjectProduct>?> GetProductByProjectId(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.RequestGoodsSupplyDetails)
            .Where(p =>
                p.ProjectId == id &&
                !p.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<ProjectProduct>?> GetProductByProjectIdAndGroupId(
        long? projectId,
        long? productGroupId,
        long? productCategoryId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.RequestGoodsSupplyDetails)
                .ThenInclude(c => c.RequestGoodsSupplyProduct)
                    .ThenInclude(c => c.RequestGoodsSupplyManagements)
            .Where(p => p.ProjectId == projectId);

        if (productGroupId.HasValue)
        {
            query = query.Where(p => p.ProductGroupId == productGroupId.Value);
        }

        if (productCategoryId.HasValue)
        {
            query = query.Where(p => p.ProductCategoryId == productCategoryId.Value);
        }

        return await query.ToListAsync(ct);
    }


    public async Task<List<ProjectProduct>?> GetProjectProductByIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet.Where(p => ids.Contains(p.Id));
        return await query.ToListAsync(ct);
    }

    public async Task<List<GetProjectProductByProjectIdModel>?> GetProjectProductByProjectId(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.ProjectId == id &&
            p.ProjectProductType == ProjectProductType.ProductGroup)

            .Select(x => new GetProjectProductByProjectIdModel
            {
                Id = x.Id,
                ProductGroupId = x.ProductGroupId,
                RequestQuantity = x.RequestQuantity,
                InProgressQuantity = x.InProgressQuantity,
                RemainingQuantity = x.RemainingQuantity,
                DefaultManagerSet = x.DefaultManagerSet,
                CompletedQuantity = x.CompletedQuantity,
                ProductType = x.ProjectProductType
            });

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<List<GetProjectCategoryProductByProjectIdModel>?> GetProjectCategoryProductByProjectId(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.ProjectId == id &&
            p.ProjectProductType == ProjectProductType.Category)

            .Select(x => new GetProjectCategoryProductByProjectIdModel
            {
                Id = x.Id,
                ProductCategoryId = x.ProductCategoryId,
                RequestQuantity = x.RequestQuantity,
                InProgressQuantity = x.InProgressQuantity,
                RemainingQuantity = x.RemainingQuantity,
                CompletedQuantity = x.CompletedQuantity,
                DefaultManagerSet = x.DefaultManagerSet,
                ProductType = x.ProjectProductType
            });

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<ProjectProduct?> GetProjectProductByProjectAndProductGroup(
        long projectId,
        long productGroupId,
        CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(p => p.ProjectId == projectId && p.ProductGroupId == productGroupId);
    }

    public async Task<List<GetProductsByProjectIdModel>?> GetProductsByProjectId(
        long projectId,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.ProjectId == projectId)

            .Select(x => new GetProductsByProjectIdModel
            {
                GroupId = x.ProductGroupId,
                TotalEstimatedCount = x.RequestQuantity,
                TotalRequestedCount = x.InProgressQuantity,
                TotalSupplyCount = x.CompletedQuantity + x.InProgressQuantity,
                TotalDifferenceCount = x.RequestQuantity - x.CompletedQuantity - x.RemainingQuantity,
                TotalRemainedCount = x.RemainingQuantity,
                RequestedCount = x.CompletedQuantity + x.InProgressQuantity,
                SupplyCount = x.CompletedQuantity,
            });

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<List<ProjectProduct>?> GetProductGoodsSupplyByProjectId(
        long projectId,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.ProjectId == projectId);

        return await query.ToListAsync();
    }

    public async Task<List<ProjectProduct>?> GetProductGoodsSupplyByGroupIds(
        List<long> groupIds,
        CT ct)
    {
        var nullableGroupIds = groupIds.Select(id => (long?)id).ToList();

        var query = DbSet
            .Where(p => nullableGroupIds.Contains(p.ProductGroupId));

        return await query.ToListAsync();
    }

    public async Task<List<ProjectProduct>> GetTotalGroupSupplyByProjectIdAsync(
    List<long>? projectIds,
    List<long>? productGroupIds,
    CT ct)
    {
        return await DbSet
            .Where(c =>
                c.ProductGroupId.HasValue &&
                c.ProjectProductType == ProjectProductType.ProductGroup &&
                !c.IsDeleted &&
                (productGroupIds == null || productGroupIds.Contains(c.ProductGroupId.Value)) &&
                (projectIds == null || projectIds.Contains(c.ProjectId))
            )
            .ToListAsync(ct);
    }

    public async Task<List<ProjectProduct>> GetTotalCategorySupplyByProjectIdAsync(
    List<long>? projectIds,
    List<long>? productCategoryIds,
    CT ct)
    {
        return await DbSet
            .Where(c =>
                c.ProductCategoryId.HasValue &&
                c.ProjectProductType == ProjectProductType.Category &&
                !c.IsDeleted &&
                (productCategoryIds == null || productCategoryIds.Contains(c.ProductCategoryId.Value)) &&
                (projectIds == null || projectIds.Contains(c.ProjectId))
            )
            .ToListAsync(ct);
    }

}