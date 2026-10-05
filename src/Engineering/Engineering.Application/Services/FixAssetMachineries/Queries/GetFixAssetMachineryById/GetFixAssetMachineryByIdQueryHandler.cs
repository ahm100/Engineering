using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;

public class GetFixAssetMachineryByIdQueryHandler : IQueryHandler<GetFixAssetMachineryByIdQuery, FixAssetMachinery?>
{
    private readonly ILogger<GetFixAssetMachineryByIdQueryHandler> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public GetFixAssetMachineryByIdQueryHandler(ILogger<GetFixAssetMachineryByIdQueryHandler> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachinery?>> Handle(GetFixAssetMachineryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, null, ct);

            return result ?? Result.Failure<FixAssetMachinery?>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachinery?>(SharedErrors.UnknownError);
        }
    }
}