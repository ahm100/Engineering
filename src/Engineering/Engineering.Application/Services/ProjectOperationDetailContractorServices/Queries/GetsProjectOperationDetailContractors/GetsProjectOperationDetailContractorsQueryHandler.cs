using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractors;

public class GetsProjectOperationDetailContractorsQueryHandler : IQueryHandler<GetsProjectOperationDetailContractorsQuery, DataResult<List<long?>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetsProjectOperationDetailContractorsQueryHandler> _logger;

    public GetsProjectOperationDetailContractorsQueryHandler(ILogger<GetsProjectOperationDetailContractorsQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long?>>?>> Handle(GetsProjectOperationDetailContractorsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailContractors(request.CostCenterId, request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, ct);

            return result.Data.Any() ?
                new DataResult<List<long?>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<long?>>>(ProjectOperationDetailErrors.GetsProjectOperationDetailContractorsFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long?>>>(SharedErrors.UnknownError);
        }
    }
}