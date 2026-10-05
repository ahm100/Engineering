using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.HumenResourceServices.Posts.Models.GetPostTitleThirdParties;

namespace Engineering.Application.WebServices.HumenResourceServices.Posts.Queries.GetPostTitleThirdParties;

public class GetPostTitleThirdPartiesQueryHandler
    : IQueryHandler<GetPostTitleThirdPartiesQuery, List<PostTitleItem>?>
{
    private readonly ILogger<GetPostTitleThirdPartiesQueryHandler> _logger;
    private readonly IHumenResourceService _humenResourceService;

    public GetPostTitleThirdPartiesQueryHandler(
        ILogger<GetPostTitleThirdPartiesQueryHandler> logger,
        IHumenResourceService humenResourceService)
    {
        _logger = logger;
        _humenResourceService = humenResourceService;
    }

    public async Task<Result<List<PostTitleItem>?>> Handle(GetPostTitleThirdPartiesQuery request, CT ct)
    {
        try
        {
            var result = await _humenResourceService.GetPostTitleThirdParties(
                new GetPostTitleThirdPartiesRequest ( 
                    request.ThirdPartyIds 
                ), ct);

            if (!result.IsSuccess || result.Value is null)
                return Result.Failure<List<PostTitleItem>?>(SharedErrors.ProviderError);

            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<PostTitleItem>?>(SharedErrors.UnknownError);
        }
    }
}