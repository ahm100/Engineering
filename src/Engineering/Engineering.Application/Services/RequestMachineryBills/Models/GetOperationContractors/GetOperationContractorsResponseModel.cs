namespace Engineering.Application.Services.RequestMachineries.Models.GetOperationContractors;

public record GetOperationContractorsResponseModel
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
};
