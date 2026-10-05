using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardsByIds;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Commands.DeleteRequestRewardByIds;

public class DeleteRequestRewardByIdsCommandHandler : ICommandHandler<DeleteRequestRewardByIdsCommand, List<RequestReward>?>
{
    private readonly IRequestRewardRepository _requestRewardRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<DeleteRequestRewardByIdsCommandHandler> _logger;

    public DeleteRequestRewardByIdsCommandHandler(ILogger<DeleteRequestRewardByIdsCommandHandler> logger, IRequestRewardRepository requestRewardRepository, IMediator mediator)
    {
        _requestRewardRepository = requestRewardRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<List<RequestReward>?>> Handle(DeleteRequestRewardByIdsCommand request, CT ct)
    {
        try
        {
            var result = await _mediator.Send(new GetRequestRewardsByIdsQuery(request.Ids), ct);
            if (result.IsBad())
                return result.Failure<List<RequestReward>>()!;
            var entities = result.Value!.Data;
            if (entities is null)
                return Result.Failure<List<RequestReward>>(RequestRewardErrors.RequestRewardWithIdNotFound)!;
            foreach (var entity in entities)
            {
                if (entity is null)
                    return Result.Failure<List<RequestReward>>(RequestRewardErrors.RequestRewardWithIdNotFound)!;
                if (entity.Status == RequestRewardStatus.Confirmed)
                    return Result.Failure<List<RequestReward>>(RequestRewardErrors.InValidStatusForDelete)!;
                if (entity.ContractorStatusStatementDiscounts.Any() || entity.ContractorStatusStatementFines.Any() || entity.ContractorStatusStatementRewards.Any())
                    return Result.Failure<List<RequestReward>>(RequestRewardErrors.IdIsRelatedToCSS)!;
                if (entity.IsDeleted)
                    return Result.Failure<List<RequestReward>>(RequestRewardErrors.IsDeleted)!;
                entity.SoftDelete();
                entity.AddHistory();
                await _requestRewardRepository.Update(entity);
            }

            return entities!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestReward>>(SharedErrors.UnknownError)!;
        }
    }
}