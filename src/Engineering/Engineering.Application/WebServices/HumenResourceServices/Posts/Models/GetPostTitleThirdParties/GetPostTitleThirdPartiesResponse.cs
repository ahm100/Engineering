namespace Engineering.Application.WebServices.HumenResourceServices.Posts.Models.GetPostTitleThirdParties;

public class GetPostTitleThirdPartiesResponse
{
    public List<PostTitleItem>? Value { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsFailure { get; set; }
    public object? Error { get; set; }
}

public class PostTitleItem
{
    public long ThirdPartyId { get; set; }
    public string? PostTitle { get; set; }
}