namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsProjectOperationDetailContractors;

public record GetsProjectOperationDetailContractorsResponseModel
{
    public long Id { get; set; }
    public string? Nickname { get; set; }
    public string? FullName { get; set; }
    public string? OrganizationCode { get; set; }
};

