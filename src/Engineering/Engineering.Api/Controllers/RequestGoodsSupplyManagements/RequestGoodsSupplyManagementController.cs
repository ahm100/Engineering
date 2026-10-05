using Engineering.Application.RequestGoodsSupplyDetailManagements;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredAlternativeProducts;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetsDestinationWarehouse;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetsManagementGoodsSupplyForWarehouse;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetsSourceWarehouse;
using Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagement;
using Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagements;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsManagementHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsProjectManagerRequestGoodsSupplie;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsRequestGoodsSupplyManagementStatus;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsRequestGoodsSupplyManagementType;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;

namespace Engineering.Api.Controllers.RequestGoodsSupplyManagements;

[ApiController]
[Route("api/engineering/v1/RequestGoodsSupplyManagement")]
public class RequestGoodsSupplyManagementController : ControllerBase
{
    private readonly IRequestGoodsSupplyManagementLogic _logic;

    public RequestGoodsSupplyManagementController(IRequestGoodsSupplyManagementLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("SetConfirmedGoodsSupplyProduct")]
    [ResponseSchema<SetConfirmedGoodsSupplyProductResponse>]
    public async Task<IResult> SetConfirmedGoodsSupplyProduct(
    [FromBody] SetConfirmedGoodsSupplyProductRequest request,
    CT ct)
    {
        var newRequest = new SetConfirmedGoodsSupplyProductModelRequest()
        {
            Id = request.Id,
            ProductEntity = null,
            Detail = request.Detail,
            Description = request.Description
        };
        var result = await _logic.SetConfirmedGoodsSupplyProduct(newRequest, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateRequestGoodsSupplyManagementResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateRequestGoodsSupplyManagementRequest request,
        CT ct)
    {
        var result = await _logic.UpdateRequestGoodsSupplyManagement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateRequestGoodsSupplyManagements")]
    [ResponseSchema<UpdateRequestGoodsSupplyManagementsResponse>]
    public async Task<IResult> UpdateRequestGoodsSupplyManagements(
        [FromBody] UpdateRequestGoodsSupplyManagementsRequest request,
        CT ct)
    {
        var result = await _logic.UpdateRequestGoodsSupplyManagements(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsGoodsSupplyManagmentBySupplyProductId")]
    [ResponseSchema<GetsGoodsSupplyManagmentBySupplyProductIdResponse>]
    public async Task<IResult> GetsGoodsSupplyManagmentBySupplyProductId(
        [FromQuery] GetsGoodsSupplyManagmentBySupplyProductIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsGoodsSupplyManagmentBySupplyProductId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredRequestGoodsSupplie")]
    [ResponseSchema<GetFilteredManagementRequestGoodsSuppliesResponse>]
    public async Task<IResult> GetsFilteredRequestGoodsSupplie(
        [FromBody] GetFilteredManagementRequestGoodsSuppliesRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredRequestGoodsSupplyManagements(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectManagerRequestGoodsSupplie")]
    [ResponseSchema<GetsProjectManagerRequestGoodsSupplieResponse>]
    public async Task<IResult> GetsProjectManagerRequestGoodsSupplie(
        [FromBody] GetsProjectManagerRequestGoodsSupplieRequest request,
        CT ct)
    {
        var result = await _logic.GetsProjectManagerRequestGoodsSupplie(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFilteredAlternatives")]
    [ResponseSchema<GetFilteredAlternativeProductsResponse>]
    public async Task<IResult> GetFilteredAlternatives(
        [FromQuery] GetFilteredAlternativeProductsRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredAlternativeProductsRequestAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestGoodsManagementHistoryById")]
    [ResponseSchema<GetRequestGoodsManagementHistoryByIdResponse>]
    public async Task<IResult> GetRequestGoodsManagementHistoryById(
        [FromQuery] GetRequestGoodsManagementHistoryByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestGoodsManagementHistoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProducts")]
    [ResponseSchema<GetRequestGoodsSupplyProductResponse>]
    public async Task<IResult> GetProducts(
        [FromQuery] GetRequestGoodsSupplyProductRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFilteredWarehouses")]
    [ResponseSchema<GetsSourceWarehouseResponse>]
    public async Task<IResult> GetFilteredWarehouses(
        [FromQuery] GetsSourceWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.GetsSourceWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDestinationWarehouse")]
    [ResponseSchema<GetsDestinationWarehouseResponse>]
    public async Task<IResult> GetsDestinationWarehouse(
        [FromQuery] GetsDestinationWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.GetsDestinationWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsManagementGoodsSupplyForWarehouse")]
    [ResponseSchema<GetsManagementGoodsSupplyForWarehouseResponse>]
    public async Task<IResult> GetsManagementGoodsSupplyForWarehouse(
        [FromQuery] GetsManagementGoodsSupplyForWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.GetsManagementGoodsSupplyForWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestGoodsSupplyManagementType")]
    [ResponseSchema<GetsRequestGoodsSupplyManagementTypeResponse>]
    public async Task<IResult> GetsRequestGoodsSupplyManagementType(
        [FromQuery] GetsRequestGoodsSupplyManagementTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyManagementType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestGoodsSupplyManagementStatus")]
    [ResponseSchema<GetsRequestGoodsSupplyManagementStatusResponse>]
    public async Task<IResult> GetsRequestGoodsSupplyManagementStatus(
        [FromQuery] GetsRequestGoodsSupplyManagementStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyManagementStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetType")]
    [ResponseSchema<GetsRequestGoodsSupplyManagementTypeResponse>]
    public async Task<IResult> GetType(
        [FromQuery] GetsRequestGoodsSupplyManagementTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyManagementType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetStatus")]
    [ResponseSchema<GetsRequestGoodsSupplyManagementStatusResponse>]
    public async Task<IResult> GetStatus(
        [FromQuery] GetsRequestGoodsSupplyManagementStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestGoodsSupplyManagementStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsSupplyManagementExcelExporter")]
    [ResponseSchema<GetsSupplyManagementExcelExporterResponse>]
    public async Task<IResult> GetsSupplyManagementExcelExporter(
        [FromBody] GetsSupplyManagementExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsSupplyManagementExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsSupplyManagementExcelEnums")]
    [ResponseSchema<GetsSupplyManagementExcelEnumsResponse>]
    public async Task<IResult> GetsSupplyManagementExcelEnums(
        [FromQuery] GetsSupplyManagementExcelEnumsRequest request,
        CT ct)
    {
        var result = await _logic.GetsSupplyManagementExcelEnums(request, ct);
        return result.GetHttpResponse();
    }
}