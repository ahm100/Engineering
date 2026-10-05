using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.ProjectServices.Queries.IsDuplicateProjectService;

public class IsDuplicateProjectServiceQueryHandler : IQueryHandler<IsDuplicateProjectServiceQuery, bool>
{
    private readonly IProjectServiceRepository _repository;
    private readonly ILogger<IsDuplicateProjectServiceQueryHandler> _logger;

    public IsDuplicateProjectServiceQueryHandler(ILogger<IsDuplicateProjectServiceQueryHandler> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(IsDuplicateProjectServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.IsDuplicateProjectService(
                request.Id,
                request.ProjectId,
                request.ServiceInfoId,
                request.ContractorId,
                ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}