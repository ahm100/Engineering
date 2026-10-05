namespace Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;

public record UpdateTransportPalletLoadWeightRequest(
    long? TransportationRequestId,
    List<TransportPalletWeightsModel> TransportPallets
     ) : IHttpRequest;

public record TransportPalletWeightsModel
{
    public long Id { get; set; }
    public decimal Weight { get; set; } = 0;
}
