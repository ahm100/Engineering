using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;

namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorModelById;

public class GetRequestContractorModelByIdQueryHandler : IQueryHandler<GetRequestContractorModelByIdQuery, GetRequestContractorByIdResponse>
{
    private readonly ILogger<GetRequestContractorModelByIdQueryHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public GetRequestContractorModelByIdQueryHandler(ILogger<GetRequestContractorModelByIdQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetRequestContractorByIdResponse?>> Handle(GetRequestContractorModelByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetModelById(request.RequestContractorId, ct);
            return result ?? Result.Failure<GetRequestContractorByIdResponse>(RequestContractorErrors.RequestContractorNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetRequestContractorByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
