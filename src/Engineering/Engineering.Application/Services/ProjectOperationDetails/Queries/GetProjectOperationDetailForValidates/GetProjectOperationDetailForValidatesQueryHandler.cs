using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForValidates;

public class GetProjectOperationDetailForValidatesQueryHandler : IQueryHandler<GetProjectOperationDetailForValidatesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailForValidatesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailForValidatesQueryHandler(ILogger<GetProjectOperationDetailForValidatesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailForValidatesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailForValidates(request.ProjectOperationId, request.OperationLocationId, request.Code, request.CompanyId, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
