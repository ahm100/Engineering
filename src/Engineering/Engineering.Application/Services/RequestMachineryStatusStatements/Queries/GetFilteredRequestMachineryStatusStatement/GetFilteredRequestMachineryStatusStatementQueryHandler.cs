using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetFilteredRequestMachineryStatusStatement;

public class GetFilteredRequestMachineryStatusStatementQueryHandler : IQueryHandler<GetFilteredRequestMachineryStatusStatementQuery, DataResult<List<RequestMachineryStatusStatement>>>
{
    private readonly ILogger<GetFilteredRequestMachineryStatusStatementQueryHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;

    public GetFilteredRequestMachineryStatusStatementQueryHandler(ILogger<GetFilteredRequestMachineryStatusStatementQueryHandler> logger,
                                                                IRequestMachineryStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachineryStatusStatement>>?>> Handle(GetFilteredRequestMachineryStatusStatementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredRequestMachineryStatusStatement(
                request.Ids, request.ContractorIds, request.CostCenterIds, request.ProjectIds, request.MachineryIds, request.Statuses, request.Unit,
                request.StartDate, request.EndDate, request.CompanyId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);


            return result.Data.Any() ?
                new DataResult<List<RequestMachineryStatusStatement>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<RequestMachineryStatusStatement>>>(RequestMachineryStatusStatementErrors.RequestMachineryStatusStatementNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<RequestMachineryStatusStatement>>>(SharedErrors.UnknownError);
        }
    }
}
