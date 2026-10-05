namespace Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;

public record GetFltrByContractorIdsRequest(
    List<long> ContractorIds
     ) : IHttpRequest;
