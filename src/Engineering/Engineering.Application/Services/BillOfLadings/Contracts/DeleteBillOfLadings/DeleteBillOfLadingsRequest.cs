namespace Engineering.Application.Services.BillOfLadings.Contracts.DeleteBillOfLadings;

public record DeleteBillOfLadingsRequest(
    List<long> Ids
    ) : IHttpRequest;