using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForContractorServices;

public class GetProjectOperationDetailForContractorServicesQueryHandler : IQueryHandler<GetProjectOperationDetailForContractorServicesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailForContractorServicesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailForContractorServicesQueryHandler(ILogger<GetProjectOperationDetailForContractorServicesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailForContractorServicesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailForContractorServices(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
