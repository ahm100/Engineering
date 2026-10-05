using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Experts.Models.GetExpertById;
using ExpertModel = Engineering.Application.WebServices.MetaDataServices.Experts.Models.Expert;

namespace Engineering.Application.WebServices.MetaDataServices.Experts.Queries.GetExpertById;

public class GetExpertByIdQueryHandler : IQueryHandler<GetExpertByIdQuery, ExpertModel?>
{
    private readonly ILogger<GetExpertByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetExpertByIdQueryHandler(ILogger<GetExpertByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<ExpertModel?>> Handle(GetExpertByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetExpertById(request.Adapt<GetExpertByIdRequest>(), ct);

            var data = result?.Value?.Data?.FirstOrDefault();

            return data ?? Result.Failure<ExpertModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ExpertModel?>(SharedErrors.UnknownError);
        }
    }
}