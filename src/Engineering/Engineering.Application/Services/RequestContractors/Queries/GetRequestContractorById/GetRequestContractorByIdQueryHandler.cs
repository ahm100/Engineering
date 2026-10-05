using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorById;

public class GetRequestContractorByIdQueryHandler : IQueryHandler<GetRequestContractorByIdQuery, RequestContractor>
{
    private readonly ILogger<GetRequestContractorByIdQueryHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public GetRequestContractorByIdQueryHandler(ILogger<GetRequestContractorByIdQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractor?>> Handle(GetRequestContractorByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.RequestContractorId, ct);
            return result ?? Result.Failure<RequestContractor>(RequestContractorErrors.RequestContractorNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestContractor>(SharedErrors.UnknownError);
        }
    }
}
