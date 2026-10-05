using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.ProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.DeleteProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequestById;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.RejectProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.SubmitCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.UpdateProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Models;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Domain.Entities.Projects.Enums;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.ProjectCostCenterRequests;

public partial class ProjectCostCenterRequestLogic : IProjectCostCenterRequestLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectCostCenterRequestLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectCostCenterRequestRepository _repository;
    private readonly ICostCenterRepository _costCenterRepo;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserInfoService _userInfoService;

    public ProjectCostCenterRequestLogic(
        IMediator mediator,
        ILogger<ProjectCostCenterRequestLogic> logger,
        IUnitOfWork unitOfWork,
        IProjectCostCenterRequestRepository repository,
        ICostCenterRepository costCenterRepo,
        IUserInfoProvider userInfoProvider,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _costCenterRepo = costCenterRepo;
        _userInfoProvider = userInfoProvider;
        _userInfoService = userInfoService;
    }

    public async Task<Result<SubmitCostCenterRequestResponse?>> SubmitCostCenterRequest(
        SubmitCostCenterRequestRequest request, CT ct)
    {
        _logger.LogInformation("SubmitCostCenterRequest");

        var isValid = await request.IsValidAsync<SubmitCostCenterRequestValidator,
            SubmitCostCenterRequestRequest>(ct);
        if (isValid.IsFailure) return Result.Failure<SubmitCostCenterRequestResponse>(isValid.Error!);

        var result = await SubmitCostCenterRequestCommand(request, ct);
        if (result.IsBad()) return result.Failure<SubmitCostCenterRequestResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new SubmitCostCenterRequestResponse(result.Value!.Id, true);
    }

    public async Task<Result<GetProjectCostCenterRequestsResponse?>> GetProjectCostCenterRequests(
    GetProjectCostCenterRequestsRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectCostCenterRequests");

        var isValid = await request.IsValidAsync<GetProjectCostCenterRequestsValidator,
            GetProjectCostCenterRequestsRequest>(ct);
        if (isValid.IsFailure)
            return Result.Failure<GetProjectCostCenterRequestsResponse>(isValid.Error!);

        var result = await GetProjectCostCenterRequestsHandler(request, ct);
        if (result.IsBad())
            return result.Failure<GetProjectCostCenterRequestsResponse>()!;

        await EnrichModels(result.Value.Data, ct);

        return new GetProjectCostCenterRequestsResponse(
            result.Value.Data,
            result.Value.RowCount);
    }

    public async Task<Result<ProjectCostCenterRequestModel?>> GetProjectCostCenterRequestById(
        GetProjectCostCenterRequestByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectCostCenterRequestById");

        var isValid = await request.IsValidAsync<GetProjectCostCenterRequestByIdValidator,
            GetProjectCostCenterRequestByIdRequest>(ct);
        if (isValid.IsFailure)
            return Result.Failure<ProjectCostCenterRequestModel>(isValid.Error!);

        var result = await GetProjectCostCenterRequestByIdHandler(request, ct);
        if (result.IsBad())
            return result.Failure<ProjectCostCenterRequestModel>()!;

        await EnrichModels(
            new List<ProjectCostCenterRequestModel> { result.Value! },
            ct);

        return result.Value;
    }

    public async Task<Result<UpdateProjectCostCenterRequestResponse?>> UpdateProjectCostCenterRequest(
        UpdateProjectCostCenterRequestRequest request, CT ct)
    {
        _logger.LogInformation("UpdateProjectCostCenterRequest");

        var isValid = await request.IsValidAsync<UpdateProjectCostCenterRequestValidator,
            UpdateProjectCostCenterRequestRequest>(ct);
        if (isValid.IsFailure) return Result.Failure<UpdateProjectCostCenterRequestResponse>(isValid.Error!);

        var result = await UpdateProjectCostCenterRequestCommand(request, ct);
        if (result.IsBad()) return result.Failure<UpdateProjectCostCenterRequestResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectCostCenterRequestResponse(true);
    }

    public async Task<Result<DeleteProjectCostCenterRequestResponse?>> DeleteProjectCostCenterRequest(
        DeleteProjectCostCenterRequestRequest request, CT ct)
    {
        _logger.LogInformation("DeleteProjectCostCenterRequest");

        var isValid = await request.IsValidAsync<DeleteProjectCostCenterRequestValidator,
            DeleteProjectCostCenterRequestRequest>(ct);
        if (isValid.IsFailure) return Result.Failure<DeleteProjectCostCenterRequestResponse>(isValid.Error!);

        var result = await DeleteProjectCostCenterRequestCommand(request, ct);
        if (result.IsBad()) return result.Failure<DeleteProjectCostCenterRequestResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectCostCenterRequestResponse(true);
    }

    public async Task<Result<RejectProjectCostCenterRequestResponse?>> RejectProjectCostCenterRequest(
        RejectProjectCostCenterRequestRequest request, CT ct)
    {
        _logger.LogInformation("RejectProjectCostCenterRequest");

        var isValid = await request.IsValidAsync<RejectProjectCostCenterRequestValidator,
            RejectProjectCostCenterRequestRequest>(ct);
        if (isValid.IsFailure) return Result.Failure<RejectProjectCostCenterRequestResponse>(isValid.Error!);

        var result = await RejectProjectCostCenterRequestCommand(request, ct);
        if (result.IsBad()) return result.Failure<RejectProjectCostCenterRequestResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new RejectProjectCostCenterRequestResponse(true);
    }
    public async Task<Result<GetsProjectStatusResponse?>> GetProjectCostCenterRequestStatuses(CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectCostCenterRequestStatus>());
        return new GetsProjectStatusResponse(result);
    }
}