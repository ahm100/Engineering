using Engineering.Application.Abstractions.Data;

namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorsByServiceIds;

public class GetContractorsByServiceIdsQueryHandler : IQueryHandler<GetContractorsByServiceIdsQuery, List<long>>
{
    private readonly ILogger<GetContractorsByServiceIdsQueryHandler> _logger;
    private readonly IContractorServicesRepository _repository;

    public GetContractorsByServiceIdsQueryHandler(ILogger<GetContractorsByServiceIdsQueryHandler> logger, IContractorServicesRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetContractorsByServiceIdsQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetContractorServicesByServiceIds(request.Ids, ct);
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<long>>(SharedErrors.UnknownError);
        }
    }
}
