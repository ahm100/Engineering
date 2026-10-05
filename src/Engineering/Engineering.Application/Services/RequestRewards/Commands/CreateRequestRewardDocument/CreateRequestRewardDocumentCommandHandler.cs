using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardDocument;

public class CreateRequestRewardDocumentCommandHandler : ICommandHandler<CreateRequestRewardDocumentCommand, RequestRewardDocument>
{
    private readonly ILogger<CreateRequestRewardDocumentCommandHandler> _logger;
    private readonly IRequestRewardDocumentRepository _repository;

    public CreateRequestRewardDocumentCommandHandler(ILogger<CreateRequestRewardDocumentCommandHandler> logger, IRequestRewardDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestRewardDocument?>> Handle(CreateRequestRewardDocumentCommand request, CT ct)
    {
        try
        {
            RequestRewardDocument? result = null;
            if (request.Id is not null)
            {
                result = await _repository.FindById(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestRewardDocument>(RequestRewardDocumentErrors.RequestRewardDocumentNotFound);

                if (request.IsDeleted)
                    result.SetIsDeleted();
                else
                {
                    result.SetData(request.Url, request.RequestReward);
                }

                await _repository.Update(result);
            }
            else
            {
                var entity = new RequestRewardDocument(request.Url, request.RequestReward);

                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestRewardDocument>(SharedErrors.UnknownError);
        }
    }
}
