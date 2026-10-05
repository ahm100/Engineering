using Engineering.Application.Services.ConsumptionStandards;
using Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.GetsConsumptionStandardByOperationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.UpdateConsumptionStandards;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.CreateExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.DisableExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertGetsByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.UpdateExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.GetProductAllowedTypes;
using Engineering.Application.Services.ConsumptionStandards.Models.GetStandardProductTypes;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.CreateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.DisableMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryGetsByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.UpdateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.CreateProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.DisableProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsNonStandardProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.UpdateProduct;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/ConsumptionStandard")]
public class ConsumptionStandardController : ControllerBase
{
    private readonly IConsumptionStandardLogic _logic;

    public ConsumptionStandardController(IConsumptionStandardLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddExpertStandard")]
    [ResponseSchema<CreateConsumptionStandardExpertResponse>]
    public async Task<IResult> AddExpertStandard([FromBody] CreateConsumptionStandardExpertRequest request, CT ct)
    {
        var result = await _logic.CreateExpertStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditExpertStandard")]
    [ResponseSchema<UpdateConsumptionStandardExpertResponse>]
    public async Task<IResult> EditExpertStandard([FromBody] UpdateConsumptionStandardExpertRequest request, CT ct)
    {
        var result = await _logic.UpdateExpertStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("ExpertGetsByOprationInfoId")]
    [ResponseSchema<ExpertGetsByOprationInfoIdResponse>]
    public async Task<IResult> ExpertGetsByOprationInfoId([FromQuery] ExpertGetsByOprationInfoIdRequest request, CT ct)
    {
        var result = await _logic.ExpertGetsByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableExpertStandard")]
    [ResponseSchema<DisableConsumptionStandardExpertResponse>]
    public async Task<IResult> DisableExpertStandard([FromBody] DisableConsumptionStandardExpertRequest request, CT ct)
    {
        var result = await _logic.DisableExpertStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddGoodsStandard")]
    [ResponseSchema<CreateConsumptionStandardProductResponse>]
    public async Task<IResult> AddGoodsStandard([FromBody] CreateConsumptionStandardProductRequest request, CT ct)
    {
        var result = await _logic.CreateProductStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditGoodsStandard")]
    [ResponseSchema<UpdateConsumptionStandardProductResponse>]
    public async Task<IResult> EditGoodsStandard([FromBody] UpdateConsumptionStandardProductRequest request, CT ct)
    {
        var result = await _logic.UpdateProductStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GoodsGetsByOprationInfoId")]
    [ResponseSchema<GetsProductByOprationInfoIdResponse>]
    public async Task<IResult> GoodsGetsByOprationInfoId([FromQuery] GetsProductByOprationInfoIdRequest request, CT ct)
    {
        var result = await _logic.GetsProductByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsNonStandardProductByOprationInfoId")]
    [ResponseSchema<GetsNonStandardProductByOprationInfoIdResponse>]
    public async Task<IResult> GetsNonStandardProductByOprationInfoId([FromQuery] GetsNonStandardProductByOprationInfoIdRequest request, CT ct)
    {
        var result = await _logic.GetsNonStandardProductByOprationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableGoodsStandard")]
    [ResponseSchema<DisableConsumptionStandardProductResponse>]
    public async Task<IResult> DisableGoodsStandard([FromBody] DisableConsumptionStandardProductRequest request, CT ct)
    {
        var result = await _logic.DisableProductStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddMachineryStandard")]
    [ResponseSchema<CreateConsumptionStandardMachineryResponse>]
    public async Task<IResult> AddMachineryStandard([FromBody] CreateConsumptionStandardMachineryRequest request, CT ct)
    {
        var result = await _logic.CreateMachineryStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditMachineryStandard")]
    [ResponseSchema<UpdateConsumptionStandardMachineryResponse>]
    public async Task<IResult> EditMachineryStandard([FromBody] UpdateConsumptionStandardMachineryRequest request, CT ct)
    {
        var result = await _logic.UpdateMachineryStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("MachineryGetsByOprationInfoId")]
    [ResponseSchema<MachineryGetsByOprationInfoIdResponse>]
    public async Task<IResult> MachineryGetsByOprationInfoId([FromQuery] MachineryGetsByOprationInfoIdRequest request, CT ct)
    {
        var result = await _logic.MachineryGetsByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableMachineryStandard")]
    [ResponseSchema<DisableConsumptionStandardMachineryResponse>]
    public async Task<IResult> DisableMachineryStandard([FromBody] DisableConsumptionStandardMachineryRequest request, CT ct)
    {
        var result = await _logic.DisableMachineryStandard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateConsumptionStandards")]
    [ResponseSchema<UpdateConsumptionStandardsResponse>]
    public async Task<IResult> UpdateConsumptionStandards([FromBody] UpdateConsumptionStandardsRequest request, CT ct)
    {
        var result = await _logic.UpdateConsumptionStandards(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsConsumptionStandardByOperationInfoId")]
    [ResponseSchema<GetsConsumptionStandardByOperationInfoIdResponse>]
    public async Task<IResult> GetsConsumptionStandardByOperationInfoId([FromQuery] GetsConsumptionStandardByOperationInfoIdRequest request, CT ct)
    {
        var result = await _logic.GetsConsumptionStandardByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProductAllowedTypes")]
    [ResponseSchema<GetProductAllowedTypesResponse>]
    public async Task<IResult> GetProductAllowedTypes([FromQuery] GetProductAllowedTypesRequest request, CT ct)
    {
        var result = await _logic.GetProductAllowedTypes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetStandardProductTypes")]
    [ResponseSchema<GetStandardProductTypesResponse>]
    public async Task<IResult> GetStandardProductTypes([FromQuery] GetStandardProductTypesRequest request, CT ct)
    {
        var result = await _logic.GetStandardProductTypes(request, ct);
        return result.GetHttpResponse();
    }
}