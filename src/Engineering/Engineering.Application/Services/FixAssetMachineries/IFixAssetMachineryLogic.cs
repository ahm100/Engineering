using Engineering.Application.Services.FixAssetMachineries.Models.ActiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryDocument;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetNotWorkDocument;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryGroupDelete;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryNotWorkGroupDelete;
using Engineering.Application.Services.FixAssetMachineries.Models.GetActiveFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorkById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorks;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryRateById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryType;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsRateByFixAssetMachineryId;
using Engineering.Application.Services.FixAssetMachineries.Models.InactiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries;

public interface IFixAssetMachineryLogic
{
    ///Commands
    Task<Result<CreateFixAssetMachineryResponse?>> CreateFixAssetMachinery(
        CreateFixAssetMachineryRequest request, CT ct);

    Task<Result<CreateFixAssetMachineryDocumentResponse?>> CreateFixAssetMachineryDocument(
        CreateFixAssetMachineryDocumentRequest request, CT ct);

    Task<Result<DisableFixAssetMachineryResponse?>> DisableFixAssetMachinery(
        DisableFixAssetMachineryRequest request, CT ct);

    Task<Result<UpdateFixAssetMachineryResponse?>> UpdateFixAssetMachinery(
        UpdateFixAssetMachineryRequest request, CT ct);

    Task<Result<InactiveFixAssetMachineryResponse?>> InactiveFixAssetMachinery(
        InactiveFixAssetMachineryRequest request, CT ct);

    Task<Result<ActiveFixAssetMachineryResponse?>> ActiveFixAssetMachinery(
        ActiveFixAssetMachineryRequest request, CT ct);

    Task<Result<StateChangerFixAssetMachineriesResponse?>> StateChangerFixAssetMachineries(
        StateChangerFixAssetMachineriesRequest request, CT ct);

    Task<Result<FixAssetMachineryGroupDeleteResponse?>> FixAssetMachineryGroupDelete(
        FixAssetMachineryGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetFixAssetMachineryByIdResponse?>> GetFixAssetMachineryById(
        GetFixAssetMachineryByIdRequest request, CT ct);

    Task<Result<GetActiveFixAssetMachineriesResponse?>> GetsActiveFixAssetMachinery(
        GetActiveFixAssetMachineriesRequest request, CT ct);

    Task<Result<GetFixAssetMachineriesResponse?>> GetsFixAssetMachinery(
        GetFixAssetMachineriesRequest request, CT ct);

    Task<Result<GetsFixAssetMachineryExcelEnumResponse?>> GetsFixAssetMachineryExcelEnum(
        GetsFixAssetMachineryExcelEnumRequest request, CT ct);

    Task<Result<GetsFixAssetMachineryExcelExporterResponse?>> GetsFixAssetMachineryExcelExporter(
        GetsFixAssetMachineryExcelExporterRequest request, CT ct);

    Task<Result<GetFixAssetMachineryTypeResponse?>> GetFixAssetMachineryType(
        GetFixAssetMachineryTypeRequest request, CT ct);

    //NotWorks
    Task<Result<CreateFixAssetMachineryNotWorkResponse?>> CreateFixAssetMachineryNotWork(
        CreateFixAssetMachineryNotWorkRequest request, CT ct);

    Task<Result<CreateFixAssetNotWorkDocumentResponse?>> CreateFixAssetNotWorkDocument(
        CreateFixAssetNotWorkDocumentRequest request, CT ct);

    Task<Result<DisableFixAssetMachineryNotWorkResponse?>> DisableFixAssetMachineryNotWork(
        DisableFixAssetMachineryNotWorkRequest request, CT ct);

    Task<Result<UpdateFixAssetMachineryNotWorkResponse?>> UpdateFixAssetMachineryNotWork(
        UpdateFixAssetMachineryNotWorkRequest request, CT ct);

    Task<Result<FixAssetMachineryNotWorkGroupDeleteResponse?>> FixAssetMachineryNotWorkGroupDelete(
        FixAssetMachineryNotWorkGroupDeleteRequest request, CT ct);

    Task<Result<GetFixAssetMachineryNotWorkByIdResponse?>> GetFixAssetMachineryNotWorkById(
        GetFixAssetMachineryNotWorkByIdRequest request, CT ct);

    Task<Result<GetFixAssetMachineryNotWorksResponse?>> GetsFixAssetMachineryNotWork(
        GetFixAssetMachineryNotWorksRequest request, CT ct);

    Task<Result<GetsFixAssetMachineryNotWorkExcelExporterResponse?>> GetsFixAssetMachineryNotWorkExcelExporter(
        GetsFixAssetMachineryNotWorkExcelExporterRequest request, CT ct);

    Task<Result<GetsFixAssetMachineryNotWorkExcelEnumResponse?>> GetsFixAssetMachineryNotworkExcelEnum(
        GetsFixAssetMachineryNotWorkExcelEnumRequest request, CT ct);

    //Rate
    Task<Result<CreateFixAssetMachineryRateResponse?>> CreateFixAssetMachineryRate(
        CreateFixAssetMachineryRateRequest request, CT ct);

    Task<Result<DisableFixAssetMachineryRateResponse?>> DisableFixAssetMachineryRate(
        DisableFixAssetMachineryRateRequest request, CT ct);

    Task<Result<UpdateFixAssetMachineryRateResponse?>> UpdateFixAssetMachineryRate(
        UpdateFixAssetMachineryRateRequest request, CT ct);

    Task<Result<GetFixAssetMachineryRateByIdResponse?>> GetFixAssetMachineryRateById(
        GetFixAssetMachineryRateByIdRequest request, CT ct);

    Task<Result<GetsRateByFixAssetMachineryIdResponse?>> GetsRateByFixAssetMachineryId(
        GetsRateByFixAssetMachineryIdRequest request, CT ct);
}