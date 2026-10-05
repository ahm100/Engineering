namespace Engineering.Application.Services.ContractorMachineries.Models.GetsByContractorId;

public record GetsByContractorIdRequest(
    long ContractorId,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
