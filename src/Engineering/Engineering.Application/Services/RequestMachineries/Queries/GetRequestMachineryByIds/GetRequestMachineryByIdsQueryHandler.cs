using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIds;

public class GetRequestMachineryByIdsQueryHandler : IQueryHandler<GetRequestMachineryByIdsQuery, List<RequestMachinery>>
{
    private readonly ILogger<GetRequestMachineryByIdsQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetRequestMachineryByIdsQueryHandler(ILogger<GetRequestMachineryByIdsQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<RequestMachinery>?>> Handle(GetRequestMachineryByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdsAsync(request.RequestMachineryIds, ct);
            return result ?? Result.Failure<List<RequestMachinery>>(RequestMachineryErrors.RequestMachinerysNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestMachinery>>(SharedErrors.UnknownError);
        }
    }
}
