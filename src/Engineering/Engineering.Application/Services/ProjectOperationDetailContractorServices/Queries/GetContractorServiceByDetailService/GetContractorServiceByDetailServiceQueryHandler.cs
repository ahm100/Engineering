using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetContractorServiceByDetailService;

public class GetContractorServiceByDetailServiceQueryHandler : IQueryHandler<GetContractorServiceByDetailServiceQuery, ProjectOperationDetailContractorService?>
{
    private readonly ILogger<GetContractorServiceByDetailServiceQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetContractorServiceByDetailServiceQueryHandler(ILogger<GetContractorServiceByDetailServiceQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(GetContractorServiceByDetailServiceQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByDetailServiceIds(
                request.ProjectOperationDetailId,
                request.ServiceInfoId,
                ct);

            return entity ?? Result.Failure<ProjectOperationDetailContractorService>(ContractorServiceErrors.ContractorServiceWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorService>(SharedErrors.UnknownError);
        }
    }
}