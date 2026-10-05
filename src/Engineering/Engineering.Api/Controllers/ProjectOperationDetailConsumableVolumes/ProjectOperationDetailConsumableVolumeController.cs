using Engineering.Application.Services.ConsumableVolumes;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.CreateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DeleteExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationIds;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.UpdateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.GetProjectOperationDetailVolumes;
using Engineering.Application.Services.ConsumableVolumes.Models.GetVolumeProductTypes;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.CreateMachineryConsumable;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DeleteMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetFilteredTotalOfConsumebleMachineries;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetsFilteredMachineriyVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.UpdateMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.CreateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.DeleteProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetsProductsByFiltered;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.UpdateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.UpdateConsumableVolumes;

namespace Engineering.Api.Controllers.ProjectOperationDetailConsumableVolumes;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetailConsumableVolume")]
public class ProjectOperationDetailConsumableVolumeController : ControllerBase
{
    private readonly IConsumableVolumeLogic _logic;

    public ProjectOperationDetailConsumableVolumeController(
        IConsumableVolumeLogic logic)
    {
        _logic = logic;
    }

    [HttpPut("UpdateConsumableVolumes")]
    [ResponseSchema<UpdateConsumableVolumesResponse>]
    public async Task<IResult> UpdateConsumableVolumes(
    [FromBody] UpdateConsumableVolumesRequest request,
    CT ct)
    {
        var result = await _logic.UpdateConsumableVolumes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDetailVolumes")]
    [ResponseSchema<GetProjectOperationDetailVolumesResponse>]
    public async Task<IResult> GetProjectOperationDetailVolumes(
        [FromQuery] GetProjectOperationDetailVolumesRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectOperationDetailVolumes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddConsumableVolumeExpert")]
    [ResponseSchema<CreateConsumableVolumeExpertResponse>]
    public async Task<IResult> AddConsumableVolumeExpert(
        [FromBody] CreateConsumableVolumeExpertRequest request,
        CT ct)
    {
        var result = await _logic.CreateConsumableVolumeExpert(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditConsumableVolumeExpert")]
    [ResponseSchema<UpdateConsumableVolumeExpertResponse>]
    public async Task<IResult> EditConsumableVolumeExpert(
        [FromBody] UpdateConsumableVolumeExpertRequest request,
        CT ct)
    {
        var result = await _logic.UpdateConsumableVolumeExpert(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsExpertByProjectOperationDetailId")]
    [ResponseSchema<GetExpertsByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetsExpertByProjectOperationDetailId(
        [FromQuery] GetExpertsByProjectOperationDetailIdRequest request,
        CT ct)
    {
        var result = await _logic.GetExpertsByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsExpertByProjectOperationId")]
    [ResponseSchema<GetExpertsByProjectOperationIdResponse>]
    public async Task<IResult> GetsExpertByProjectOperationId(
        [FromQuery] GetExpertsByProjectOperationIdRequest request,
        CT ct)
    {
        var result = await _logic.GetExpertsByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsExpertByProjectOperationIds")]
    [ResponseSchema<GetExpertsByProjectOperationIdsResponse>]
    public async Task<IResult> GetsExpertByProjectOperationIds(
        [FromBody] GetExpertsByProjectOperationIdsRequest request,
        CT ct)
    {
        var result = await _logic.GetExpertsByProjectOperationIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteConsumableVolumeExpert")]
    [ResponseSchema<DeleteConsumableVolumeExpertResponse>]
    public async Task<IResult> DeleteConsumableVolumeExpert(
        [FromBody] DeleteConsumableVolumeExpertRequest request,
        CT ct)
    {
        var result = await _logic.DeleteConsumableVolumeExpert(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddConsumableVolumeMachinery")]
    [ResponseSchema<CreateConsumableVolumeMachineryResponse>]
    public async Task<IResult> AddConsumableVolumeMachinery(
        [FromBody] CreateConsumableVolumeMachineryRequest request,
        CT ct)
    {
        var result = await _logic.CreateConsumableVolumeMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditConsumableVolumeMachinery")]
    [ResponseSchema<UpdateConsumableVolumeMachineryResponse>]
    public async Task<IResult> EditConsumableVolumeMachinery(
        [FromBody] UpdateConsumableVolumeMachineryRequest request,
        CT ct)
    {
        var result = await _logic.UpdateConsumableVolumeMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineryByProjectOperationDetailId")]
    [ResponseSchema<GetMachineriesByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetsMachineryByProjectOperationDetailId(
        [FromQuery] GetMachineriesByProjectOperationDetailIdRequest request,
        CT ct)
    {
        var result = await _logic.GetMachineriesByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineryByProjectOperationId")]
    [ResponseSchema<GetMachineriesByProjectOperationIdResponse>]
    public async Task<IResult> GetsMachineryByProjectOperationId(
        [FromQuery] GetMachineriesByProjectOperationIdRequest request,
        CT ct)
    {
        var result = await _logic.GetMachineriesByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFilteredMachineriyVolume")]
    [ResponseSchema<GetsFilteredMachineriyVolumeResponse>]
    public async Task<IResult> GetsFilteredMachineriyVolume(
    [FromQuery] GetsFilteredMachineriyVolumeRequest request,
    CT ct)
    {
        var result = await _logic.GetsFilteredMachineriyVolume(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredTotalOfConsumebleMachineries")]
    [ResponseSchema<GetFilteredTotalOfConsumebleMachineriesResponse>]
    public async Task<IResult> GetFilteredTotalOfConsumebleMachineries(
        [FromBody] GetFilteredTotalOfConsumebleMachineriesRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredTotalOfConsumebleMachineries(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteConsumableVolumeMachinery")]
    [ResponseSchema<DeleteConsumableVolumeMachineryResponse>]
    public async Task<IResult> DeleteConsumableVolumeMachinery(
        [FromBody] DeleteConsumableVolumeMachineryRequest request,
        CT ct)
    {
        var result = await _logic.DeleteConsumableVolumeMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddConsumableVolumeProduct")]
    [ResponseSchema<CreateConsumableVolumeProductResponse>]
    public async Task<IResult> AddConsumableVolumeProduct(
        [FromBody] CreateConsumableVolumeProductRequest request,
        CT ct)
    {
        var result = await _logic.CreateConsumableVolumeProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditConsumableVolumeProduct")]
    [ResponseSchema<UpdateConsumableVolumeProductResponse>]
    public async Task<IResult> EditConsumableVolumeProduct(
        [FromBody] UpdateConsumableVolumeProductRequest request,
        CT ct)
    {
        var result = await _logic.UpdateConsumableVolumeProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProductByProjectOperationDetailId")]
    [ResponseSchema<GetProductsByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetsProductByProjectOperationDetailId(
        [FromQuery] GetProductsByProjectOperationDetailIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProductsByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProductByProjectOperationId")]
    [ResponseSchema<GetProductsByProjectOperationIdResponse>]
    public async Task<IResult> GetsProductByProjectOperationId(
        [FromQuery] GetProductsByProjectOperationIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProductsByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProductsByFiltered")]
    [ResponseSchema<GetsProductsByFilteredResponse>]
    public async Task<IResult> GetsProductsByFiltered(
        [FromBody] GetsProductsByFilteredRequest request,
        CT ct)
    {
        var result = await _logic.GetsProductsByFiltered(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetVolumeProductTypes")]
    [ResponseSchema<GetVolumeProductTypesResponse>]
    public async Task<IResult> GetVolumeProductTypes(
        [FromQuery] GetVolumeProductTypesRequest request,
        CT ct)
    {
        var result = await _logic.GetVolumeProductTypes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteConsumableVolumeProduct")]
    [ResponseSchema<DeleteConsumableVolumeProductResponse>]
    public async Task<IResult> DeleteConsumableVolumeProduct(
        [FromBody] DeleteConsumableVolumeProductRequest request,
        CT ct)
    {
        var result = await _logic.DeleteConsumableVolumeProduct(request, ct);
        return result.GetHttpResponse();
    }
}