using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationProduct;

public class DeleteDailyProjectOperationProductCommandHandler : ICommandHandler<DeleteDailyProjectOperationProductCommand, DailyProjectOperationProduct>
{
    private readonly ILogger<DeleteDailyProjectOperationProductCommandHandler> _logger;
    private readonly IDailyProjectOperationProductRepository _repository;

    public DeleteDailyProjectOperationProductCommandHandler(ILogger<DeleteDailyProjectOperationProductCommandHandler> logger,
                                                            IDailyProjectOperationProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationProduct?>> Handle(DeleteDailyProjectOperationProductCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DailyProjectOperationProductId, ct);

            if (entity is null)
                return Result.Failure<DailyProjectOperationProduct?>(DailyProjectOperationProductErrors.DailyProjectOperationProductWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DailyProjectOperationProduct?>(DailyProjectOperationProductErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationProduct?>(SharedErrors.UnknownError);
        }
    }
}
