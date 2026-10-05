namespace Engineering.Application.Services.BillOfLadings.Contracts.DeleteBillOfLadings;

public record DeleteBillOfLadingRequest(
    long Id
    ) : IHttpRequest;