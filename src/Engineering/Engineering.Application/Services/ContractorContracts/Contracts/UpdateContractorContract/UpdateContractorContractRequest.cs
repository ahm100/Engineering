using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract.ItemPriceList;

namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

public record UpdateContractorContractRequest(
    long Id,
    long ProjectId,
    long ContractorId,
    long CurrencyId,
    List<string>? Urls,
    List<CreateFixCCRequest>? CreateFixContractors,
    List<UpdateFixCCRequest>? UpdateFixContractors,

    List<CreateServiceCCRequest>? CreateServiceContractors,
    List<UpdateServiceCCRequest>? UpdateServiceContractors,

    List<CreateItemPriceListCCRequest>? CreateItemPriceListContracts,
    List<UpdateItemPriceListCCRequest>? UpdateItemPriceListContracts,

    List<UpdateProfessionalWorkdayCCRequest>? UpdateProfessionalWorkdayContracts,
    string? Description
    ) : IHttpRequest;
