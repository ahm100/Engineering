using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationById;
using Engineering.Application.Services.ProjectOperationDetailInspections.Commands.CreateProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Commands.DeleteProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Commands.UpdateProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.CreateProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.DeleteProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetFilteredProjectOperationDetailInspections;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetProjectOperationDetailInspectionById;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionCreator;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelEnum;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelExporter;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsTotalInspectionReport;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetTotalDailiesByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionDocumentModel;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionGroupDelete;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.UpdateProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetFilteredProjectOperationDetailInspections;
using Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetProjectOperationDetailInspectionById;
using Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetsInspectionCreator;
using Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetTotalDailiesByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdNoIncluding;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Gita.Backend.Shared.Domain.Shared.Contracts;

namespace Engineering.Application.Services.ProjectOperationDetailInspections;

public partial class ProjectOperationDetailInspectionLogic : IProjectOperationDetailInspectionLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationDetailInspectionLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;

    public ProjectOperationDetailInspectionLogic(IMediator mediator, ILogger<ProjectOperationDetailInspectionLogic> logger, IUnitOfWork unitOfWork, IUserProfileService userProfileService, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateProjectOperationDetailInspectionResponse?>> CreateProjectOperationDetailInspection(CreateProjectOperationDetailInspectionRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateProjectOperationDetailInspectionValidator, CreateProjectOperationDetailInspectionRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectOperationDetailInspectionResponse>(isValidRequest.Error!);

        var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.projectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<CreateProjectOperationDetailInspectionResponse>(getProject.Error!);
        var project = getProject.Value!;

        ProjectOperation? projectOperation = null;
        if (request.ProjectOperationId != null && request.ProjectOperationId > 0)
        {
            var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.ProjectOperationId.Value), ct);
            if (getProjectOperation.IsFailure)
                return Result.Failure<CreateProjectOperationDetailInspectionResponse>(getProjectOperation.Error!);
            projectOperation = getProjectOperation.Value!;
        }

        ProjectOperationDetail? projectOperationDetail = null;
        if (request.ProjectOperationDetailId != null && request.ProjectOperationDetailId > 0)
        {
            var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdNoIncludingQuery(request.ProjectOperationDetailId.Value), ct);
            if (getProjectOperationDetail.IsFailure)
                return Result.Failure<CreateProjectOperationDetailInspectionResponse>(getProjectOperationDetail.Error!);
            projectOperationDetail = getProjectOperationDetail.Value!;
        }

        OperationLocation? operationLocation = null;
        if (request.ProjectOperationDetailId == null || request.ProjectOperationDetailId <= 0)
            if (request.OperationLocationId is not null && request.OperationLocationId > 0)
            {
                var getOperationLocation = await _mediator.Send(new GetOperationLocationByIdQuery(request.OperationLocationId.Value), ct);
                if (getOperationLocation.IsFailure)
                    return Result.Failure<CreateProjectOperationDetailInspectionResponse>(getOperationLocation.Error!);
                operationLocation = getOperationLocation.Value!;
            }

        OperationInfo? operationInfo = null;
        if ((request.ProjectOperationDetailId == null || request.ProjectOperationDetailId <= 0) &&
            (request.ProjectOperationId == null || request.ProjectOperationId <= 0))
            if (request.OperationInfoId is not null && request.OperationInfoId > 0)
            {
                var getOperationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoId.Value), ct);
                if (getOperationInfo.IsFailure)
                    return Result.Failure<CreateProjectOperationDetailInspectionResponse>(getOperationInfo.Error!);
                operationInfo = getOperationInfo.Value!;
            }

        var createResponse = await _mediator.Send(new CreateProjectOperationDetailInspectionCommand(request.Length, request.Width, request.Height,
            request.Weight, request.Number, request.InspectionDate, request.Description, project, operationInfo, operationLocation, projectOperation, projectOperationDetail,
            request.Documents), ct);
        if (createResponse.IsFailure)
            return Result.Failure<CreateProjectOperationDetailInspectionResponse>(createResponse.Error!);
        var ProjectOperationDetailInspection = createResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationDetailInspectionResponse(ProjectOperationDetailInspection.Id);
    }

    public async Task<Result<UpdateProjectOperationDetailInspectionResponse?>> UpdateProjectOperationDetailInspection(UpdateProjectOperationDetailInspectionRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationDetailInspectionValidator, UpdateProjectOperationDetailInspectionRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(isValidRequest.Error!);

        var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.projectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(getProject.Error!);
        var project = getProject.Value!;

        ProjectOperation? projectOperation = null;
        if (request.ProjectOperationId != null && request.ProjectOperationId > 0)
        {
            var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.ProjectOperationId.Value), ct);
            if (getProjectOperation.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(getProjectOperation.Error!);
            projectOperation = getProjectOperation.Value!;
        }

        ProjectOperationDetail? projectOperationDetail = null;
        if (request.ProjectOperationDetailId != null && request.ProjectOperationDetailId > 0)
        {
            var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdNoIncludingQuery(request.ProjectOperationDetailId.Value), ct);
            if (getProjectOperationDetail.IsFailure)
                return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(getProjectOperationDetail.Error!);
            projectOperationDetail = getProjectOperationDetail.Value!;
        }

        OperationLocation? operationLocation = null;
        if (request.ProjectOperationDetailId == null || request.ProjectOperationDetailId <= 0)
            if (request.OperationLocationId is not null && request.OperationLocationId > 0)
            {
                var getOperationLocation = await _mediator.Send(new GetOperationLocationByIdQuery(request.OperationLocationId.Value), ct);
                if (getOperationLocation.IsFailure)
                    return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(getOperationLocation.Error!);
                operationLocation = getOperationLocation.Value!;
            }

        OperationInfo? operationInfo = null;
        if ((request.ProjectOperationDetailId == null || request.ProjectOperationDetailId <= 0) &&
            (request.ProjectOperationId == null || request.ProjectOperationId <= 0))
            if (request.OperationInfoId is not null && request.OperationInfoId > 0)
            {
                var getOperationInfo = await _mediator.Send(new GetOperationInfoByIdIncludeLessQuery(request.OperationInfoId.Value), ct);
                if (getOperationInfo.IsFailure)
                    return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(getOperationInfo.Error!);
                operationInfo = getOperationInfo.Value!;
            }

        var updateResponse = await _mediator.Send(new UpdateProjectOperationDetailInspectionCommand(request.Id, request.Length, request.Width, request.Height,
            request.Weight, request.Number, request.InspectionDate, request.Description, project, operationInfo, operationLocation, projectOperation, projectOperationDetail, request.Documents), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateProjectOperationDetailInspectionResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDetailInspectionResponse(response.Id);
    }

    public async Task<Result<DeleteProjectOperationDetailInspectionResponse?>> DeleteProjectOperationDetailInspection(DeleteProjectOperationDetailInspectionRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteProjectOperationDetailInspectionValidator, DeleteProjectOperationDetailInspectionRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailInspectionResponse>(isValidRequest.Error!);

        var deleteResponse = await _mediator.Send(new DeleteProjectOperationDetailInspectionCommand(request.Id), ct);
        if (deleteResponse.IsFailure)
            return Result.Failure<DeleteProjectOperationDetailInspectionResponse>(deleteResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationDetailInspectionResponse(request.Id);
    }

    public async Task<Result<ProjectOperationDetailInspectionGroupDeleteResponse?>> ProjectOperationDetailInspectionGroupDelete(ProjectOperationDetailInspectionGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationDetailInspectionGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectOperationDetailInspectionGroupDeleteValidator, ProjectOperationDetailInspectionGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationDetailInspectionGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var deleteResponse = await _mediator.Send(new DeleteProjectOperationDetailInspectionCommand(item), ct);
            if (deleteResponse.IsFailure)
                return Result.Failure<ProjectOperationDetailInspectionGroupDeleteResponse>(deleteResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationDetailInspectionGroupDeleteResponse(true);
    }

    public async Task<Result<GetFilteredProjectOperationDetailInspectionsResponse?>> GetFilteredProjectOperationDetailInspections(GetFilteredProjectOperationDetailInspectionsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredProjectOperationDetailInspectionsValidator, GetFilteredProjectOperationDetailInspectionsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredProjectOperationDetailInspectionsResponse>(isValidRequest.Error!);

        var getFiltered = await _mediator.Send(new GetFilteredProjectOperationDetailInspectionsQuery(request.CostCenterId, request.ProjectId, request.OperationInfoId, request.OperationLocationId,
            request.ProjectOperationId, request.ProjectOperationDetailId, request.FromDate, request.ToDate, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetFilteredProjectOperationDetailInspectionsResponse>(getFiltered.Error!);
        var inspections = getFiltered.Value!.Data!;

        var creatorIds = inspections.Select(x => x.CreatorId).Where(x => x > 0).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var data = inspections.Adapt<List<GetFilteredProjectOperationDetailInspectionsModel>>();
        foreach (var item in data)
        {
            var value = inspections.FirstOrDefault(x => x.Id == item.Id);
            item.OperationInfoId = value?.ProjectOperation is null ? value?.OperationInfo?.Id : value?.ProjectOperation.OperationInfo.Id;
            item.OperationInfoName = value?.ProjectOperation is null ? value?.OperationInfo?.OperationInfoName : value?.ProjectOperation.OperationInfo.OperationInfoName;
            item.OperationInfoCode = value?.ProjectOperation is null ? value?.OperationInfo?.OperationInfoCode : value?.ProjectOperation.OperationInfo.OperationInfoCode;
            item.OperationLocationId = value?.ProjectOperationDetail is null ? value?.OperationLocation?.Id : value?.ProjectOperationDetail.OperationLocation.Id;
            item.PublicName = value?.ProjectOperationDetail is null ? value?.OperationLocation?.PublicName : value?.ProjectOperationDetail.OperationLocation.PublicName;
            item.PublicCode = value?.ProjectOperationDetail is null ? value?.OperationLocation?.PublicCode : value?.ProjectOperationDetail.OperationLocation.PublicCode;
            item.PrivateName = value?.ProjectOperationDetail is null ? value?.OperationLocation?.PrivateName : value?.ProjectOperationDetail.OperationLocation.PrivateName;
            item.PrivateCode = value?.ProjectOperationDetail is null ? value?.OperationLocation?.PrivateCode : value?.ProjectOperationDetail.OperationLocation.PrivateCode;
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            var values = inspections.FirstOrDefault(x => x.Id == item.Id)?.ProjectOperationDetailInspectionDocuments.ToList();
            if (values != null && values.Count > 0)
                item.Documents = values.Adapt<List<ProjectOperationDetailInspectionDocumentResponseModel>>();
        }

        return new GetFilteredProjectOperationDetailInspectionsResponse(data ?? new List<GetFilteredProjectOperationDetailInspectionsModel>(0),
            getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProjectOperationDetailInspectionByIdResponse?>> GetProjectOperationDetailInspectionById(GetProjectOperationDetailInspectionByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailInspectionByIdValidator, GetProjectOperationDetailInspectionByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailInspectionByIdResponse>(isValidRequest.Error!);

        var getProjectOperationDetailInspection = await _mediator.Send(new GetProjectOperationDetailInspectionByIdQuery(request.Id), ct);
        if (getProjectOperationDetailInspection.IsFailure)
            return Result.Failure<GetProjectOperationDetailInspectionByIdResponse>(getProjectOperationDetailInspection.Error!);
        var inspection = getProjectOperationDetailInspection.Value!;

        var creatorId = inspection.CreatorId;
        var creators = await WebServicesLogic.UserDataReceiver([creatorId], null, _mediator, ct);

        var response = inspection.Adapt<GetProjectOperationDetailInspectionByIdResponse>();
        response.OperationInfoId = inspection?.ProjectOperation is null ? inspection?.OperationInfo?.Id : inspection?.ProjectOperation.OperationInfo.Id;
        response.OperationInfoName = inspection?.ProjectOperation is null ? inspection?.OperationInfo?.OperationInfoName : inspection?.ProjectOperation.OperationInfo.OperationInfoName;
        response.OperationInfoCode = inspection?.ProjectOperation is null ? inspection?.OperationInfo?.OperationInfoCode : inspection?.ProjectOperation.OperationInfo.OperationInfoCode;
        response.OperationLocationId = inspection?.ProjectOperationDetail is null ? inspection?.OperationLocation?.Id : inspection?.ProjectOperationDetail.OperationLocation.Id;
        response.PublicName = inspection?.ProjectOperationDetail is null ? inspection?.OperationLocation?.PublicName : inspection?.ProjectOperationDetail.OperationLocation.PublicName;
        response.PublicCode = inspection?.ProjectOperationDetail is null ? inspection?.OperationLocation?.PublicCode : inspection?.ProjectOperationDetail.OperationLocation.PublicCode;
        response.PrivateName = inspection?.ProjectOperationDetail is null ? inspection?.OperationLocation?.PrivateName : inspection?.ProjectOperationDetail.OperationLocation.PrivateName;
        response.PrivateCode = inspection?.ProjectOperationDetail is null ? inspection?.OperationLocation?.PrivateCode : inspection?.ProjectOperationDetail.OperationLocation.PrivateCode;
        response.Creator = creators?.FirstOrDefault()?.FullName;
        if (inspection?.ProjectOperationDetailInspectionDocuments is not null &&
            inspection?.ProjectOperationDetailInspectionDocuments.Count > 0)
            response.Documents = inspection.ProjectOperationDetailInspectionDocuments.Adapt<List<ProjectOperationDetailInspectionDocumentResponseModel>>();

        return response;
    }

    public async Task<Result<GetTotalDailiesByProjectOperationDetailIdResponse?>> GetTotalDailiesByProjectOperationDetailId(GetTotalDailiesByProjectOperationDetailIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetTotalDailiesByProjectOperationDetailIdValidator, GetTotalDailiesByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalDailiesByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var getProjectOperationDetail = await _mediator.Send(new GetTotalDailiesByProjectOperationDetailIdQuery(request.ProjectOperationDetailId), ct);
        if (getProjectOperationDetail.IsFailure)
            return Result.Failure<GetTotalDailiesByProjectOperationDetailIdResponse>(getProjectOperationDetail.Error!);
        var projectOperationDetail = getProjectOperationDetail.Value!;

        bool? havePermission = null;
        if (request.IncpectionRead == true)
        {
            var Incpector = await _userProfileService.HasUserAccessToAction(1247, ct);
            if (Incpector.Status == ResponseStatusType.Ok)
                havePermission = true;
            else
                havePermission = false;
        }
        else
        {
            havePermission = true;
        }

        var Dailies = projectOperationDetail.DailyOperations.ToList();
        var deductionsAmount = projectOperationDetail.ProjectOperationDetailDeductions.Select(x => x.FinalAmount).ToList().Sum(x => x);

        var response = projectOperationDetail.Adapt<GetTotalDailiesByProjectOperationDetailIdResponse>();
        response.TotalHeights = havePermission == true ? Dailies!.Sum(x => x.Height) : 0;
        response.TotalLengths = havePermission == true ? Dailies!.Sum(x => x.Length) : 0;
        response.TotalNumbers = havePermission == true ? Dailies!.Sum(x => x.Number) : 0;
        response.TotalWidths = havePermission == true ? Dailies!.Sum(x => x.Width) : 0;
        response.TotalWeights = havePermission == true ? Dailies!.Sum(x => x.Weight) : 0;
        response.TotalAmounts = havePermission == true ? Dailies!.Sum(x => x.FinalAmount) : 0;
        response.ProjectOperationDetailFinalAmount = projectOperationDetail!.FinalAmount - deductionsAmount;
        response.TotalDeductionFinalAmount = deductionsAmount;
        response.TotalProjectOperationDetailFinalAmount = projectOperationDetail.FinalAmount;
        response.ProjectOperationWorkload = projectOperationDetail.ProjectOperation.Workload;
        return response;
    }

    public async Task<Result<GetsInspectionReportResponse?>> GetsInspectionReport(GetsInspectionReportRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsInspectionReportValidator, GetsInspectionReportRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsInspectionReportResponse>(isValidRequest.Error!);

        var result = await GetFilteredInspectionReports(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.OperationInfoIds,
            request.OperationLocationIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CreatorIds,
            request.FromDate,
            request.ToDate,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            false,
            _mediator,
            ct);
        if (result.IsFailure)
            return Result.Failure<GetsInspectionReportResponse>(result.Error!);
        var response = result.Value;

        return new GetsInspectionReportResponse(response.data ?? new List<GetsInspectionReportModel>(0), response.count);
    }

    public async Task<Result<GetsInspectionReportExcelExporterResponse?>> GetsInspectionReportExcelExporter(GetsInspectionReportExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsInspectionReportExcelExporterValidator, GetsInspectionReportExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsInspectionReportExcelExporterResponse>(isValidRequest.Error!);

        var result = await GetFilteredInspectionReports(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.OperationInfoIds,
            request.OperationLocationIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CreatorIds,
            request.FromDate,
            request.ToDate,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            false,
            _mediator,
            ct);
        if (result.IsFailure)
            return Result.Failure<GetsInspectionReportExcelExporterResponse>(result.Error!);
        var response = result.Value;

        var file = new FileContentResult(InspectionExcels.InspectionReportsToExcel(response.data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperationDetail-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsInspectionReportExcelExporterResponse(file);
    }

    public async Task<Result<GetsInspectionReportExcelEnumResponse?>> GetsInspectionReportExcelEnum(GetsInspectionReportExcelEnumRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<InspectionReportExcelEnum>());
        return new GetsInspectionReportExcelEnumResponse(response);
    }

    public async Task<Result<GetsTotalInspectionReportResponse?>> GetsTotalInspectionReport(GetsTotalInspectionReportRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsTotalInspectionReportValidator, GetsTotalInspectionReportRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalInspectionReportResponse>(isValidRequest.Error!);

        var result = await GetFilteredInspectionReports(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.OperationInfoIds,
            request.OperationLocationIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CreatorIds,
            request.FromDate,
            request.ToDate,
            request.FilterData,
            null,
            0,
            0,
            true,
            _mediator,
            ct);
        if (result.IsFailure)
            return Result.Failure<GetsTotalInspectionReportResponse>(result.Error!);
        var response = result.Value;

        return new GetsTotalInspectionReportResponse()
        {
            TotalAmounts = response.data?.Sum(x => x.FinalAmount),
            TotalHeights = response.data?.Sum(x => x.Height),
            TotalLengths = response.data?.Sum(x => x.Length),
            TotalNumbers = response.data?.Sum(x => x.Number),
            TotalWeights = response.data?.Sum(x => x.Weight),
            TotalWidths = response.data?.Sum(x => x.Width),
        };
    }

    public async Task<Result<GetsInspectionCreatorResponse?>> GetsInspectionCreator(GetsInspectionCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsInspectionCreator");

        var isValidRequest = await request.IsValidAsync<GetsInspectionCreatorRequestValidator, GetsInspectionCreatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsInspectionCreatorResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsInspectionCreatorQuery(), ct);
        if (response.IsFailure || response is null)
            return Result.Failure<GetsInspectionCreatorResponse>(DailyProjectOperationErrors.CreatorIdsAreEmpty);
        var value = response!.Value!;

        List<FilteredUserResponseModel?> creators = new();
        var data = new List<GetsInspectionCreatorResponseModel>();
        if (value.Data?.Count > 0)
        {
            var responseValue = await WebServicesLogic.UserDataReceiver(value.Data!, request.FilterData, _mediator, ct);
            if (responseValue is not null)
                creators.AddRange(responseValue);

            if (!string.IsNullOrEmpty(request.FilterData))
                foreach (var id in value.Data!)
                {
                    if (!creators.Any(x => x?.UserId == id))
                        continue;

                    var creator = creators.Where(x => x?.UserId == id).FirstOrDefault();
                    if (creator is not null)
                        data.Add(new GetsInspectionCreatorResponseModel()
                        {
                            CreatorId = id,
                            ThirdPartyId = creator?.Id,
                            FullName = creator?.FullName,
                            FirstName = creator?.FirstName,
                            LastName = creator?.LastName,
                            DefaultPhoneNo = creator?.DefaultPhoneNo,
                            OrganizationCode = creator?.OrganizationCode,
                            IdentityNo = creator?.IdentityNo,
                            Nickname = creator?.Nickname
                        });
                }
            else
                foreach (var id in value.Data!)
                {
                    var creator = creators.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetsInspectionCreatorResponseModel()
                    {
                        CreatorId = id,
                        ThirdPartyId = creator?.Id,
                        FullName = creator?.FullName,
                        FirstName = creator?.FirstName,
                        LastName = creator?.LastName,
                        DefaultPhoneNo = creator?.DefaultPhoneNo,
                        OrganizationCode = creator?.OrganizationCode,
                        IdentityNo = creator?.IdentityNo,
                        Nickname = creator?.Nickname,
                    });
                }
        }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsInspectionCreatorResponse(responseData ?? new List<GetsInspectionCreatorResponseModel>(0), creators?.Count ?? 0);
    }

}
