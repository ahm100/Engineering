
namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;

public record GetsActiveTransportationContractorMachineResponse(
    List<GetsActiveTransportationContractorMachineResponseModel> Data,
    int RowCount
    );
