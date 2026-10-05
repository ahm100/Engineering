using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetOperationInfoSeasonById;

public class GetOperationInfoSeasonByIdQueryHandler : IQueryHandler<GetOperationInfoSeasonByIdQuery, OperationInfoSeason>
{
    private readonly ILogger<GetOperationInfoSeasonByIdQueryHandler> _logger;
    private readonly IOperationInfoSeasonRepository _repository;

    public GetOperationInfoSeasonByIdQueryHandler(ILogger<GetOperationInfoSeasonByIdQueryHandler> logger, IOperationInfoSeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoSeason?>> Handle(GetOperationInfoSeasonByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoSeasonById(request.Id, ct);
            return result ?? Result.Failure<OperationInfoSeason>(OperationInfoSeasonErrors.OperationInfoSeasonWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoSeason>(SharedErrors.UnknownError);
        }
    }
}