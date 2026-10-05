namespace Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryContractor;

public record GetsFilteredMachineryContractorResponseModel
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
};
