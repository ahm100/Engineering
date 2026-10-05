using Engineering.Application.WebServices.HumenResourceServices.Posts.Models.GetPostTitleThirdParties;

namespace Engineering.Infra.Providers.HumenResource;

public interface IHumenResourceProvider
{
    // دریافت سمت کاربر
    [Post("/v1/PersonnelPost/GetPostTitlesByThirdPartyIds")]
    Task<GetPostTitleThirdPartiesResponse> GetPostTitleThirdParties(
        [Body] GetPostTitleThirdPartiesRequest request, CT ct);
}
