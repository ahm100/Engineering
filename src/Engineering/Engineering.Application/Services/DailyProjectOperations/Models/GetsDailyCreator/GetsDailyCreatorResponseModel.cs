namespace Engineering.Application.Services.TransportationRequests.Models.GetsDailyCreator;

public record GetsDailyCreatorResponseModel
{
    public long? ThirdPartyId { get; set; }
    public long CreatorId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? IdentityNo { get; set; }
};