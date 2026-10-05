namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractors;

public record GetsProjectOperationDetailContractorsQuery(
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<long?>>>;