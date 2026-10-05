
namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;

public record GetsActiveTransportationContractorResponse(
    List<GetsActiveTransportationContractorResponseModel> Data,
    int RowCount
    );
