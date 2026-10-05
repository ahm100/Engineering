using Engineering.ClientSdk.Models.TransportationContractor;
using IdentityServer.ClientSdk.Services.IdentityManagement;
using Microsoft.Extensions.Logging;

namespace Engineering.ClientSdk.Services.HttpImpl;

public class HttpTransportationContractorService(
    HttpClient httpClient,
    IEngineeringClientSdkJsonSerializer serializer,
    ITokenProvider tokenProvider,
    ILogger<HttpTransportationContractorService> logger
) : HttpServiceBase(httpClient, serializer, logger), ITransportationContractorService
{
    public async Task<GetTransportationContractorResponseDto> GetTransportationContractor(
         GetTransportationContractorRequestDto request, CancellationToken ct)
    {
        var token = await tokenProvider.GetAccessToken(ct);
        return await SendWithResponse<GetTransportationContractorResponseDto>(
            GetRequest(HttpMethod.Post, GetsTransportationContractorUrl(), token, request), ct
        );
    }

    public async Task<GetDeliveryMethodResponseDto> GetDeliveryMethod(
         GetDeliveryMethodRequestDto request, CancellationToken ct)
    {
        var token = await tokenProvider.GetAccessToken(ct);
        return await SendWithResponse<GetDeliveryMethodResponseDto>(
            GetRequest(HttpMethod.Get, GetDeliveryMethodUrl(), token, request), ct
        );
    }

    public async Task<GetDeliveryTypeResponseDto> GetDeliveryType(
        GetDeliveryTypeRequestDto request, CancellationToken ct)
    {
        var token = await tokenProvider.GetAccessToken(ct);
        return await SendWithResponse<GetDeliveryTypeResponseDto>(
            GetRequest(HttpMethod.Get, GetDeliveryTypeUrl(), token, request), ct
        );
    }

    public async Task<CreateWarehouseTransportationResponseDto> CreateWarehouseTransportation(
        CreateWarehouseTransportationRequestDto request, CancellationToken ct)
    {
        var token = await tokenProvider.GetAccessToken(ct);
        return await SendWithResponse<CreateWarehouseTransportationResponseDto>(
            GetRequest(HttpMethod.Post, CreateWarehouseTransportationUrl(), token, request), ct
        );
    }

    private static string CreateWarehouseTransportationUrl() => "/api/engineering/v1/TransportationRequest/AddWarehouseTransportation";
    private static string GetsTransportationContractorUrl() => "/api/engineering/v1/TransportationContractor/GetsFilteredTransportationContractor";
    private static string GetDeliveryMethodUrl() => "/api/engineering/v1/TransportationContractor/GetsDeliveryMethod";
    private static string GetDeliveryTypeUrl() => "/api/engineering/v1/TransportationContractor/GetsDeliveryType";

}
