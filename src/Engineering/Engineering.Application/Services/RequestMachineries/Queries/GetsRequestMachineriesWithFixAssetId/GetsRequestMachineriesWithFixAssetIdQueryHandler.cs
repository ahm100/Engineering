using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetsRequestMachineriesWithFixAssetId;

public class GetsRequestMachineriesWithFixAssetIdQueryHandler : IQueryHandler<GetsRequestMachineriesWithFixAssetIdQuery, List<RequestMachinery>>
{
    private readonly ILogger<GetsRequestMachineriesWithFixAssetIdQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetsRequestMachineriesWithFixAssetIdQueryHandler(ILogger<GetsRequestMachineriesWithFixAssetIdQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<RequestMachinery>?>> Handle(GetsRequestMachineriesWithFixAssetIdQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByFixAssetId(
                request.FixAssetId,
                ct);

            return entities ?? Result.Failure<List<RequestMachinery>>(RequestMachineryErrors.RequestMachinerysNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestMachinery>>(SharedErrors.UnknownError);
        }
    }
}
