using Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetailDate;
using Engineering.Application.Services.ProjectOperationDetails.Queries.ValidatesProjectOperationDetailForScheduling;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.Enums;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetProjectOperationDetailSchedulingTypes;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperations;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDate;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDay;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetProjectOperationSchedulings;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperations;
using Engineering.Domain.Entities.OperationInfos.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings;

public class ProjectOperationDetailSchedulingLogic : IProjectOperationDetailSchedulingLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationDetailSchedulingLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectOperationDetailSchedulingLogic(IMediator mediator, ILogger<ProjectOperationDetailSchedulingLogic> logger, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateProjectOperationDetailDatesByDateResponse?>> UpdateProjectOperationDetailDatesByDateAsync(UpdateProjectOperationDetailDatesByDateRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailDatesByDateValidator, UpdateProjectOperationDetailDatesByDateRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(isValidRequest.Error!);

        var validate = await _mediator.Send(new ValidatesProjectOperationDetailForSchedulingQuery(request.OperationInfoIds, request.OperationLocationIds), ct);
        if (validate.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(ProjectOperationDetailErrors.ValidateForScheduling);

        var updateList = new List<UpdateProjectOperationDetailModel>();
        foreach (var operationInfo in request.OperationInfoIds)
        {
            var getsProjectOperation = await _mediator.Send(new GetProjectOperationSchedulingsQuery(request.ProjectId, operationInfo), ct);
            if (getsProjectOperation.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(getsProjectOperation.Error!);
            if (getsProjectOperation.Value is null)
                return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(ProjectOperationErrors.DataNotFoundWithOpAndPId);

            var projectOperations = getsProjectOperation.Value!.OrderBy(oo => oo.Priority).ToList();
            foreach (var projectOperation in projectOperations)
            {
                var details = projectOperation.ProjectOperationDetails?.Where(oo => request.OperationLocationIds.Contains(oo.OperationLocation.Id)).OrderBy(oo => oo.Priority).ToList();
                if (details?.Count <= 0)
                    continue;

                if (details is not null && details.Count > 0)
                {
                    foreach (var item in details)
                    {
                        if (item.DailyOperations.Any(x => x.StartDate.Date < request.StartDate.Date))
                            return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(ProjectOperationErrors.HaveDaily);

                        var updatedItem = await SetProjectOperationDateByDate(item, details.Where(x => x.Id != item.Id).ToList(), request.StartDate, request.EndDate, ct);
                        if (updatedItem.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(updatedItem.Error!);

                        updateList.AddRange(updatedItem.Value!);
                    }
                }
            }
        }

        foreach (var update in updateList)
        {
            var updateDate = await _mediator.Send(new UpdateProjectOperationDetailDateCommand(update.Id, update.StartDate, update.EndDate), ct);
            if (updateDate.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailDatesByDateResponse>(updateDate.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailDatesByDateResponse();
    }

    public async Task<Result<UpdateProjectOperationDetailDatesByDayResponse?>> UpdateProjectOperationDetailDatesByDayAsync(UpdateProjectOperationDetailDatesByDayRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailDatesByDayValidator, UpdateProjectOperationDetailDatesByDayRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(isValidRequest.Error!);

        var validate = await _mediator.Send(new ValidatesProjectOperationDetailForSchedulingQuery(request.OperationInfoIds, request.OperationLocationIds), ct);
        if (validate.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(ProjectOperationDetailErrors.ValidateForScheduling);

        var updateList = new List<UpdateProjectOperationDetailModel>();
        foreach (var operationInfo in request.OperationInfoIds)
        {
            var projectOperationInfosQuery = await _mediator.Send(new GetProjectOperationSchedulingsQuery(request.ProjectId, operationInfo), ct);
            if (projectOperationInfosQuery.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(projectOperationInfosQuery.Error!);
            if (projectOperationInfosQuery.Value is null)
                return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(OperationInfoErrors.OperationInfoWithIdNotFound);

            var projectOperations = projectOperationInfosQuery.Value!.OrderBy(oo => oo.Priority).ToList();
            foreach (var projectOperation in projectOperations)
            {
                var projectOperationDetails = projectOperation.ProjectOperationDetails.ToList();
                var selectedProjectOperationDetails = projectOperationDetails.Where(oo => request.OperationLocationIds.Contains(oo.OperationLocation.Id)).OrderBy(oo => oo.Priority).ToList();

                if (selectedProjectOperationDetails is not null && selectedProjectOperationDetails.Count > 0)
                {
                    foreach (var selectedProjectOperationDetail in selectedProjectOperationDetails)
                    {
                        DateTime? startDate = null;
                        DateTime? endDate = null;

                        if (selectedProjectOperationDetail.StartDate is not null && request.DependencyType == OperationInfoDependencyType.StartDate)
                        {
                            startDate = selectedProjectOperationDetail!.StartDate?.AddDays(request.CountDay);
                            endDate = startDate?.AddTicks(TimeCalculator.DayAndHourToTicks(selectedProjectOperationDetail.Day, selectedProjectOperationDetail.Hour));
                        }
                        else
                        {
                            startDate = selectedProjectOperationDetail!.EndDate?.AddDays(request.CountDay);
                            endDate = startDate?.AddTicks(TimeCalculator.DayAndHourToTicks(selectedProjectOperationDetail.Day, selectedProjectOperationDetail.Hour));
                        }

                        if (startDate == null || endDate == null)
                            return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(ProjectOperationDetailErrors.DateTimeNotValid);

                        var projectOperationDetailCommand = await SetProjectOperationDateByDate(selectedProjectOperationDetail, projectOperationDetails, startDate.Value, endDate.Value, ct);
                        if (projectOperationDetailCommand.IsFailure)
                            return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(projectOperationDetailCommand.Error!);

                        var updateProjectOperation = projectOperationDetailCommand.Value!;
                        updateList.AddRange(updateProjectOperation);
                        updateProjectOperation.ForEach(oo =>
                        {
                            var projectOperationDetail = projectOperationDetails.Where(c => c.Id.Equals(oo.Id)).FirstOrDefault()!;
                            projectOperationDetail.SetStartDate(oo.StartDate);
                            projectOperationDetail.SetEndDate(oo.EndDate);
                        });
                    }
                }
            }
        }

        foreach (var update in updateList)
        {
            var updateProjectOperationDetailDate = await _mediator.Send(new UpdateProjectOperationDetailDateCommand(update.Id, update.StartDate, update.EndDate), ct);
            if (updateProjectOperationDetailDate.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailDatesByDayResponse>(updateProjectOperationDetailDate.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailDatesByDayResponse();
    }

    public async Task<Result<GetSchedulingOperationLocationsResponse?>> GetSchedulingOperationLocationsAsync(GetSchedulingOperationLocationsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetSchedulingOperationLocationsValidator, GetSchedulingOperationLocationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSchedulingOperationLocationsResponse>(isValidRequest.Error!);

        var operationLocationsQuery = await _mediator.Send(new GetSchedulingOperationLocationsQuery(request.CostCenterId, request.ProjectId, request.FilterData,
            request.OperationInfoIds, request.OperationLocationsIds, request.PageIndex, request.PageSize), ct);
        if (operationLocationsQuery.IsFailure)
            return Result.Failure<GetSchedulingOperationLocationsResponse>(operationLocationsQuery.Error!);
        if (operationLocationsQuery.Value is null)
            return Result.Failure<GetSchedulingOperationLocationsResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

        var operationLocations = operationLocationsQuery.Value!.Data!.Adapt<List<GetSchedulingOperationLocationsResponseModel>>();
        int rowCount = operationLocationsQuery.Value!.RowCount!;

        return new GetSchedulingOperationLocationsResponse(operationLocations, rowCount);
    }

    public async Task<Result<GetSchedulingOperationInfosResponse?>> GetSchedulingOperationInfosAsync(GetSchedulingOperationInfosRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetSchedulingOperationInfosValidator, GetSchedulingOperationInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSchedulingOperationInfosResponse>(isValidRequest.Error!);

        var operationInfosQuery = await _mediator.Send(new GetSchedulingOperationInfosQuery(request.CostCenterId, request.ProjectId, request.FilterData,
            request.OperationLocationsIds, request.OperationInfoIds, request.PageIndex, request.PageSize), ct);
        if (operationInfosQuery.IsFailure)
            return Result.Failure<GetSchedulingOperationInfosResponse>(operationInfosQuery.Error!);
        if (operationInfosQuery.Value is null)
            return Result.Failure<GetSchedulingOperationInfosResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

        var operationLocations = operationInfosQuery.Value!.Data!.Adapt<List<GetSchedulingOperationInfosResponseModel>>();
        int rowCount = operationInfosQuery.Value!.RowCount!;

        return new GetSchedulingOperationInfosResponse(operationLocations, rowCount);
    }

    public async Task<Result<GetProjectOperationDetailSchedulingTypeResponse?>> GetSchedulingTypesAsync(GetProjectOperationDetailSchedulingTypeRequest request, CT ct)
    {
        return await Task.FromResult(new GetProjectOperationDetailSchedulingTypeResponse(EnumExt.GetEnumObjectList<SchedulingType>()));
    }

    private async Task<Result<List<UpdateProjectOperationDetailModel>?>> SetProjectOperationDateByDate(ProjectOperationDetail projectOperationDetail, List<ProjectOperationDetail>? projectOperationDetails, DateTime startDate, DateTime? endDate, CT ct)
    {
        var updateList = new List<UpdateProjectOperationDetailModel>();

        var updateByDate = await UpdateProjectOperationDetailDate(projectOperationDetail, startDate, endDate, ct);
        if (updateByDate.IsFailure)
            return Result.Failure<List<UpdateProjectOperationDetailModel>>(updateByDate.Error!);

        var updateDate = updateByDate.Value!;
        updateList.Add(updateDate);

        var nextDetails = projectOperationDetails?.Where(oo => oo.Priority >= projectOperationDetail.Priority).OrderBy(oo => oo.Priority).ToList();
        if (nextDetails is not null && nextDetails.Count > 0)
        {
            var nextStartDate = updateDate.EndDate;
            foreach (var item in nextDetails)
            {
                var nextEndDate = nextStartDate.AddTicks(TimeCalculator.DayAndHourToTicks(item.Day, item.Hour));

                var nextUpdate = await UpdateProjectOperationDetailDate(item, nextStartDate, nextEndDate, ct);
                if (nextUpdate.IsFailure)
                    return Result.Failure<List<UpdateProjectOperationDetailModel>>(nextUpdate.Error!);

                var nextUpdated = nextUpdate.Value!;
                updateList.Add(nextUpdated);
                nextStartDate = nextUpdated.EndDate;
            }
        }

        return updateList;
    }

    private async Task<Result<UpdateProjectOperationDetailModel?>> UpdateProjectOperationDetailDate(ProjectOperationDetail item, DateTime startDate, DateTime? endDate, CT ct)
    {
        // اگر در حال انجام بود و تاریخ شروع داشت تاریخ شروع تغییر نده در غیر این صورت تاریخ شروع آپدیت کن
        var detailStartDate = item.Status == ProjectOperationDetailStatus.Doing && item.StartDate is not null ? item.StartDate!.Value : startDate;
        var detailEndDate = endDate;
        if (item.Day > 0 || item.Hour > 0)
            detailEndDate = detailStartDate.AddTicks(TimeCalculator.DayAndHourToTicks(item.Day, item.Hour));
        else
            detailEndDate = endDate!.Value;

        if (detailStartDate > detailEndDate)
            return Result.Failure<UpdateProjectOperationDetailModel>(ProjectOperationDetailErrors.SchedulingUpdateDate);

        return await Task.Run(() =>
        {
            return new UpdateProjectOperationDetailModel()
            {
                Id = item.Id,
                StartDate = detailStartDate,
                EndDate = detailEndDate!.Value,
            };
        }, ct);
    }
}
