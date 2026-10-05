using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorkById;

public class GetFixAssetMachineryNotWorkByIdQueryHandler : IQueryHandler<GetFixAssetMachineryNotWorkByIdQuery, FixAssetMachineryNotWork?>
{
    private readonly ILogger<GetFixAssetMachineryNotWorkByIdQueryHandler> _logger;
    private readonly IFixAssetMachineryNotWorkRepository _repository;

    public GetFixAssetMachineryNotWorkByIdQueryHandler(ILogger<GetFixAssetMachineryNotWorkByIdQueryHandler> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryNotWork?>> Handle(GetFixAssetMachineryNotWorkByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<FixAssetMachineryNotWork?>(FixAssetMachineryErrors.FixAssetMachineryNotWorkNotFoundWithId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryNotWork?>(SharedErrors.UnknownError);
        }
    }
}