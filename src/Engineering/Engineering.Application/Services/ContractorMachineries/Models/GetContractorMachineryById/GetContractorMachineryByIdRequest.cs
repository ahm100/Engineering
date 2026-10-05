namespace Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryById;

public record GetContractorMachineryByIdRequest(
    long Id
     ) : IHttpRequest;
