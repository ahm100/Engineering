namespace Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;

public record GetCCHVersionByCCHIdRequest(
    long Id
    ) : IHttpRequest;