using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public record UpdateTransportationContractorRequest(
    long Id,
    DeliveryMethod[]? DeliveryMethod,
    DeliveryType[]? DeliveryType,
    TransportationContractorCalculateType Type,
    long? ThirdPartyId,
    string? Title,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? IdentityNo,
    string? Description,
    long? LegalId,
    bool? LegalIsDeleted,
    string? CompanyName,
    string? RegistrationNo,
    DateTime? StartOfContract,
    DateTime? EndOfContract,
    bool IsActive,
    long? LagacyId,
    decimal? PercentageValue,
    decimal? FixedNumber,
    string FirstPrefix,
    long? SecondPrefix,
    decimal? TaxPercent,
    decimal? ServicePrice,
    List<long>? Managers,
    List<string>? Documents,
    List<UpdateContractorPersonnelModel>? ContractorPersonnels,
    UpdateTransportationContractorAddressModel? Address,
    List<CreateContractorPriceWeightModel>? PriceWeights,
    List<UpdateContractorPriceWeightModel>? UpdatePriceWeights,
    List<long>? DeletePriceWeights,
    List<CreateContractorInsuranceModel>? Insurances,
    List<UpdateContractorInsuranceModel>? UpdateInsurances,
    List<long>? DeleteInsurances,
    bool? IsDeleted
    ) : IHttpRequest;