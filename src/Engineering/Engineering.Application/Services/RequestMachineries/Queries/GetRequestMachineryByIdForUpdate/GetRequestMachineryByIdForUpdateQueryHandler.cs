using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdForUpdate;

public class GetRequestMachineryByIdForUpdateQueryHandler : IQueryHandler<GetRequestMachineryByIdForUpdateQuery, RequestMachinery>
{
    private readonly ILogger<GetRequestMachineryByIdForUpdateQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetRequestMachineryByIdForUpdateQueryHandler(ILogger<GetRequestMachineryByIdForUpdateQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(GetRequestMachineryByIdForUpdateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdForUpdateMachineryAsync(request.RequestMachineryId, ct);
            return result ?? Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
