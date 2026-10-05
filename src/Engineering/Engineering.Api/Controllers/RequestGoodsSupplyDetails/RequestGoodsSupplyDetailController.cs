using Engineering.Api.Controllers.RequestGoodsSupplyDetails.Reports;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductDocuments;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsDetailHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailImportance;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailStatus;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGroupPdfGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsPdfGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct.GetsRequestGoodsSupplyProductNewExcelEnum;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsTotalPriceRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductGroupStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyProduct;

namespace Engineering.Api.Controllers.RequestGoodsSupplyDetails;

[ApiController]
[Route("api/engineering/v1/RequestGoodsSupplyDetail")]
public class RequestGoodsSupplyDetailController : ControllerBase
{
    private readonly IRequestGoodsSupplyDetailLogic _logic;
    private readonly GetsGroupPdfGoodsSupplyProductHandle _groupPdf;
    private readonly GetsPdfGoodsSupplyProductHandle _gSPPdf;
    private readonly GetRGSProductXlsxReportHandle _rgsExcel;

    public RequestGoodsSupplyDetailController(
        IRequestGoodsSupplyDetailLogic logic,
        GetsGroupPdfGoodsSupplyProductHandle groupPdf,
        GetRGSProductXlsxReportHandle rgsExcel,
        GetsPdfGoodsSupplyProductHandle gSPPdf)
    {
        _logic = logic;
        _groupPdf = groupPdf;
        _rgsExcel = rgsExcel;
        _gSPPdf = gSPPdf;
    }

