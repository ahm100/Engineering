using Engineering.Application.Services.MachineTypes.Models.ActiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.CreateMachineType;
using Engineering.Application.Services.MachineTypes.Models.DisableMachineType;
using Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;
using Engineering.Application.Services.MachineTypes.Models.GetMachineTypes;
using Engineering.Application.Services.MachineTypes.Models.GetsActiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelEnum;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;
using Engineering.Application.Services.MachineTypes.Models.InactiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeCodeCreator;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeExcelImports;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeGroupDelete;
using Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;
using Engineering.Application.Services.MachineTypes.Models.UpdateMachineType;

namespace Engineering.Application.Services.MachineTypes;
public interface IMachineTypeLogic
{
    Task<Result<CreateMachineTypeResponse?>> CreateMachineType(
        CreateMachineTypeRequest request, CT ct);

    Task<Result<MachineTypeExcelImportsResponse?>> MachineTypeExcelImports(
        MachineTypeExcelImportsRequest request, CT ct);

    Task<Result<UpdateMachineTypeResponse?>> UpdateMachineType(
        UpdateMachineTypeRequest request, CT ct);

    Task<Result<DisableMachineTypeResponse?>> DisableMachineType(
        DisableMachineTypeRequest request, CT ct);

    Task<Result<InactiveMachineTypeResponse?>> InactiveMachineType(
        InactiveMachineTypeRequest request, CT ct);

    Task<Result<ActiveMachineTypeResponse?>> ActiveMachineType(
        ActiveMachineTypeRequest request, CT ct);

    Task<Result<StateChangerMachineTypesResponse?>> StateChangerMachineTypes(
        StateChangerMachineTypesRequest request, CT ct);

    Task<Result<MachineTypeCodeCreatorResponse?>> MachineTypeCodeCreator(
        MachineTypeCodeCreatorRequest request, CT ct);

    Task<Result<MachineTypeGroupDeleteResponse?>> MachineTypeGroupDelete(
        MachineTypeGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetMachineTypeByIdResponse?>> GetMachineTypeById(
        GetMachineTypeByIdRequest request, CT ct);

    Task<Result<GetMachineTypesResponse?>> GetMachineTypes(
        GetMachineTypesRequest request, CT ct);

    Task<Result<GetsActiveMachineTypeResponse?>> GetsActiveMachineType(
        GetsActiveMachineTypeRequest request, CT ct);

    Task<Result<GetsMachineTypeExcelEnumResponse?>> GetsMachineTypeExcelEnum(
        GetsMachineTypeExcelEnumRequest request, CT ct);

    Task<Result<GetsMachineTypeExcelExporterResponse?>> GetsMachineTypeExcelExporter(
        GetsMachineTypeExcelExporterRequest request, CT ct);

}
