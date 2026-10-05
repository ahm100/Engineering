using Engineering.Application.Services.CostOvers.Models.ActiveCostOver;
using Engineering.Application.Services.CostOvers.Models.Approval;
using Engineering.Application.Services.CostOvers.Models.CodeCreator;
using Engineering.Application.Services.CostOvers.Models.CostOverExcelImports;
using Engineering.Application.Services.CostOvers.Models.CostOverGroupDelete;
using Engineering.Application.Services.CostOvers.Models.CreateCostOver;
using Engineering.Application.Services.CostOvers.Models.DisableCostOver;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;
using Engineering.Application.Services.CostOvers.Models.GetCostOverById;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelEnum;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelExporter;
using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;
using Engineering.Application.Services.CostOvers.Models.InactiveCostOver;
using Engineering.Application.Services.CostOvers.Models.StateChangerCostOvers;
using Engineering.Application.Services.CostOvers.Models.UpdateCostOver;

namespace Engineering.Application.Services.CostOvers;

public interface ICostOverLogic
{
    /// <summary>تلاش شکست خورده یا لغوشده جاری را با کنترل ایجادکننده برای ارسال جدید تعیین تکلیف می کند.</summary>
    Task<Result<CostOverApprovalRequest?>> RecoverApproval(
        RecoverCostOverApprovalRequest request, CT ct);
    /// <summary>هزینه ردشده را همراه بستن درخواست جاری به پیش نویس برمی گرداند.</summary>
    Task<Result<CostOverApprovalRequest?>> ReturnToDraft(
        CostOverApprovalRequest request, CT ct);

    /// <summary>درخواست تأیید و پیام خروجی را در یک واحد کار ثبت می کند.</summary>
    Task<Result<CostOverApprovalResponse?>> SubmitForApproval(
        CostOverApprovalRequest request, CT ct);
    Task<Result<SetPendingApprovalResponse?>> SetPendingApproval(
        SetPendingApprovalRequest request, CT ct);

    ///Commands
    Task<Result<CreateCostOverResponse?>> CreateCostOver(
        CreateCostOverRequest request, CT ct);

    Task<Result<UpdateCostOverResponse?>> UpdateCostOver(
        UpdateCostOverRequest request, CT ct);

    Task<Result<ActiveCostOverResponse?>> ActiveCostOver(
        ActiveCostOverRequest request, CT ct);

    Task<Result<DisableCostOverResponse?>> DisableCostOver(
        DisableCostOverRequest request, CT ct);

    Task<Result<InactiveCostOverResponse?>> InactiveCostOver(
        InactiveCostOverRequest request, CT ct);

    Task<Result<CostOverCodeCreatorResponse?>> CostOverCodeCreator(
        CostOverCodeCreatorRequest request, CT ct);

    Task<Result<CostOverGroupDeleteResponse?>> CostOverGroupDelete(
        CostOverGroupDeleteRequest request, CT ct);

    Task<Result<CostOverExcelImportsResponse?>> CostOverExcelImports(
        CostOverExcelImportsRequest request, CT ct);

    Task<Result<StateChangerCostOversResponse?>> StateChangerCostOvers(
        StateChangerCostOversRequest request, CT ct);

    ///Queries
    Task<Result<GetsCostOversResponse?>> GetsCostOvers(
        GetsCostOversRequest request, CT ct);

    Task<Result<GetCostOverByIdResponse?>> GetCostOverById(
        GetCostOverByIdRequest request, CT ct);

    Task<Result<GetCostOverByCodeResponse?>> GetCostOverByCode(
        GetCostOverByCodeRequest request, CT ct);

    Task<Result<GetCostOverByNameResponse?>> GetCostOverByName(
        GetCostOverByNameRequest request, CT ct);

    Task<Result<GetsActiveCostOversResponse?>> GetsActiveCostOvers(
        GetsActiveCostOversRequest request, CT ct);

    Task<Result<GetsCostOverExcelEnumResponse?>> GetsCostOverExcelEnum(
        GetsCostOverExcelEnumRequest request, CT ct);

    Task<Result<GetsCostOverByNameOrCodeResponse?>> GetsCostOverByNameOrCode(
        GetsCostOverByNameOrCodeRequest request, CT ct);

    Task<Result<GetsCostOverExcelExporterResponse?>> GetsCostOverExcelExporter(
        GetsCostOverExcelExporterRequest request, CT ct);
}
