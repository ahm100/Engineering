using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByContractor;

public class GetProjectOperationDetailByContractorQueryHandler : IQueryHandler<GetProjectOperationDetailByContractorQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailByContractorQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailByContractorQueryHandler(ILogger<GetProjectOperationDetailByContractorQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailByContractorQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailByContractor(request.ProjectId, request.ContractorId, ct);

            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
