using Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetFltrByContractorIds;

public record GetFltrByContractorIdsQuery(
    List<long> ContractorIds
    ) : IQuery<GetFltrByContractorIdsResponse?>;