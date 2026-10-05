using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProductsByProjectId;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectProductRepository : IBaseRepository<ProjectProduct>
{
    Task<ProjectProduct?> GetProjectProductById(
        long id,
        CT ct);

    Task<ProjectProduct?> GetById(
        long id, CT ct);

    Task<List<GetProjectProductModel>?> GetPPByProjectId(
        long projectId,
        ProjectProductType type,
        CT ct);

    Task<List<long>?> GetUsedPPGroupByProjectId(
        long projectId,
        CT ct);

    Task<List<long>?> GetUsedPPCategoryByProjectId(
        long projectId,
        CT ct);

    Task<List<ProjectProduct>?> GetProductByProjectId(
        long id,
        CT ct);

    Task<List<ProjectProduct>?> GetProjectProductByIds(
        List<long> ids,
        CT ct);

    Task<List<GetProjectProductByProjectIdModel>?> GetProjectProductByProjectId(
        long id,
        CT ct);

    Task<List<GetProjectCategoryProductByProjectIdModel>?> GetProjectCategoryProductByProjectId(
        long id,
        CT ct);

    Task<ProjectProduct?> GetProjectProductByProjectAndProductGroup(
        long projectId,
        long productGroupId,
        CT ct);

    Task<List<GetProductsByProjectIdModel>?> GetProductsByProjectId(
        long id,
        CT ct);

    Task<List<ProjectProduct>?> GetProductGoodsSupplyByProjectId(
        long projectId,
        CT ct);

    Task<List<ProjectProduct>?> GetProductGoodsSupplyByGroupIds(
        List<long> groupIds,
        CT ct);

    Task<List<ProjectProduct>> GetTotalGroupSupplyByProjectIdAsync(
    List<long>? projectIds,
    List<long>? productGroupIds,
    CT ct);

    Task<List<ProjectProduct>> GetTotalCategorySupplyByProjectIdAsync(
    List<long>? projectIds,
    List<long>? productCategoryIds,
    CT ct);

    Task<List<ProjectProduct>?> GetProductByProjectIdAndGroupId(
        long? projectId,
        long? productGroupId,
        long? productCategoryId,
        CT ct);
}