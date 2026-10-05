using Engineering.Application.Services.GoodsManagerAssignments.Contracts.CreateGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.UpdateGoodsManagerAssignment;
using Engineering.Domain.Entities.GoodsManager;
using Engineering.Domain.Errors.RequestGoodsSupplies;

namespace Engineering.Application.Services.GoodsManagerAssignments;

public partial class GoodsManagerAssignmentLogic
{
    private async Task<Result<bool>> CreateGoodsManagerAssignmentCommand(
        CreateGoodsManagerAssignmentRequest request, CT ct)
    {
        try
        {
            foreach (var item in request.ProductIds)
            {
                // Enforce rules: prevent duplicate assignment
                var isDuplicate = await _repository.ExistsDuplicate(
                    request.OrganizationId, item, null, ct);
                if (isDuplicate)
                    return Result.Failure<bool>(GoodsManagerAssignmentErrors.DuplicateAssignment);

                // Enforce rules: verify IDs actually exist
                var existenceCheck = await ValidateReferencesExist(
                    request.OrganizationId, item, ct);

                if (existenceCheck.IsBad())
                    return Result.Failure<bool>(existenceCheck.Error!);

                // Mutate domain
                var entity = GoodsManagerAssignment.Create(
                    request.OrganizationId, item);

                // Repo
                var result = await _repository.Create(entity, ct);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> UpdateGoodsManagerAssignmentCommand(
        UpdateGoodsManagerAssignmentRequest request, CT ct)
    {
        try
        {
            // Load
            var entities = await _repository.GetByOrganizationId(request.OrganizationId, ct);
            if (entities is null)
                return Result.Failure<bool>(GoodsManagerAssignmentErrors.NotFound);

            if (request.DeleteProductIds is not null && request.DeleteProductIds.Count > 0)
                foreach (var productId in request.DeleteProductIds)
                    if (entities.Any(x => x.ProductId == productId))
                    {
                        entities.First(x => x.ProductId == productId).SetIsDeleted();
                        await _repository.Update(entities.First(x => x.ProductId == productId));
                    }

            if (request.CreateProductIds is not null && request.CreateProductIds.Count > 0)
            {
                foreach (var productId in request.CreateProductIds)
                {
                    if (entities.Any(x => request.CreateProductIds.Contains(x.ProductId)))
                        continue;

                    var entity = GoodsManagerAssignment.Create(
                    request.OrganizationId, productId);

                    var result = await _repository.Create(entity, ct);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteGoodsManagerAssignmentCommand(
        long organizationId, CT ct)
    {
        try
        {
            // Load
            var entities = await _repository.GetByOrganizationId(organizationId, ct);
            if (entities is null)
                return Result.Failure<bool>(GoodsManagerAssignmentErrors.NotFound);

            foreach (var entity in entities)
            {
                //HTODO validate for open RGS 

                // Mutate domain (soft delete)
                entity.SetIsDeleted();

                // ⬇️ Fixed: Removed 'ct' argument to match IBaseRepository signature
                await _repository.Update(entity);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

}