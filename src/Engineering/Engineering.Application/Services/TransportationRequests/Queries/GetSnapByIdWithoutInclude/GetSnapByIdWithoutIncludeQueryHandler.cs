using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetSnapByIdWithoutInclude;

public class GetSnapByIdWithoutIncludeQueryHandler : IQueryHandler<GetSnapByIdWithoutIncludeQuery, GetSnapByIdResponse?>
{
    private readonly ILogger<GetSnapByIdWithoutIncludeQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetSnapByIdWithoutIncludeQueryHandler(ILogger<GetSnapByIdWithoutIncludeQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetSnapByIdResponse?>> Handle(GetSnapByIdWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetSnapByIdWithoutInclude(request.Id, ct);

            return item ?? Result.Failure<GetSnapByIdResponse?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetSnapByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}