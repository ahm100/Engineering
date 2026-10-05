using Engineering.Application.Services.OperationInfos.Commands.CreateOperationInfo;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsForServiceInfo;
using Engineering.Application.Services.OperationInfoServices.Commands.CreateOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Commands.DeleteOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.DeleteOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceFiltered;
using Engineering.Application.Services.OperationInfoServices.Models.OperationInfoServiceModels;
using Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByProjectId;
using Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceFiltered;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices;

public class OperationInfoServiceLogic : IOperationInfoServiceLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoServiceLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public OperationInfoServiceLogic(
        IMediator mediator,
        ILogger<OperationInfoServiceLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateOperationInfoServiceResponse?>> CreateOperationInfoService(
        CreateOperationInfoServiceModelRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationInfoService, OperationInfoId:{OperationInfoId},", request.OperationInfoIds);

        List<OperationInfo>? operationInfos = null;
        if (request.OperationInfos is not null && request.OperationInfos.Any())
            operationInfos = request.OperationInfos;
        else if (request.OperationInfoIds is not null && request.OperationInfoIds.Any())
        {
            var operationInfoResponse = await _mediator.Send(new GetsOperationInfoByIdsForServiceInfoQuery(request.OperationInfoIds, 0, 0), ct);
            if (operationInfoResponse.IsFailure)
                return Result.Failure<CreateOperationInfoServiceResponse>(operationInfoResponse.Error!);
            if (operationInfoResponse.Value is null)
                return Result.Failure<CreateOperationInfoServiceResponse>(operationInfoResponse.Error!);
            operationInfos = operationInfoResponse.Value.Data;
        }
        else
            return Result.Failure<CreateOperationInfoServiceResponse>(OperationInfoErrors.IdIsEmpty!);

        if (operationInfos != null && operationInfos.Any())
            foreach (var operationInfo in operationInfos)
            {
                if (request.ServiceInfos is not null && request.ServiceInfos.Count > 0)
                {
                    if (request.ServiceInfos.Any(x => x.IsDeleted))
                    {
                        var deletedItems = request.ServiceInfos.Where(x => x.IsDeleted).ToList();
                        foreach (var item in deletedItems)
                        {
                            if (item.IsDeleted)
                            {
                                var deleteService = await _mediator.Send(new DeleteOperationInfoServiceCommand(operationInfo.Id, item.Id), ct); // بره سراغ متا دیتا
                                if (deleteService.IsFailure)
                                    return Result.Failure<CreateOperationInfoServiceResponse>(deleteService.Error!);
                            }
                        }
                    }
                    if (request.ServiceInfos.Any(x => !x.IsDeleted))
                    {
                        var services = request.ServiceInfos.Where(x => !x.IsDeleted).Select(x => x.Id).ToList();
                        var serviceIds = new List<long>();
                        foreach (var item in services)
                        {
                            if (operationInfo.OperationInfoServices.Any(x => x.ServiceInfo.Id == item))
                                continue;
                            else if (serviceIds.Any(x => x == item))
                                continue;
                            else
                                serviceIds.Add(item);
                        }
                        if (serviceIds.Count <= 0)
                            continue;

                        var serviceInfosData = await _mediator.Send(new GetsServiceInfoByIdsQuery(1, serviceIds!.Count, serviceIds), ct); // بره سراغ متا دیتا
                        if (serviceInfosData.IsFailure || serviceInfosData.Value is null || serviceInfosData.Value.Data is null)
                            return Result.Failure<CreateOperationInfoServiceResponse>(ProjectOperationDetailErrors.UnValidServiceInfos);
                        var serviceInfos = serviceInfosData.Value.Data;
                        var serviceForCreate = new List<OperationInfoServiceModel>();
                        foreach (var item in serviceIds)
                        {
                            var service = serviceInfos.FirstOrDefault(x => x.Id == item)!;
                            var timeSpant = request.ServiceInfos.FirstOrDefault(x => x.Id == item)!.TimeSpant;

                            if (!string.IsNullOrEmpty(timeSpant))
                            {
                                if (!(timeSpant.Split(':')[0].Count() >= 2 && timeSpant.Split(':')[1].Count() == 2))
                                    return Result.Failure<CreateOperationInfoServiceResponse>(MachineryStandardErrors.TimeSpantCountError);

                                if (int.Parse(timeSpant.Split(':')[1]) > 59)
                                    return Result.Failure<CreateOperationInfoServiceResponse>(MachineryStandardErrors.MoreThan59Min);
                            }

                            serviceForCreate.Add(new(service, TimeCalculator.StringToTicks(timeSpant)));
                        }

                        var response = await _mediator.Send(new CreateOperationInfoServiceCommand(operationInfo, serviceForCreate), ct);
                        if (response.IsFailure)
                            return Result.Failure<CreateOperationInfoServiceResponse>(response.Error!);
                    }
                }
                else
                    return Result.Failure<CreateOperationInfoServiceResponse>(OperationInfoServiceErrors.OneIdMustSelect);

            }

        await _unitOfWork.CommitAsync(ct);
        return new CreateOperationInfoServiceResponse(true);
    }

    public async Task<Result<DeleteOperationInfoServiceResponse?>> DeleteOperationInfoService(
        DeleteOperationInfoServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteOperationInfoService, Id:{Id}", request.OperationInfoId);

        var isValidRequest = await request.IsValidAsync<DeleteOperationInfoServiceValidator, DeleteOperationInfoServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteOperationInfoServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteOperationInfoServiceCommand(request.OperationInfoId, request.ServiceInfoId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteOperationInfoServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteOperationInfoServiceResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetsByOperationInfoIdResponse?>> GetsByOperationInfoId(
        GetsByOperationInfoIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByOperationInfoIdValidator, GetsByOperationInfoIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByOperationInfoIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsByOperationInfoIdQuery(request.OprationInfoId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByOperationInfoIdResponse>(response.Error!);

        var allIds = response.Value?.Data!.Select(x => x.ServiceInfo.UnitOfMeasurementId).Distinct().ToList(); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct); // بره سراغ متا دیتا
        var data = new List<GetsByOperationInfoIdModel>();
        foreach (var item in response.Value?.Data!)
            data.Add(new GetsByOperationInfoIdModel(item.ServiceInfo.Id, item.ServiceInfo.ServiceInfoName, item.ServiceInfo.ServiceInfoCode,
                TimeCalculator.TicksToStringHM(item.TimeSpant), measureUnitsData?.Where(x => x.Id == item.ServiceInfo.UnitOfMeasurementId).FirstOrDefault()?.Name));

        return new GetsByOperationInfoIdResponse(data ?? new List<GetsByOperationInfoIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationInfoServiceFilteredResponse?>> GetsOperationInfoServiceFiltered(
        GetsOperationInfoServiceFilteredRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoServiceFiltered, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoServiceFilteredValidator, GetsOperationInfoServiceFilteredRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoServiceFilteredResponse>(isValidRequest.Error!);

        if (request.CategoryId is null && request.CategoryId == default && request.BranchId is null && request.BranchId == default && request.SeasonId is null && request.SeasonId == default)
            return Result.Failure<GetsOperationInfoServiceFilteredResponse>(OperationInfoServiceErrors.OneIdMustSelect);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsOperationInfoServiceFilteredQuery(request.CategoryId, request.BranchId, request.SeasonId, request.FilterData, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoServiceFilteredResponse>(response.Error!);

        var allIds = response.Value?.Data!.Select(x => x.ServiceInfo.UnitOfMeasurementId).ToList(); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct); // بره سراغ متا دیتا
        var data = new List<GetsByOperationInfoIdModel>();
        foreach (var item in response.Value?.Data!)
            data.Add(new GetsByOperationInfoIdModel(item.ServiceInfo.Id, item.ServiceInfo.ServiceInfoName, item.ServiceInfo.ServiceInfoCode,
                TimeCalculator.TicksToStringHM(item.TimeSpant), measureUnitsData?.Where(x => x.Id == item.ServiceInfo.UnitOfMeasurementId).FirstOrDefault()?.Name));

        return new GetsOperationInfoServiceFilteredResponse(data ?? new List<GetsByOperationInfoIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationInfoServiceByProjectIdResponse?>> GetsOperationInfoServiceByProjectId(
        GetsOperationInfoServiceByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoServiceByProjectId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoServiceByProjectIdValidator, GetsOperationInfoServiceByProjectIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoServiceByProjectIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsOperationInfoServiceByProjectIdQuery(
            request.ProjectId,
            request.ExcludedServiceInfoId,
            request.ServiceInfoFilters,
            request.OperationInfoFilters,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoServiceByProjectIdResponse>(response.Error!);

        var measureUnitIds = response.Value?.Data!.SelectMany(x => new[] { x.OperationInfoMeasurementId, x.MeasurementId }).Distinct().ToList();
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        foreach (var item in response.Value?.Data!)
        {
            item.MeasurementName = measureUnitsData?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;
            item.OperationInfoMeasurementName = measureUnitsData?.FirstOrDefault(x => x.Id == item.OperationInfoMeasurementId)?.Name;
        }

        return new GetsOperationInfoServiceByProjectIdResponse(response.Value?.Data! ?? new List<GetsOperationInfoServiceByProjectIdModel>(0), response.Value?.RowCount ?? 0);
    }

}