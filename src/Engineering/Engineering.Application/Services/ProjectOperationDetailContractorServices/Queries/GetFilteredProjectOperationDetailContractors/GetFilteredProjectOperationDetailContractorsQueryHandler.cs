using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetFilteredProjectOperationDetailContractors;

public class GetFilteredProjectOperationDetailContractorsQueryHandler : IQueryHandler<GetFilteredProjectOperationDetailContractorsQuery, DataResult<List<long?>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetFilteredProjectOperationDetailContractorsQueryHandler> _logger;

    public GetFilteredProjectOperationDetailContractorsQueryHandler(ILogger<GetFilteredProjectOperationDetailContractorsQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long?>>?>> Handle(GetFilteredProjectOperationDetailContractorsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredProjectOperationDetailContractors(request.CostCenterIds, request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds, ct);

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