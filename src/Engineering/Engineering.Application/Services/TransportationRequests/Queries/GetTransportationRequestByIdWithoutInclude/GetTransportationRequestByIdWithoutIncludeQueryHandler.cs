using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetById;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetTransportationRequestByIdWithoutInclude;

public class GetTransportationRequestByIdWithoutIncludeQueryHandler : IQueryHandler<GetTransportationRequestByIdWithoutIncludeQuery, GetTransportationRequestByIdResponse?>
{
    private readonly ILogger<GetTransportationRequestByIdWithoutIncludeQueryHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public GetTransportationRequestByIdWithoutIncludeQueryHandler(ILogger<GetTransportationRequestByIdWithoutIncludeQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetTransportationRequestByIdResponse?>> Handle(GetTransportationRequestByIdWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetByIdWithoutInclude(request.Id, ct);

            return item ?? Result.Failure<GetTransportationRequestByIdResponse?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetTransportationRequestByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}