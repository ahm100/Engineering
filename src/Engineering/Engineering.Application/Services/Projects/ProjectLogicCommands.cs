using Engineering.Application.Services.Projects.Models.AddAuthorizedThirdPartyToProject;
using Engineering.Application.Services.Projects.Models.CreateProjectProduct;
using Engineering.Application.Services.Projects.Models.DeleteProjectProduct;
using Engineering.Application.Services.Projects.Models.DeleteProjectThirdParty;
using Engineering.Application.Services.Projects.Models.UpdateProjectProduct;
using Engineering.Application.Services.Projects.Models.UpdateProjectProductQuantities;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Application.Services.Projects;

partial class ProjectLogic
{
    public async Task<Result<List<ProjectProduct>?>> CreateProjectProductCommand(
       CreateProjectProductRequest request, Project project, long? companyId, CT ct)
    {
        try
        {
            List<ProjectProduct> response = [];
            foreach (var product in request.Products)
            {
                var create = await _ppRepo.Create(new ProjectProduct(project, product.RequestQuantity, product.ProductGroupId, product.ProductCategoryId, product.TolerancePercentage, product.ProductType, product.IsActive, product.DefaultManagerSet, companyId), ct);
                response.Add(create);
            }
            return response ?? Result.Failure<List<ProjectProduct>?>(SharedErrors.UnknownError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectProduct>?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<ProjectThirdParty>?>> CreateProjectThirdPartiesCommand(
       CreateProjectThirdPartyRequest request, Project project, CT ct)
    {
        try
        {
            List<ProjectThirdParty> response = [];
            var ids = request.ThirdPartyIds.Distinct().ToList();
            var thirdParties = await _thirdPartyRepo.GetByIds(ids, ct);
            if (thirdParties is null || thirdParties.Count != ids.Count)
                return Result.Failure<List<ProjectThirdParty>>(ProjectErrors.ThirdPartiesWithIdsNotFound);
            foreach (var id in ids)
            {
                var create = await _projectThirdPartyRepository.Create(new ProjectThirdParty(project, id), ct);
                response.Add(create);
            }
            return response ?? Result.Failure<List<ProjectThirdParty>?>(SharedErrors.UnknownError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectThirdParty>?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<ProjectProduct>?>> UpdateProjectProductCommand(
       UpdateProjectProductRequest request, CT ct)
    {
        try
        {
            var ids = request.Products
                .Where(x => x.ProjectProductId.HasValue)
                .Select(x => x.ProjectProductId!.Value)
                .ToList();

            var entities = await _ppRepo.GetProjectProductByIds(ids, ct);
            foreach (var update in request.Products)
            {
                var entity = entities.FirstOrDefault(x => x.Id == update.ProjectProductId);

                if (entity.IsDeleted)
                    return Result.Failure<List<ProjectProduct>?>(ProjectErrors.ProjectProductWithIdsIsDeleted)!;

                entity.Update(update.RequestQuantity, update.DefaultManagerSet, update.TolerancePercentage);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectProduct>?>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<ProjectProduct?>> UpdateProjectProductQuantityCommand(
       UpdateProjectProductQuantitiesRequest request, CT ct)
    {
        try
        {
            var entity = await _ppRepo.GetProjectProductById(request.ProjectProductId, ct);
            if (entity is null)
                return Result.Failure<ProjectProduct>(ProjectErrors.ProjectProductWithIdsNotFound);
            entity.UpdateQuantities(request.CompletedQuantity, request.InProgressQuantity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectProduct?>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<List<ProjectProduct>?>> DeleteProjectProductCommand(
       DeleteProjectProductRequest request, CT ct)
    {
        try
        {
            var entities = await _ppRepo.GetProjectProductByIds(request.Ids, ct)!;
            var groupIds = entities!.NullListed(x => x.ProductGroupId);
            var requestGoods = await _requestGoodsSupplyRepo.DoesProjectProductHaveRequestGoodsSupply(groupIds, ct);
            if (requestGoods == true)
                return Result.Failure<List<ProjectProduct>>(ProjectErrors.CanNotDeletePP);
            foreach (var entity in entities)
                entity.SoftDelete();
            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectProduct>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<bool?>> DeleteProjectThirdPartyCommand(
       DeleteProjectThirdPartyRequest request, CT ct)
    {
        try
        {
            var entities = await _projectThirdPartyRepository.GetProjectThirdPartyByIds(request.Ids, ct)!;
            if (entities is null || entities.Count < 1)
                return Result.Failure<bool?>(ProjectErrors.PThirdPartyNotFound);
            foreach (var entity in entities)
                entity.SoftDelete();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<ProjectProduct>?>> UpdateProjectProductQuantitiesCommand(
       UpdateProjectProductRequest request, CT ct)
    {
        try
        {
            var ids = request.Products
                .Where(x => x.ProjectProductId.HasValue)
                .Select(x => x.ProjectProductId!.Value)
                .ToList();

            var entities = await _ppRepo.GetProjectProductByIds(ids, ct);
            foreach (var update in request.Products)
            {
                var entity = entities.FirstOrDefault(x => x.Id == update.ProjectProductId);

                if (entity.IsDeleted)
                    return Result.Failure<List<ProjectProduct>?>(ProjectErrors.ProjectProductWithIdsIsDeleted)!;

                entity.Update(update.RequestQuantity, null, update.TolerancePercentage);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectProduct>?>(SharedErrors.UnknownError)!;
        }
    }
}
