using Engineering.Application.Services.OperationLocations.Models.ActiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithCostCenter;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithParent;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithCostCenter;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithParent;
using Engineering.Application.Services.OperationLocations.Models.DisableOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.GetActiveOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationById;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationCodeByCostCenter;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationNameByCostCenter;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.GetsByCostCenterId;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationChild;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelEnum;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;
using Engineering.Application.Services.OperationLocations.Models.GetsWithoutParentOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.InactiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.OperationLocationGroupDelete;
using Engineering.Application.Services.OperationLocations.Models.SetPriority;
using Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.UpdateOperationLocation;

namespace Engineering.Application.Services.OperationLocations;

public interface IOperationLocationLogic
{
    ///Commands
    Task<Result<CreateOperationLocationWithCostCenterResponse?>> CreateOperationLocationWithCostCenter(
        CreateOperationLocationWithCostCenterRequest request, CT ct);

    Task<Result<CreateOperationLocationWithParentResponse?>> CreateOperationLocationWithParent(
        CreateOperationLocationWithParentRequest request, CT ct);

    Task<Result<CreateOperationLocationCodeWithCostCenterResponse?>> CreateOperationLocationCodeWithCostCenter(
        CreateOperationLocationCodeWithCostCenterRequest request, CT ct);

    Task<Result<CreateOperationLocationCodeWithParentResponse?>> CreateOperationLocationCodeWithParent(
        CreateOperationLocationCodeWithParentRequest request, CT ct);

    Task<Result<DisableOperationLocationResponse?>> DisableOperationLocation(
        DisableOperationLocationRequest request, CT ct);

    Task<Result<UpdateOperationLocationResponse?>> UpdateOperationLocation(
        UpdateOperationLocationRequest request, CT ct);

    Task<Result<SetOperationLocationPriorityResponse?>> SetOperationLocationPriority(
        SetOperationLocationPriorityRequest request, CT ct);

    Task<Result<InactiveOperationLocationResponse?>> InactiveOperationLocation(
        InactiveOperationLocationRequest request, CT ct);

    Task<Result<ActiveOperationLocationResponse?>> ActiveOperationLocation(
        ActiveOperationLocationRequest request, CT ct);

    Task<Result<StateChangerOperationLocationsResponse?>> StateChangerOperationLocations(
        StateChangerOperationLocationsRequest request, CT ct);

    Task<Result<OperationLocationGroupDeleteResponse?>> OperationLocationGroupDelete(
        OperationLocationGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetOperationLocationByIdResponse?>> GetOperationLocationById(
        GetOperationLocationByIdRequest request, CT ct);

    Task<Result<GetOperationLocationByNameResponse?>> GetOperationLocationByName(
        GetOperationLocationByNameRequest request, CT ct);

    Task<Result<GetOperationLocationCodeByCostCenterResponse?>> GetOperationLocationCodeByCostCenter(
        GetOperationLocationCodeByCostCenterRequest request, CT ct);

    Task<Result<GetOperationLocationNameByCostCenterResponse?>> GetOperationLocationNameByCostCenter(
        GetOperationLocationNameByCostCenterRequest request, CT ct);

    Task<Result<GetOperationLocationByCodeResponse?>> GetOperationLocationByCode(
        GetOperationLocationByCodeRequest request, CT ct);

    Task<Result<GetActiveOperationLocationsResponse?>> GetsActiveOperationLocation(
        GetActiveOperationLocationsRequest request, CT ct);

    Task<Result<GetsOperationLocationChildResponse?>> GetsOperationLocationChild(
        GetsOperationLocationChildRequest request, CT ct);

    Task<Result<GetsOperationLocationByCostCenterIdResponse?>> GetsByCostCenterId(
        GetsOperationLocationByCostCenterIdRequest request, CT ct);

    Task<Result<GetOperationLocationsResponse?>> GetOperationLocations(
        GetOperationLocationsRequest request, CT ct);

    Task<Result<GetsOperationLocationResponse?>> GetsOperationLocation(
        GetsOperationLocationRequest request, CT ct);

    Task<Result<GetsWithoutParentOperationLocationResponse?>> GetsWithoutParentOperationLocation(
        GetsWithoutParentOperationLocationRequest request, CT ct);

    Task<Result<GetsOperationLocationExcelEnumResponse?>> GetsOperationLocationExcelEnum(
        GetsOperationLocationExcelEnumRequest request, CT ct);

    Task<Result<GetsOperationLocationExcelExporterResponse?>> GetsOperationLocationExcelExporter(
        GetsOperationLocationExcelExporterRequest request, CT ct);

}