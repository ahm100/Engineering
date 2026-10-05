using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryProjectOperationDetail;

public class GetRequestMachineryProjectOperationDetailQueryHandler : IQueryHandler<GetRequestMachineryProjectOperationDetailQuery, DataResult<List<RequestMachineryProjectOperationDetail>>>
{
    private readonly ILogger<GetRequestMachineryProjectOperationDetailQueryHandler> _logger;
    private readonly IRequestMachineryProjectOperationDetailRepository _repository;

    public GetRequestMachineryProjectOperationDetailQueryHandler(ILogger<GetRequestMachineryProjectOperationDetailQueryHandler> logger, IRequestMachineryProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachineryProjectOperationDetail>>?>> Handle(GetRequestMachineryProjectOperationDetailQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFilteredProjectOperationDetail(request.RequestMachineryId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                ? new DataResult<List<RequestMachineryProjectOperationDetail>>
                {
                    Data = entities.Data,
                    RowCount = entities.RowCount
                } : Result.Failure<DataResult<List<RequestMachineryProjectOperationDetail>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestMachineryProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}