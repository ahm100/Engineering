using Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.CreateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.DeleteProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.UpdateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.CreateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.DeleteProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetDeductionAmountByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetProjectOperationDetailDeductionById;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetsDeductionByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.ProjectOperationDetailDeductionGroupDelete;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.UpdateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetDeductionAmountByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetProjectOperationDetailDeductionById;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetsDeductionByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailById;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions;

public class ProjectOperationDetailDeductionLogic : IProjectOperationDetailDeductionLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationDetailDeductionLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectOperationDetailDeductionLogic(IMediator mediator, ILogger<ProjectOperationDetailDeductionLogic> logger, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectOperationDetailDeductionResponse?>> CreateProjectOperationDetailDeduction(CreateProjectOperationDetailDeductionRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectOperationDetailDeduction");

        var isValidRequest = await request.IsValidAsync<CreateProjectOperationDetailDeductionValidator, CreateProjectOperationDetailDeductionRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectOperationDetailDeductionResponse>(isValidRequest.Error!);

        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure || projectOperationDetail.Value is null)
            return Result.Failure<CreateProjectOperationDetailDeductionResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectDetail = projectOperationDetail.Value;

        var finalAmounts = request.Height * request.Weight * request.Width * request.Number * request.Length;
        var detailDeductions = projectDetail.ProjectOperationDetailDeductions.Sum(x => x.FinalAmount);
        if ((finalAmounts + detailDeductions) >= projectDetail.FinalAmount)
            return Result.Failure<CreateProjectOperationDetailDeductionResponse>(ProjectOperationDetailErrors.DeductionsUnValid);

        var response = await _mediator.Send(new CreateProjectOperationDetailDeductionCommand(projectDetail, request.Length, request.Width, request.Height, request.Weight, request.Number), ct);
        if (response.IsFailure)
            return Result.Failure<CreateProjectOperationDetailDeductionResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationDetailDeductionResponse(response.Value!.Id);
    }

    public async Task<Result<UpdateProjectOperationDetailDeductionResponse?>> UpdateProjectOperationDetailDeduction(UpdateProjectOperationDetailDeductionRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDetailDeduction");

        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailDeductionValidator, UpdateProjectOperationDetailDeductionRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailDeductionResponse>(isValidRequest.Error!);


        var updateData = await _mediator.Send(new UpdateProjectOperationDetailDeductionCommand(request.Id, request.Length, request.Width, request.Height, request.Weight, request.Number), ct);
        if (updateData.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailDeductionResponse>(updateData.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailDeductionResponse(true);
    }

    public async Task<Result<DeleteProjectOperationDetailDeductionResponse?>> DeleteProjectOperationDetailDeduction(DeleteProjectOperationDetailDeductionRequest request, CT ct)
    {
        _logger.LogInformation("Delete ProjectOperationDetailDeduction, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteProjectOperationDetailDeductionValidator, DeleteProjectOperationDetailDeductionRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailDeductionResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteProjectOperationDetailDeductionCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailDeductionResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationDetailDeductionResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<ProjectOperationDetailDeductionGroupDeleteResponse?>> ProjectOperationDetailDeductionGroupDelete(ProjectOperationDetailDeductionGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationDetailDeductionGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectOperationDetailDeductionGroupDeleteValidator, ProjectOperationDetailDeductionGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationDetailDeductionGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteProjectOperationDetailDeductionCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ProjectOperationDetailDeductionGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationDetailDeductionGroupDeleteResponse(true);
    }


    public async Task<Result<GetProjectOperationDetailDeductionByIdResponse?>> GetProjectOperationDetailDeductionById(GetProjectOperationDetailDeductionByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationDetailDeductionById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailDeductionByIdValidator, GetProjectOperationDetailDeductionByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailDeductionByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectOperationDetailDeductionByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectOperationDetailDeductionByIdResponse>(response.Error!);
        var value = response.Value!;

        var data = value.Adapt<GetProjectOperationDetailDeductionByIdResponse>();
        return data;
    }

    public async Task<Result<GetsDeductionByProjectOperationDetailIdResponse?>> GetsDeductionByProjectOperationDetailId(GetsDeductionByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsDeductionByProjectOperationDetailId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsDeductionByProjectOperationDetailIdValidator, GetsDeductionByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDeductionByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsDeductionByProjectOperationDetailIdQuery(request.ProjectOperationDetailId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsDeductionByProjectOperationDetailIdResponse>(response.Error!);
        var value = response.Value.Data;

        var data = value.Adapt<List<GetsDeductionByProjectOperationDetailIdModel>>();
        return new GetsDeductionByProjectOperationDetailIdResponse(data ?? new List<GetsDeductionByProjectOperationDetailIdModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetDeductionAmountByProjectOperationDetailIdResponse?>> GetDeductionAmountByProjectOperationDetailId(GetDeductionAmountByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetDeductionAmountByProjectOperationDetailId");

        var isValidRequest = await request.IsValidAsync<GetDeductionAmountByProjectOperationDetailIdValidator, GetDeductionAmountByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDeductionAmountByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetDeductionAmountByProjectOperationDetailIdQuery(request.ProjectOperationDetailId), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetDeductionAmountByProjectOperationDetailIdResponse>(response.Error!);
        var value = response.Value;

        var total = value.Sum(x => x);
        return new GetDeductionAmountByProjectOperationDetailIdResponse(request.ProjectOperationDetailId, total);
    }

}