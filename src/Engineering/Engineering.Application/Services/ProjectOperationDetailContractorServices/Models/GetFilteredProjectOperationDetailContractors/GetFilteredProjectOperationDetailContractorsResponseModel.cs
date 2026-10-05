namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetFilteredProjectOperationDetailContractors;

public record GetFilteredProjectOperationDetailContractorsResponseModel
{
    public long Id { get; set; }
    public string? Nickname { get; set; }
    public string? FullName { get; set; }
    public string? OrganizationCode { get; set; }
};

