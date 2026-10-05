using Engineering.Application.Services.FiduciaryProductManages;
using Engineering.Application.Services.FiduciaryProductManages.Models.CreateFiduciaryProductDetailReturn;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailManagementStatus;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnDocumentById;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnType;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManageWarehouses;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductPending;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductRejected;
using Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailDelivary;
using Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailNoDelivary;

namespace Engineering.Api.Controllers.FiduciaryProductManages;

[ApiController]
[Route("api/engineering/v1/FiduciaryProductManage")]
public class FiduciaryProductManageController : ControllerBase
{
    private readonly IFiduciaryProductManageLogic _logic;

    public FiduciaryProductManageController(IFiduciaryProductManageLogic logic)
    {
        _logic = logic;
    }

    [HttpGet("GetStatus")]
    [ResponseSchema<GetFiduciaryProductDetailManagementStatusResponse>]
    public async Task<IResult> GetStatus([FromQuery] GetFiduciaryProductDetailManagementStatusRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductDetailManagementStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetReturnInfoById")]
    [ResponseSchema<GetFiduciaryProductManagementByIdResponse>]
    public async Task<IResult> GetReturnInfoById([FromQuery] GetFiduciaryProductManagementByIdRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductManagementById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFilteredDocuments")]
    [ResponseSchema<GetFiduciaryProductDetailReturnDocumentByIdResponse>]
    public async Task<IResult> GetFilteredDocuments([FromQuery] GetFiduciaryProductDetailReturnDocumentByIdRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductDetailReturnDocumentById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetReturnTypes")]
    [ResponseSchema<GetFiduciaryProductDetailReturnTypeResponse>]
    public async Task<IResult> GetReturnTypes([FromQuery] GetFiduciaryProductDetailReturnTypeRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductDetailReturnType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetReturnById")]
    [ResponseSchema<GetFiduciaryProductDetailReturnByDetailIdResponse>]
    public async Task<IResult> GetReturnById([FromQuery] GetFiduciaryProductDetailReturnByDetailIdRequest request, CT ct)
    {
        var result = await _logic.GetFiduciaryProductDetailReturnByDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFilteredWarehouses")]
    [ResponseSchema<GetFilteredFiduciaryProductManageWarehousesResponse>]
    public async Task<IResult> GetFilteredWarehouses([FromQuery] GetFilteredFiduciaryProductManageWarehousesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredFiduciaryProductManageWarehouses(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetPending")]
    [ResponseSchema<SetFiduciaryProductPendingResponse>]
    public async Task<IResult> SetPending([FromBody] SetFiduciaryProductPendingRequest request, CT ct)
    {
        var result = await _logic.SetFiduciaryProductPending(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRejected")]
    [ResponseSchema<SetFiduciaryProductRejectedResponse>]
    public async Task<IResult> SetRejected([FromBody] SetFiduciaryProductRejectedRequest request, CT ct)
    {
        var result = await _logic.SetFiduciaryProductRejected(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Deliver")]
    [ResponseSchema<SetFiduciaryProductDetailDelivaryResponse>]
    public async Task<IResult> Deliver([FromBody] SetFiduciaryProductDetailDelivaryRequest request, CT ct)
    {
        var result = await _logic.SetFiduciaryProductDetailDelivary(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("NoDeliver")]
    [ResponseSchema<SetFiduciaryProductDetailNoDelivaryResponse>]
    public async Task<IResult> NoDeliver([FromBody] SetFiduciaryProductDetailNoDelivaryRequest request, CT ct)
    {
        var result = await _logic.SetFiduciaryProductDetailNoDelivary(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Confirm")]
    [ResponseSchema<SetFiduciaryProductConfirmedResponse>]
    public async Task<IResult> Confirm([FromBody] SetFiduciaryProductConfirmedRequest request, CT ct)
    {
        var result = await _logic.SetFiduciaryProductConfirmed(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateFiduciaryProductDetailReturnResponse>]
    public async Task<IResult> Create([FromBody] CreateFiduciaryProductDetailReturnRequest request, CT ct)
    {
        var result = await _logic.CreateFiduciaryProductDetailReturn(request, ct);
        return result.GetHttpResponse();
    }
}