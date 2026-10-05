using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.TransportationRequests;
using Engineering.Application.Services.TransportationRequests.Models.AddTransportationRequestBill;
using Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;
using Engineering.Application.Services.TransportationRequests.Models.CargoReformsTransport;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToAccepted;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToPaid;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToPending;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestRejection;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestResended;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToSecurityConfirmTransportation;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToSendDoneTransportation;
using Engineering.Application.Services.TransportationRequests.Models.Create;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirPlanePaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnap;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnapPaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateTransportationRequestPaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.Disable;
using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Models.GetsPalletTransportStatus;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalSnapPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoWithoutContractor;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationPaymentType;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestType;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportationExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportationExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationContractorCalculateType;
using Engineering.Application.Services.TransportationRequests.Models.GroupTransportationRequestStatusChanger;
using Engineering.Application.Services.TransportationRequests.Models.PackingReleaseFromTransport;
using Engineering.Application.Services.TransportationRequests.Models.PackingRivision;
using Engineering.Application.Services.TransportationRequests.Models.TransportationRequestGroupDelete;
using Engineering.Application.Services.TransportationRequests.Models.Update;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAfterCargoDeclaration;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAggregateTransportationWarehouse;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.UpdateFreeCargosTransportInfo;
using Engineering.Application.Services.TransportationRequests.Models.UpdateMachineDriver;
using Engineering.Application.Services.TransportationRequests.Models.UpdateSnap;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportLoadWeight;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportVolume;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Domain.Exceptions;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Api.Controllers.TransportationRequests;

[ApiController]
[Route("api/engineering/v1/TransportationRequest")]
public class TransportationRequestController : ControllerBase
{
    private readonly ITransportationRequestLogic _logic;

