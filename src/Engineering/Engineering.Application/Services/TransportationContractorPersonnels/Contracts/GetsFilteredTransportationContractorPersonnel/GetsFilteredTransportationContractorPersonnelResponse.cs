namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;

public record GetsFilteredTransportationContractorPersonnelResponse(
    List<GetsFilteredTransportationContractorPersonnelResponseModel> Data,
    int RowCount
    );
