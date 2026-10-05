using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdNew;

public record GetContractorContractHeaderByIdNewQuery(
    long Id,
    long CompanyId
    ) : IQuery<GetContractorContractHeaderByIdResponse?>;
