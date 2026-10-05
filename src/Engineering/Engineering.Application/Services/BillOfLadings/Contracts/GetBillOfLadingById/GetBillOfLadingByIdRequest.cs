namespace Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;

public record GetBillOfLadingByIdRequest(
    long Id)
    : IHttpRequest;