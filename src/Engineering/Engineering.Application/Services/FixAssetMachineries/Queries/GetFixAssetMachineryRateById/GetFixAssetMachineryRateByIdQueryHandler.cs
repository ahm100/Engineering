using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryRateById;

public class GetFixAssetMachineryRateByIdQueryHandler : IQueryHandler<GetFixAssetMachineryRateByIdQuery, FixAssetMachineryRate?>
{
    private readonly ILogger<GetFixAssetMachineryRateByIdQueryHandler> _logger;
    private readonly IFixAssetMachineryRateRepository _repository;

    public GetFixAssetMachineryRateByIdQueryHandler(ILogger<GetFixAssetMachineryRateByIdQueryHandler> logger, IFixAssetMachineryRateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryRate?>> Handle(GetFixAssetMachineryRateByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<FixAssetMachineryRate?>(FixAssetMachineryErrors.RateNotfound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryRate?>(SharedErrors.UnknownError);
        }
    }
}