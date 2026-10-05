using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.HumenResourceServices.Posts.Models.GetPostTitleThirdParties;

namespace Engineering.Infra.Providers.HumenResource;

public class HumenResourceService : IHumenResourceService
{
    private readonly IHumenResourceProvider _humenResourceProvider;

    public HumenResourceService(IHumenResourceProvider humenResourceProvider)
    {
        _humenResourceProvider = humenResourceProvider;
    }

    public async Task<GetPostTitleThirdPartiesResponse> GetPostTitleThirdParties(
        GetPostTitleThirdPartiesRequest request, CT ct)
    {
        return await _humenResourceProvider.GetPostTitleThirdParties(request, ct);
    }
}
