using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsByProjectOperationIdInEmployerContract;

public class GetsByProjectOperationIdInEmployerContractQueryHandler : IQueryHandler<GetsByProjectOperationIdInEmployerContractQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsByProjectOperationIdInEmployerContractQueryHandler> _logger;

    public GetsByProjectOperationIdInEmployerContractQueryHandler(ILogger<GetsByProjectOperationIdInEmployerContractQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsByProjectOperationIdInEmployerContractQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationIdInEmployerContract(request.ProjectOperationId, 0, 0, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}