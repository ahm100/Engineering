using Engineering.Application.Services.OperationInfoDependencies.Commands.UpdateOperationInfoDependency;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencies;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencyById;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetsOperationInfoDependencyType;
using Engineering.Application.Services.OperationInfoDependencies.Models.OperationInfoDependencyModels;
using Engineering.Application.Services.OperationInfoDependencies.Models.UpdateOperationInfoDependency;
using Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencies;
using Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencyById;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;
using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfoDependencies;

public class OperationInfoDependencyLogic : IOperationInfoDependencyLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoDependencyLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OperationInfoDependencyLogic(
        IMediator mediator,
        ILogger<OperationInfoDependencyLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateOperationInfoDependencyResponse?>> UpdateOperationInfoDependency(
        UpdateOperationInfoDependencyRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateOperationInfoDependency, Id:{Id}, RelationId:{RelationId}, WorkingDays:{WorkingDays}",
           request.Id, request.RelationId, request.WorkingDays);

        var isValidRequest = await request.IsValidAsync<UpdateOperationInfoDependencyValidator, UpdateOperationInfoDependencyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(isValidRequest.Error!);

        var getDependency = await _mediator.Send(new GetOperationInfoDependencyByIdQuery(request.Id), ct);
        if (getDependency.IsFailure)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoDependencyErrors.DependencyWithIdNotFound);

        var getOperationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.RelationId), ct);
        if (getOperationInfo.IsFailure)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        if (getOperationInfo.Value is null)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        if (getOperationInfo.Value.Priority == null)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.PriorityIsNull);

        var getOperationInfoDependency = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(getDependency.Value!.RelationId), ct);
        if (getOperationInfoDependency.IsFailure)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);
        if (getOperationInfoDependency.Value!.Priority == null)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.PriorityIsNull);
        if (getOperationInfoDependency.Value.Priority == 1)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.PriorityIsOne);

        if (getOperationInfo.Value!.Priority == getOperationInfoDependency.Value.Priority)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.CanNotRealatedtwoSamePriority);
        if (getOperationInfo.Value!.Priority > getOperationInfoDependency.Value.Priority)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.CanNotRealatedforDependencyPriority);
        if (request.DependencyType == OperationInfoDependencyType.StartDate && request.WorkingDays < 0)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(OperationInfoErrors.WorkingDayIsLow);

        var response = await _mediator.Send(new UpdateOperationInfoDependencyCommand(request.Id, getOperationInfo.Value, request.WorkingDays, request.DependencyType), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateOperationInfoDependencyResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateOperationInfoDependencyResponse(response.Value!.Id);
    }

    public async Task<Result<GetOperationInfoDependencyByIdResponse?>> GetOperationInfoDependencyById(
        GetOperationInfoDependencyByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoDependencyById, id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<GetOperationInfoDependencyByIdValidator, GetOperationInfoDependencyByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoDependencyByIdResponse>(isValidRequest.Error!);

        var getDependencyResponse = await _mediator.Send(new GetOperationInfoDependencyByIdQuery(request.Id), ct);
        if (getDependencyResponse.IsFailure)
            return Result.Failure<GetOperationInfoDependencyByIdResponse>(getDependencyResponse.Error!);
        var getDependency = getDependencyResponse.Value!;

        var getOperationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(getDependency.RelationId), ct);
        if (getOperationInfo.IsFailure)
            return Result.Failure<GetOperationInfoDependencyByIdResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

        var typeTitle = getDependency.DependencyType == Domain.Entities.OperationInfos.Enums.OperationInfoDependencyType.StartDate ? "تاریخ شروع" : "تاریخ پایان";
        var response = new GetOperationInfoDependencyByIdResponse(getDependency.Id, getDependency.RelationId, getOperationInfo.Value!.OperationInfoCode,
            getOperationInfo.Value.OperationInfoName, getDependency.WorkingDays, typeTitle);
        return response;
    }

    public async Task<Result<GetOperationInfoDependenciesResponse?>> GetOperationInfoDependencies(
        GetOperationInfoDependenciesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoDependencies, OperationInfoId:{OperationInfoId}, pageSize:{PageSize}, pageSize:{PageSize}",
            request.OperationInfoId, request.PageIndex, request.PageSize);

        var isValidRequest =
            await request.IsValidAsync<GetOperationInfoDependenciesValidator, GetOperationInfoDependenciesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoDependenciesResponse>(isValidRequest.Error!);

        var getOperationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoId), ct);
        if (getOperationInfo.IsFailure)
            return Result.Failure<GetOperationInfoDependenciesResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

        var getOperationInfoDependencies = await _mediator.Send(
            new GetOperationInfoDependenciesQuery(request.OperationInfoId, request.DependencyType, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getOperationInfoDependencies.IsFailure)
            return Result.Failure<GetOperationInfoDependenciesResponse>(getOperationInfoDependencies.Error!);

        var data = new OperationInfoDependenciesModel(getOperationInfo.Value!.Id, getOperationInfo.Value.OperationInfoCode,
            getOperationInfo.Value.OperationInfoName, new List<DependenciesModel>());
        if (getOperationInfoDependencies.Value!.Data is not null)
        {
            foreach (var dependencyData in getOperationInfoDependencies.Value?.Data!)
            {
                var operationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(dependencyData.RelationId), ct);
                if (operationInfo.IsFailure)
                    return Result.Failure<GetOperationInfoDependenciesResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

                var typeTitle = dependencyData.DependencyType == Domain.Entities.OperationInfos.Enums.OperationInfoDependencyType.StartDate ? "تاریخ شروع" : "تاریخ پایان";
                var dataResult = new DependenciesModel(dependencyData.Id, dependencyData.RelationId, operationInfo.Value!.OperationInfoCode,
                    operationInfo.Value.OperationInfoName, dependencyData.WorkingDays, typeTitle);
                data.DependenciesModel?.Add(dataResult);
            }
        }

        var response = new GetOperationInfoDependenciesResponse(data, getOperationInfoDependencies.Value?.RowCount ?? 0);
        return response;
    }

    public async Task<Result<GetsOperationInfoDependencyTypeResponse?>> GetsOperationInfoDependencyType(
        GetsOperationInfoDependencyTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoDependencyType");

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoDependencyTypeValidator, GetsOperationInfoDependencyTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoDependencyTypeResponse>(isValidRequest.Error!);

        var data = new List<GetsOperationInfoDependencyTypesModel>
        {
            new ((int)OperationInfoDependencyType.StartDate, OperationInfoDependencyType.StartDate.GetEnumDescription()),
            new ((int)OperationInfoDependencyType.EndDate, OperationInfoDependencyType.EndDate.GetEnumDescription())
        };

        return new GetsOperationInfoDependencyTypeResponse(data);
    }

}