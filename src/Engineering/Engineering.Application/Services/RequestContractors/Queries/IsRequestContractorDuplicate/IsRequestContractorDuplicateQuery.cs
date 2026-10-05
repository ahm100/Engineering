namespace Engineering.Application.Services.RequestContractors.Queries.IsRequestContractorDuplicate;

public record IsRequestContractorDuplicateQuery(
    long ProjectOperationDetailId,
    long ServiceInfoId
    ) : IQuery<bool>;

