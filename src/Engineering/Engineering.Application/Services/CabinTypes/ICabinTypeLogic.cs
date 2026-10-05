using Engineering.Application.Services.CabinTypes.Models.ActiveCabinType;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeCodeCreator;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeExcelImports;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeGroupDelete;
using Engineering.Application.Services.CabinTypes.Models.CreateCabinType;
using Engineering.Application.Services.CabinTypes.Models.DeleteCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;
using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelEnum;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelExporter;
using Engineering.Application.Services.CabinTypes.Models.InactiveCabinType;
using Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

namespace Engineering.Application.Services.CabinTypes;

public interface ICabinTypeLogic
{
    ///Commands
    Task<Result<CreateCabinTypeResponse?>> CreateCabinType(
        CreateCabinTypeRequest request, CT ct);

    Task<Result<UpdateCabinTypeResponse?>> UpdateCabinType(
        UpdateCabinTypeRequest request, CT ct);

    Task<Result<DeleteCabinTypeResponse?>> DeleteCabinType(
        DeleteCabinTypeRequest request, CT ct);

    Task<Result<ActiveCabinTypeResponse?>> ActiveCabinType(
        ActiveCabinTypeRequest request, CT ct);

    Task<Result<InactiveCabinTypeResponse?>> InactiveCabinType(
        InactiveCabinTypeRequest request, CT ct);

    Task<Result<CabinTypeCodeCreatorResponse?>> CabinTypeCodeCreator(
        CabinTypeCodeCreatorRequest request, CT ct);

    Task<Result<CabinTypeGroupDeleteResponse?>> CabinTypeGroupDelete(
        CabinTypeGroupDeleteRequest request, CT ct);

    Task<Result<CabinTypeExcelImportsResponse?>> CabinTypeExcelImports(
        CabinTypeExcelImportsRequest request, CT ct);

    Task<Result<StateChangerCabinTypesResponse?>> StateChangerCabinTypes(
        StateChangerCabinTypesRequest request, CT ct);

    ///Queries
    Task<Result<GetsCabinTypeResponse?>> GetsCabinType(
        GetsCabinTypeRequest request, CT ct);

    Task<Result<GetCabinTypeByIdResponse?>> GetCabinTypeById(
        GetCabinTypeByIdRequest request, CT ct);

    Task<Result<GetCabinTypeByNameResponse?>> GetCabinTypeByName(
        GetCabinTypeByNameRequest request, CT ct);

    Task<Result<GetCabinTypeByCodeResponse?>> GetCabinTypeByCode(
        GetCabinTypeByCodeRequest request, CT ct);

    Task<Result<GetsActiveCabinTypesResponse?>> GetsActiveCabinTypes(
        GetsActiveCabinTypesRequest request, CT ct);

    Task<Result<GetsCabinTypeExcelEnumResponse?>> GetsCabinTypeExcelEnum(
        GetsCabinTypeExcelEnumRequest request, CT ct);

    Task<Result<GetsCabinTypeExcelExporterResponse?>> GetsCabinTypeExcelExporter(
        GetsCabinTypeExcelExporterRequest request, CT ct);
}