using Engineering.ClientSdk.Models.TransportationContractor;

namespace Engineering.ClientSdk.Services;

public interface ITransportationContractorService
{
    Task<CreateWarehouseTransportationResponseDto> CreateWarehouseTransportation(CreateWarehouseTransportationRequestDto request, CancellationToken ct);

    Task<GetTransportationContractorResponseDto> GetTransportationContractor(GetTransportationContractorRequestDto request, CancellationToken ct);

    Task<GetDeliveryMethodResponseDto> GetDeliveryMethod(GetDeliveryMethodRequestDto request, CancellationToken ct);

    Task<GetDeliveryTypeResponseDto> GetDeliveryType(GetDeliveryTypeRequestDto request, CancellationToken ct);
}
