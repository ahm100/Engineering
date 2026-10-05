using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdNoIncluding;

public class GetProjectOperationDetailByIdNoIncludingQueryHandler : IQueryHandler<GetProjectOperationDetailByIdNoIncludingQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailByIdNoIncludingQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailByIdNoIncludingQueryHandler(ILogger<GetProjectOperationDetailByIdNoIncludingQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailByIdNoIncludingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailByIdNoIncluding(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
