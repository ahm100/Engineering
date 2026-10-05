using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationProduct;

public class CreateDailyProjectOperationProductCommandHandler : ICommandHandler<CreateDailyProjectOperationProductCommand, DailyProjectOperationProduct>
{
    private readonly ILogger<CreateDailyProjectOperationProductCommandHandler> _logger;
    private readonly IDailyProjectOperationProductRepository _repository;

    public CreateDailyProjectOperationProductCommandHandler(ILogger<CreateDailyProjectOperationProductCommandHandler> logger,
                                                            IDailyProjectOperationProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationProduct?>> Handle(CreateDailyProjectOperationProductCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperationProduct(request.ProductId,
                                                          request.FinalValue,
                                                          request.UnusedPercentage,
                                                          request.DailyProjectOperation,
                                                          request.ConsumableVolumeProduct);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationProduct?>(SharedErrors.UnknownError);
        }
    }
}
