
using Engineering.ClientSdk.Enums;

namespace Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

public record CreateTransportationContractorRequest(
    string? Title,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? IdentityNo,
    string? Description,
    string? CompanyName,
    string? RegistrationNo,
    DateTime? StartOfContract,
    DateTime? EndOfContract,
    bool IsActive,
    long? LagacyId,
    List<string>? Documents,
    DeliveryMethod[]? DeliveryMethod,
    DeliveryType[]? DeliveryType,
    TransportationContractorCalculateType Type,
    decimal? PercentageValue,
    decimal? FixedNumber,
    string FirstPrefix,
    long? SecondPrefix,
    decimal? TaxPercent,
    decimal? ServicePrice,
    List<long>? Managers,
    List<CreateContractorPersonnelModel>? ContractorPersonnels,
    CreateTransportationContractorAddressModel? Address,
    List<CreateContractorPriceWeightModel>? PriceWeights,
    List<CreateContractorInsuranceModel>? Insurances
    ) : IHttpRequest;