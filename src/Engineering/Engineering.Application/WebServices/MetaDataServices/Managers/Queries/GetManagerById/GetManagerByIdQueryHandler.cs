using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Managers.Models.GetManagerById;
using ManagerModel = Engineering.Application.WebServices.MetaDataServices.Managers.Models.Manager;

namespace Engineering.Application.WebServices.MetaDataServices.Managers.Queries.GetManagerById;

public class GetManagerByIdQueryHandler : IQueryHandler<GetManagerByIdQuery, ManagerModel?>
{
    private readonly ILogger<GetManagerByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetManagerByIdQueryHandler(ILogger<GetManagerByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<ManagerModel?>> Handle(GetManagerByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetManagerById(request.Adapt<GetManagerByIdRequest>(), ct);

            var data = result?.Data!.FirstOrDefault();

            return data ?? Result.Failure<ManagerModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ManagerModel?>(SharedErrors.UnknownError);
        }
    }
}