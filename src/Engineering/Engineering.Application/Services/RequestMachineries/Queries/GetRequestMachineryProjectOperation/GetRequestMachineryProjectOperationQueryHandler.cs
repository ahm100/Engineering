using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryProjectOperation;

public class GetRequestMachineryProjectOperationQueryHandler : IQueryHandler<GetRequestMachineryProjectOperationQuery, DataResult<List<RequestMachineryProjectOperation>>>
{
    private readonly ILogger<GetRequestMachineryProjectOperationQueryHandler> _logger;
    private readonly IRequestMachineryProjectOperationRepository _repository;

    public GetRequestMachineryProjectOperationQueryHandler(ILogger<GetRequestMachineryProjectOperationQueryHandler> logger, IRequestMachineryProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachineryProjectOperation>>?>> Handle(GetRequestMachineryProjectOperationQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFilteredProjectOperation(request.RequestMachineryId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                ? new DataResult<List<RequestMachineryProjectOperation>>
                {
                    Data = entities.Data,
                    RowCount = entities.RowCount
                } : Result.Failure<DataResult<List<RequestMachineryProjectOperation>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestMachineryProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}