    [HttpPut("UpdateRequestGoodsSupplyProduct")]
    [ResponseSchema<UpdateRequestGoodsSupplyProductResponse>]
    public async Task<IResult> UpdateRequestGoodsSupplyProduct(
    [FromBody] UpdateRequestGoodsSupplyProductRequest request,
    CT ct)
    {
        var result = await _logic.UpdateRequestGoodsSupplyProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateRequestGoodsSupplyDetailResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateRequestGoodsSupplyDetailRequest request,
        CT ct)
    {
        var result = await _logic.UpdateRequestGoodsDetailSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteRequestGoodsSupplyProduct")]
    [ResponseSchema<DeleteRequestGoodsSupplyProductResponse>]
    public async Task<IResult> DeleteRequestGoodsSupplyProduct(
        [FromQuery] DeleteRequestGoodsSupplyProductRequest request,
        CT ct)
    {
        var newRequest = new DeleteRequestGoodsSupplyProductModelRequest(request.Id, null, true);
        var result = await _logic.DeleteRequestGoodsSupplyProduct(newRequest, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetGoodsSupplyProductById")]
    [ResponseSchema<GetGoodsSupplyProductByIdResponse>]
    public async Task<IResult> GetGoodsSupplyProductById(
        [FromQuery] GetGoodsSupplyProductByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetGoodsSupplyProductById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetAllGoodsSupplyProductDocument")]
    [ResponseSchema<GetAllGoodsSupplyProductDocumentResponse>]
    public async Task<IResult> GetAllGoodsSupplyProductDocument(
        [FromBody] GetAllGoodsSupplyProductDocumentRequest request,
        CT ct)
    {
        var result = await _logic.GetAllGoodsSupplyProductDocument(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectDetailData")]
    [ResponseSchema<GetProjectDetailDataResponse>]
    public async Task<IResult> GetProjectDetailData(
        [FromBody] GetProjectDetailDataRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectDetailData(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsGoodsSupplyDetailBySupplyProductId")]
    [ResponseSchema<GetsGoodsSupplyDetailBySupplyProductIdResponse>]
    public async Task<IResult> GetsGoodsSupplyDetailBySupplyProductId(
        [FromQuery] GetsGoodsSupplyDetailBySupplyProductIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsGoodsSupplyDetailBySupplyProductId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetGoodsSupplyProductDocuments")]
    [ResponseSchema<GetGoodsSupplyProductDocumentsResponse>]
    public async Task<IResult> GetGoodsSupplyProductDocuments(
        [FromQuery] GetGoodsSupplyProductDocumentsRequest request,
        CT ct)
    {
        var result = await _logic.GetGoodsSupplyProductDocuments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RequestGoodsSupplyProductStatusChanger")]
    [ResponseSchema<RequestGoodsSupplyProductStatusChangerResponse>]
    public async Task<IResult> RequestGoodsSupplyProductStatusChanger(
        [FromBody] RequestGoodsSupplyProductStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.RequestGoodsSupplyProductStatusChanger(
            new(request.Id, null, request.Status, request.SendToSupply, request.Description), false, ct);

        return result.GetHttpResponse();
    }

    [HttpPost("GetsGoodsSupplyProduct")]
    [ResponseSchema<GetsGoodsSupplyProductResponse>]
    public async Task<IResult> GetsGoodsSupplyProduct(
        [FromBody] GetsGoodsSupplyProductRequest request,
        CT ct)
    {
        var result = await _logic.GetsGoodsSupplyProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RequestGoodsSupplyProductGroupStatusChanger")]
    [ResponseSchema<RequestGoodsSupplyProductGroupStatusChangerResponse>]
    public async Task<IResult> RequestGoodsSupplyProductGroupStatusChanger(
        [FromBody] RequestGoodsSupplyProductGroupStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.RequestGoodsSupplyProductGroupStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectOperationDetails")]
    [ResponseSchema<GetProjectOperationDetailsByRequestIdResponse>]
    public async Task<IResult> ProjectOperationDetails(
        [FromBody] GetProjectOperationDetailsByRequestIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectOperationDetailsByRequestId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProducts")]
    [ResponseSchema<GetGoodsSupplyDetailProductsResponse>]
    public async Task<IResult> GetProducts(
        [FromBody] GetGoodsSupplyDetailProductsRequest request,
        CT ct)
    {

        if (!request.IsProjectSupply)
        {
            var result = await _logic.GetGoodsSupplyProducts(request, ct);
            return result.GetHttpResponse();
        }
        else
        {
            var result = await _logic.GetProjectGoodsSupplyProducts(request, ct);
            return result.GetHttpResponse();
        }
    }

    [HttpGet("GetRequestGoodsSupplyProductById")]
    [ResponseSchema<GetRequestGoodsSupplyProductByIdResponse>]
    public async Task<IResult> GetRequestGoodsSupplyProductById(
        [FromQuery] GetRequestGoodsSupplyProductByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyProductById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRequestGoodsSupplyDetailStatus")]
    [ResponseSchema<GetRequestGoodsSupplyDetailStatusResponse>]
    public async Task<IResult> GetRequestGoodsSupplyDetailStatus(
        [FromBody] GetRequestGoodsSupplyDetailStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyDetailStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationDetailData")]
    [ResponseSchema<GetsProjectOperationDetailDataResponse>]
    public async Task<IResult> GetsProjectOperationDetailData(
        [FromQuery] GetsProjectOperationDetailDataRequest request,
        CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailData(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetGoodsSupplyProductHistoryById")]
    [ResponseSchema<GetGoodsSupplyProductHistoryByIdResponse>]
    public async Task<IResult> GetGoodsSupplyProductHistoryById(
    [FromQuery] GetGoodsSupplyProductHistoryByIdRequest request,
    CT ct)
    {
        var result = await _logic.GetGoodsSupplyProductHistoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestGoodsDetailHistoryById")]
    [ResponseSchema<GetRequestGoodsDetailHistoryByIdResponse>]
    public async Task<IResult> GetRequestGoodsDetailHistoryById(
        [FromQuery] GetRequestGoodsDetailHistoryByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestGoodsDetailHistoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetImportance")]
    [ResponseSchema<GetRequestGoodsSupplyDetailImportanceResponse>]
    public async Task<IResult> GetImportance(
        [FromQuery] GetRequestGoodsSupplyDetailImportanceRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyDetailImportance(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestGoodsSupplyProduct")]
    [ResponseSchema<GetsRequestGoodsSupplyProductResponse>]
    public async Task<IResult> GetsRequestGoodsSupplyProduct(
        [FromBody] GetsRequestGoodsSupplyProductRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestGoodsSupplyProductExcelExporter")]
    [ResponseSchema<GetsRequestGoodsSupplyProductExcelExporterResponse>]
    public async Task<IResult> GetsRequestGoodsSupplyProductExcelExporter(
        [FromBody] GetsRequestGoodsSupplyProductExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyProductExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestGoodsSupplyProductExcelEnums")]
    [ResponseSchema<GetsRequestGoodsSupplyProductExcelEnumsResponse>]
    public async Task<IResult> GetsRequestGoodsSupplyProductExcelEnums(
        [FromQuery] GetsRequestGoodsSupplyProductExcelEnumsRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyProductExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalPriceRequestGoodsSupplyProduct")]
    [ResponseSchema<GetsTotalPriceRequestGoodsSupplyProductResponse>]
    public async Task<IResult> GetsTotalPriceRequestGoodsSupplyProduct(
        [FromBody] GetsTotalPriceRequestGoodsSupplyProductRequest request,
        CT ct)
    {
        var result = await _logic.GetsTotalPriceRequestGoodsSupplyProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("Delete")]
    [ResponseSchema<DeleteRequestGoodsSupplyDetailResponse>]
    public async Task<IResult> Delete(
        [FromBody] DeleteRequestGoodsSupplyDetailRequest request,
        CT ct)
    {
        var newRequest = new DeleteRequestGoodsSupplyDetailModelRequest(
            request.RequestGoodsSupplyDetailId, null, null, true, null);

        var result = await _logic.DeleteRequestGoodsSupplyDetail(newRequest, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSupplyProductByIdWithScale")]
    [ResponseSchema<GetsGoodsSupplyProductByIdWithScaleResponse>]
    public async Task<IResult> GetSupplyProductByIdWithScale(
        [FromBody] GetsGoodsSupplyProductByIdWithScaleRequest request,
        CT ct)
    {
        var result = await _logic.GetsGoodsSupplyProductByIdWithScale(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsPdfGoodsSupplyProduct")]
    [ResponseSchema<GetsPdfGoodsSupplyProductResponse>]
    public async Task<IResult> GetsPdfGoodsSupplyProduct(
        [FromBody] GetsPdfGoodsSupplyProductRequest request,
        CT ct)
    {
        var result = await _gSPPdf.Handle(request, ct);
        return result;
    }

    [HttpPost("GetsGroupPdfGoodsSupplyProduct")]
    [ResponseSchema<GetsGroupPdfGoodsSupplyProductResponse>]
    public async Task<IResult> GetsGroupPdfGoodsSupplyProduct(
        [FromBody] GetsGroupPdfGoodsSupplyProductRequest request,
        CT ct)
    {
        var result = await _groupPdf.Handle(request, ct);
        return result;
    }

    [HttpPost("GetRGSProductXlsxReport")]
    [ResponseSchema<GetRGSProductXlsxReportResponse>]
    public async Task<IResult> GetRGSProductXlsxReport(
        [FromBody] GetRGSProductXlsxReportRequest request,
        CT ct)
    {
        var result = await _rgsExcel.Handle(request, ct);
        return result;
    }
}