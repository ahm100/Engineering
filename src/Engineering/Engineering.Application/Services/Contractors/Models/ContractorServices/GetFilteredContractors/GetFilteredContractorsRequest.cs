namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;

public record GetFilteredContractorsRequest(long ProjectId,
                                            List<long>? ProjectOperationIds,
                                            string? FilterData,
                                            int PageIndex,
                                            int PageSize) : IHttpRequest;

