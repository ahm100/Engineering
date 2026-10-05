namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;

public record GetsFilteredTransportationContractorMachineResponse(
    List<GetsFilteredTransportationContractorMachineResponseModel> Data,
    int RowCount
    );
