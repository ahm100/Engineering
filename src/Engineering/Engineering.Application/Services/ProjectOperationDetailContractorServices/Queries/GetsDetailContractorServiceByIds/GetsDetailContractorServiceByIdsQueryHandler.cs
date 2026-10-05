using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Queries.GetsDetailContractorServiceByIds;

public class GetsDetailContractorServiceByIdsQueryHandler : IQueryHandler<GetsDetailContractorServiceByIdsQuery, List<ProjectOperationDetailContractorService>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetsDetailContractorServiceByIdsQuery> _logger;

    public GetsDetailContractorServiceByIdsQueryHandler(ILogger<GetsDetailContractorServiceByIdsQuery> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperationDetailContractorService>?>> Handle(GetsDetailContractorServiceByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDetailContractorServiceByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationDetailContractorService>>(SharedErrors.UnknownError);
        }
    }
}