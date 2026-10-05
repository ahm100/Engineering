using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByIds;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;
using Engineering.Application.Services.ProjectServices.Commands.ActiveProjectService;
using Engineering.Application.Services.ProjectServices.Commands.CreateProjectService;
using Engineering.Application.Services.ProjectServices.Commands.DeleteProjectServiceDetail;
using Engineering.Application.Services.ProjectServices.Commands.DisableProjectService;
using Engineering.Application.Services.ProjectServices.Commands.InactiveProjectService;
using Engineering.Application.Services.ProjectServices.Commands.StateChangerProjectServices;
using Engineering.Application.Services.ProjectServices.Commands.UpdateProjectService;
using Engineering.Application.Services.ProjectServices.Models.ActiveProjectService;
using Engineering.Application.Services.ProjectServices.Models.CreateProjectService;
using Engineering.Application.Services.ProjectServices.Models.DeleteProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetActiveProjectServices;
using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;
using Engineering.Application.Services.ProjectServices.Models.GetsContractorProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;
using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;
using Engineering.Application.Services.ProjectServices.Models.InactiveProjectService;
using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;
using Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;
using Engineering.Application.Services.ProjectServices.Models.UpdateProjectService;
using Engineering.Application.Services.ProjectServices.Queries.GetActiveProjectServices;
using Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceById;
using Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceByIdNoInclude;
using Engineering.Application.Services.ProjectServices.Queries.GetsContractorProjectService;
using Engineering.Application.Services.ProjectServices.Queries.GetsProjectService;
using Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceByIds;
using Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceDetail;
using Engineering.Application.Services.ProjectServices.Queries.GetsServiceInfoByProjectId;
using Engineering.Application.Services.ProjectServices.Queries.IsDuplicateProjectService;
using Engineering.Application.Services.ServiceInfos.Queries.GetServiceById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.ProjectServices;

