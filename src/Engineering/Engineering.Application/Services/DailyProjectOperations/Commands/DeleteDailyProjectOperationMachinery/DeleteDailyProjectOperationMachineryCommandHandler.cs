using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationMachinery;

public class DeleteDailyProjectOperationMachineryCommandHandler : ICommandHandler<DeleteDailyProjectOperationMachineryCommand, DailyProjectOperationMachinery>
{
    private readonly ILogger<DeleteDailyProjectOperationMachineryCommandHandler> _logger;
    private readonly IDailyProjectOperationMachineryRepository _repository;

    public DeleteDailyProjectOperationMachineryCommandHandler(ILogger<DeleteDailyProjectOperationMachineryCommandHandler> logger,
                                                              IDailyProjectOperationMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationMachinery?>> Handle(DeleteDailyProjectOperationMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DailyProjectOperationMachineryId, ct);

            if (entity is null)
                return Result.Failure<DailyProjectOperationMachinery?>(DailyProjectOperationMachineryErrors.DailyProjectOperationMachineryWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DailyProjectOperationMachinery?>(DailyProjectOperationMachineryErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationMachinery?>(SharedErrors.UnknownError);
        }
    }
}
