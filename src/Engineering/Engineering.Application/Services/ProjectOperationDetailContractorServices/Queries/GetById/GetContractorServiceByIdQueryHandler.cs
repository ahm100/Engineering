using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;

public class GetContractorServiceByIdQueryHandler : IQueryHandler<GetContractorServiceByIdQuery, ProjectOperationDetailContractorService?>
{
    private readonly ILogger<GetContractorServiceByIdQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetContractorServiceByIdQueryHandler(ILogger<GetContractorServiceByIdQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(GetContractorServiceByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);

            return entity ?? Result.Failure<ProjectOperationDetailContractorService>(CostCenterErrors.TypeCodeIsDuplicate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorService>(SharedErrors.UnknownError);
        }
    }
}