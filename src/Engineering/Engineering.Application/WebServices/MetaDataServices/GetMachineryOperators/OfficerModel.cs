namespace Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators;

public record OfficerModel(long Id,
                      bool? IsIndividual,
                      long? UserId,
                      string? FullName,
                      string? DefaultPhoneNo,
                      string? DefaultEmailAdd,
                      string? AvatarUrl,
                      string? NationalCode,
                      string? OrganizationCode,
                      string? UniqueCode,
                      bool? IsActive,
                      DateTime? birthDate,
                      LegalModel? Legal);