using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;
namespace Engineering.Application.Services.OperationInfoSeasons.Commands.CreateOperationInfoSeason;

public class CreateOperationInfoSeasonCommandHandler : ICommandHandler<CreateOperationInfoSeasonCommand, List<OperationInfoSeason>?>
{
    private readonly ILogger<CreateOperationInfoSeasonCommand> _logger;
    private readonly IOperationInfoSeasonRepository _repository;

    public CreateOperationInfoSeasonCommandHandler(ILogger<CreateOperationInfoSeasonCommand> logger, IOperationInfoSeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<OperationInfoSeason>?>> Handle(CreateOperationInfoSeasonCommand request, CT ct)
    {
        try
        {
            var result = new List<OperationInfoSeason>();
            foreach (var item in request.Seasons)
                result.Add(await _repository.Create(new OperationInfoSeason(request.OperationInfo, item), ct));

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfoSeason>>(SharedErrors.UnknownError)!;
        }
    }
}