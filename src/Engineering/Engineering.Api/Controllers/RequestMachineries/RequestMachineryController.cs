using Engineering.Application.Extensions.Excels.Exporters;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.RequestMachineries;
using Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryBillDocument;
using Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryDocument;
using Engineering.Application.Services.RequestMachineries.Models.DeleteRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Models.GetFilteredRequestMachineries;
using Engineering.Application.Services.RequestMachineries.Models.GetManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryHistories;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryMachineries;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryProjectOperation;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryStatus;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryUnits;
using Engineering.Application.Services.RequestMachineries.Models.GetSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetsMachineryRequesteExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalOnProjectMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalOnProjectRequestReports;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GroupRequestMachineryStatusChanger;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryGroupDelete;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDateTime;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDriver;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryMachinery;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Api.Controllers.RequestMachineries;

[ApiController]
[Route("api/engineering/v1/RequestMachinery")]
public class RequestMachineryController : ControllerBase
{
    private readonly IRequestMachineryLogic _logic;

    public RequestMachineryController(IRequestMachineryLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateRequestMachineryResponse>]
    public async Task<IResult> Create([FromBody] CreateRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.CreateRequestMachineryAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateRequestMachineryBillDocument")]
    [ResponseSchema<CreateRequestMachineryBillDocumentResponse>]
    public async Task<IResult> CreateRequestMachineryBillDocument([FromBody] CreateRequestMachineryBillDocumentRequest request, CT ct)
    {
        var result = await _logic.CreateRequestMachineryBillDocument(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateRequestMachineryDocument")]
    [ResponseSchema<CreateRequestMachineryDocumentResponse>]
    public async Task<IResult> CreateRequestMachineryDocument([FromBody] CreateRequestMachineryDocumentRequest request, CT ct)
    {
        var result = await _logic.CreateRequestMachineryDocument(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RequestMachineryGroupDelete")]
    [ResponseSchema<RequestMachineryGroupDeleteResponse>]
    public async Task<IResult> RequestMachineryGroupDelete([FromBody] RequestMachineryGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.RequestMachineryGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFiltered")]
    [ResponseSchema<GetFilteredRequestMachineriesResponse>]
    public async Task<IResult> GetFiltered([FromBody] GetFilteredRequestMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestMachineriesAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("GroupRequestMachineryStatusChanger")]
    [ResponseSchema<GroupRequestMachineryStatusChangerResponse>]
    public async Task<IResult> GroupRequestMachineryStatusChanger([FromBody] GroupRequestMachineryStatusChangerRequest request, CT ct)
    {
        var result = await _logic.GroupRequestMachineryStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetById")]
    [ResponseSchema<GetRequestMachineryByIdResponse>]
    public async Task<IResult> GetById([FromQuery] GetRequestMachineryByIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryByIdAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetHistories")]
    [ResponseSchema<GetRequestMachineryHistoriesResponse>]
    public async Task<IResult> GetHistories([FromQuery] GetRequestMachineryHistoriesRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryHistoriesAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetUnits")]
    [ResponseSchema<GetRequestMachineryUnitsResponse>]
    public async Task<IResult> GetUnits([FromQuery] GetRequestMachineryUnitsRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryUnitsAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetStatus")]
    [ResponseSchema<GetRequestMachineryStatusResponse>]
    public async Task<IResult> GetStatus([FromQuery] GetRequestMachineryStatusRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryStatusAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperations")]
    [ResponseSchema<GetRequestMachineryProjectOperationResponse>]
    public async Task<IResult> GetProjectOperations([FromQuery] GetRequestMachineryProjectOperationRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryProjectOperationAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDetails")]
    [ResponseSchema<GetRequestMachineryProjectOperationDetailResponse>]
    public async Task<IResult> GetProjectOperationDetails([FromQuery] GetRequestMachineryProjectOperationDetailRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryProjectOperationDetailAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDetailsByProjectOperation")]
    [ResponseSchema<GetsRequestMachineryProjectOperationDetailResponse>]
    public async Task<IResult> GetProjectOperationDetailsByProjectOperation([FromQuery] GetsRequestMachineryProjectOperationDetailRequest request, CT ct)
    {
        var result = await _logic.GetsRequestMachineryProjectOperationDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMachineryRequesteExcelExporter")]
    [ResponseSchema<GetsMachineryRequesteExcelExporterResponse>]
    public async Task<IResult> GetsMachineryRequesteExcelExporter([FromBody] GetsMachineryRequesteExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsMachineryRequesteExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestMachineryExcelEnums")]
    [ResponseSchema<GetsRequestMachineryExcelEnumsResponse>]
    public async Task<IResult> GetsRequestMachineryExcelEnums([FromQuery] GetsRequestMachineryExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetsRequestMachineryExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRequestMachineryMachineries")]
    [ResponseSchema<GetRequestMachineryMachineriesResponse>]
    public async Task<IResult> GetRequestMachineryMachineries([FromBody] GetRequestMachineryMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryMachineries(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetManagerConfirmMachineryReports")]
    [ResponseSchema<GetManagerConfirmMachineryReportsResponse>]
    public async Task<IResult> GetManagerConfirmMachineryReports([FromBody] GetManagerConfirmMachineryReportsRequest request, CT ct)
    {
        var result = await _logic.GetManagerConfirmMachineryReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetTotalManagerConfirmMachineryReports")]
    [ResponseSchema<GetTotalManagerConfirmMachineryReportsResponse>]
    public async Task<IResult> GetTotalManagerConfirmMachineryReports([FromBody] GetTotalManagerConfirmMachineryReportsRequest request, CT ct)
    {
        var result = await _logic.GetTotalManagerConfirmMachineryReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetOnProjectMachineryReports")]
    [ResponseSchema<GetOnProjectMachineryReportsResponse>]
    public async Task<IResult> GetOnProjectMachineryReports([FromBody] GetOnProjectMachineryReportsRequest request, CT ct)
    {
        var result = await _logic.GetOnProjectMachineryReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetTotalOnProjectMachineryReports")]
    [ResponseSchema<GetTotalOnProjectMachineryReportsResponse>]
    public async Task<IResult> GetTotalOnProjectMachineryReports([FromBody] GetTotalOnProjectMachineryReportsRequest request, CT ct)
    {
        var result = await _logic.GetTotalOnProjectMachineryReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetOnProjectMachineryReportsExcelExporter")]
    [ResponseSchema<GetOnProjectMachineryReportsExcelExporterResponse>]
    public async Task<IResult> GetOnProjectMachineryReportsExcelExporter([FromBody] GetOnProjectMachineryReportsExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetOnProjectMachineryReportsExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOnProjectMachineryReportsExcelEnums")]
    [ResponseSchema<GetOnProjectMachineryReportsExcelEnumsResponse>]
    public async Task<IResult> GetOnProjectMachineryReportsExcelEnums([FromQuery] GetOnProjectMachineryReportsExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetOnProjectMachineryReportsExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetOnProjectRequestReports")]
    [ResponseSchema<GetOnProjectRequestReportsResponse>]
    public async Task<IResult> GetOnProjectRequestReports([FromBody] GetOnProjectRequestReportsRequest request, CT ct)
    {
        var result = await _logic.GetOnProjectRequestReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetTotalOnProjectRequestReports")]
    [ResponseSchema<GetTotalOnProjectRequestReportsResponse>]
    public async Task<IResult> GetTotalOnProjectRequestReports([FromBody] GetTotalOnProjectRequestReportsRequest request, CT ct)
    {
        var result = await _logic.GetTotalOnProjectRequestReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetOnProjectRequestReportsExcelExporter")]
    [ResponseSchema<GetOnProjectRequestReportsExcelExporterResponse>]
    public async Task<IResult> GetOnProjectRequestReportsExcelExporter([FromBody] GetOnProjectRequestReportsExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetOnProjectRequestReportsExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOnProjectRequestReportsExcelEnums")]
    [ResponseSchema<GetOnProjectRequestReportsExcelEnumsResponse>]
    public async Task<IResult> GetOnProjectRequestReportsExcelEnums(
    [FromQuery] GetOnProjectRequestReportsExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetOnProjectRequestReportsExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("Delete")]
    [ResponseSchema<DeleteRequestMachineryResponse>]
    public async Task<IResult> Delete([FromQuery] DeleteRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.DeleteRequestMachineryAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateRequestMachineryResponse>]
    public async Task<IResult> Update([FromBody] UpdateRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestMachineryAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateMachinery")]
    [ResponseSchema<UpdateRequestMachineryMachineryResponse>]
    public async Task<IResult> UpdateMachinery([FromBody] UpdateRequestMachineryMachineryRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestMachineryMachineryAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateDateTime")]
    [ResponseSchema<UpdateRequestMachineryDateTimeResponse>]
    public async Task<IResult> UpdateDateTime([FromBody] UpdateRequestMachineryDateTimeRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestMachineryDateTime(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateDriver")]
    [ResponseSchema<UpdateRequestMachineryDriverResponse>]
    public async Task<IResult> UpdateDriver([FromBody] UpdateRequestMachineryDriverRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestMachineryDriverAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSendManagerMachineryReports")]
    [ResponseSchema<GetSendManagerMachineryReportsResponse>]
    public async Task<IResult> GetSendManagerMachineryReports(
        [FromBody] GetSendManagerMachineryReportsRequest request, CT ct)
    {
        var result = await _logic.GetSendManagerMachineryReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetTotalSendManagerMachineryReports")]
    [ResponseSchema<GetTotalSendManagerMachineryReportsResponse>]
    public async Task<IResult> GetTotalSendManagerMachineryReports(
        [FromBody] GetTotalSendManagerMachineryReportsRequest request, CT ct)
    {
        var result = await _logic.GetTotalSendManagerMachineryReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSendManagerMachineryReportsExcelExporter")]
    [ResponseSchema<GetSendManagerMachineryReportsExcelExporterResponse>]
    public async Task<IResult> GetSendManagerMachineryReportsExcelExporter(
        [FromBody] GetSendManagerMachineryReportsExcelExporterRequest request, CT ct)
    {
        var response = await _logic.GetSendManagerMachineryReports(
            request.Adapt<GetSendManagerMachineryReportsRequest>(), ct);

        var values = response.Value?.Data;
        var details = values?.Where(x => x.Details != null && x.Details.Count > 0)
                             .SelectMany(x => x.Details!)
                             .ToList();

        FileContentResult? result = null;

        if (values != null && values.Count > 0)
        {
            result = new FileContentResult(
                MachineryRequesteExcels.SendToManagerRequestMachineriesToExcel(values, details, request.ExcelFilters),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"RequestMachineriesReports-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
                LastModified = DateTime.UtcNow
            };
        }

        return Result.Success<GetSendManagerMachineryReportsExcelExporterResponse?>(new(result))
            .GetHttpResponse();
    }

    [HttpGet("GetSendManagerMachineryReportsExcelEnums")]
    [ResponseSchema<GetSendManagerMachineryReportsExcelEnumsResponse>]
    public async Task<IResult> GetSendManagerMachineryReportsExcelEnums(
        [FromQuery] GetSendManagerMachineryReportsExcelEnumsRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<SendManagerRequestMachineryExcelEnum>());
        return Result.Success<GetSendManagerMachineryReportsExcelEnumsResponse?>(new(result))
            .GetHttpResponse();
    }
}