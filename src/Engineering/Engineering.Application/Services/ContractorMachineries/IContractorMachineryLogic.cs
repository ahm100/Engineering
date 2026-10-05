using Engineering.Application.Services.ContractorMachineries.Models.ActiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryGroupDelete;
using Engineering.Application.Services.ContractorMachineries.Models.CreateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.DisableContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.GetActiveContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryById;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryUnit;
using Engineering.Application.Services.ContractorMachineries.Models.GetsByContractorId;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelEnum;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelExporter;
using Engineering.Application.Services.ContractorMachineries.Models.InactiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.UpdateContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries;

public interface IContractorMachineryLogic
{
    ///Commands
    Task<Result<CreateContractorMachineryResponse?>> CreateContractorMachinery(
        CreateContractorMachineryRequest request, CT ct);

    Task<Result<DisableContractorMachineryResponse?>> DisableContractorMachinery(
        DisableContractorMachineryRequest request, CT ct);

    Task<Result<UpdateContractorMachineryResponse?>> UpdateContractorMachinery(
        UpdateContractorMachineryRequest request, CT ct);

    Task<Result<InactiveContractorMachineryResponse?>> InactiveContractorMachinery(
        InactiveContractorMachineryRequest request, CT ct);

    Task<Result<ActiveContractorMachineryResponse?>> ActiveContractorMachinery(
        ActiveContractorMachineryRequest request, CT ct);

    Task<Result<StateChangerContractorMachineriesResponse?>> StateChangerContractorMachineries(
        StateChangerContractorMachineriesRequest request, CT ct);

    Task<Result<ContractorMachineryGroupDeleteResponse?>> ContractorMachineryGroupDelete(
        ContractorMachineryGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetContractorMachineryByIdResponse?>> GetContractorMachineryById(
        GetContractorMachineryByIdRequest request, CT ct);

    Task<Result<GetActiveContractorMachineriesResponse?>> GetsActiveContractorMachinery(
        GetActiveContractorMachineriesRequest request, CT ct);

    Task<Result<GetContractorMachineriesResponse?>> GetsContractorMachinery(
        GetContractorMachineriesRequest request, CT ct);

    Task<Result<GetsContractorMachineryExcelEnumResponse?>> GetsContractorMachineryExcelEnum(
        GetsContractorMachineryExcelEnumRequest request, CT ct);

    Task<Result<GetsContractorMachineryExcelExporterResponse?>> GetsContractorMachineryExcelExporter(
        GetsContractorMachineryExcelExporterRequest request, CT ct);

    Task<Result<GetContractorMachineryUnitResponse?>> GetContractorMachineryUnit(
        GetContractorMachineryUnitRequest request, CT ct);

    Task<Result<GetsByContractorIdResponse?>> GetsByContractorId(
        GetsByContractorIdRequest request, CT ct);
}