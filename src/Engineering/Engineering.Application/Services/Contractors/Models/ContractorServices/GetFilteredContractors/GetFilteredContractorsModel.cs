namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetFilteredContractors;

public record GetFilteredContractorsModel
{
    public long Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? OrganizationCode { get; set; } = string.Empty;
    public string? DefaultPhoneNo { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}
