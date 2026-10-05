using Engineering.Application.Services.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.DeleteFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.FiduciaryProductGroupDelete;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductDetailStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;
using Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;

namespace Engineering.Api.Controllers.FiduciaryProducts;

[ApiController]
[Route("api/engineering/v1/FiduciaryProduct")]
public class FiduciaryProductController : ControllerBase
{
    private readonly IFiduciaryProductLogic _logic;

    public FiduciaryProductController(IFiduciaryProductLogic logic)
    {
        _logic = logic;
    }

    [HttpGet("GetDetailStatus")]
    [ResponseSchema<GetFiduciaryProductDetailStatusResponse>]
    public async Task<IResult> GetDetailStatus([FromQuery] GetFiduciaryProductDetailStatusRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductDetailStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetStatus")]
    [ResponseSchema<GetFiduciaryProductStatusResponse>]
    public async Task<IResult> GetStatus([FromQuery] GetFiduciaryProductStatusRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetHistories")]
    [ResponseSchema<GetFilteredFiduciaryProductHistoriesResponse>]
    public async Task<IResult> GetHistories([FromQuery] GetFilteredFiduciaryProductHistoriesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredFiduciaryProductHistories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDetailHistories")]
    [ResponseSchema<GetFilteredFiduciaryProductDetailHistoriesResponse>]
    public async Task<IResult> GetDetailHistories([FromQuery] GetFilteredFiduciaryProductDetailHistoriesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredFiduciaryProductDetailHistories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetById")]
    [ResponseSchema<GetFiduciaryProductByIdResponse>]
    public async Task<IResult> GetById([FromQuery] GetFiduciaryProductByIdRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFiltered")]
    [ResponseSchema<GetFilteredFiduciaryProductsResponse>]
    public async Task<IResult> GetFiltered([FromBody] GetFilteredFiduciaryProductsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredFiduciaryProducts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateFiduciaryProductResponse>]
    public async Task<IResult> Create([FromBody] CreateFiduciaryProductRequest request, CT ct)
    {
        var result = await _logic.CreateFiduciaryProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("FiduciaryProductGroupDelete")]
    [ResponseSchema<FiduciaryProductGroupDeleteResponse>]
    public async Task<IResult> FiduciaryProductGroupDelete([FromBody] FiduciaryProductGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.FiduciaryProductGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateFiduciaryProductResponse>]
    public async Task<IResult> Update([FromBody] UpdateFiduciaryProductRequest request, CT ct)
    {
        var result = await _logic.UpdateFiduciaryProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredExcelExporter")]
    [ResponseSchema<GetFilteredFiduciaryProductsExcelExporterResponse>]
    public async Task<IResult> GetFilteredExcelExporter([FromBody] GetFilteredFiduciaryProductsExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetFilteredFiduciaryProductsExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFilteredExcelEnums")]
    [ResponseSchema<GetFilteredFiduciaryProductsExcelEnumsResponse>]
    public async Task<IResult> GetFilteredExcelEnums([FromQuery] GetFilteredFiduciaryProductsExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredFiduciaryProductsExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("Delete")]
    [ResponseSchema<DeleteFiduciaryProductResponse>]
    public async Task<IResult> Delete([FromBody] DeleteFiduciaryProductRequest request, CT ct)
    {
        var result = await _logic.DeleteFiduciaryProductAsync(request, ct);
        return result.GetHttpResponse();
    }
}