using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetFilteredContractors;

public class GetFilteredContractorsQueryHandler : IQueryHandler<GetFilteredContractorsQuery, List<long>>
{
    private readonly ILogger<GetFilteredContractorsQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetFilteredContractorsQueryHandler(ILogger<GetFilteredContractorsQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetFilteredContractorsQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetFilteredContractors(request.ProjectId, request.ProjectOperationIds, ct);
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<long>>(SharedErrors.UnknownError);
        }
    }
}

