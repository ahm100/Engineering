using Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingCodeCreator;
using Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingExcelImports;
using Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;
using Engineering.Application.Services.BillOfLadings.Contracts.CreateBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.DeleteBillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelEnum;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.UpdateBillOfLading;
using Engineering.Domain.Entities.BillOfLadings;

namespace Engineering.Application.Services.BillOfLadings;

public interface IBillOfLadingLogic
{
    Task<Result<CreateBillOfLadingResponse?>> CreateBillOfLading(
        CreateBillOfLadingRequest request, CT ct);

    Task<Result<ChangeBillOfLadingStateResponse?>> ChangeBillOfLadingState(
        ChangeBillOfLadingStateRequest request, CT ct);

    Task<Result<UpdateBillOfLadingResponse?>> UpdateBillOfLading(
        UpdateBillOfLadingRequest request, CT ct);

    Task<Result<BillOfLadingCodeCreatorResponse?>> BillOfLadingCodeCreator(
        BillOfLadingCodeCreatorRequest request, CT ct);

    Task<Result<DeleteBillOfLadingsResponse?>> DeleteBillOfLadings(
        DeleteBillOfLadingsRequest request, CT ct);

    Task<Result<BillOfLadingExcelImportsResponse?>> BillOfLadingExcelImports(
        BillOfLadingExcelImportsRequest request, CT ct);

    Task<Result<GetBillOfLadingByIdResponse?>> GetBillOfLadingById(
        GetBillOfLadingByIdRequest request, CT ct);

    Task<Result<BillOfLading?>> GetBillOfLadingByIdHandle(
        long id, CT ct);

    Task<Result<GetBillOfLadingByIdResponse?>> GetBillOfLadingByIdForResponseHandle(
        long id, CT ct);

    Task<Result<GetsActiveBillOfLadingResponse?>> GetsActiveBillOfLading(
        GetsActiveBillOfLadingRequest request, CT ct);

    Task<Result<GetsFilteredBillOfLadingResponse?>> GetsFilteredBillOfLading(
        GetsFilteredBillOfLadingRequest request, CT ct);

    Task<Result<GetsBillOfLadingExcelEnumResponse?>> GetsBillOfLadingExcelEnum(
        GetsBillOfLadingExcelEnumRequest request, CT ct);

    Task<Result<GetsBillOfLadingExcelExporterResponse?>> GetsBillOfLadingExcelExporter(
        GetsBillOfLadingExcelExporterRequest request, CT ct);

}