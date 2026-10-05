using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardProduct;

public class CreateRequestRewardProductCommandHandler : ICommandHandler<CreateRequestRewardProductCommand, RequestRewardProduct>
{
    private readonly ILogger<CreateRequestRewardProductCommandHandler> _logger;
    private readonly IRequestRewardProductRepository _repository;

    public CreateRequestRewardProductCommandHandler(ILogger<CreateRequestRewardProductCommandHandler> logger, IRequestRewardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestRewardProduct?>> Handle(CreateRequestRewardProductCommand request, CT ct)
    {
        try
        {
            RequestRewardProduct? result = null;
            if (request.Id is not null)
            {
                result = await _repository.FindById(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestRewardProduct>(RequestRewardProductErrors.RequestRewardProductNotFound);

                if (request.IsDeleted)
                    result.SetIsDeleted();
                else
                {
                    result.SetData(request.ProductId, request.CurrencyId, request.Count, request.Price, request.RequestReward);
                }

                await _repository.Update(result);
            }
            else
            {
                var entity = new RequestRewardProduct(request.ProductId, request.CurrencyId, request.Count, request.Price, request.RequestReward);

                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestRewardProduct>(SharedErrors.UnknownError);
        }
    }
}
