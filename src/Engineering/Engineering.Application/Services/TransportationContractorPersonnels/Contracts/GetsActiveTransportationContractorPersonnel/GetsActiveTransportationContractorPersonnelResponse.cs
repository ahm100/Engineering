
namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;

public record GetsActiveTransportationContractorPersonnelResponse(
    List<GetsActiveTransportationContractorPersonnelResponseModel> Data,
    int RowCount
    );
