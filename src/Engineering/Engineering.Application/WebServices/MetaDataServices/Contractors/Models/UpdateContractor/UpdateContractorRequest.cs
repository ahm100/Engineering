namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Models.UpdateContractor;


public record Legal(
string LogoUrl,
string CompanyName,
string RegistrationNo,
string RegistrationDate,
string RegistrationLocation
);

public record UpdateContractorRequest(
long Id,
bool IsIndividual,
string FirstName,
string LastName,
string DefaultPhoneNo,
string DefaultEmailAdd,
string OrganizationCode,
string FatherName,
string NationalCode,
string IdentitySerialNo,
string IdentityNo,
string AvatarUrl,
bool IsActive,
string UserId,
DateTime BirthDate,
string UniqueCode,
int EconomicCode,
Legal Legal
);

