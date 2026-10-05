using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;

namespace Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;

public record GetCCHVersionByCCHIdResponse(
    List<GetContractorContractHeaderByIdResponse>? Data);