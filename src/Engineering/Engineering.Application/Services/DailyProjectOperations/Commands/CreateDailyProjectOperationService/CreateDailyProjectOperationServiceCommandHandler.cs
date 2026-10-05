using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationService;

public class CreateDailyProjectOperationServiceCommandHandler : ICommandHandler<CreateDailyProjectOperationServiceCommand, DailyProjectOperationService>
{
    private readonly ILogger<CreateDailyProjectOperationServiceCommandHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public CreateDailyProjectOperationServiceCommandHandler(ILogger<CreateDailyProjectOperationServiceCommandHandler> logger,
                                                            IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationService?>> Handle(CreateDailyProjectOperationServiceCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperationService(
                request.DailyProjectOperation,
                request.ContractorService,
                request.Volume,
                request.ProjectServiceVolume,
                request.ContractorService.ContractorId,
                request.ThirdPartyId,
                request.TimeSpant,
                request.IsActive);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationService?>(SharedErrors.UnknownError);
        }
    }
}
