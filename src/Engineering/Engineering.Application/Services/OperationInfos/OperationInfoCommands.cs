using Engineering.Application.Services.OperationInfos.Models.DeleteOperationInfoAction;
using Engineering.Domain.Entities.OperationInfos;
using Action = Engineering.Domain.Entities.Actions.Action;

namespace Engineering.Application.Services.OperationInfos;

public partial class OperationInfoLogic : IOperationInfoLogic
{
    private async Task<Result<List<OperationInfoAction>?>> CreateOperationInfoActionsHandler(
         List<Action> actions, OperationInfo oInfo, decimal? price, CT ct)
    {
        try
        {
            List<OperationInfoAction> entities = [];
            foreach (var item in actions)
            {
                var entity = await _operationInfoActionRepository.Create(new OperationInfoAction(
                oInfo,
                item,
                price), ct);

                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoAction>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<OperationInfoAction?>> CreateOperationInfoActionHandler(
         Action action, OperationInfo oInfo, decimal? price, CT ct)
    {
        try
        {
            var entity = await _operationInfoActionRepository.Create(new OperationInfoAction(
            oInfo,
            action,
            price), ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoAction>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<OperationInfoAction?>> DeleteOperationInfoActionHandler(
         DeleteOperationInfoActionRequest request, CT ct)
    {
        try
        {
            var entity = await _operationInfoActionRepository.GetOperationInfoActionByActionId(request.OperationInfoId, request.ActionId, ct);
            if (entity is null)
                return Result.Failure<OperationInfoAction>(OperationInfoErrors.OperationInfoActionWithIdNotFound);

            if (entity.IsDeleted)
                return Result.Failure<OperationInfoAction>(OperationInfoErrors.OperationInfoActionIsDeleted);

            entity.SoftDelete();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoAction>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<OperationInfoAction>?>> DeleteOperationInfoActionsHandler(
         List<long> ids, CT ct)
    {
        try
        {
            var entities = await _operationInfoActionRepository.GetOperationInfoActionByActionId(ids, ct);
            if (entities is null)
                return Result.Failure<List<OperationInfoAction>>(OperationInfoErrors.OperationInfoActionWithIdNotFound);

            foreach (var entity in entities)
            {
                if (entity.IsDeleted)
                    return Result.Failure<List<OperationInfoAction>>(OperationInfoErrors.OperationInfoActionIsDeleted);

                entity.SoftDelete();
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoAction>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<OperationInfoAction>?>> UpdateOperationInfoActionsHandler(
    List<(OperationInfoAction entity, decimal price)> items, CT ct)
    {
        try
        {
            foreach (var (entity, price) in items)
            {
                if (entity.IsDeleted)
                    return Result.Failure<List<OperationInfoAction>>(
                        OperationInfoErrors.OperationInfoActionIsDeleted);

                entity.Update(price);
            }

            return items.Select(x => x.entity).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoAction>>(SharedErrors.UnknownError);
        }
    }

}