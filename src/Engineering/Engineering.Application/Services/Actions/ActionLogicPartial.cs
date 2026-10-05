using Engineering.Application.Services.Actions.Contracts.CreateAction;
using Engineering.Application.Services.Actions.Contracts.GetFilteredActions;
using Engineering.Application.Services.Actions.Contracts.GetsActiveActions;
using Engineering.Application.Services.Actions.Contracts.UpdateAction;
using Engineering.Domain.Errors.Actions;
using Action = Engineering.Domain.Entities.Actions.Action;
namespace Engineering.Application.Services.Actions;

public partial class ActionLogic : IActionLogic
{
    private async Task<Result<Action?>> CreateActionHandler(
         CreateActionRequest request, long companyId, CT ct)
    {
        try
        {
            var entity = await _repository.Create(new Action(
                request.Name,
                request.Code,
                companyId), ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Action>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<Action?>> UpdateActionHandler(
         Action entity, UpdateActionRequest request, CT ct)
    {
        try
        {
            entity.Update(
                request.Name,
                request.Code);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Action?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteActionHandler(
         Action entity, CT ct)
    {
        try
        {
            entity.SoftDelete();

            await _repository.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<string?>> ActionCodeCreatorHandle(CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string?>(SharedErrors.UnknownError);
        }
    }


    public async Task<Result<Action?>> GetActionByIdHandler(
       long id, CT ct)
    {
        try
        {
            var item = await _repository.GetActionById(id, ct);
            return item ?? Result.Failure<Action?>(ActionErrors.ActionWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Action?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetsActiveActionsResponse>> GetsActiveActionsHandler(
    GetsActiveActionsRequest request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveActions(
                request.FilterData,
                request.Code,
                request.Name,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = new GetsActiveActionsResponse(
                result.Adapt<List<GetsActiveActionsModel>>(),
                result.Count);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsActiveActionsResponse>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<GetFilteredActionsResponse>> GetFilteredActionsHandler(
    GetFilteredActionsRequest request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredActions(
                request.FilterData,
                request.Code,
                request.Name,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = new GetFilteredActionsResponse(
                result.Adapt<List<GetFilteredActionsModel>>(),
                result.Count);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFilteredActionsResponse>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<Action?>> GetActionByCodeHandle(
        string billOfLadingCode, CT ct)
    {
        try
        {
            var item = await _repository.GetActionByCode(
                billOfLadingCode, ct);

            return item ?? Result.Failure<Action?>(ActionErrors.ActionWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Action?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<Action?>> GetActionByIdHandle(
        long id, CT ct)
    {
        try
        {
            var item = await _repository.GetActionById(id, ct);
            return item ?? Result.Failure<Action?>(ActionErrors.ActionWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Action?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<Action?>> GetActionByNameHandle(
        string billOfLadingName, CT ct)
    {
        try
        {
            var item = await _repository.GetActionByName(billOfLadingName, ct);
            return item ?? Result.Failure<Action?>(ActionErrors.ActionWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Action?>(SharedErrors.UnknownError);
        }
    }
}

