using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfos;

public class GetOperationInfosQueryHandler : IQueryHandler<GetOperationInfosQuery, List<OperationInfo>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetOperationInfosQueryHandler> _logger;

    public GetOperationInfosQueryHandler(
        ILogger<GetOperationInfosQueryHandler> logger,
        IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<OperationInfo>?>> Handle(GetOperationInfosQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfos(request.Ids, ct);
            if (result == null || result.Count != request.Ids.Count)
                return Result.Failure<List<OperationInfo>>(OperationInfoErrors.FilteredOperationInfoNotFound);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfo>>(SharedErrors.UnknownError);
        }
    }
}