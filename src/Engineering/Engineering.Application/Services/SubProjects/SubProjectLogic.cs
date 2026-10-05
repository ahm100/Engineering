using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.SubProjects.Contracts.ChangeSubProjectStatus;
using Engineering.Application.Services.SubProjects.Contracts.CreateSubProject;
using Engineering.Application.Services.SubProjects.Contracts.DeleteSubProject;
using Engineering.Application.Services.SubProjects.Contracts.GetAllowedSubProjectManagers;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjectByCode;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjectById;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjects;
using Engineering.Application.Services.SubProjects.Contracts.UpdateSubProject;
using Engineering.Application.Services.SubProjects.Models;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.Histories;
using Engineering.Domain.Errors.Projects;
using IdentityServer.ClientSdk.Services;
using Microsoft.EntityFrameworkCore;

namespace Engineering.Application.Services.SubProjects;

public partial class SubProjectLogic : ISubProjectLogic
{
    private readonly ISubProjectRepository _repository;
    private readonly IProjectRepository _projectRepository;
    private readonly IViewThirdPartyRepository _thirdPartyRepository;
    private readonly IUserInfoProvider _userInfo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubProjectLogic> _logger;

    public SubProjectLogic(
        ISubProjectRepository repository,
        IProjectRepository projectRepository,
        IViewThirdPartyRepository thirdPartyRepository,
        IUserInfoProvider userInfo,
        IUnitOfWork unitOfWork,
        ILogger<SubProjectLogic> logger)
    {
        _repository = repository;
        _projectRepository = projectRepository;
        _thirdPartyRepository = thirdPartyRepository;
        _userInfo = userInfo;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<CreateSubProjectResponse?>> CreateSubProject(
        CreateSubProjectRequest request, CT ct)
    {
        _logger.LogInformation("CreateSubProject");
        var validation = await request.IsValidAsync<CreateSubProjectValidator, CreateSubProjectRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<CreateSubProjectResponse>(validation.Error!);

        var project = await _projectRepository.GetProjectByIdIncludeLess(request.ProjectId, ct);
        if (project is null)
            return Result.Failure<CreateSubProjectResponse>(ProjectErrors.ProjectWithIdNotFound);

        if (!project.IsActive || project.IsDeleted || project.Status is ProjectStatus.Closed or ProjectStatus.Canceled)
            return Result.Failure<CreateSubProjectResponse>(SubProjectErrors.ParentNotUsable);

        if (!await HasAccess(project.Id, ct))
            return Result.Failure<CreateSubProjectResponse>(SubProjectErrors.NoProjectAccess);

        if (string.IsNullOrWhiteSpace(project.ProjectCode))
            return Result.Failure<CreateSubProjectResponse>(ProjectErrors.ProjectCodeIsEmpty);

        if (request.ManagerId.HasValue &&
            !(await _repository.GetAllowedManagerIds(project.Id, ct)).Contains(request.ManagerId.Value))
            return Result.Failure<CreateSubProjectResponse>(ProjectErrors.UnValidManagerId);

        try
        {
            var sequence = await _repository.AllocateSequence(project.Id, ct);
            var code = $"{project.ProjectCode}-SP-{sequence:00}";
            var entity = new SubProject(project, code, sequence, request.Name, request.Type,
                request.Description, request.ManagerId, request.StartDate, request.EndDate);
            await _repository.Create(entity, ct);
            await _repository.AddHistory(new SubProjectHistory(entity, SubProjectHistoryOperation.Create), ct);
            await _unitOfWork.CommitAsync(ct);
            return new CreateSubProjectResponse(entity.Id, entity.SubProjectCode, entity.SequenceNumber);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex,
                "SubProject unique constraint rejected generated code for Project {ProjectId}",
                request.ProjectId);
            return Result.Failure<CreateSubProjectResponse>(SubProjectErrors.DuplicateCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create SubProject");
            return Result.Failure<CreateSubProjectResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<UpdateSubProjectResponse?>> UpdateSubProject(
        UpdateSubProjectRequest request, CT ct)
    {
        _logger.LogInformation("UpdateSubProject");
        var validation = await request.IsValidAsync<UpdateSubProjectValidator, UpdateSubProjectRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<UpdateSubProjectResponse>(validation.Error!);

        var entity = await _repository.GetSubProjectById(request.Id, ct);
        if (entity is null)
            return Result.Failure<UpdateSubProjectResponse>(SubProjectErrors.NotFound);

        if (!await HasAccess(entity.ProjectId, ct))
            return Result.Failure<UpdateSubProjectResponse>(SubProjectErrors.NoProjectAccess);

        if (request.ManagerId.HasValue &&
            !(await _repository.GetAllowedManagerIds(entity.ProjectId, ct)).Contains(request.ManagerId.Value))
            return Result.Failure<UpdateSubProjectResponse>(ProjectErrors.UnValidManagerId);

        entity.Update(request.Name, request.Type, request.Description,
            request.ManagerId, request.StartDate, request.EndDate);
        await _repository.AddHistory(new SubProjectHistory(entity, SubProjectHistoryOperation.Update), ct);
        await _repository.Update(entity);
        await _unitOfWork.CommitAsync(ct);
        return new UpdateSubProjectResponse(entity.Id, true);
    }

    public async Task<Result<DeleteSubProjectResponse?>> DeleteSubProject(
        DeleteSubProjectRequest request, CT ct)
    {
        _logger.LogInformation("DeleteSubProject");
        var validation = await request.IsValidAsync<DeleteSubProjectValidator, DeleteSubProjectRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<DeleteSubProjectResponse>(validation.Error!);

        var entity = await _repository.GetSubProjectById(request.Id, ct);
        if (entity is null)
            return Result.Failure<DeleteSubProjectResponse>(SubProjectErrors.NotFound);

        if (!await HasAccess(entity.ProjectId, ct))
            return Result.Failure<DeleteSubProjectResponse>(SubProjectErrors.NoProjectAccess);

        if (await _repository.HasDependencies(entity.Id, ct))
            return Result.Failure<DeleteSubProjectResponse>(SubProjectErrors.HasDependencies);

        await _repository.AddHistory(new SubProjectHistory(entity, SubProjectHistoryOperation.Delete), ct);
        entity.Delete();
        await _repository.Update(entity);
        await _unitOfWork.CommitAsync(ct);
        return new DeleteSubProjectResponse(true);
    }

    public async Task<Result<ChangeSubProjectStatusResponse?>> ChangeSubProjectStatus(
        ChangeSubProjectStatusRequest request, CT ct)
    {
        _logger.LogInformation("ChangeSubProjectStatus");
        var validation = await request.IsValidAsync<ChangeSubProjectStatusValidator, ChangeSubProjectStatusRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<ChangeSubProjectStatusResponse>(validation.Error!);

        var entity = await _repository.GetSubProjectById(request.Id, ct);
        if (entity is null)
            return Result.Failure<ChangeSubProjectStatusResponse>(SubProjectErrors.NotFound);

        if (!await HasAccess(entity.ProjectId, ct))
            return Result.Failure<ChangeSubProjectStatusResponse>(SubProjectErrors.NoProjectAccess);

        if (!entity.CanChangeStatusTo(request.Status))
            return Result.Failure<ChangeSubProjectStatusResponse>(SubProjectErrors.InvalidStatusTransition);

        entity.ChangeStatus(request.Status);
        await _repository.AddHistory(new SubProjectHistory(entity, SubProjectHistoryOperation.StatusChange), ct);
        await _repository.Update(entity);
        await _unitOfWork.CommitAsync(ct);
        return new ChangeSubProjectStatusResponse(entity.Id, entity.Status);
    }

    public async Task<Result<GetSubProjectByIdResponse?>> GetSubProjectById(
        GetSubProjectByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjectById");
        var validation = await request.IsValidAsync<GetSubProjectByIdValidator, GetSubProjectByIdRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetSubProjectByIdResponse>(validation.Error!);

        var data = await _repository.GetSubProjectDetailsById(request.Id, ct);
        if (data is null)
            return Result.Failure<GetSubProjectByIdResponse>(SubProjectErrors.NotFound);

        if (!await HasAccess(data.ProjectId, ct))
            return Result.Failure<GetSubProjectByIdResponse>(SubProjectErrors.NoProjectAccess);

        return new GetSubProjectByIdResponse(data);
    }

    public async Task<Result<GetSubProjectByCodeResponse?>> GetSubProjectByCode(
        GetSubProjectByCodeRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjectByCode");
        var validation = await request.IsValidAsync<GetSubProjectByCodeValidator, GetSubProjectByCodeRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetSubProjectByCodeResponse>(validation.Error!);

        var data = await _repository.GetSubProjectDetailsByCode(request.Code, ct);
        if (data is null)
            return Result.Failure<GetSubProjectByCodeResponse>(SubProjectErrors.NotFound);

        if (!await HasAccess(data.ProjectId, ct))
            return Result.Failure<GetSubProjectByCodeResponse>(SubProjectErrors.NoProjectAccess);

        return new GetSubProjectByCodeResponse(data);
    }

    public async Task<Result<GetSubProjectsResponse?>> GetSubProjects(
        GetSubProjectsRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjects");
        var validation = await request.IsValidAsync<GetSubProjectsValidator, GetSubProjectsRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetSubProjectsResponse>(validation.Error!);

        if (!await HasAccess(request.ProjectId, ct))
            return Result.Failure<GetSubProjectsResponse>(SubProjectErrors.NoProjectAccess);

        var result = await _repository.GetSubProjects(request.ProjectId, request.FilterData, request.Status,
            request.Type, request.PageIndex, request.PageSize, ct);
        return new GetSubProjectsResponse(result.Data, result.RowCount);
    }

    public async Task<Result<GetAllowedSubProjectManagersResponse?>> GetAllowedSubProjectManagers(
        GetAllowedSubProjectManagersRequest request, CT ct)
    {
        _logger.LogInformation("GetAllowedSubProjectManagers");
        var validation = await request
            .IsValidAsync<GetAllowedSubProjectManagersValidator, GetAllowedSubProjectManagersRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetAllowedSubProjectManagersResponse>(validation.Error!);

        if (!await HasAccess(request.ProjectId, ct))
            return Result.Failure<GetAllowedSubProjectManagersResponse>(SubProjectErrors.NoProjectAccess);

        var managerIds = await _repository.GetAllowedManagerIds(request.ProjectId, ct);
        var managers = await _thirdPartyRepository.GetByIds(managerIds, ct);
        return new GetAllowedSubProjectManagersResponse(
            managers.Select(oo => new SubProjectManager(oo.Id, oo.FullName)).ToList());
    }
}
