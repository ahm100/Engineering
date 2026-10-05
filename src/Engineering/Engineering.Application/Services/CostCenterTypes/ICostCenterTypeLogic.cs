using Engineering.Application.Services.CostCenterTypes.Models.ActiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.CodeCreator;
using Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeExcelImports;
using Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeGroupDelete;
using Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.DisableCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelEnum;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;
using Engineering.Application.Services.CostCenterTypes.Models.InactiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;

namespace Engineering.Application.Services.CostCenterTypes;

public interface ICostCenterTypeLogic
{
    ///Commands
    Task<Result<CreateCostCenterTypeResponse?>> CreateCostCenterType(
        CreateCostCenterTypeRequest request, CT ct);

    Task<Result<UpdateCostCenterTypeResponse?>> UpdateCostCenterType(
        UpdateCostCenterTypeRequest request, CT ct);

    Task<Result<ActiveCostCenterTypeResponse?>> ActiveCostCenterType(
        ActiveCostCenterTypeRequest request, CT ct);

    Task<Result<DisableCostCenterTypeResponse?>> DisableCostCenterType(
        DisableCostCenterTypeRequest request, CT ct);

    Task<Result<InactiveCostCenterTypeResponse?>> InactiveCostCenterType(
        InactiveCostCenterTypeRequest request, CT ct);

    Task<Result<CostCenterTypeCodeCreatorResponse?>> CostCenterTypeCodeCreator(
        CostCenterTypeCodeCreatorRequest request, CT ct);

    Task<Result<CostCenterTypeGroupDeleteResponse?>> CostCenterTypeGroupDelete(
        CostCenterTypeGroupDeleteRequest request, CT ct);

    Task<Result<CostCenterTypeExcelImportsResponse?>> CostCenterTypeExcelImports(
        CostCenterTypeExcelImportsRequest request, CT ct);

    Task<Result<StateChangerCostCenterTypesResponse?>> StateChangerCostCenterTypes(
        StateChangerCostCenterTypesRequest request, CT ct);

    ///Queries
    Task<Result<GetsCostCenterTypeResponse?>> GetsCostCenterType(
        GetsCostCenterTypeRequest request, CT ct);

    Task<Result<GetCostCenterTypeByIdResponse?>> GetCostCenterTypeById(
        GetCostCenterTypeByIdRequest request, CT ct);

    Task<Result<GetCostCenterTypeByCodeResponse?>> GetCostCenterTypeByCode(
        GetCostCenterTypeByCodeRequest request, CT ct);

    Task<Result<GetCostCenterTypeByNameResponse?>> GetCostCenterTypeByName(
        GetCostCenterTypeByNameRequest request, CT ct);

    Task<Result<GetsActiveCostCenterTypesResponse?>> GetsActiveCostCenterTypes(
        GetsActiveCostCenterTypesRequest request, CT ct);

    Task<Result<GetsCostCenterTypeExcelEnumResponse?>> GetsCostCenterTypeExcelEnum(
        GetsCostCenterTypeExcelEnumRequest request, CT ct);

    Task<Result<GetsCostCenterTypeExcelExporterResponse?>> GetsCostCenterTypeExcelExporter(
        GetsCostCenterTypeExcelExporterRequest request, CT ct);
}