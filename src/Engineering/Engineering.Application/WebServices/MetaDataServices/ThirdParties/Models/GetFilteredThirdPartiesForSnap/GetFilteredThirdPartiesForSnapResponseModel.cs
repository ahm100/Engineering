namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdPartiesForSnap;

public record GetFilteredThirdPartiesForSnapResponseModel
{
    [JsonProperty("data")]
    public List<GetFilteredForSnapModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public record GetFilteredForSnapModel(
    long Id,
    long UserId,
    string FirstName,
    string LastName,
    string? Nickname,
    string FullName,
    string OrganizationCode,
    string DefaultPhoneNo,
    string? IdentityNo);