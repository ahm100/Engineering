using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperationService;

public class UpdateDailyProjectOperationServiceCommandHandler : ICommandHandler<UpdateDailyProjectOperationServiceCommand, DailyProjectOperationService>
{
    private readonly ILogger<UpdateDailyProjectOperationServiceCommandHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public UpdateDailyProjectOperationServiceCommandHandler(ILogger<UpdateDailyProjectOperationServiceCommandHandler> logger,
                                                     IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<DailyProjectOperationService?>> Handle(UpdateDailyProjectOperationServiceCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var entity = request.Entity;

            if (request.ContractorService is not null)
                entity.SetContractorId(request.ContractorService.ContractorId);

            entity.SetTimeSpant(request.TimeSpant);

            if (entity.Volume != request.Volume)
                if (!entity.ContractorStatusStatementServiceDailies.Any())
                    entity.SetVolume(request.Volume);

            entity.SetProjectServiceVolume(request.ProjectServiceVolume);

            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationService?>(SharedErrors.UnknownError);
        }
    }
}
