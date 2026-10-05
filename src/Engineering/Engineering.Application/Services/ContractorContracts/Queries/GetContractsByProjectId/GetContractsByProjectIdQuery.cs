using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractsByProjectId;

public record GetContractsByProjectIdQuery(
    long ProjectId,
    int PageIndex,
    int PageSize,
    long CompanyId) : IQuery<GetContractsByProjectIdResponse>;
