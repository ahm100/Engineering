using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsForEmployerStatusStatement;

public class GetsForEmployerStatusStatementQueryHandler : IQueryHandler<GetsForEmployerStatusStatementQuery, DataResult<List<GetsForEmployerStatusStatementModel>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsForEmployerStatusStatementQueryHandler> _logger;

    public GetsForEmployerStatusStatementQueryHandler(ILogger<GetsForEmployerStatusStatementQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsForEmployerStatusStatementModel>>?>> Handle(GetsForEmployerStatusStatementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsForEmployerStatusStatement(
                request.EmployerId,
                request.EmployerStatusStatementId,
                request.ProjectId,
                request.CostCenterId,
                request.EmployerContractId,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsForEmployerStatusStatementModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsForEmployerStatusStatementModel>>>(ProjectOperationErrors.UnValidData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsForEmployerStatusStatementModel>>>(SharedErrors.UnknownError);
        }
    }
}