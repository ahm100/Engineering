using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationService;

public class DeleteDailyProjectOperationServiceCommandHandler : ICommandHandler<DeleteDailyProjectOperationServiceCommand, DailyProjectOperationService>
{
    private readonly ILogger<DeleteDailyProjectOperationServiceCommandHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public DeleteDailyProjectOperationServiceCommandHandler(ILogger<DeleteDailyProjectOperationServiceCommandHandler> logger,
                                                            IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationService?>> Handle(DeleteDailyProjectOperationServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DailyProjectOperationServiceId, ct);

            if (entity is null)
                return Result.Failure<DailyProjectOperationService?>(DailyProjectOperationServiceErrors.DailyProjectOperationServiceWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DailyProjectOperationService?>(DailyProjectOperationServiceErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationService?>(SharedErrors.UnknownError);
        }
    }
}