public class ProjectServiceLogic : IProjectServiceLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectServiceLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectServiceLogic(IMediator mediator, ILogger<ProjectServiceLogic> logger, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectServiceResponse?>> CreateProjectService(CreateProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectService");

        var isValidRequest = await request.IsValidAsync<CreateProjectServiceValidator, CreateProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectServiceResponse>(isValidRequest.Error!);

        var getProject = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);
        if (getProject.IsFailure || getProject.Value is null)
            return Result.Failure<CreateProjectServiceResponse>(getProject.Error!);
        var project = getProject.Value;
        if (!project.CollectiveService)
            return Result.Failure<CreateProjectServiceResponse>(ProjectServiceErrors.ProjectNotCollectiveService);

        var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 10, [request.ContractorId], null, false, null), ct);
        if (contractorQuery.IsFailure || contractorQuery.Value is null || contractorQuery.Value.Data is null || !contractorQuery.Value.Data.Any())
            return Result.Failure<CreateProjectServiceResponse>(ProjectServiceErrors.InvalidContractor!);

        var getByIdResponse = await _mediator.Send(new GetServiceInfoByIdQuery(request.ServiceInfoId), ct);
        if (getByIdResponse.IsFailure || getByIdResponse.Value is null)
            return Result.Failure<CreateProjectServiceResponse>(getByIdResponse.Error!);
        var serviceInfo = getByIdResponse.Value;

        var isDuplicate = await _mediator.Send(new IsDuplicateProjectServiceQuery(null, request.ProjectId, request.ServiceInfoId, request.ContractorId), ct);
        if (isDuplicate is { IsSuccess: true, Value: true })
            return Result.Failure<CreateProjectServiceResponse>(ProjectServiceErrors.ProjectServiceIsDuplicate);

        List<OperationInfoService>? operationInfoServices = null;
        if (request.OperationInfoServices is not null && request.OperationInfoServices.Count > 0)
        {
            var operationInfoServiceIds = request.OperationInfoServices.Select(x => x.OperationInfoServiceId).Distinct().ToList();
            var getsOperationInfoService = await _mediator.Send(new GetsOperationInfoServiceByIdsQuery(operationInfoServiceIds, 1, operationInfoServiceIds.Count), ct);
            if (getsOperationInfoService.IsFailure || getsOperationInfoService.Value is null || getsOperationInfoService.Value.Data is null)
                return Result.Failure<CreateProjectServiceResponse>(getsOperationInfoService.Error!);
            if (operationInfoServiceIds.Count != getsOperationInfoService.Value.Data.Count)
                return Result.Failure<CreateProjectServiceResponse>(ProjectServiceErrors.OperationInfoServicesNotValid);
            operationInfoServices = getsOperationInfoService.Value.Data;
        }

        var response = await _mediator.Send(new CreateProjectServiceCommand(
            project,
            serviceInfo,
            request.ContractorId,
            request.Volume,
            request.DoneVolume,
            request.IsActive,
            operationInfoServices), ct);
        if (response.IsFailure)
            return Result.Failure<CreateProjectServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectServiceResponse(true);
    }

    public async Task<Result<UpdateProjectServiceResponse?>> UpdateProjectService(UpdateProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectService");

        var isValidRequest = await request.IsValidAsync<UpdateProjectServiceValidator, UpdateProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectServiceResponse>(isValidRequest.Error!);

        var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 10, [request.ContractorId], null, false, null), ct);
        if (contractorQuery.IsFailure || contractorQuery.Value is null || contractorQuery.Value.Data is null || !contractorQuery.Value.Data.Any())
            return Result.Failure<UpdateProjectServiceResponse>(contractorQuery.Error!);

        var getByIdResponse = await _mediator.Send(new GetServiceInfoByIdQuery(request.ServiceInfoId), ct);
        if (getByIdResponse.IsFailure || getByIdResponse.Value is null)
            return Result.Failure<UpdateProjectServiceResponse>(getByIdResponse.Error!);
        var serviceInfo = getByIdResponse.Value;

        var getResponse = await _mediator.Send(new GetProjectServiceByIdQuery(request.Id), ct);
        if (getResponse.IsFailure || getResponse.Value is null)
            return Result.Failure<UpdateProjectServiceResponse>(getResponse.Error!);
        var value = getResponse.Value;

        if (value.ServiceInfo.Id != request.ServiceInfoId)
        {
            var isDuplicate = await _mediator.Send(new IsDuplicateProjectServiceQuery(null, value.Project.Id, request.ServiceInfoId, request.ContractorId), ct);
            if (isDuplicate is { IsSuccess: true, Value: true })
                return Result.Failure<UpdateProjectServiceResponse>(ProjectServiceErrors.ProjectServiceIsDuplicate);
        }

        List<OperationInfoService>? newOperationInfoServices = null;
        if (request.NewOperationInfoServices is not null && request.NewOperationInfoServices.Count > 0)
        {
            var operationInfoServiceIds = request.NewOperationInfoServices.Select(x => x.OperationInfoServiceId).Distinct().ToList();
            var getsOperationInfoService = await _mediator.Send(new GetsOperationInfoServiceByIdsQuery(operationInfoServiceIds, 1, operationInfoServiceIds.Count), ct);
            if (getsOperationInfoService.IsFailure || getsOperationInfoService.Value is null || getsOperationInfoService.Value.Data is null)
                return Result.Failure<UpdateProjectServiceResponse>(getsOperationInfoService.Error!);
            if (operationInfoServiceIds.Count != getsOperationInfoService.Value.Data.Count)
                return Result.Failure<UpdateProjectServiceResponse>(ProjectServiceErrors.OperationInfoServicesNotValid);
            newOperationInfoServices = getsOperationInfoService.Value.Data;
        }

        if (request.DeleteOperationInfoServices is not null && request.DeleteOperationInfoServices.Count > 0)
            foreach (var item in request.DeleteOperationInfoServices)
            {
                var getsOperationInfoService = await _mediator.Send(new DeleteProjectServiceDetailCommand(item.ProjectServiceDetailId), ct);
                if (getsOperationInfoService.IsFailure)
                    return Result.Failure<UpdateProjectServiceResponse>(getsOperationInfoService.Error!);
            }

        var response = await _mediator.Send(new UpdateProjectServiceCommand(
            value,
            serviceInfo,
            request.ContractorId,
            request.Volume,
            request.DoneVolume,
            request.IsActive,
            newOperationInfoServices), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectServiceResponse(true);
    }

    public async Task<Result<StateChangerProjectServicesResponse?>> StateChangerProjectServices(StateChangerProjectServicesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerProjectServices, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerProjectServicesValidator, StateChangerProjectServicesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerProjectServicesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerProjectServicesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsProjectServiceByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerProjectServicesResponse>(responses.Error!);
        var values = responses.Value;

        var response = await _mediator.Send(new StateChangerProjectServicesCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerProjectServicesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerProjectServicesResponse(true);
    }

    public async Task<Result<InactiveProjectServiceResponse?>> InactiveProjectService(InactiveProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveProjectService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveProjectServiceValidator, InactiveProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveProjectServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveProjectServiceCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveProjectServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveProjectServiceResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveProjectServiceResponse?>> ActiveProjectService(ActiveProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveProjectService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveProjectServiceValidator, ActiveProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveProjectServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveProjectServiceCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveProjectServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveProjectServiceResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<DeleteProjectServiceResponse?>> DeleteProjectService(DeleteProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteProjectServiceValidator, DeleteProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteProjectServiceCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteProjectServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectServiceResponse(true);
    }

    public async Task<Result<GetProjectServiceByIdResponse?>> GetProjectServiceById(GetProjectServiceByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectServiceById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectServiceByIdValidator, GetProjectServiceByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectServiceByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectServiceByIdNoIncludeQuery(request.Id), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetProjectServiceByIdResponse>(response.Error!);
        var value = response.Value;

        if (value.ServiceInfoMeasurId > 0)
        {
            List<long>? measureUnitIds = new();
            if (value.ProjectServiceDetails is not null && value.ProjectServiceDetails.Count > 0)
                measureUnitIds.AddRange(value.ProjectServiceDetails!.SelectMany(x => new[] { x.ServiceInfoMeasurId, x.OperationInfoMeasurId }).Distinct().ToList());
            measureUnitIds.Add(value.ServiceInfoMeasurId!.Value);

            var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);
            value.ServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == value.ServiceInfoMeasurId)?.Name;
            if (value.ProjectServiceDetails is not null && value.ProjectServiceDetails.Count > 0)
            {
                value.ProjectServiceDetails?.ForEach(item =>
                {
                    item.ServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.ServiceInfoMeasurId)?.Name;
                    item.OperationInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurId)?.Name;
                });
            }
        }

        UserModel? contractor = null;
        if (value.ContractorId > 0)
        {
            var user = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([value.ContractorId], null, null, _mediator, ct);
            contractor = user?.FirstOrDefault();
            value.ContractorName = contractor?.FullName;
            value.ContractorNickName = contractor?.Nickname;
        }

        return value;
    }

    public async Task<Result<GetsProjectServiceResponse?>> GetsProjectService(GetsProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectService, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectServiceValidator, GetsProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectServiceQuery(
            request.Ids,
            request.CostcenterIds,
            request.ProjectIds,
            request.ServiceInfoIds,
            request.ContractorIds,
            request.IsActive,
            request.ServiceFilterData,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsProjectServiceResponse>(response.Error!);
        var values = response.Value.Data;

        var measurIds = values?.Where(x => x.ServiceInfoMeasurId != null && x.ServiceInfoMeasurId > 0).Select(x => (long)x.ServiceInfoMeasurId!).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measurIds, _mediator, ct);

        var contractorIds = values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        values?.ForEach(item =>
        {
            item.ServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.ServiceInfoMeasurId)?.Name;
            item.ContractorName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.Nickname;
        });

        return new GetsProjectServiceResponse(values ?? new List<GetsProjectServiceModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectServiceDetailResponse?>> GetsProjectServiceDetail(GetsProjectServiceDetailRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectServiceDetail, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectServiceDetailValidator, GetsProjectServiceDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectServiceDetailResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectServiceDetailQuery(
            request.Ids,
            request.CostcenterIds,
            request.ProjectIds,
            request.ServiceInfoIds,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.IsActive,
            request.ServiceFilterData,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsProjectServiceDetailResponse>(response.Error!);
        var values = response.Value.Data;

        var measureUnitIds = values!.SelectMany(x => new[] { x.OperationInfoMeasurId, x.ServiceInfoMeasurId, x.ProjectServiceInfoMeasurId }).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        var contractorIds = values?.Where(x => x.ContractorId.HasValue && x.ContractorId > 0).Select(x => (long)x.ContractorId!.Value).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        values?.ForEach(item =>
        {
            item.ServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.ServiceInfoMeasurId)?.Name;
            item.ProjectServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.ProjectServiceInfoMeasurId)?.Name;
            item.OperationInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurId)?.Name;
            item.ContractorName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.Nickname;
        });

        return new GetsProjectServiceDetailResponse(values ?? new List<GetsProjectServiceDetailModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetActiveProjectServicesResponse?>> GetActiveProjectServices(GetActiveProjectServicesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveProjectServices, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveProjectServicesValidator, GetActiveProjectServicesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveProjectServicesResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetActiveProjectServicesQuery(
            request.Ids,
            request.CostcenterIds,
            request.ProjectIds,
            request.ServiceInfoIds,
            request.ContractorIds,
            request.ServiceFilterData,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetActiveProjectServicesResponse>(response.Error!);
        var values = response.Value.Data;

        var measurIds = values?.Where(x => x.ServiceInfoMeasurId != null && x.ServiceInfoMeasurId > 0).Select(x => (long)x.ServiceInfoMeasurId!).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measurIds, _mediator, ct);

        var contractorIds = values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        values?.ForEach(item =>
        {
            item.ServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.ServiceInfoMeasurId)?.Name;
            item.ContractorName = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = contractors?.FirstOrDefault(x => x is not null && x?.Id == item.ContractorId)?.Nickname;
        });

        return new GetActiveProjectServicesResponse(values ?? new List<GetsActiveProjectServiceModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsServiceInfoByProjectIdResponse?>> GetsServiceInfoByProjectId(GetsServiceInfoByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectService, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsServiceInfoByProjectIdValidator, GetsServiceInfoByProjectIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsServiceInfoByProjectIdResponse>(isValidRequest.Error!);

        var projectQuery = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);
        if (projectQuery.IsFailure || projectQuery.Value is null)
            return Result.Failure<GetsServiceInfoByProjectIdResponse>(projectQuery.Error!);
        var project = projectQuery.Value;

        var response = await _mediator.Send(new GetsServiceInfoByProjectIdQuery(
            request.ProjectId,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsServiceInfoByProjectIdResponse>(response.Error!);
        var values = response.Value.Data;

        var measurIds = values?.Where(x => x.ServiceInfoMeasurId != null && x.ServiceInfoMeasurId > 0).Select(x => (long)x.ServiceInfoMeasurId!).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measurIds, _mediator, ct);

        values?.ForEach(item =>
        {
            item.ServiceInfoMeasur = measurments?.FirstOrDefault(x => x.Id == item.ServiceInfoMeasurId)?.Name;
        });

        return new GetsServiceInfoByProjectIdResponse()
        {
            Data = values ?? new List<GetsServiceInfoByProjectIdModel>(0),
            RowCount = response.Value?.RowCount ?? 0
        };
    }


    public async Task<Result<GetsContractorProjectServiceResponse?>> GetsContractorProjectService(GetsContractorProjectServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractorProjectService, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsContractorProjectServiceValidator, GetsContractorProjectServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorProjectServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsContractorProjectServiceQuery(request.ProjectId, request.ServiceInfoId, 0, 0), ct);
        if (response.IsFailure || response.Value?.Data is null)
            return Result.Failure<GetsContractorProjectServiceResponse>(response.Error!);
        if (response.Value?.Data.Count <= 0)
            return Result.Failure<GetsContractorProjectServiceResponse>(ProjectOperationDetailErrors.ContractorsNotFound);

        var allIds = response.Value!.Data.ToList();
        var userInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, request.FilterData, null, _mediator, ct); // بره سراغ متا دیتا
        if (userInfos is null)
            return Result.Failure<GetsContractorProjectServiceResponse>(ProjectOperationDetailErrors.ContractorsNotFound);

        var data = new List<GetsContractorProjectServiceModel>();
        if (!string.IsNullOrEmpty(request.FilterData))
            foreach (var item in allIds)
            {
                if (!userInfos!.Any(x => x?.Id == item))
                    continue;

                var user = userInfos?.Where(x => x?.Id == item)?.FirstOrDefault();
                data.Add(new GetsContractorProjectServiceModel()
                {
                    ContractorId = (long)item!,
                    ContractorName = user?.FullName,
                    ContractorNickName = user?.Nickname,
                });
            }
        else
            foreach (var item in allIds)
            {
                var user = userInfos?.Where(x => x?.Id == item)?.FirstOrDefault();
                data.Add(new GetsContractorProjectServiceModel()
                {
                    ContractorId = (long)item!,
                    ContractorName = user?.FullName,
                    ContractorNickName = user?.Nickname,
                });
            }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsContractorProjectServiceResponse(responseData ?? new List<GetsContractorProjectServiceModel>(0), userInfos?.Count ?? 0);
    }

}