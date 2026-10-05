using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Queries.GetsByOperationInfoGroupId;

public class GetsByOperationInfoGroupIdQueryHandler : IQueryHandler<GetsByOperationInfoGroupIdQuery, DataResult<List<OperationInfoGroupRelation>>>
{
    private readonly IOperationInfoGroupRelationRepository _repository;
    private readonly ILogger<GetsByOperationInfoGroupIdQueryHandler> _logger;

    public GetsByOperationInfoGroupIdQueryHandler(ILogger<GetsByOperationInfoGroupIdQueryHandler> logger, IOperationInfoGroupRelationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoGroupRelation>>?>> Handle(GetsByOperationInfoGroupIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByOprationInfoGroupId(request.OprationInfoGroupId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoGroupRelation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoGroupRelation>>>(OperationInfoGroupRelationErrors.OperationInfoGroupRelationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoGroupRelation>>>(SharedErrors.UnknownError);
        }
    }
}
