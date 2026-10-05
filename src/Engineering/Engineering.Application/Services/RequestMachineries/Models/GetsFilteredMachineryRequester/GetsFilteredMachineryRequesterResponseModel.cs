namespace Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryRequester;

public record GetsFilteredMachineryRequesterResponseModel
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FirstName + " " + LastName;
    public string? Nickname { get; set; }
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? IdentityNo { get; set; }
};
