using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.ProjectServices.Queries.GetServiceContractors;

public class GetServiceContractorsQueryHandler : IQueryHandler<GetServiceContractorsQuery, List<long>?>
{
    private readonly IProjectServiceRepository _repository;
    private readonly ILogger<GetServiceContractorsQueryHandler> _logger;

    public GetServiceContractorsQueryHandler(ILogger<GetServiceContractorsQueryHandler> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetServiceContractorsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetServiceContractors(
                request.ProjectId,
                request.ServiceInfoIds,
                ct);

            return result ?? Result.Failure<List<long>?>(ProjectServiceErrors.ContractorsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<long>?>(SharedErrors.UnknownError);
        }
    }
}