namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;

public record GetCSSDailyServiceUrlsRequest(
    long Id,
    bool? IsDraft
    ) : IHttpRequest;
