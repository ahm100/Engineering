using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByCode;

public class GetProjectOperationDetailByCodeQueryHandler : IQueryHandler<GetProjectOperationDetailByCodeQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailByCodeQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailByCodeQueryHandler(ILogger<GetProjectOperationDetailByCodeQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailByCodeQuery request, CT ct)
    {
        try
        {
            var ProjectOperationDetailResponse = await _repository.GetProjectOperationDetailByCode(request.Code, request.OperationLocationId, request.CompanyId, ct);

            return ProjectOperationDetailResponse ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.DataNotFoundWithCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}