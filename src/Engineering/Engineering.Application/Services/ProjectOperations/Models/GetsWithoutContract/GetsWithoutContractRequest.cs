namespace Engineering.Application.Services.ProjectOperations.Models.GetsWithoutContract;

public record GetsWithoutContractRequest(
    List<long>? Ids,
    long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
