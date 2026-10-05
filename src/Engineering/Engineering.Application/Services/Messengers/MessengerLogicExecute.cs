using Engineering.Application.Services.GetFltrMessengerChannels.Contracts.GetFltrMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;
using Engineering.Application.Services.Messengers.Contracts.CreateMessenger;
using Engineering.Application.Services.Messengers.Contracts.CreateMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessenger;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessenger;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessengerChannel;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Errors.Messengers;

namespace Engineering.Application.Services.Messengers;

public partial class MessengerLogic
{
    public async Task<Result<Messenger?>> CreateMessengerCommand(
        CreateMessengerRequest request,
        long companyId, CT ct)
    {
        try
        {
            var create = new Messenger(request.Type, request.TargetType, request.TargetId, request.Description, request.IsActive, companyId);
            await _messengerRepository.Create(create, ct);
            await _unitOfWork.CommitAsync(ct);
            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Messenger>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<MessengerChannel?>> CreateMessengerChannelCommand(
        CreateMessengerChannelRequest request,
        long companyId, CT ct)
    {
        try
        {
            var messenger = await _messengerRepository.GetMessenger(request.MessengerId, ct);
            if (messenger is null)
                return Result.Failure<MessengerChannel>(MessengerErrors.NotFound);
            var create = new MessengerChannel(messenger, request.Type, request.ChatId, request.ChatUrl, request.ChatName);
            await _messengerChannelRepository.Create(create, ct);
            await _unitOfWork.CommitAsync(ct);
            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MessengerChannel>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<UpdateMessengerResponse?>> UpdateMessengerCommand(
        UpdateMessengerRequest request, CT ct)
    {
        try
        {
            var entity = await _messengerRepository.GetMessenger(request.Id, ct);
            if (entity is null)
                return Result.Failure<UpdateMessengerResponse>(MessengerErrors.NotFound);

            entity.Update(request.Type, request.TargetType, request.TargetId, request.Description, request.IsActive);
            await _messengerRepository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new UpdateMessengerResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateMessengerResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<UpdateMessengerChannelResponse?>> UpdateMessengerChannelCommand(
        UpdateMessengerChannelRequest request, CT ct)
    {
        try
        {
            var entity = await _messengerChannelRepository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<UpdateMessengerChannelResponse>(MessengerErrors.NotFound);

            entity.Update(request.Type, request.ChatId, request.ChatUrl, request.ChatName);
            await _messengerChannelRepository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new UpdateMessengerChannelResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateMessengerChannelResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DeleteMessengerResponse?>> DeleteMessengerCommand(
        DeleteMessengerRequest request, CT ct)
    {
        try
        {
            var entity = await _messengerRepository.GetMessenger(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteMessengerResponse>(MessengerErrors.NotFound404);
            entity.SoftDelete();
            await _messengerRepository.Update(entity);
            return new DeleteMessengerResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteMessengerResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DeleteMessengerChannelResponse?>> DeleteMessengerChannelCommand(
        DeleteMessengerChannelRequest request, CT ct)
    {
        try
        {
            var entity = await _messengerChannelRepository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteMessengerChannelResponse>(MessengerErrors.NotFound404);
            entity.SoftDelete();
            await _messengerChannelRepository.Update(entity);
            return new DeleteMessengerChannelResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteMessengerChannelResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetMessengerByIdResponse?>> GetMessengerByIdCommand(
        GetMessengerByIdRequest request, CT ct)
    {
        try
        {
            var result = await _messengerRepository.GetMessengerById(request.Id, ct);
            if (result is null)
                return Result.Failure<GetMessengerByIdResponse>(MessengerErrors.NotFound);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetMessengerByIdResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetMessengersResponse>> GetMessengersHandler(
        GetMessengersRequest request, CT ct)
    {
        try
        {
            var result = await _messengerRepository.GetMessengers(
                request.TargetId,
                request.MessengerTargetType,
                request.MessengerType,
                request.PageIndex,
                request.PageSize,
                ct);
            if (!result.Data.Any())
                return Result.Failure<GetMessengersResponse>(MessengerErrors.NotFound)!;

            return new GetMessengersResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetMessengersResponse>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<GetMessengerChannelByIdResponse?>> GetMessengerChannelByIdCommand(
        GetMessengerChannelByIdRequest request, CT ct)
    {
        try
        {
            var result = await _messengerChannelRepository.GetMessengerChannelById(request.Id, ct);
            if (result is null)
                return Result.Failure<GetMessengerChannelByIdResponse>(MessengerErrors.NotFound);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetMessengerChannelByIdResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetMessengerChannelsResponse>> GetMessengerChannelsHandler(
        GetMessengerChannelsRequest request, CT ct)
    {
        try
        {
            var result = await _messengerChannelRepository.GetMessengerChannels(
               request.ProjectIds,
               request.CostCenterIds,
               request.MessengerMessageType,
               request.PageIndex,
               request.PageSize,
               ct);
            if (!result.Data.Any())
                return Result.Failure<GetMessengerChannelsResponse>(MessengerErrors.NotFound)!;

            return new GetMessengerChannelsResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetMessengerChannelsResponse>(SharedErrors.UnknownError)!;
        }
    }
    public async Task<Result<Messenger?>> ChangeMessengerStateHandle(
         ChangeMessengerStateRequest request, CT ct)
    {
        try
        {
            var entities = await _messengerRepository.GetMessengers(request.Ids, ct);
            if (entities.Count < request.Ids.Count)
                return Result.Failure<Messenger>(MessengerErrors.NotFound);

            foreach (var entity in entities)
            {
                if (request.IsActive)
                {
                    if (entity.IsActive)
                        return Result.Failure<Messenger>(GlobalErrors.IsActive);
                    entity.SetActive();
                }
                else
                {
                    if (!entity.IsActive)
                        return Result.Failure<Messenger>(GlobalErrors.InActive);
                    entity.SetDeactivate();
                }
                await _messengerRepository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Messenger>(SharedErrors.UnknownError);
        }
    }
}