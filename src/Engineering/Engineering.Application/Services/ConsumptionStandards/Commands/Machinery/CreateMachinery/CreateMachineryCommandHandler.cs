using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.CreateMachinery;

public class CreateMachineryCommandHandler : ICommandHandler<CreateMachineryCommand, ConsumptionStandardMachinery>
{
    private readonly ILogger<CreateMachineryCommand> _logger;
    private readonly IConsumptionStandardMachineryRepository _repository;

    public CreateMachineryCommandHandler(ILogger<CreateMachineryCommand> logger, IConsumptionStandardMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardMachinery?>> Handle(CreateMachineryCommand request, CT ct)
    {
        try
        {
            var entity = new ConsumptionStandardMachinery(request.OperationInfo, request.Machinery, request.MachineryNumber,
                request.TimeSpant, request.UnusedPercentage);
            var result = await _repository.Create(entity, ct);
            request.OperationInfo.SetStandard();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumptionStandardMachinery>(SharedErrors.UnknownError);
        }
    }
}