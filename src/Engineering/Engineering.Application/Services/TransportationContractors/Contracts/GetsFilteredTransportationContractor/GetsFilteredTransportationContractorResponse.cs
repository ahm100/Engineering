namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;

public record GetsFilteredTransportationContractorResponse(
    List<GetsFilteredTransportationContractorResponseModel> Data,
    int RowCount
    );
