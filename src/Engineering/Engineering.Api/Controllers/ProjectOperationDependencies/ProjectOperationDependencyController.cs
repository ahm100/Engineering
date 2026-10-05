using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.ProjectOperationDependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.Create;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.DeleteProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.UpdateProjectOperationDependency;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using System.ComponentModel;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDependencies")]
public class ProjectOperationDependencyController : ControllerBase
{
    private readonly IProjectOperationDependencyLogic _logic;

    public ProjectOperationDependencyController(IProjectOperationDependencyLogic logic)
    {
        _logic = logic;
    }

    [HttpPost]
    [ResponseSchema<CreateProjectOperationDependencyResponse>]
    public async Task<IResult> CreateProjectOperationDependency([FromBody] CreateProjectOperationDependencyRequest request, CT ct)
    {
        var result = await _logic.CreateProjectOperationDependency(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("{id}")]
    [ResponseSchema<UpdateProjectOperationDependencyResponse>]
    public async Task<IResult> UpdateProjectOperationDependency(
        [FromRoute] long id,
        [FromBody] UpdateProjectOperationDependencyModel request,
        CT ct)
    {
        var result = await _logic.UpdateProjectOperationDependency(new(id, request), ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("{id}")]
    [ResponseSchema<DeleteProjectOperationDependencyResponse>]
    public async Task<IResult> DeleteProjectOperationDependency(
        [FromRoute] long id,
        CT ct)
    {
        var result = await _logic.DeleteProjectOperationDependency(new(id), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDependencyByPOId")]
    [ResponseSchema<GetDependencyByPOIdResponse>]
    public async Task<IResult> GetDependencyByPOId(
        [FromQuery] GetDependencyByPOIdRequest request,
        CT ct)
    {
        var result = await _logic.GetDependencyByPOId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrDependency")]
    [ResponseSchema<GetFltrDependencyResponse>]
    public async Task<IResult> GetFltrDependency(
        [FromBody] GetFltrDependencyRequest request,
        CT ct)
    {
        var result = await _logic.GetFltrDependency(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPORequirements")]
    [ResponseSchema<GetPORequirementsResponse>]
    public async Task<IResult> GetPORequirements(
        [FromQuery] GetPORequirementsRequest request,
        CT ct)
    {
        var result = await _logic.GetPORequirements(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPODependencies")]
    [ResponseSchema<GetPODependenciesResponse>]
    public async Task<IResult> GetPODependencies(
        [FromQuery] GetPODependenciesRequest request,
        CT ct)
    {
        var result = await _logic.GetPODependencies(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("dependency-types")]
    [Description("Get enum values for dependency Type.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetDependencyType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ProjectOperationDependencyType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }
}