namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;

public record GetContractorServicesByContractorIdRequest(long Id,
                                                         string? FilterData,
                                                         string[]? OrderBy,
                                                         int PageIndex,
                                                         int PageSize) : IHttpRequest;
