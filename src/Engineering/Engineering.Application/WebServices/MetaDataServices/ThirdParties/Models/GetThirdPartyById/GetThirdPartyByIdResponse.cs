
using Engineering.Application.WebServices.MetaDataServices.Contractors.Commands.CreateContractor;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetThirdPartyById;

public class GetThirdPartyByIdResponse
{
    [JsonProperty("value")]
    public ThirdPartyUserModel? Value { get; set; }

}

public record ThirdPartyUserModel(
    long Id,
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
    Legal? legal,
    string? firstName,
    string? lastName,
    string? fatherName,
    string? identityNo,
    string? identitySerialNo,
    string? economicCode,
    DateTime? birthDate,
    List<AddressDataResponse?>? addresses,
    List<UserSkill?>? Skills,
    List<Leader?>? leaders
    );

public record UserSkill(
    long? Id,
    string? Name,
    string? Code
    );

public record Leader(
    long? Id,
    bool? IsIndividual,
    long? UserId,
    string? FullName
    );
public record AddressDataResponse(
    long Id,
    long? CityId,
    long ThirdPartyId,
    bool? IsDefault,
    bool IsActive,
    string Title,
    string AddressText,
    string? PostalCode,
    string? ApartmentNo,
    string? BuzzerNo,
    string? FloorNo,
    decimal? Latitude,
    decimal? Longitude,
    CityDataResponse? City);

public record CityDataResponse(
    long Id,
    string Name,
    string Code,
    bool IsActive,
    string EnglishName,
    string Iso,
    long ProvinceId,
    string? ProvinceName
    );