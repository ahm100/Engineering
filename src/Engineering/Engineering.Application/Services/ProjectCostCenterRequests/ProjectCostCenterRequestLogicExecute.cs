using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.DeleteProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequestById;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.RejectProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.SubmitCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.UpdateProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Models;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;

namespace Engineering.Application.Services.ProjectCostCenterRequests;

public partial class ProjectCostCenterRequestLogic
{
    public async Task<Result<ProjectCostCenterRequest?>> SubmitCostCenterRequestCommand(
        SubmitCostCenterRequestRequest request, CT ct)
    {
        try
        {
            var projectResult = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.ProjectId), ct);
            if (projectResult.IsBad() || projectResult.Value is null)
                return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.ProjectWithIdNotFound);

            var project = projectResult.Value;
            var currentThirdPartyId = _userInfoProvider.ThirdPartyId;
            var currentUserId = _userInfoProvider.UserId;

            if (project.ProjectManager != currentThirdPartyId && project.ProjectManager != currentUserId)
                return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.OnlyProjectManagerCanRequestCostCenter);

            var entity = new ProjectCostCenterRequest(
                projectId: request.ProjectId,
                suggestedName: request.Name,
                description: request.Description,
                thirdPartyId: currentThirdPartyId,
                userId: currentUserId);

            await _repository.Create(entity, ct);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectCostCenterRequest>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ProjectCostCenterRequest?>> UpdateProjectCostCenterRequestCommand(
        UpdateProjectCostCenterRequestRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null) return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.CostCenterRequestNotFound);
            if (!entity.CanEdit()) return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.CostCenterRequestCannotBeEdited);

            var projectResult = await _mediator.Send(new GetProjectByIdIncludelessQuery(entity.ProjectId), ct);
            if (projectResult.IsBad() || projectResult.Value is null)
                return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.ProjectWithIdNotFound);

            var project = projectResult.Value;
            if (project.ProjectManager != _userInfoProvider.UserId && project.ProjectManager != _userInfoProvider.ThirdPartyId)
                return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.OnlyProjectManagerCanRequestCostCenter);

            entity.SetSuggestedName(request.Name);
            entity.SetDescription(request.Description);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectCostCenterRequest>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<bool>> DeleteProjectCostCenterRequestCommand(
        DeleteProjectCostCenterRequestRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null) return Result.Failure<bool>(ProjectErrors.CostCenterRequestNotFound);
            if (!entity.CanEdit()) return Result.Failure<bool>(ProjectErrors.CostCenterRequestCannotBeEdited);

            var projectResult = await _mediator.Send(new GetProjectByIdIncludelessQuery(entity.ProjectId), ct);
            if (projectResult.IsBad() || projectResult.Value is null)
                return Result.Failure<bool>(ProjectErrors.ProjectWithIdNotFound);

            var project = projectResult.Value;
            if (project.ProjectManager != _userInfoProvider.UserId && project.ProjectManager != _userInfoProvider.ThirdPartyId)
                return Result.Failure<bool>(ProjectErrors.OnlyProjectManagerCanRequestCostCenter);

            entity.SoftDelete();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ProjectCostCenterRequest?>> RejectProjectCostCenterRequestCommand(
        RejectProjectCostCenterRequestRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null) return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.CostCenterRequestNotFound);
            if (!entity.CanEdit()) return Result.Failure<ProjectCostCenterRequest>(ProjectErrors.CostCenterRequestCannotBeEdited);

            entity.Reject(request.Reason);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectCostCenterRequest>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<(List<ProjectCostCenterRequestModel> Data, int RowCount)>> GetProjectCostCenterRequestsHandler(
    GetProjectCostCenterRequestsRequest request, CT ct)
    {
        try
        {
            return await _repository.GetRequests(
                request.ProjectId,
                request.Status,
                request.PageIndex,
                request.PageSize,
                ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<(List<ProjectCostCenterRequestModel> Data, int RowCount)>(
                SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ProjectCostCenterRequestModel?>> GetProjectCostCenterRequestByIdHandler(
        GetProjectCostCenterRequestByIdRequest request, CT ct)
    {
        try
        {
            var model = await _repository.GetModelById(request.Id, ct);
            if (model is null)
                return Result.Failure<ProjectCostCenterRequestModel>(
                    ProjectErrors.CostCenterRequestNotFound);
            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectCostCenterRequestModel>(SharedErrors.UnknownError);
        }
    }
}