    public TransportationRequestController(ITransportationRequestLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTransportationRequest")]
    [ResponseSchema<CreateTransportationRequestResponse>]
    public async Task<IResult> CreateTransportationRequest(
        [FromBody] CreateTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.CreateTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddWarehouseTransportation")]
    [ResponseSchema<CreateWarehouseTransportationResponse>]
    public async Task<IResult> AddWarehouseTransportation(
        [FromBody] CreateWarehouseTransportationRequest request,
        CT ct)
    {
        var result = await _logic.CreateWarehouseTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddSnapRequest")]
    [ResponseSchema<CreateSnapResponse>]
    public async Task<IResult> CreateSnapRequest(
        [FromBody] CreateSnapRequest request,
        CT ct)
    {
        var result = await _logic.CreateSnap(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddAirplaneRequest")]
    [ResponseSchema<CreateAirplaneResponse>]
    public async Task<IResult> CreateAirplaneRequest(
        [FromBody] CreateAirplaneRequest request,
        CT ct)
    {
        var result = await _logic.CreateAirplane(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TransportationRequestGroupDelete")]
    [ResponseSchema<TransportationRequestGroupDeleteResponse>]
    public async Task<IResult> TransportationRequestGroupDelete(
        [FromBody] TransportationRequestGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.TransportationRequestGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTransportationRequest")]
    [ResponseSchema<UpdateTransportationRequestResponse>]
    public async Task<IResult> UpdateTransportationRequest(
        [FromBody] UpdateTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.UpdateTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditSnapRequest")]
    [ResponseSchema<UpdateSnapResponse>]
    public async Task<IResult> UpdateSnapRequest(
        [FromBody] UpdateSnapRequest request,
        CT ct)
    {
        var result = await _logic.UpdateSnap(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditAirplaneRequest")]
    [ResponseSchema<UpdateAirplaneResponse>]
    public async Task<IResult> UpdateAirplaneRequest(
        [FromBody] UpdateAirplaneRequest request,
        CT ct)
    {
        var result = await _logic.UpdateAirplane(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("GroupTransportationRequestStatusChanger")]
    [ResponseSchema<GroupTransportationRequestStatusChangerResponse>]
    public async Task<IResult> GroupStatusChanger(
    [FromBody] GroupTransportationRequestStatusChangerRequest request,
    CT ct)
    {
        var result = await _logic.GroupTransportationRequestStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeStatuesToAccepted")]
    [ResponseSchema<ChangeToAcceptedTransportationRequestResponse>]
    public async Task<IResult> ChangeToAccepted(
        [FromBody] ChangeToAcceptedTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.ChangeToAcceptedTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeStatuesToPaid")]
    [ResponseSchema<ChangeToPaidTransportationRequestResponse>]
    public async Task<IResult> ChangeToPaid(
        [FromBody] ChangeToPaidTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.ChangeToPaidTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeStatuesToPending")]
    [ResponseSchema<ChangeToPendingTransportationRequestResponse>]
    public async Task<IResult> ChangeToPending(
        [FromBody] ChangeToPendingTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.ChangeToPendingTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeStatuesToRequestRejection")]
    [ResponseSchema<ChangeToRequestRejectionTransportationRequestResponse>]
    public async Task<IResult> ChangeToRequestRejection(
        [FromBody] ChangeToRequestRejectionTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.ChangeToRequestRejectionTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeStatuesToRequestResended")]
    [ResponseSchema<ChangeToRequestResendedTransportationRequestResponse>]
    public async Task<IResult> ChangeToRequestResended(
        [FromBody] ChangeToRequestResendedTransportationRequestRequest request,
        CT ct)
    {
        var result = await _logic.ChangeToRequestResendedTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeToSendDoneTransportation")]
    [ResponseSchema<ChangeToSendDoneTransportationResponse>]
    public async Task<IResult> ChangeToSendDone(
    [FromBody] ChangeToSendDoneTransportationRequest request,
    CT ct)
    {
        var result = await _logic.ChangeToSendDoneTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeToSecurityConfirmTransportation")]
    [ResponseSchema<ChangeToSecurityConfirmTransportationResponse>]
    public async Task<IResult> ChangeToSecurityConfirm(
        [FromBody] ChangeToSecurityConfirmTransportationRequest request,
        CT ct)
    {
        var result = await _logic.ChangeToSecurityConfirmTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationRequestById")]
    [ResponseSchema<GetTransportationRequestByIdResponse>]
    public async Task<IResult> GetTransportationRequestById(
    [FromQuery] GetTransportationRequestByIdRequest request,
    CT ct)
    {
        var result = await _logic.GetTransportationRequestById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSnapRequestById")]
    [ResponseSchema<GetSnapByIdResponse>]
    public async Task<IResult> GetSnapRequestById(
        [FromQuery] GetSnapByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetSnapById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetAirplaneRequestById")]
    [ResponseSchema<GetAirplaneByIdResponse>]
    public async Task<IResult> GetAirplaneRequestById(
        [FromQuery] GetAirplaneByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetAirplaneById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationRequestType")]
    [ResponseSchema<GetsTransportationRequestTypeResponse>]
    public async Task<IResult> GetsTransportationRequestType(
    [FromQuery] GetsTransportationRequestTypeRequest request,
    CT ct)
    {
        var result = await _logic.GetsTransportationRequestType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFilteredTransportationRequesters")]
    [ResponseSchema<GetsFilteredRequesterResponse>]
    public async Task<IResult> GetsFilteredTransportationRequesters(
        [FromQuery] GetsFilteredRequesterRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredRequester(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredTransportationRequest")]
    [ResponseSchema<GetsFilteredTransportationRequestResponse>]
    public async Task<IResult> GetsFilteredTransportationRequest(
    [FromBody] GetsFilteredTransportationRequestRequest request,
    CT ct)
    {
        var result = await _logic.GetsFilteredTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalTransportationRequestPrice")]
    [ResponseSchema<GetsTotalTransportationRequestPriceResponse>]
    public async Task<IResult> GetsTotalTransportationRequestPrice(
        [FromBody] GetsTotalTransportationRequestPriceRequest request,
        CT ct)
    {
        var result = await _logic.GetsTotalTransportationRequestPrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredSnapRequest")]
    [ResponseSchema<GetsFilteredSnapResponse>]
    public async Task<IResult> GetsFilteredSnapRequest(
        [FromBody] GetsFilteredSnapRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredSnap(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalSnapPrice")]
    [ResponseSchema<GetsTotalSnapPriceResponse>]
    public async Task<IResult> GetsTotalSnapPrice(
        [FromBody] GetsTotalSnapPriceRequest request,
        CT ct)
    {
        var result = await _logic.GetsTotalSnapPrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredAirplaneRequest")]
    [ResponseSchema<GetsFilteredAirplaneResponse>]
    public async Task<IResult> GetsFilteredAirplaneRequest(
        [FromBody] GetsFilteredAirplaneRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredAirplane(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalAirplanePrice")]
    [ResponseSchema<GetsTotalAirplanePriceResponse>]
    public async Task<IResult> GetsTotalAirplanePrice(
        [FromBody] GetsTotalAirplanePriceRequest request,
        CT ct)
    {
        var result = await _logic.GetsTotalAirplanePrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableTransportationRequest")]
    [ResponseSchema<DisableTransportationRequestResponse>]
    public async Task<IResult> DisableTransportationRequest(
    [FromQuery] DisableTransportationRequestRequest request,
    CT ct)
    {
        var result = await _logic.DisableTransportationRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationRequestExcelExporter")]
    [ResponseSchema<GetsTransportationRequestExcelExporterResponse>]
    public async Task<IResult> GetsTransportationRequestExcelExporter(
        [FromBody] GetsTransportationRequestExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationRequestExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationRequestExcelEnum")]
    [ResponseSchema<GetsTransportationRequestExcelEnumResponse>]
    public async Task<IResult> GetsTransportationRequestExcelEnum(
        [FromQuery] GetsTransportationRequestExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationRequestExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsSnapRequestExcelExporter")]
    [ResponseSchema<GetsSnapExcelExporterResponse>]
    public async Task<IResult> GetsSnapRequestExcelExporter(
        [FromBody] GetsSnapExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsSnapExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsSnapRequestExcelEnum")]
    [ResponseSchema<GetsSnapExcelEnumResponse>]
    public async Task<IResult> GetsSnapRequestExcelEnum(
        [FromQuery] GetsSnapExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsSnapExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsAirplaneRequestExcelExporter")]
    [ResponseSchema<GetsAirplaneExcelExporterResponse>]
    public async Task<IResult> GetsAirplaneRequestExcelExporter(
        [FromBody] GetsAirplaneExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsAirplaneExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsAirplaneRequestExcelEnum")]
    [ResponseSchema<GetsAirplaneExcelEnumResponse>]
    public async Task<IResult> GetsAirplaneRequestExcelEnum(
        [FromQuery] GetsAirplaneExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsAirplaneExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationPaymentType")]
    [ResponseSchema<GetsTransportationPaymentTypeResponse>]
    public async Task<IResult> GetsTransportationPaymentType(
        [FromQuery] GetsTransportationPaymentTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationPaymentType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationRequestHistory")]
    [ResponseSchema<GetsTransportationRequestHistoryResponse>]
    public async Task<IResult> GetsTransportationRequestHistory(
        [FromQuery] GetsTransportationRequestHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationRequestHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateTransportationRequestPaymentOrder")]
    [ResponseSchema<CreateTransportationRequestPaymentOrderResponse>]
    public async Task<IResult> CreateTransportationRequestPaymentOrder(
        [FromBody] CreateTransportationRequestPaymentOrderRequest request,
        CT ct)
    {
        var result = await _logic.CreateTransportationRequestPaymentOrder(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateSnapPaymentOrder")]
    [ResponseSchema<CreateSnapPaymentOrderResponse>]
    public async Task<IResult> CreateSnapPaymentOrder(
        [FromBody] CreateSnapPaymentOrderRequest request,
        CT ct)
    {
        var result = await _logic.CreateSnapPaymentOrder(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateAirPlanePaymentOrder")]
    [ResponseSchema<CreateAirPlanePaymentOrderResponse>]
    public async Task<IResult> CreateAirPlanePaymentOrder(
        [FromBody] CreateAirPlanePaymentOrderRequest request,
        CT ct)
    {
        var result = await _logic.CreateAirPlanePaymentOrder(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateWarehouseTransportation")]
    [ResponseSchema<CreateWarehouseTransportationResponse>]
    public async Task<IResult> CreateWarehouseTransportation(
        [FromBody] CreateWarehouseTransportationRequest request,
        CT ct)
    {
        var result = await _logic.CreateWarehouseTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AggregateWarehouseTransportation")]
    [ResponseSchema<AggregateWarehouseTransportationResponse>]
    public async Task<IResult> AggregateWarehouseTransportation(
        [FromBody] AggregateWarehouseTransportationRequest request,
        CT ct)
    {
        var result = await _logic.AggregateWarehouseTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CargoReformsTransport")]
    [ResponseSchema<CargoReformsTransportResponse>]
    public async Task<IResult> CargoReformsTransport(
        [FromBody] CargoReformsTransportRequest request,
        CT ct)
    {
        var result = await _logic.CargoReformsTransport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("PackingReleaseFromTransport")]
    [ResponseSchema<PackingReleaseFromTransportResponse>]
    public async Task<IResult> PackingReleaseFromTransport(
    [FromBody] PackingReleaseFromTransportRequest request,
    CT ct)
    {
        var result = await _logic.PackingReleaseFromTransport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsWarehouseTransportation")]
    [ResponseSchema<GetsWarehouseTransportationResponse>]
    public async Task<IResult> GetsWarehouseTransportation(
        [FromBody] GetsWarehouseTransportationRequest request,
        CT ct)
    {
        var result = await _logic.GetsWarehouseTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsWarehouseTransportationExcelExporter")]
    [ResponseSchema<GetsWarehouseTransportationExcelExporterResponse>]
    public async Task<IResult> GetsWarehouseTransportationExcelExporter(
        [FromBody] GetsWarehouseTransportationExcelExporterRequest request,
        CT ct)
    {
        var response = await _logic.GetsWarehouseTransportation(request.Adapt<GetsWarehouseTransportationRequest>(), ct);
        if (response.IsFailure)
            throw new GitaValidationException(TransportationRequestErrors.FilteredTransportationRequestNotFound);

        var values = response.Value?.Data;
        var packs = response.Value?.Data
            .Where(x => x.PackingData != null && x.PackingData.Count > 0)
            .SelectMany(z => z.PackingData!)
            .ToList();

        var result = new FileContentResult(
            ExcelExporter.ExportToExcel<GetsWarehouseTransportationResponseModel,
            GetTransportWarehouseModel,
            WarehouseTransportationExcelEnum,
            WarehouseTransportationPackingExcelEnum>(
                values!, packs, request.ExcelFilters, null,
                "ترابری های پیمانکار حمل", "بسته ها"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"WarehouseTransportations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return Result.Success<GetsWarehouseTransportationExcelExporterResponse?>(new(result))
            .GetHttpResponse();
    }

    [HttpGet("GetsWarehouseTransportationExcelEnum")]
    [ResponseSchema<GetsWarehouseTransportationExcelEnumResponse>]
    public async Task<IResult> GetsWarehouseTransportationExcelEnum(
        [FromQuery] GetsWarehouseTransportationExcelEnumRequest request,
        CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<WarehouseTransportationExcelEnum>());
        return Result.Success<GetsWarehouseTransportationExcelEnumResponse?>(new(result))
            .GetHttpResponse();
    }

    [HttpPost("GetsAggregateWarehouseTransportation")]
    [ResponseSchema<GetsAggregateWarehouseTransportationResponse>]
    public async Task<IResult> GetsAggregateWarehouseTransportation(
        [FromBody] GetsAggregateWarehouseTransportationRequest request,
        CT ct)
    {
        var result = await _logic.GetsAggregateWarehouseTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsAggregateWarehouseTransportationExcelExporter")]
    [ResponseSchema<GetsAggregateWarehouseTransportationExcelExporterResponse>]
    public async Task<IResult> GetsAggregateWarehouseTransportationExcelExporter(
        [FromBody] GetsAggregateWarehouseTransportationExcelExporterRequest request,
        CT ct)
    {
        var response = await _logic.GetsAggregateWarehouseTransportation(request.Adapt<GetsAggregateWarehouseTransportationRequest>(), ct);
        if (response.IsFailure)
            throw new GitaValidationException(TransportationRequestErrors.FilteredTransportationRequestNotFound);

        var values = response.Value?.Data;
        // var packs = response.Value?.Data
        //     .Where(x => x.PackingData != null && x.PackingData.Count > 0)
        //     .SelectMany(z => z.PackingData!)
        //     .ToList();

        var result = new FileContentResult(
            ExcelExporter.ExportToExcel<
                GetsAggregateWarehouseTransportationResponseModel,
            AggregateWarehouseTransportationExcelEnum>(
                values!, request.ExcelFilters,
                "حمل ها"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"WarehouseTransportations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return Result.Success<GetsAggregateWarehouseTransportationExcelExporterResponse?>(new(result))
            .GetHttpResponse();
    }

    [HttpGet("GetsAggregateWarehouseTransportationExcelEnum")]
    [ResponseSchema<GetsAggregateWarehouseTransportationExcelEnumResponse>]
    public async Task<IResult> GetsAggregateWarehouseTransportationExcelEnum(
        [FromQuery] GetsAggregateWarehouseTransportationExcelEnumRequest request,
        CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<AggregateWarehouseTransportationExcelEnum>());
        return Result.Success<GetsAggregateWarehouseTransportationExcelEnumResponse?>(new(result))
            .GetHttpResponse();
    }

    [HttpPost("UpdateMachineDriver")]
    [ResponseSchema<UpdateMachineDriverResponse>]
    public async Task<IResult> UpdateMachineDriver([FromBody] UpdateMachineDriverRequest request, CT ct)
    {
        var result = await _logic.UpdateMachineDriver(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateTransportLoadWeight")]
    [ResponseSchema<UpdateTransportLoadWeightResponse>]
    public async Task<IResult> UpdateTransportLoadWeight([FromBody] UpdateTransportLoadWeightRequest request, CT ct)
    {
        var result = await _logic.UpdateTransportLoadWeight(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateTransportPalletLoadWeight")]
    [ResponseSchema<UpdateTransportPalletLoadWeightResponse>]
    public async Task<IResult> UpdateTransportPalletLoadWeight([FromBody] UpdateTransportPalletLoadWeightRequest request, CT ct)
    {
        var result = await _logic.UpdateTransportPalletLoadWeight(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateTransportVolume")]
    [ResponseSchema<UpdateTransportVolumeResponse>]
    public async Task<IResult> UpdateTransportVolume([FromBody] UpdateTransportVolumeRequest request, CT ct)
    {
        var result = await _logic.UpdateTransportVolume(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CalculatePriceOfTransport")]
    [ResponseSchema<CalculatePriceOfTransportResponse>]
    public async Task<IResult> CalculatePriceOfTransport([FromBody] CalculatePriceOfTransportRequest request, CT ct)
    {
        var result = await _logic.CalculatePriceOfTransport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateAfterCargoDeclaration")]
    [ResponseSchema<UpdateAfterCargoDeclarationResponse>]
    public async Task<IResult> UpdateAfterCargoDeclaration([FromBody] UpdateAfterCargoDeclarationRequest request, CT ct)
    {
        var result = await _logic.UpdateAfterCargoDeclaration(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateFreeCargosTransportInfo")]
    [ResponseSchema<UpdateFreeCargosTransportInfoResponse>]
    public async Task<IResult> UpdateFreeCargosTransportInfo([FromBody] UpdateFreeCargosTransportInfoRequest request, CT ct)
    {
        var result = await _logic.UpdateFreeCargosTransportInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateAggregateTransportationWarehouse")]
    [ResponseSchema<UpdateAggregateTransportationWarehouseResponse>]
    public async Task<IResult> UpdateAggregateTransportationWarehouse([FromBody] UpdateAggregateTransportationWarehouseRequest request, CT ct)
    {
        var result = await _logic.UpdateAggregateTransportationWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddTransportationRequestBill")]
    [ResponseSchema<AddTransportationRequestBillResponse>]
    public async Task<IResult> AddTransportationRequestBill([FromBody] AddTransportationRequestBillRequest request, CT ct)
    {
        var result = await _logic.AddTransportationRequestBill(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsAggregateWarehouseTransportationById")]
    [ResponseSchema<GetsAggregateWarehouseTransportationByIdResponse>]
    public async Task<IResult> GetsAggregateWarehouseTransportationById([FromQuery] GetsAggregateWarehouseTransportationByIdRequest request, CT ct)
    {
        var result = await _logic.GetsAggregateWarehouseTransportationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationContractorCalculateType")]
    [ResponseSchema<GetTransportationContractorCalculateTypeResponse>]
    public async Task<IResult> GetTransportationContractorCalculateType([FromQuery] GetTransportationContractorCalculateTypeRequest request, CT ct)
    {
        var result = await _logic.GetTransportationContractorCalculateType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPackingLogesticDetail")]
    [ResponseSchema<GetPackingLogesticDetailResponse>]
    public async Task<IResult> GetPackingLogesticDetail([FromQuery] GetPackingLogesticDetailRequest request, CT ct)
    {
        var result = await _logic.GetPackingLogesticDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("PackingRivision")]
    [ResponseSchema<PackingRivisionResponse>]
    public async Task<IResult> PackingRivision([FromBody] PackingRivisionRequest request, CT ct)
    {
        var result = await _logic.PackingRivision(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationCargoPalletWithoutContractor")]
    [ResponseSchema<GetsTransportationCargoPalletResponse>]
    public async Task<IResult> GetsTransportationCargoWithoutContractor([FromBody] GetsTransportationCargoWithoutContractorRequest request, CT ct)
    {
        var result = await _logic.GetsTransportationCargoWithoutContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationCargoPallet")]
    [ResponseSchema<GetsTransportationCargoPalletResponse>]
    public async Task<IResult> GetsTransportationCargoPallet([FromBody] GetsTransportationCargoPalletRequest request, CT ct)
    {
        var result = await _logic.GetsTransportationCargoPallet(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationCargoPallet")]
    [ResponseSchema<GetTransportationCargoPalletResponse>]
    public async Task<IResult> GetTransportationCargoPallet([FromQuery] GetTransportationCargoPalletRequest request, CT ct)
    {
        var result = await _logic.GetTransportationCargoPallet(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredTransportationCargo")]
    [ResponseSchema<GetsFilteredTransportationCargoResponse>]
    public async Task<IResult> GetsFilteredTransportationCargo([FromBody] GetsFilteredTransportationCargoRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredTransportationCargo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationCargoById")]
    [ResponseSchema<GetTransportationCargoByIdResponse>]
    public async Task<IResult> GetTransportationCargoById([FromQuery] GetTransportationCargoByIdRequest request, CT ct)
    {
        var result = await _logic.GetTransportationCargoById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPalletTransportStatus")]
    [ResponseSchema<GetsPalletTransportStatusResponse>]
    public async Task<IResult> GetPalletTransportStatus(
        [FromQuery] GetsPalletTransportStatusRequest request,
        CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<PalletTransportStatus>());
        return Result.Success<GetsPalletTransportStatusResponse?>(new(result))
            .GetHttpResponse();
    }
}