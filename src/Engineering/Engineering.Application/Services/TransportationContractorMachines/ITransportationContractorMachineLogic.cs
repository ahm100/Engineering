using Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.CreateTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.DeleteTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelEnum;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelExporter;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.UpdateTransportationContractorMachine;

namespace Engineering.Application.Services.TransportationContractorMachines;

public interface ITransportationContractorMachineLogic
{
    ///Commands
    Task<Result<CreateTransportationContractorMachineResponse?>> CreateTransportationContractorMachine(
        CreateTransportationContractorMachineRequest request, CT ct);
    Task<Result<DeleteTransportationContractorMachineResponse?>> DeleteTransportationContractorMachine(
        DeleteTransportationContractorMachineRequest request, CT ct);
    Task<Result<UpdateTransportationContractorMachineResponse?>> UpdateTransportationContractorMachine(
        UpdateTransportationContractorMachineRequest request, CT ct);
    Task<Result<ChangeTransportationContractorMachineStateResponse?>> ChangeTransportationContractorMachineState(
        ChangeTransportationContractorMachineStateRequest request, CT ct);

    ///Queries
    Task<Result<GetTransportationContractorMachineByIdResponse?>> GetTransportationContractorMachineById(
        GetTransportationContractorMachineByIdRequest request, CT ct);
    Task<Result<GetsActiveTransportationContractorMachineResponse?>> GetsActiveTransportationContractorMachine(
        GetsActiveTransportationContractorMachineRequest request, CT ct);
    Task<Result<GetsFilteredTransportationContractorMachineResponse?>> GetsFilteredTransportationContractorMachine(
        GetsFilteredTransportationContractorMachineRequest request, CT ct);
    Task<Result<GetsTransportationContractorMachineExcelExporterResponse?>> GetsTransportationContractorMachineExcelExporter(
        GetsTransportationContractorMachineExcelExporterRequest request, CT ct);
    Task<Result<GetsTransportationContractorMachineExcelEnumResponse?>> GetsTransportationContractorMachineExcelEnum(
        GetsTransportationContractorMachineExcelEnumRequest request, CT ct);
}