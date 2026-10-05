using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;
namespace Engineering.Application.Services.OperationInfoServices.Commands.CreateOperationInfoService;

public class CreateOperationInfoServiceCommandHandler : ICommandHandler<CreateOperationInfoServiceCommand, List<OperationInfoService>?>
{
    private readonly ILogger<CreateOperationInfoServiceCommand> _logger;
    private readonly IOperationInfoServiceRepository _repository;

    public CreateOperationInfoServiceCommandHandler(ILogger<CreateOperationInfoServiceCommand> logger, IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<OperationInfoService>?>> Handle(CreateOperationInfoServiceCommand request, CT ct)
    {
        try
        {
            var result = new List<OperationInfoService>();
            foreach (var item in request.ServiceInfos)
            {
                if (request.OperationInfo.OperationInfoServices.Any(x => x.ServiceInfo.Id == item.ServiceInfo.Id))
                    continue;

                result.Add(await _repository.Create(new OperationInfoService(request.OperationInfo, item.ServiceInfo, item.TimeSpant), ct));
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoService>>(SharedErrors.UnknownError)!;
        }
    }
}