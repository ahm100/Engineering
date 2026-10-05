using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetProjectOperationDetailByIds;

public record GetInfoByContractorServiceIdQuery
(
    List<long?> ContractorServiceIds
) : IQuery<DataResult<List<GetInfoByContractorServiceIdResponse>>>;
