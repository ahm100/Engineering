
namespace Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

public record CreateContractorContractRequest(
    long ProjectId,
    long ContractorId,
    long CurrencyId,
    List<string>? Urls,
    List<CreateFixCCRequest>? FixContractors,
    List<CreateServiceCCRequest>? ServiceContractors,
    List<CreateProfessionalWorkdayCCRequest>? ProfessionalWorkdayContractors,
    List<CreateItemPriceListCCRequest>? ItemPriceListContracts,
    string? Description
    ) : IHttpRequest;
