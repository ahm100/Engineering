using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorByIds;

public class GetRequestContractorByIdsQueryHandler : IQueryHandler<GetRequestContractorByIdsQuery, List<RequestContractor>>
{
    private readonly ILogger<GetRequestContractorByIdsQueryHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public GetRequestContractorByIdsQueryHandler(ILogger<GetRequestContractorByIdsQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<RequestContractor>?>> Handle(GetRequestContractorByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIds(request.RequestContractorIds, ct);
            return result ?? Result.Failure<List<RequestContractor>>(RequestContractorErrors.RequestContractorsNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestContractor>>(SharedErrors.UnknownError);
        }
    }
}
