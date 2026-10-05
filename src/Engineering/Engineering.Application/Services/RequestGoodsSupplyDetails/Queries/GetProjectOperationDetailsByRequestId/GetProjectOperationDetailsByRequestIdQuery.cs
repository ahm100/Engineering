using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailsByRequestId;

public record GetProjectOperationDetailsByRequestIdQuery(
    long ProjectOperationId,
    long ProductGroupId,
    long? ProjectOperationDetailId,
    string? FilterData,
    long? ContractorId,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<ProjectOperationDetail>>>;

