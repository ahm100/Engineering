namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;

public record GetsFilteredRequesterResponseModel
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FirstName + " " + LastName;
    public string? Nickname { get; set; }
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? IdentityNo { get; set; }
};