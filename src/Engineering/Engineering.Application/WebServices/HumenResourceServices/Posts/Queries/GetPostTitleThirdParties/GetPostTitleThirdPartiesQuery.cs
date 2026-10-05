using Engineering.Application.WebServices.HumenResourceServices.Posts.Models.GetPostTitleThirdParties;

namespace Engineering.Application.WebServices.HumenResourceServices.Posts.Queries.GetPostTitleThirdParties;

public record GetPostTitleThirdPartiesQuery(
    List<long> ThirdPartyIds
    ) : IQuery<List<PostTitleItem>?>;