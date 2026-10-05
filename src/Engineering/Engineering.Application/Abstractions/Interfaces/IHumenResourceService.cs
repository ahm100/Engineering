using Engineering.Application.WebServices.HumenResourceServices.Posts.Models.GetPostTitleThirdParties;

namespace Engineering.Application.Abstractions.Interfaces;

public interface IHumenResourceService
{
    Task<GetPostTitleThirdPartiesResponse> GetPostTitleThirdParties(
        GetPostTitleThirdPartiesRequest request, CT ct);
}
