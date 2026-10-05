namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;

public record GetContractorsByServiceIdsRequest(List<long> Ids,
                                                long? ProjectId,
                                                string? FilterData,
                                                int PageIndex,
                                                int PageSize) : IHttpRequest;

