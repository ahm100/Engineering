namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers;

public record Officer(long Id,
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
                      Legal? Legal);