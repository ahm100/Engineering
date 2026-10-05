using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationExpert;

public class DeleteDailyProjectOperationExpertCommandHandler : ICommandHandler<DeleteDailyProjectOperationExpertCommand, DailyProjectOperationExpert>
{
    private readonly ILogger<DeleteDailyProjectOperationExpertCommandHandler> _logger;
    private readonly IDailyProjectOperationExpertRepository _repository;

    public DeleteDailyProjectOperationExpertCommandHandler(ILogger<DeleteDailyProjectOperationExpertCommandHandler> logger,
                                                           IDailyProjectOperationExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationExpert?>> Handle(DeleteDailyProjectOperationExpertCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DeleteDailyProjectOperationExpertId, ct);

            if (entity is null)
                return Result.Failure<DailyProjectOperationExpert?>(DailyProjectOperationExpertErrors.DailyProjectOperationExpertWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DailyProjectOperationExpert?>(DailyProjectOperationExpertErrors.IsDeleted);

            entity.SetIsDeleted();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationExpert?>(SharedErrors.UnknownError);
        }
    }
}
