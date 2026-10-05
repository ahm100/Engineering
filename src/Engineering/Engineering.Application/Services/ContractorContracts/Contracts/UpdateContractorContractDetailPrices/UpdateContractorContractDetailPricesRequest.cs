using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContractDetailPrices;

public record UpdateContractorContractDetailPricesRequest(
    List<UpdateContractorContractDetailModelPrices> Details
    ) : IHttpRequest;

public record UpdateContractorContractDetailModelPrices(
    long ContractorContractDetailId,
    List<CreateServiceContractorContractPriceModel>? CreatePrices,
    List<UpdateServiceContractorContractPriceModel>? UpdatePrices,
    List<long>? DeleteContractorContractDetailPriceIds
    );

