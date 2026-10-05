using Engineering.Application.WebServices.MetaDataServices.Contractors.Models;

namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Commands.UpdateContractor;

public record Legal(
string LogoUrl,
string CompanyName,
string RegistrationNo,
string RegistrationDate,
string RegistrationLocation
);

public class UpdateContractorCommand : ICommand<Contractor?>
{
    public long Id { get; set; }
    public bool IsIndividual { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? DefaultEmailAdd { get; set; }
    public string? OrganizationCode { get; set; }
    public string? FatherName { get; set; }
    public string? NationalCode { get; set; }
    public string? IdentitySerialNo { get; set; }
    public string? IdentityNo { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; }
    public string? UserId { get; set; }
    public DateTime BirthDate { get; set; }
    public string? UniqueCode { get; set; }
    public int EconomicCode { get; set; }
    public Legal? Legal { get; set; }
}

