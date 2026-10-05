using Engineering.Application.Services.CostOvers;
using Engineering.Application.Services.CostOvers.Models.ActiveCostOver;
using Engineering.Application.Services.CostOvers.Models.Approval;
using Engineering.Application.Services.CostOvers.Models.CodeCreator;
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

[ApiController]
[Route("api/engineering/v1/CostOver")]
public class CostOverController : ControllerBase
{
    private readonly ICostOverLogic _logic;
    public CostOverController(ICostOverLogic logic)
    {
        _logic = logic;
    }

    /// <summary>پس از دریافت Failed یا Cancelled، فقط ایجادکننده می تواند همان تلاش را ببندد و موجودیت را به Draft برگرداند.</summary>
    [HttpPost("RecoverApproval")]
    [ResponseSchema<CostOverApprovalRequest>]
    public async Task<IResult> RecoverApproval(
        [FromBody] RecoverCostOverApprovalRequest request, CT ct)
    {
        var result = await _logic.RecoverApproval(request, ct);
        return result.GetHttpResponse();
    }
    /// <summary>هزینه ردشده را برای اصلاح و ارسال مجدد به پیش نویس برمی گرداند.</summary>
    [HttpPost("ReturnToDraft")]
    public async Task<IResult> ReturnToDraft(
        [FromBody] CostOverApprovalRequest request, CT ct)
    {
        var result = await _logic.ReturnToDraft(request, ct);
        return result.GetHttpResponse();
    }

    /// <summary>هزینه بالاسری را با هویت کاربر جاری برای تأیید ارسال می کند.</summary>
    [HttpPost("SetPendingApproval")]
    [ResponseSchema<SetPendingApprovalResponse>]
    public async Task<IResult> SetPendingApproval(
        [FromBody] SetPendingApprovalRequest request, CT ct)
    {
        var result = await _logic.SetPendingApproval(request, ct);
        return result.GetHttpResponse();
    }

    /// <summary>هزینه بالاسری را با هویت کاربر جاری برای تأیید ارسال می کند.</summary>
    [HttpPost("SubmitForApproval")]
    [ResponseSchema<CostOverApprovalResponse>]
    public async Task<IResult> SubmitForApproval(
        [FromBody] CostOverApprovalRequest request, CT ct)
    {
        var result = await _logic.SubmitForApproval(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddCostOver")]
    [ResponseSchema<CreateCostOverResponse>]
    public async Task<IResult> AddCostOver([
        FromBody] CreateCostOverRequest request, CT ct)
    {
        var result = await _logic.CreateCostOver(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CostOverCodeCreator")]
    [ResponseSchema<CostOverCodeCreatorResponse>]
    public async Task<IResult> CostOverCodeCreator(
        [FromBody] CostOverCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.CostOverCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CostOverGroupDelete")]
    [ResponseSchema<CostOverGroupDeleteResponse>]
    public async Task<IResult> CostOverGroupDelete(
        [FromBody] CostOverGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.CostOverGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateCostOvers")]
    [ResponseSchema<StateChangerCostOversResponse>]
    public async Task<IResult> ActivateCostOvers(
        [FromBody] ActivateCostOversRequest request, CT ct)
    {
        var result = await _logic.StateChangerCostOvers(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateCostOvers")]
    [ResponseSchema<StateChangerCostOversResponse>]
    public async Task<IResult> InactivateCostOvers(
        [FromBody] InactivateCostOversRequest request, CT ct)
    {
        var result = await _logic.StateChangerCostOvers(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditCostOver")]
    [ResponseSchema<UpdateCostOverResponse>]
    public async Task<IResult> EditCostOver(
        [FromBody] UpdateCostOverRequest request, CT ct)
    {
        var result = await _logic.UpdateCostOver(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveCostOver")]
    [ResponseSchema<ActiveCostOverResponse>]
    public async Task<IResult> ActiveCostOver(
        [FromBody] ActiveCostOverRequest request, CT ct)
    {
        var result = await _logic.ActiveCostOver(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveCostOver")]
    [ResponseSchema<InactiveCostOverResponse>]
    public async Task<IResult> InactiveCostOver(
        [FromBody] InactiveCostOverRequest request, CT ct)
    {
        var result = await _logic.InactiveCostOver(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostOverById")]
    [ResponseSchema<GetCostOverByIdResponse>]
    public async Task<IResult> GetCostOverById(
        [FromQuery] GetCostOverByIdRequest request, CT ct)
    {
        var result = await _logic.GetCostOverById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostOverByName")]
    [ResponseSchema<GetCostOverByNameResponse>]
    public async Task<IResult> GetCostOverByName(
        [FromQuery] GetCostOverByNameRequest request, CT ct)
    {
        var result = await _logic.GetCostOverByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostOverByCode")]
    [ResponseSchema<GetCostOverByCodeResponse>]
    public async Task<IResult> GetCostOverByCode(
        [FromQuery] GetCostOverByCodeRequest request, CT ct)
    {
        var result = await _logic.GetCostOverByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveCostOver")]
    [ResponseSchema<GetsActiveCostOversResponse>]
    public async Task<IResult> GetsActiveCostOver(
        [FromQuery] GetsActiveCostOversRequest request, CT ct)
    {
        var result = await _logic.GetsActiveCostOvers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostOver")]
    [ResponseSchema<GetsCostOversResponse>]
    public async Task<IResult> GetsCostOver(
        [FromQuery] GetsCostOversRequest request, CT ct)
    {
        var result = await _logic.GetsCostOvers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByNameOrCode")]
    [ResponseSchema<GetsCostOverByNameOrCodeResponse>]
    public async Task<IResult> GetsByNameOrCode(
        [FromQuery] GetsCostOverByNameOrCodeRequest request, CT ct)
    {
        var result = await _logic.GetsCostOverByNameOrCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostOverExcelEnum")]
    [ResponseSchema<GetsCostOverExcelEnumResponse>]
    public async Task<IResult> GetsCostOverExcelEnum(
        [FromQuery] GetsCostOverExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsCostOverExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCostOverExcelExporter")]
    [ResponseSchema<GetsCostOverExcelExporterResponse>]
    public async Task<IResult> GetsCostOverExcelExporter(
        [FromBody] GetsCostOverExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsCostOverExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableCostOver")]
    [ResponseSchema<DisableCostOverResponse>]
    public async Task<IResult> DisableCostOver(
        [FromQuery] DisableCostOverRequest request, CT ct)
    {
        var result = await _logic.DisableCostOver(request, ct);
        return result.GetHttpResponse();
    }
}
