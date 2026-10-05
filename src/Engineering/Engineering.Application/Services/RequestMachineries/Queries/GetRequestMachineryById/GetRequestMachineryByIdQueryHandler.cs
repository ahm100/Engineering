using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;

public class GetRequestMachineryByIdQueryHandler : IQueryHandler<GetRequestMachineryByIdQuery, RequestMachinery>
{
    private readonly ILogger<GetRequestMachineryByIdQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetRequestMachineryByIdQueryHandler(ILogger<GetRequestMachineryByIdQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(GetRequestMachineryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdAsync(request.RequestMachineryId, ct);
            return result ?? Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
