using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectContractors;

public class GetProjectContractorsQueryHandler : IQueryHandler<GetProjectContractorsQuery, GetProjectContractorsResponse?>
{
    private readonly ILogger<GetProjectContractorsQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetProjectContractorsQueryHandler(ILogger<GetProjectContractorsQueryHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetProjectContractorsResponse?>> Handle(GetProjectContractorsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectContractors(request.ProjectId, request.PageIndex, request.PageSize, ct);

            return result.Data is null || result.RowCount < 1 ? new GetProjectContractorsResponse(result.Data, result.RowCount)
                : Result.Failure<GetProjectContractorsResponse?>(ProjectOperationDetailErrors.DataNotFoundWithCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectContractorsResponse?>(SharedErrors.UnknownError);
        }
    }
}