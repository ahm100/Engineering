using Engineering.Domain.Entities.Synonyms.MetaData.Addresses;
using Engineering.Domain.Entities.Synonyms.MetaData.Legals;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;

[NotMapped]
public class ViewThirdParty : ActivateEntity<ViewThirdParty>
{
    private ViewThirdParty()
    {
        _addresses = new List<ViewAddress>();
    }

    public long? UserId { get; set; }
    public long? LegacyId { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool? IsIndividual { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? FatherName { get; set; }
    public string? IdentityNo { get; set; }
    public string? IdentitySerialNo { get; set; }
    public string? NationalCode { get; set; }
    public string? AvatarUrl { get; set; }
    public string? DefaultEmailAdd { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? ExtensionPhoneNo { get; set; }
    public string? OrganizationCode { get; set; }
    public long? EconomicCode { get; set; }
    public string? UniqueCode { get; set; }
    public long? LegalId { get; set; }
    public ViewLegal? Legal { get; set; }
    public long? CountryId { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
    public long? EmploymentTypeId { get; set; }
    public long? EmploymentStatusId { get; set; }
    public string? NationalCodeSerialNo { get; set; }
    public int? Gender { get; set; }
    public int? DutyStatus { get; set; }
    public int? MaritalStatus { get; set; }
    public string? FirstNameEn { get; set; }
    public string? LastNameEn { get; set; }
    [NotMapped]
    public string? FullName => FirstName + " " + LastName;
    public string? Description { get; set; }
    public string? ExportationPlace { get; set; }
    public string? Religion { get; set; }
    public string? Sect { get; set; }
    public bool? HasWorkPermit { get; set; }
    public bool? AsSoldierInComplex { get; set; }
    public bool? HasVeteranStatus { get; set; }
    public bool? HasDisability { get; set; }
    public int? WorkPermitIssuer { get; set; }
    public DateTime? ValidityDateWorkPermit { get; set; }
    public DateTime? IssuanceDate { get; set; }
    public string? WorkCertificateNo { get; set; }
    public string? BirthLocation { get; set; }
    public long? PersonnelId { get; set; }
    public Nationality? Nationality { get; private set; }
    public Citizenship? Citizenship { get; private set; }
    public long? OrganizationId { get; set; }
    public string? OrganizationDescription { get; set; }
    private readonly IList<ViewAddress> _addresses;
    public IEnumerable<ViewAddress> Addresses => _addresses.AsReadOnly();
}

public enum Nationality
{
    [Description("ایرانی")] Iranian = 1,
}

public enum Citizenship
{
    [Description("ایرانی")] Iranian = 1,
    [Description("غیر ایرانی")] NonIranian = 2,
}