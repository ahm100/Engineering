using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineries;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineries;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;
using Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryBillDocument;
using Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryDocument;
using Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperation;
using Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryBillDocument;
using Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocument;
using Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocuments;
using Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryProjectOperation;
using Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDateTime;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDocument;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDriver;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryMachinery;
using Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryBillDocument;
using Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryDocument;
using Engineering.Application.Services.RequestMachineries.Models.DeleteRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Models.GetFilteredRequestMachineries;
using Engineering.Application.Services.RequestMachineries.Models.GetManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryHistories;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryMachineries;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryProjectOperation;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryStatus;
using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryUnits;
using Engineering.Application.Services.RequestMachineries.Models.GetsEmployerStatusStatementExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetsMachineryRequesteExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalOnProjectMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalOnProjectRequestReports;
using Engineering.Application.Services.RequestMachineries.Models.GetTotalSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GroupRequestMachineryStatusChanger;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryBillDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryGroupDelete;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryModel;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachinery;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDateTime;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDriver;
using Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryMachinery;
using Engineering.Application.Services.RequestMachineries.Queries.GetFilteredRequestMachineries;
using Engineering.Application.Services.RequestMachineries.Queries.GetManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineries.Queries.GetOnProjectMachineryReports;
using Engineering.Application.Services.RequestMachineries.Queries.GetOnProjectRequestReports;
using Engineering.Application.Services.RequestMachineries.Queries.GetProjectOperationDetailByProjectOperation;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdForUpdate;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdIncludeLess;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryProjectOperation;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryProjectOperationDetail;
using Engineering.Application.Services.RequestMachineries.Queries.GetSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Queries.IsExistRequestMachinery;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Queries.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Models;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Queries.GetActiveFilteredPreferentials;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.Currencies.Queries.GetDefaultCurrency;
using IdentityServer.ClientSdk.Services;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.RequestMachineries;

public partial class RequestMachineryLogic : IRequestMachineryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestMachineryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserInfoProvider _userInfoProvider;

    public RequestMachineryLogic(
        IMediator mediator,
        ILogger<RequestMachineryLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        IUserInfoProvider userInfoProvider)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateRequestMachineryResponse?>> CreateRequestMachineryAsync(CreateRequestMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestMachineryValidator, CreateRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateRequestMachineryResponse>(companyResponse.Error!);
        }

        decimal? totalHours = null;
        if (request.Unit == RequestMachineryUnit.Hourly)
        {
            if (!(request.TimeRequired.Split(':')[0].Count() >= 2 && request.TimeRequired.Split(':')[1].Count() == 2))
                return Result.Failure<CreateRequestMachineryResponse>(RequestMachineryErrors.TimeSpantCountError);

            if (int.Parse(request.TimeRequired.Split(':')[1]) > 59)
                return Result.Failure<CreateRequestMachineryResponse>(RequestMachineryErrors.TimeMoreThan59Min);

            TimeSpan time = TimeSpan.Parse(request.TimeRequired);
            totalHours = (decimal)time.TotalHours;
        }
        else
        {
            totalHours = Convert.ToDecimal(request.TimeRequired);
        }

        if (request.Id == null && request.IsDeleted == false)
        {
            var isExists = await _mediator.Send(new IsExistRequestMachineryQuery(request.ProjectId, request.MachineryId, totalHours.Value, request.RequestCount, companyId), ct);
            if (isExists.Value == true)
                return Result.Failure<CreateRequestMachineryResponse>(RequestMachineryErrors.IsExist);
        }

        if (request.ToDate.Date < request.FromDate.Date)
            return Result.Failure<CreateRequestMachineryResponse>(RequestMachineryErrors.InValidDates);

        var getProject = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<CreateRequestMachineryResponse>(getProject.Error!);
        var project = getProject.Value!;

        var getMachinery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
        if (getMachinery.IsFailure)
            return Result.Failure<CreateRequestMachineryResponse>(getMachinery.Error!);
        var machinery = getMachinery.Value!;

        DateTime? fromDate = null;
        if (request.FromTime != null)
            fromDate = request.FromDate.Date.Add(request.FromTime.Value);

        DateTime? toDate = null;
        if (request.ToTime != null)
            toDate = request.ToDate.Date.Add(request.ToTime.Value);

        var createResponse = await _mediator.Send(new CreateRequestMachineryCommand(request.Id,
            totalHours ?? 0, request.Unit, request.RequestCount, fromDate != null ? fromDate.Value : request.FromDate,
            toDate != null ? toDate.Value : request.ToDate, request.Description, project, machinery, request.IsDeleted,
           companyId), ct);
        if (createResponse.IsFailure)
            return Result.Failure<CreateRequestMachineryResponse>(createResponse.Error!);
        var requestMachinery = createResponse.Value!;

        if (request.Id != null)
        {
            foreach (var projectOperation in requestMachinery.ProjectOperations)
            {
                var deleteProjectOperationResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationCommand(projectOperation.Id), ct);
                if (deleteProjectOperationResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryResponse>(deleteProjectOperationResponse.Error!);
            }

            foreach (var projectOperationDetail in requestMachinery.ProjectOperationDetails)
            {
                var deletepProjectOperationDetailResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationDetailCommand(projectOperationDetail.Id), ct);
                if (deletepProjectOperationDetailResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryResponse>(deletepProjectOperationDetailResponse.Error!);
            }
        }

        if (!request.IsDeleted)
        {
            if (request.ProjectOperationIds is not null && request.ProjectOperationIds.Count > 0)
            {
                foreach (var projectOperationId in request.ProjectOperationIds)
                {
                    var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(projectOperationId), ct);
                    if (getProjectOperation.IsFailure)
                        return Result.Failure<CreateRequestMachineryResponse>(getProjectOperation.Error!);
                    var projectOperation = getProjectOperation.Value!;

                    var createProjectOperationResponse = await _mediator.Send(new CreateRequestMachineryProjectOperationCommand(null, projectOperation, requestMachinery, false), ct);
                    if (createProjectOperationResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryResponse>(createProjectOperationResponse.Error!);
                }
            }

            if (request.ProjectOperationDetailIds is not null && request.ProjectOperationDetailIds.Count > 0)
            {
                foreach (var ProjectOperationDetailId in request.ProjectOperationDetailIds)
                {
                    var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(ProjectOperationDetailId), ct);
                    if (getProjectOperationDetail.IsFailure)
                        return Result.Failure<CreateRequestMachineryResponse>(getProjectOperationDetail.Error!);
                    var projectOperationDetail = getProjectOperationDetail.Value!;

                    if (requestMachinery.FromDate.HasValue)
                        if (projectOperationDetail.StartDate.HasValue)
                            if (requestMachinery.FromDate.Value.Date < projectOperationDetail.StartDate.Value.Date)
                                return Result.Failure<CreateRequestMachineryResponse>(RequestMachineryErrors.FromDateNotBiggerThanDetailDate);

                    if (!(request!.ProjectOperationIds!.Contains(projectOperationDetail.ProjectOperation.Id)))
                        return Result.Failure<CreateRequestMachineryResponse>(RequestMachineryErrors.InValidProjectOperationDetail);

                    var createProjectOperationDetailResponse = await _mediator.Send(new CreateRequestMachineryProjectOperationDetailCommand(null, projectOperationDetail, requestMachinery, false), ct);
                    if (createProjectOperationDetailResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryResponse>(createProjectOperationDetailResponse.Error!);
                }
            }
        }

        if (request.RequestMachineryDocuments is not null && request.RequestMachineryDocuments.Count > 0)
        {
            if ((request.Id.HasValue && request.Id > 0))
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteRequestMachineryDocumentsCommand(request.Id.Value), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryResponse>(deleteDocumentResponse.Error!);
            }

            if (request.IsDeleted == false)
                foreach (var document in request.RequestMachineryDocuments)
                {
                    var createDocumentResponse = await _mediator.Send(new CreateRequestMachineryDocumentCommand(document.DocumentUrl, requestMachinery), ct);
                    if (createDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestMachineryResponse>(createDocumentResponse.Error!);
                }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestMachineryResponse(requestMachinery.Id);
    }

    public async Task<Result<CreateRequestMachineryBillDocumentResponse?>> CreateRequestMachineryBillDocument(CreateRequestMachineryBillDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestMachineryBillDocumentValidator, CreateRequestMachineryBillDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestMachineryBillDocumentResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdIncludeLessQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<CreateRequestMachineryBillDocumentResponse>(getRequestMachinery.Error!);

        var requestMachinery = getRequestMachinery.Value!;

        if (requestMachinery.RequestMachineryBillDocuments is not null && requestMachinery.RequestMachineryBillDocuments.Count > 0)
        {
            foreach (var item in requestMachinery.RequestMachineryBillDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteRequestMachineryBillDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryBillDocumentResponse>(deleteDocumentResponse.Error!);
            }
        }

        if (request.BillDocuments is not null && request.BillDocuments.Count > 0)
        {
            foreach (var document in request.BillDocuments)
            {
                var createDocumentResponse = await _mediator.Send(new CreateRequestMachineryBillDocumentCommand(document, requestMachinery), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryBillDocumentResponse>(createDocumentResponse.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestMachineryBillDocumentResponse(requestMachinery.Id);
    }

    public async Task<Result<CreateRequestMachineryDocumentResponse?>> CreateRequestMachineryDocument(CreateRequestMachineryDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestMachineryDocumentValidator, CreateRequestMachineryDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestMachineryDocumentResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdIncludeLessQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<CreateRequestMachineryDocumentResponse>(getRequestMachinery.Error!);

        var requestMachinery = getRequestMachinery.Value!;

        if (requestMachinery.RequestMachineryDocuments is not null && requestMachinery.RequestMachineryDocuments.Count > 0)
            foreach (var item in requestMachinery.RequestMachineryDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteRequestMachineryDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryDocumentResponse>(deleteDocumentResponse.Error!);
            }

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateRequestMachineryDocumentCommand(document, requestMachinery), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<CreateRequestMachineryDocumentResponse>(createDocumentResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestMachineryDocumentResponse(requestMachinery.Id);
    }

    public async Task<Result<UpdateRequestMachineryResponse?>> UpdateRequestMachineryAsync(UpdateRequestMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestMachineryValidator, UpdateRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestMachineryResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<UpdateRequestMachineryResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateRequestMachineryResponse>(companyResponse.Error!);
        }

        if (request.ToDate.Date < request.FromDate.Date)
            return Result.Failure<UpdateRequestMachineryResponse>(RequestMachineryErrors.InValidDates);

        var getProject = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<UpdateRequestMachineryResponse>(getProject.Error!);
        var project = getProject.Value!;

        var getMachinery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
        if (getMachinery.IsFailure)
            return Result.Failure<UpdateRequestMachineryResponse>(getMachinery.Error!);
        var machinery = getMachinery.Value!;

        if (!(requestMachinery.Status == RequestMachineryStatus.New ||
              requestMachinery.Status == RequestMachineryStatus.Rejected ||
              requestMachinery.Status == RequestMachineryStatus.Returned))
            return Result.Failure<UpdateRequestMachineryResponse>(RequestMachineryErrors.InValidStatus);

        DateTime? fromDate = null;
        if (request.FromTime != null)
            fromDate = request.FromDate.Date.Add(request.FromTime.Value);

        DateTime? toDate = null;
        if (request.ToTime != null)
            toDate = request.ToDate.Date.Add(request.ToTime.Value);

        decimal? totalHours = null;
        if (request.Unit == RequestMachineryUnit.Hourly)
        {
            if (!(request.TimeRequired.Split(':')[0].Count() >= 2 && request.TimeRequired.Split(':')[1].Count() == 2))
                return Result.Failure<UpdateRequestMachineryResponse>(RequestMachineryErrors.TimeSpantCountError);

            if (int.Parse(request.TimeRequired.Split(':')[1]) > 59)
                return Result.Failure<UpdateRequestMachineryResponse>(RequestMachineryErrors.TimeMoreThan59Min);

            TimeSpan time = TimeSpan.Parse(request.TimeRequired);
            totalHours = (decimal)time.TotalHours;
        }
        else
        {
            totalHours = Convert.ToDecimal(request.TimeRequired);
        }

        var updateResponse = await _mediator.Send(new UpdateRequestMachineryCommand(request.RequestMachineryId, totalHours ?? 0, request.Unit,
            request.RequestCount, fromDate != null ? fromDate.Value : request.FromDate, toDate != null ? toDate.Value : request.ToDate, request.Description,
            request.MachineryIdentifier, machinery, project, companyId), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateRequestMachineryResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        foreach (var projectOperation in requestMachinery.ProjectOperations)
        {
            var deleteProjectOperationResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationCommand(projectOperation.Id), ct);
            if (deleteProjectOperationResponse.IsFailure)
                return Result.Failure<UpdateRequestMachineryResponse>(deleteProjectOperationResponse.Error!);
        }

        foreach (var projectOperationDetail in requestMachinery.ProjectOperationDetails)
        {
            var deletepProjectOperationDetailResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationDetailCommand(projectOperationDetail.Id), ct);
            if (deletepProjectOperationDetailResponse.IsFailure)
                return Result.Failure<UpdateRequestMachineryResponse>(deletepProjectOperationDetailResponse.Error!);
        }

        if (request.ProjectOperationIds is not null && request.ProjectOperationIds.Count > 0)
        {
            foreach (var projectOperationId in request.ProjectOperationIds)
            {
                var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(projectOperationId), ct);
                if (getProjectOperation.IsFailure)
                    return Result.Failure<UpdateRequestMachineryResponse>(getProjectOperation.Error!);
                var projectOperation = getProjectOperation.Value!;

                var createProjectOperationResponse = await _mediator.Send(new CreateRequestMachineryProjectOperationCommand(null, projectOperation, response, false), ct);
                if (createProjectOperationResponse.IsFailure)
                    return Result.Failure<UpdateRequestMachineryResponse>(createProjectOperationResponse.Error!);
            }
        }

        if (request.ProjectOperationDetailsIds is not null && request.ProjectOperationDetailsIds.Count > 0)
        {
            foreach (var ProjectOperationDetailId in request.ProjectOperationDetailsIds)
            {
                var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(ProjectOperationDetailId), ct);
                if (getProjectOperationDetail.IsFailure)
                    return Result.Failure<UpdateRequestMachineryResponse>(getProjectOperationDetail.Error!);
                var projectOperationDetail = getProjectOperationDetail.Value!;

                if (requestMachinery.FromDate.HasValue)
                    if (projectOperationDetail.StartDate.HasValue)
                        if (!(requestMachinery.FromDate.Value.Date < projectOperationDetail.StartDate.Value.Date))
                            return Result.Failure<UpdateRequestMachineryResponse>(RequestMachineryErrors.FromDateNotBiggerThanDetailDate);

                if (!(request!.ProjectOperationIds!.Contains(projectOperationDetail.ProjectOperation.Id)))
                    return Result.Failure<UpdateRequestMachineryResponse>(RequestMachineryErrors.InValidProjectOperationDetail);

                var createProjectOperationDetailResponse = await _mediator.Send(new CreateRequestMachineryProjectOperationDetailCommand(null, projectOperationDetail, response, false), ct);
                if (createProjectOperationDetailResponse.IsFailure)
                    return Result.Failure<UpdateRequestMachineryResponse>(createProjectOperationDetailResponse.Error!);
            }
        }

        if (request.RequestMachineryDocuments is not null && request.RequestMachineryDocuments.Count > 0)
        {
            foreach (var document in request.RequestMachineryDocuments)
            {
                if (document.Id == null)
                {
                    var createDocumentResponse = await _mediator.Send(new CreateRequestMachineryDocumentCommand(document.DocumentUrl, requestMachinery), ct);
                    if (createDocumentResponse.IsFailure)
                        return Result.Failure<UpdateRequestMachineryResponse>(createDocumentResponse.Error!);
                }
                else if (document.Id != null && document.Id > 0 && document.IsDeleted == false)
                {
                    var updateDocumentResponse = await _mediator.Send(new UpdateRequestMachineryDocumentCommand(document.Id.Value, document.DocumentUrl), ct);
                    if (updateDocumentResponse.IsFailure)
                        return Result.Failure<UpdateRequestMachineryResponse>(updateDocumentResponse.Error!);
                }
                else if (document.Id != null && document.Id > 0 && document.IsDeleted == true)
                {
                    var deleteDocumentResponse = await _mediator.Send(new DeleteRequestMachineryDocumentCommand(document.Id.Value), ct);
                    if (deleteDocumentResponse.IsFailure)
                        return Result.Failure<UpdateRequestMachineryResponse>(deleteDocumentResponse.Error!);
                }
            }
        }
        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestMachineryResponse(response.Id);
    }

    public async Task<Result<UpdateRequestMachineryMachineryResponse?>> UpdateRequestMachineryMachineryAsync(UpdateRequestMachineryMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestMachineryMachineryValidator, UpdateRequestMachineryMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestMachineryMachineryResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<UpdateRequestMachineryMachineryResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        if (requestMachinery.Status != RequestMachineryStatus.OnProject)
            return Result.Failure<UpdateRequestMachineryMachineryResponse>(RequestMachineryErrors.InValidStatus);

        var getMachinery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
        if (getMachinery.IsFailure)
            return Result.Failure<UpdateRequestMachineryMachineryResponse>(getMachinery.Error!);
        var machinery = getMachinery.Value!;

        var updateResponse = await _mediator.Send(new UpdateRequestMachineryMachineryCommand(request.RequestMachineryId, machinery), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateRequestMachineryMachineryResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestMachineryMachineryResponse(response.Id);
    }

    public async Task<Result<UpdateRequestMachineryDateTimeResponse?>> UpdateRequestMachineryDateTime(UpdateRequestMachineryDateTimeRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestMachineryDateTimeValidator, UpdateRequestMachineryDateTimeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestMachineryDateTimeResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<UpdateRequestMachineryDateTimeResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        if (requestMachinery.Status != RequestMachineryStatus.OnProject)
            return Result.Failure<UpdateRequestMachineryDateTimeResponse>(RequestMachineryErrors.InValidStatus);

        decimal? confirmTotalHours = null;
        if (!string.IsNullOrEmpty(request.TimeRequired))
        {
            if (requestMachinery.Unit == RequestMachineryUnit.Hourly)
            {
                if (!(request.TimeRequired.Split(':')[0].Count() >= 2 && request.TimeRequired.Split(':')[1].Count() == 2))
                    return Result.Failure<UpdateRequestMachineryDateTimeResponse>(RequestMachineryErrors.TimeSpantCountError);

                if (int.Parse(request.TimeRequired.Split(':')[1]) > 59)
                    return Result.Failure<UpdateRequestMachineryDateTimeResponse>(RequestMachineryErrors.TimeMoreThan59Min);

                TimeSpan time = TimeSpan.Parse(request.TimeRequired);
                confirmTotalHours = (decimal)time.TotalHours;
            }
            else
            {
                confirmTotalHours = Convert.ToDecimal(request.TimeRequired);
            }
        }

        var updateResponse = await _mediator.Send(new UpdateRequestMachineryDateTimeCommand(request.RequestMachineryId,
            confirmTotalHours, request.FromDate, request.ToDate, request.FromTime, request.ToTime), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateRequestMachineryDateTimeResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestMachineryDateTimeResponse(response.Id);
    }

    public async Task<Result<UpdateRequestMachineryDriverResponse?>> UpdateRequestMachineryDriverAsync(UpdateRequestMachineryDriverRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestMachineryDriverValidator, UpdateRequestMachineryDriverRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestMachineryDriverResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<UpdateRequestMachineryDriverResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        if (request.DriverId != null && request.DriverId > 0)
        {
            List<long> ids = [];
            ids.Add(request.DriverId.Value);
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<UpdateRequestMachineryDriverResponse>(driverQuery.Error!);
        }

        if (request.DriverId is not null || request.DriverName is not null)
        {
            var updateDriver = await _mediator.Send(new UpdateRequestMachineryDriverCommand(requestMachinery!, request.DriverId, request.DriverName), ct);
            if (updateDriver.IsFailure)
                return Result.Failure<UpdateRequestMachineryDriverResponse>(updateDriver.Error!);
        }
        else
            return Result.Failure<UpdateRequestMachineryDriverResponse>(FixAssetMachineryErrors.DriverInfoUnValid);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestMachineryDriverResponse(requestMachinery.Id);
    }

    public async Task<Result<GroupRequestMachineryStatusChangerResponse?>> GroupRequestMachineryStatusChanger(GroupRequestMachineryStatusChangerRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GroupRequestMachineryStatusChangerValidator, GroupRequestMachineryStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupRequestMachineryStatusChangerResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(item), ct);
            if (getRequestMachinery.IsFailure)
                return Result.Failure<GroupRequestMachineryStatusChangerResponse>(getRequestMachinery.Error!);
            var requestMachinery = getRequestMachinery.Value!;

            if (!(requestMachinery.Status == RequestMachineryStatus.New))
                return Result.Failure<GroupRequestMachineryStatusChangerResponse>(RequestMachineryErrors.InValidStatus);

            var statusChangeResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, request.Status, request.Description, null), ct);
            if (statusChangeResponse.IsFailure)
                return Result.Failure<GroupRequestMachineryStatusChangerResponse>(statusChangeResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new GroupRequestMachineryStatusChangerResponse(true);
    }

    public async Task<Result<DeleteRequestMachineryResponse?>> DeleteRequestMachineryAsync(DeleteRequestMachineryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteRequestMachineryValidator, DeleteRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteRequestMachineryResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<DeleteRequestMachineryResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        if (!(RequestMachineryStatusValidator.AllowStatusForDelete.Any(x => x == requestMachinery.Status)))
            return Result.Failure<DeleteRequestMachineryResponse>(RequestMachineryErrors.InValidStatus);

        var deleteResponse = await _mediator.Send(new DeleteRequestMachineryCommand(request.RequestMachineryId), ct);
        if (deleteResponse.IsFailure)
            return Result.Failure<DeleteRequestMachineryResponse>(deleteResponse.Error!);

        foreach (var projectOperation in requestMachinery.ProjectOperations)
        {
            var deleteProjectOperationResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationCommand(projectOperation.Id), ct);
            if (deleteProjectOperationResponse.IsFailure)
                return Result.Failure<DeleteRequestMachineryResponse>(deleteProjectOperationResponse.Error!);
        }

        foreach (var projectOperationDetail in requestMachinery.ProjectOperationDetails)
        {
            var deleteProjectOperationDetailResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationDetailCommand(projectOperationDetail.Id), ct);
            if (deleteProjectOperationDetailResponse.IsFailure)
                return Result.Failure<DeleteRequestMachineryResponse>(deleteProjectOperationDetailResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new DeleteRequestMachineryResponse(requestMachinery.Id);
    }

    public async Task<Result<RequestMachineryGroupDeleteResponse?>> RequestMachineryGroupDelete(RequestMachineryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for RequestMachineryGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<RequestMachineryGroupDeleteValidator, RequestMachineryGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<RequestMachineryGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(item), ct);
            if (getRequestMachinery.IsFailure)
                return Result.Failure<RequestMachineryGroupDeleteResponse>(getRequestMachinery.Error!);
            var requestMachinery = getRequestMachinery.Value!;

            if (!(requestMachinery.Status == RequestMachineryStatus.New))
                return Result.Failure<RequestMachineryGroupDeleteResponse>(RequestMachineryErrors.InValidStatus);

            var deleteResponse = await _mediator.Send(new DeleteRequestMachineryCommand(item), ct);
            if (deleteResponse.IsFailure)
                return Result.Failure<RequestMachineryGroupDeleteResponse>(deleteResponse.Error!);

            foreach (var projectOperation in requestMachinery.ProjectOperations)
            {
                var deleteProjectOperationResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationCommand(projectOperation.Id), ct);
                if (deleteProjectOperationResponse.IsFailure)
                    return Result.Failure<RequestMachineryGroupDeleteResponse>(deleteProjectOperationResponse.Error!);
            }

            foreach (var projectOperationDetail in requestMachinery.ProjectOperationDetails)
            {
                var deleteProjectOperationDetailResponse = await _mediator.Send(new DeleteRequestMachineryProjectOperationDetailCommand(projectOperationDetail.Id), ct);
                if (deleteProjectOperationDetailResponse.IsFailure)
                    return Result.Failure<RequestMachineryGroupDeleteResponse>(deleteProjectOperationDetailResponse.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new RequestMachineryGroupDeleteResponse(true);
    }

    public async Task<Result<GetRequestMachineryMachineriesResponse?>> GetRequestMachineryMachineries(GetRequestMachineryMachineriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryMachineriesValidator, GetRequestMachineryMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryMachineriesResponse>(isValidRequest.Error!);

        var getRequestMachineryQuery = await _mediator.Send(new GetRequestMachineryByIdForUpdateQuery(request.RequestMachineryId), ct);
        if (getRequestMachineryQuery.IsFailure)
            return Result.Failure<GetRequestMachineryMachineriesResponse>(getRequestMachineryQuery.Error!);
        var requestMachinery = getRequestMachineryQuery.Value;

        if (requestMachinery!.Status != RequestMachineryStatus.OnProject)
            return Result.Failure<GetRequestMachineryMachineriesResponse>(RequestMachineryErrors.InValidStatus);

        var getFixAssets = await _mediator.Send(new GetFixAssetMachineriesQuery(null, null, null, null, null, null, null, null, null, request.FilterData, null, null, null, 0, 0), ct);
        var fixAssetMachineries = getFixAssets.Value!.Data!;

        var contractorMachineriesQuery = await _mediator.Send(new GetContractorMachineriesQuery(null, null, null, null, null, null, request.FilterData, null, null, null, 0, 0), ct);
        var contractorMachineries = contractorMachineriesQuery.Value!.Data!;

        List<FixAssetMachinery>? fixAssets = [];
        if (fixAssetMachineries is not null && fixAssetMachineries.Count > 0)
        {
            var withoutReserve = fixAssetMachineries
                .Where(x => x.MachineryReservations is null || x.MachineryReservations.Count <= 0 &&
                (!x.FixAssetMachineryNotWorks.Any(z => z.StartDate >= requestMachinery.FromDate && z.EndDate <= requestMachinery.ToDate))).ToList();
            var withReserve = fixAssetMachineries.Where(x => x.MachineryReservations is not null && x.MachineryReservations.Count > 0 &&
                (!x.FixAssetMachineryNotWorks.Any(z => z.StartDate >= requestMachinery.FromDate && z.EndDate <= requestMachinery.ToDate))).ToList();

            foreach (var item in withReserve)
            {
                if (item.MachineryReservations
                    .Any(x => x.RequestMachinery.Id != requestMachinery.Id &&
                    ((x.StartDate < requestMachinery.FromDate && x.EndDate < requestMachinery.FromDate) ||
                     (x.StartDate > requestMachinery.ToDate && x.EndDate > requestMachinery.ToDate))))
                    fixAssets.Add(item);
            }

            fixAssets.AddRange(withoutReserve ?? []);
        }

        List<ContractorMachinery>? contractorMachineriesData = [];
        if (contractorMachineries is not null && contractorMachineries.Count > 0)
        {
            List<ContractorMachinery>? contractorMachines = [];
            foreach (var item in contractorMachineries)
            {
                if (item.RequestMachineries is null || item.RequestMachineries.Count <= 0)
                    contractorMachines.Add(item);
                else if (item.RequestMachineries is not null && item.RequestMachineries.Count > 0 && (item.RequestMachineries.Any(x => x.Id != requestMachinery.Id)))
                    contractorMachines.Add(item);
                else
                    continue;
            }

            if (contractorMachines is not null && contractorMachines.Count > 0)
            {
                contractorMachineriesData.AddRange(contractorMachines.Where(x => x.RequestMachineries is null || x.RequestMachineries.Count <= 0).ToList() ?? []);
                contractorMachineriesData.AddRange(
                            contractorMachines.Where(x => x.RequestMachineries
                                .Any(x => x.Id != requestMachinery.Id &&
                                    ((x.FromDate < requestMachinery.FromDate && x.ToDate < requestMachinery.FromDate) ||
                                     (x.FromDate > requestMachinery.ToDate && x.ToDate > requestMachinery.ToDate)))).ToList() ?? []);
            }
        }

        List<long>? thirdpartyIds = [];
        thirdpartyIds.AddRange(fixAssets!.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdpartyIds.AddRange(contractorMachineriesData!.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds.Distinct().ToList(), null, null, _mediator, ct);

        List<GetRequestMachineryMachineriesModel>? data = [];
        if (fixAssets is not null && fixAssets.Count > 0)
            foreach (var item in fixAssets)
            {
                decimal? price = null;
                if (requestMachinery.Unit == RequestMachineryUnit.Daily)
                    price = item.DailyRate;
                else if (requestMachinery.Unit == RequestMachineryUnit.Hourly)
                    price = item.HourlyRate;
                else if (requestMachinery.Unit == RequestMachineryUnit.Serviced)
                    price = item.ServiceRate;
                else
                    price = item.VolumeRate;

                var priceWithComma = price is not null ? price.Value.ToString("N0") + " ریال" : null;

                data.Add(new GetRequestMachineryMachineriesModel()
                {
                    ContractorId = item.ContractorId,
                    Contractor = item.ContractorId is not null && item.ContractorId > 0 ?
                    contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName : "ماشین آلات شرکت",
                    MachineryId = item.Machinery.Id,
                    MachineryName = item.Machinery.MachineryName,
                    MachineryGroupId = item.Machinery.MachineriesGroup is not null ? item.Machinery.MachineriesGroup.Id : null,
                    MachineryGroupName = item.Machinery.MachineriesGroup is not null ? item.Machinery.MachineriesGroup.GroupName : null,
                    MachineryPrice = price
                });
            }

        if (contractorMachineriesData is not null && contractorMachineriesData.Count > 0)
            foreach (var item in contractorMachineriesData)
                data.Add(new GetRequestMachineryMachineriesModel()
                {
                    ContractorId = item.ContractorId,
                    Contractor = contractors?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName,
                    MachineryId = item.Machinery.Id,
                    MachineryName = item.Machinery.MachineryName,
                    MachineryGroupId = item.Machinery.MachineriesGroup is not null ? item.Machinery.MachineriesGroup.Id : null,
                    MachineryGroupName = item.Machinery.MachineriesGroup is not null ? item.Machinery.MachineriesGroup.GroupName : null,
                    MachineryPrice = item.MachineryPrice
                });

        List<GetRequestMachineryMachineriesModel>? responseData = [];
        if (data is not null && data.Count > 0)
            foreach (var item in data)
            {
                if (!responseData.Any(x => x.MachineryId == item.MachineryId && x.ContractorId == item.ContractorId))
                    responseData.Add(item);
                else
                    continue;
            }

        var response = responseData.SetPaging(request.PageIndex - 1, request.PageSize);

        return new GetRequestMachineryMachineriesResponse(response ?? new List<GetRequestMachineryMachineriesModel>(0), responseData?.Count ?? 0);
    }

    public async Task<Result<GetFilteredRequestMachineriesResponse?>> GetFilteredRequestMachineriesAsync(GetFilteredRequestMachineriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestMachineriesValidator, GetFilteredRequestMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestMachineriesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getFiltered = await _mediator.Send(new GetFilteredRequestMachineriesQuery(
            null,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.OperationInfoIds,
            request.MachineriesGroupId,
            request.MachineryId,
            request.Status,
            request.PaymentType,
            request.FromDate,
            request.ToDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            null,
            null,
            null,
            request.DriverName,
            request.FilterData,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetFilteredRequestMachineriesResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        List<long> allIds = new();
        var allIdofRequestMachineries = IdCollectors(requestMachineries!).ToList();
        allIds.AddRange(allIdofRequestMachineries);

        if (histories is not null)
        {
            var allIdOfHistories = RequestMachineryHistoryIdCollectors(histories!).ToList();
            allIds.AddRange(allIdOfHistories);
        }

        allIds.Distinct();
        var thirdParties = await WebServicesLogic.UserDataReceiver(allIds, null, _mediator, ct);

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? thirdpartyIds = [];
        thirdpartyIds.AddRange(requestMachineries!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdpartyIds.AddRange(requestMachineries!.Where(x => x.DriverId != null && x.DriverId > 0).Select(x => (long)x.DriverId!).Distinct().ToList() ?? []);
        if (_userInfoProvider.CompanyThirdPartyId > 0)
            thirdpartyIds.Add(_userInfoProvider.CompanyThirdPartyId);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds, null, null, _mediator, ct);

        var models = new List<GetFilteredRequestMachineriesModel>();
        foreach (var requestMachinery in requestMachineries!)
        {
            var Times = GetTimeRequireds(requestMachinery);

            List<RequestMachineryDocumentResponseModel>? documents = [];
            if (requestMachinery.RequestMachineryDocuments is not null && requestMachinery.RequestMachineryDocuments.Count > 0)
                foreach (var item in requestMachinery.RequestMachineryDocuments)
                {
                    documents.Add(new RequestMachineryDocumentResponseModel(item.Id, item.Url));
                }

            List<RequestMachineryBillDocumentResponseModel>? billDocuments = [];
            if (requestMachinery.RequestMachineryBillDocuments is not null && requestMachinery.RequestMachineryBillDocuments.Count > 0)
                foreach (var item in requestMachinery.RequestMachineryBillDocuments)
                {
                    billDocuments.Add(new RequestMachineryBillDocumentResponseModel(item.Id, item.Url));
                }

            var projectOperations = string.Join(",", requestMachinery.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
            var projectOperationDetails = string.Join(",", requestMachinery.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());
            var history = histories?.FirstOrDefault(x => x.RequestMachinery.Id == requestMachinery.Id);
            var company = companies?.FirstOrDefault(x => x.Id == requestMachinery.CompanyId);
            var driver = contractors?.FirstOrDefault(x => x is not null && x.Id == requestMachinery.DriverId);

            string? contractorName = null;
            if (requestMachinery.Status == RequestMachineryStatus.OnProject)
            {
                if (requestMachinery.ContractorId is not null && requestMachinery.ContractorId > 0)
                {
                    contractorName = contractors?.FirstOrDefault(x => x?.Id == requestMachinery.ContractorId)?.FullName;
                }
                else
                    contractorName = contractors?.FirstOrDefault(x => x?.Id == _userInfoProvider.CompanyThirdPartyId)?.FullName ?? company?.NameFa;
            }
            else
            {
                contractorName = null;
            }

            models.Add(new GetFilteredRequestMachineriesModel()
            {
                CostCenterName = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                Created = requestMachinery.Created,
                FromDate = requestMachinery.FromDate,
                FromTime = requestMachinery.FromDate != null ? requestMachinery.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ToDate = requestMachinery.ToDate,
                ToTime = requestMachinery.ToDate != null ? requestMachinery.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                MachineryGroupName = requestMachinery.Machinery!.MachineriesGroup.GroupName,
                MachineryName = requestMachinery.Machinery?.MachineryName,
                ProjectName = requestMachinery.Project.ProjectName,
                RequestCount = requestMachinery.RequestCount,
                RequestMachineryId = requestMachinery.Id,
                Status = requestMachinery.Status,
                TimeRequired = Times.timeRequired,
                Unit = requestMachinery.Unit,
                ProjectOperations = projectOperations,
                ProjectOperationDetails = projectOperationDetails,
                Creator = thirdParties?.Where(x => x.UserId == requestMachinery.CreatorId).FirstOrDefault()?.FullName,
                CreatorId = thirdParties?.Where(x => x.UserId == requestMachinery.CreatorId).FirstOrDefault()?.UserId,
                ConfirmDate = history?.Created,
                ConfirmUserId = thirdParties?.Where(x => x.UserId == history?.CreatorId).FirstOrDefault()?.UserId,
                ConfirmUser = thirdParties?.Where(x => x.UserId == history?.CreatorId).FirstOrDefault()?.FullName,
                CompanyId = requestMachinery.CompanyId,
                CompanyNameFa = company?.NameFa,
                RequestMachineryDocuments = documents,
                ConfirmFromDate = requestMachinery!.ConfirmFromDate,
                ConfirmFromTime = requestMachinery!.ConfirmFromDate != null ? requestMachinery!.ConfirmFromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ConfirmToDate = requestMachinery!.ConfirmToDate,
                ConfirmToTime = requestMachinery!.ConfirmToDate != null ? requestMachinery!.ConfirmToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ContractorId = requestMachinery?.ContractorId,
                Contractor = contractorName,
                ConfirmedDescription = requestMachinery?.ConfirmedDescription,
                ConfirmedTimeRequired = Times.confirmTimeRequired,
                RequestNumber = requestMachinery?.RequestNumber,
                DriverId = requestMachinery?.DriverId,
                DriverName = requestMachinery?.DriverName,
                Driver = driver?.FullName,
                RequestMachineryBillDocuments = billDocuments,
                Description = requestMachinery?.Description,
                UnitPrice = requestMachinery?.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.UnitPrice,
                TotalPrice = requestMachinery?.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.TotalPrice
            });
        }
        return new GetFilteredRequestMachineriesResponse(models, getFiltered.Value!.RowCount!);
    }

    public async Task<Result<GetRequestMachineryByIdResponse?>> GetRequestMachineryByIdAsync(GetRequestMachineryByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryByIdValidator, GetRequestMachineryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryByIdResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<GetRequestMachineryByIdResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        var projectOperations = new List<ProjectOperationModel>();
        var projectOperationDetails = new List<ProjectOperationDetailModel>();

        var history = requestMachinery.Histories.Where(c => c.Status == RequestMachineryStatus.Confirmed).FirstOrDefault();

        List<long> allIds = new();
        var allIdofRequestMachineries = IdCollectors(new List<RequestMachinery?>() { requestMachinery! }).ToList();
        allIds.AddRange(allIdofRequestMachineries);
        if (history is not null)
        {
            var allIdOfHistories = RequestMachineryHistoryIdCollectors(new List<RequestMachineryHistory?>() { history! }).ToList();
            allIds.AddRange(allIdOfHistories);
        }

        allIds.Distinct();
        var users = await WebServicesLogic.UserDataReceiver(allIds, null, _mediator, ct);

        var requestMachineryProjectOperations = requestMachinery.ProjectOperations.ToList();
        var ids = new List<long>();
        foreach (var item in requestMachineryProjectOperations)
            ids.Add(item.ProjectOperation.UnitOfMeasurementId);

        var measurements = await WebServicesLogic.MeasurementDataReceiver(ids, _mediator, ct);

        Company? company = null;
        if (requestMachinery.CompanyId is not null && requestMachinery.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestMachinery.CompanyId, _mediator, ct);

        List<UserModel?>? thirdParties = null;
        List<long>? thirdPartyIds = [];
        thirdPartyIds.Add(requestMachinery.DriverId ?? 0);
        thirdPartyIds.Add(requestMachinery.ContractorId ?? 0);
        if (_userInfoProvider.CompanyThirdPartyId > 0)
            thirdPartyIds.Add(_userInfoProvider.CompanyThirdPartyId);
        thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds.Where(x => x > 0).Distinct().ToList(), null, null, _mediator, ct);

        foreach (var projectOperation in requestMachineryProjectOperations)
        {
            var measurementName = measurements?.Where(x => x.Id == projectOperation.ProjectOperation.UnitOfMeasurementId).FirstOrDefault()?.Name;

            projectOperations.Add(new ProjectOperationModel()
            {
                OperationInfoCode = projectOperation.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = projectOperation.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationId = projectOperation.ProjectOperation.Id,
                Id = projectOperation.Id,
                MeasurementName = measurementName != null || measurementName != string.Empty ? measurementName : null,
            });
        }

        var requestMachineryProjectOperationDetails = requestMachinery.ProjectOperationDetails.ToList();
        foreach (var projectOperationDetail in requestMachineryProjectOperationDetails)
        {
            projectOperationDetails.Add(new ProjectOperationDetailModel()
            {
                PrivateName = projectOperationDetail.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = projectOperationDetail.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = projectOperationDetail.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = projectOperationDetail.ProjectOperationDetail.OperationLocation.PublicCode,
                ProjectOperationDetailId = projectOperationDetail.ProjectOperationDetail.Id,
                Id = projectOperationDetail.Id
            });
        }

        List<RequestMachineryDocumentResponseModel>? documents = [];
        if (requestMachinery.RequestMachineryDocuments is not null && requestMachinery.RequestMachineryDocuments.Count > 0)
            foreach (var item in requestMachinery.RequestMachineryDocuments)
            {
                documents.Add(new RequestMachineryDocumentResponseModel(item.Id, item.Url));
            }

        List<RequestMachineryBillDocumentResponseModel>? billDocuments = [];
        if (requestMachinery.RequestMachineryBillDocuments is not null && requestMachinery.RequestMachineryBillDocuments.Count > 0)
            foreach (var item in requestMachinery.RequestMachineryBillDocuments)
            {
                billDocuments.Add(new RequestMachineryBillDocumentResponseModel(item.Id, item.Url));
            }

        var identifier = requestMachinery.RequestMachineryAssignments.Where(x => !x.IsDeleted).FirstOrDefault()?.MachineryIdentifier;
        RequestNumberPlatesModel? plateModel = null;
        if (identifier != "ماشین آلات فاقد پلاک" && (identifier?.Count(char.IsLetter) == 1 && identifier?.Count(char.IsDigit) == 7))
        {
            plateModel = SetNumberPlates(identifier);
        }

        string? contractorName = null;
        if (requestMachinery.Status == RequestMachineryStatus.OnProject)
        {
            if (requestMachinery.ContractorId is not null && requestMachinery.ContractorId > 0)
            {
                contractorName = thirdParties?.FirstOrDefault(x => x?.Id == requestMachinery.ContractorId)?.FullName;
            }
            else
                contractorName = contractorName = thirdParties?.FirstOrDefault(x => x?.Id == _userInfoProvider.CompanyThirdPartyId)?.FullName ?? company?.NameFa;
        }
        else
        {
            contractorName = null;
        }

        var Times = GetTimeRequireds(requestMachinery);
        var data = GetDatas(requestMachinery);
        var response = new GetRequestMachineryByIdResponse()
        {
            CostCenterId = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
            CostCenterName = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            Description = requestMachinery.Description,
            FromDate = requestMachinery.FromDate,
            FromTime = requestMachinery.FromDate != null ? requestMachinery.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            ToDate = requestMachinery.ToDate,
            ToTime = requestMachinery.ToDate != null ? requestMachinery.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            MachineriesGroupId = requestMachinery.Machinery.MachineriesGroup.Id,
            MachineriesGroupName = requestMachinery.Machinery.MachineriesGroup.GroupName,
            MachineryId = requestMachinery.Machinery?.Id,
            MachineryName = requestMachinery.Machinery?.MachineryName,
            ProjectId = requestMachinery.Project.Id,
            ProjectName = requestMachinery.Project.ProjectName,
            RequestCount = requestMachinery.RequestCount,
            RequestMachineryId = requestMachinery.Id,
            Status = requestMachinery.Status,
            TimeRequired = Times.timeRequired,
            ConfirmTimeRequired = Times.confirmTimeRequired,
            Unit = requestMachinery.Unit,
            ProjectOperations = projectOperations,
            ProjectOperationDetails = projectOperationDetails,
            BillNumberPlatesModel = plateModel,
            MachineryIdentifier = identifier,
            ConfirmDate = history?.Created,
            ConfirmUserId = users?.Where(x => x.UserId == history?.CreatorId).FirstOrDefault()?.UserId,
            ConfirmUser = users?.Where(x => x.UserId == history?.CreatorId).FirstOrDefault()?.FullName,
            Creator = users?.Where(x => x.UserId == requestMachinery.CreatorId).FirstOrDefault()?.FullName,
            CreatorId = users?.Where(x => x.UserId == requestMachinery.CreatorId).FirstOrDefault()?.UserId,
            CompanyId = requestMachinery.CompanyId,
            CompanyNameFa = company?.NameFa,
            RequestMachineryDocuments = documents,
            RequestMachineryBillDocuments = billDocuments,
            ConfirmFromDate = requestMachinery!.ConfirmFromDate,
            ConfirmFromTime = requestMachinery!.ConfirmFromDate != null ? requestMachinery!.ConfirmFromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            ConfirmToDate = requestMachinery!.ConfirmToDate,
            ConfirmToTime = requestMachinery!.ConfirmToDate != null ? requestMachinery!.ConfirmToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            RequestNumber = requestMachinery!.RequestNumber,
            ContractorId = requestMachinery!.ContractorId,
            Contractor = contractorName,
            DriverId = requestMachinery!.DriverId,
            Driver = requestMachinery.DriverId is not null && requestMachinery.DriverId > 0 ? thirdParties?.FirstOrDefault(x => x?.Id == requestMachinery.DriverId)?.FullName : null,
            DriverName = requestMachinery.DriverName,
            UnitPrice = data.unitPrice,
            TotalPrice = data.totalPrice,
            OperationWork = data.operationWork
        };

        return response;
    }

    public async Task<Result<GetRequestMachineryHistoriesResponse?>> GetRequestMachineryHistoriesAsync(GetRequestMachineryHistoriesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryHistoriesValidator, GetRequestMachineryHistoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryHistoriesResponse>(isValidRequest.Error!);

        var getRequestMachinery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
        if (getRequestMachinery.IsFailure)
            return Result.Failure<GetRequestMachineryHistoriesResponse>(getRequestMachinery.Error!);
        var requestMachinery = getRequestMachinery.Value!;

        var allIds = IdCollectors(new List<RequestMachinery?>() { requestMachinery }).ToList();
        var thirdParties = await WebServicesLogic.UserDataReceiver(allIds, null, _mediator, ct);

        Company? company = null;
        if (requestMachinery.CompanyId is not null && requestMachinery.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestMachinery.CompanyId, _mediator, ct);

        var contractorId = requestMachinery.ContractorId;
        List<UserModel?>? contractors = [];
        if (contractorId is not null && contractorId > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([contractorId.Value], null, null, _mediator, ct);

        var model = new List<GetRequestMachineryHistoriesModel>();
        foreach (var history in requestMachinery.Histories)
        {
            OfficerModel? machineryInquiryOfficer = null;

            if (history.OperatorAppoinmentId != null)
            {
                var getMachineryInquiryOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(history.OperatorAppoinmentId, null, null, null, null, 1, 1), ct);
                machineryInquiryOfficer = getMachineryInquiryOfficers.Value?.Data?.FirstOrDefault()!;
            }

            Machinery? machinery = null;
            if (history.MachineryId is not null && history.MachineryId > 0)
            {
                var getMachinerybyId = await _mediator.Send(new GetMachineryByIdQuery(history.MachineryId.Value), ct);
                machinery = getMachinerybyId.Value;
            }

            model.Add(new GetRequestMachineryHistoriesModel()
            {
                Created = history.Created,
                Description = history.RequestDescription != null ? history.RequestDescription.ToString() : history.Description,
                Status = history.Status,
                Creator = thirdParties?.Where(x => x.UserId == history.CreatorId).FirstOrDefault()?.FullName,
                CreatorId = thirdParties?.Where(x => x.UserId == history.CreatorId).FirstOrDefault()?.UserId,
                Operator = machineryInquiryOfficer?.FullName,
                MachineryId = history.MachineryId,
                Machinery = machinery?.MachineryName,
                ConfirmDescription = history.ConfirmedDescription,
                ConfirmFromDate = history.ConfirmFromDate,
                ConfirmToDate = history.ConfirmToDate,
                ConfirmTimeRequired = history.ConfirmedTimeRequired,
                ContractorId = history.ContractorId,
                Contractor = contractors?.FirstOrDefault(x => x?.Id == history.ContractorId)?.FullName,
                ManagerDescription = history.ManagerDescription,
                RequestDescription = history.RequestDescription,
            });
        }

        return new GetRequestMachineryHistoriesResponse()
        {
            MachineryName = requestMachinery.Machinery.MachineryName,
            MachineriesGroupName = requestMachinery.Machinery.MachineriesGroup.GroupName,
            Data = model.OrderByDescending(x => x.Created).ToList(),
            RowCount = requestMachinery.Histories.Count,
            CompanyId = requestMachinery.CompanyId,
            CompanyNameFa = company?.NameFa,
            RequestNumber = requestMachinery.RequestNumber
        };
    }

    public async Task<Result<GetRequestMachineryStatusResponse?>> GetRequestMachineryStatusAsync(GetRequestMachineryStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestMachineryStatus>());
        return new GetRequestMachineryStatusResponse(response);
    }

    public async Task<Result<GetRequestMachineryUnitsResponse?>> GetRequestMachineryUnitsAsync(GetRequestMachineryUnitsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestMachineryUnit>());
        return new GetRequestMachineryUnitsResponse(response);
    }

    public async Task<Result<GetRequestMachineryProjectOperationResponse?>> GetRequestMachineryProjectOperationAsync(GetRequestMachineryProjectOperationRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryProjectOperationRequestValidator, GetRequestMachineryProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryProjectOperationResponse>(isValidRequest.Error!);

        var getProjectOperatio = await _mediator.Send(new GetRequestMachineryProjectOperationQuery(request.RequestMachineryId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getProjectOperatio.IsFailure)
            return Result.Failure<GetRequestMachineryProjectOperationResponse>(getProjectOperatio.Error!);
        var ProjectOperations = getProjectOperatio.Value!.Data!;

        var model = new List<ProjectOperationModel>();
        foreach (var item in ProjectOperations)
        {
            model.Add(new ProjectOperationModel()
            {
                Id = item.Id,
                ProjectOperationId = item.ProjectOperation.Id,
                OperationInfoCode = item.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = item.ProjectOperation.OperationInfo.OperationInfoName,
            });
        }
        return new GetRequestMachineryProjectOperationResponse(model, getProjectOperatio.Value!.RowCount!);
    }

    public async Task<Result<GetRequestMachineryProjectOperationDetailResponse?>> GetRequestMachineryProjectOperationDetailAsync(GetRequestMachineryProjectOperationDetailRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryProjectOperationDetailRequestValidator, GetRequestMachineryProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryProjectOperationDetailResponse>(isValidRequest.Error!);

        var getProjectOperationDetail = await _mediator.Send(new GetRequestMachineryProjectOperationDetailQuery(request.RequestMachineryId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getProjectOperationDetail.IsFailure)
            return Result.Failure<GetRequestMachineryProjectOperationDetailResponse>(getProjectOperationDetail.Error!);
        var ProjectOperationDetails = getProjectOperationDetail.Value!.Data!;

        var model = new List<ProjectOperationDetailModel>();
        foreach (var item in ProjectOperationDetails)
        {
            model.Add(new ProjectOperationDetailModel()
            {
                Id = item.Id,
                ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                PrivateName = item.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = item.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetail.OperationLocation.PublicCode,
            });
        }
        return new GetRequestMachineryProjectOperationDetailResponse(model, getProjectOperationDetail.Value!.RowCount!);
    }

    public async Task<Result<GetsRequestMachineryProjectOperationDetailResponse?>> GetsRequestMachineryProjectOperationDetail(GetsRequestMachineryProjectOperationDetailRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsRequestMachineryProjectOperationDetailValidator, GetsRequestMachineryProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsRequestMachineryProjectOperationDetailResponse>(isValidRequest.Error!);

        var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByProjectOperationQuery(request.RequestMachineryId, request.ProjectOperationId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getProjectOperationDetail.IsFailure)
            return Result.Failure<GetsRequestMachineryProjectOperationDetailResponse>(getProjectOperationDetail.Error!);
        var ProjectOperationDetails = getProjectOperationDetail.Value!.Data!;

        var model = new List<ProjectOperationDetailModel>();
        foreach (var item in ProjectOperationDetails)
        {
            model.Add(new ProjectOperationDetailModel()
            {
                Id = item.Id,
                ProjectOperationDetailId = item.ProjectOperationDetail.Id,
                PrivateName = item.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = item.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetail.OperationLocation.PublicCode,
            });
        }
        return new GetsRequestMachineryProjectOperationDetailResponse(model, getProjectOperationDetail.Value!.RowCount!);
    }

    public async Task<Result<GetsMachineryRequesteExcelExporterResponse?>> GetsMachineryRequesteExcelExporter(GetsMachineryRequesteExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineryRequesteExcelExporter, ProjectId:{ProjectId},", request.ProjectId);

        var isValidRequest = await request.IsValidAsync<GetsMachineryRequesteExcelExporterValidator, GetsMachineryRequesteExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMachineryRequesteExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFilteredRequestMachineriesQuery(
            request.Ids,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.OperationInfoIds,
            request.MachineriesGroupId,
            request.MachineryId,
            request.Status,
            request.PaymentType,
            request.FromDate,
            request.ToDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            null,
            null,
            null,
            request.DriverName,
            request.FilterData,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsMachineryRequesteExcelExporterResponse>(RequestMachineryErrors.FilteredMachineryRequestNotFound!);
        var values = response.Value!.Data!;

        List<FilteredUserResponseModel>? usersInfo = [];
        var userIds = values.Select(x => x.CreatorId).Distinct().ToList();
        userIds.AddRange(values.SelectMany(x => x.Histories.Where(x => x.Status == RequestMachineryStatus.Confirmed).Select(x => x.CreatorId)).Distinct().ToList());
        var creatorResponse = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);
        if (creatorResponse is not null && creatorResponse.Count > 0)
            usersInfo.AddRange(creatorResponse);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var contractorIds = values!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var data = response.Value.Data.Adapt<List<GetsMachineryRequesteExcelExporterResponseModel>>();
        foreach (var item in data)
        {
            var value = response.Value.Data.FirstOrDefault(x => x.Id == item.Id);

            item.Contractor = contractors?.FirstOrDefault(x => x is not null && x.Id == value?.ContractorId)?.FullName;
            item.FromDate = TimeCalculator.ConvertToShamsi(value!.FromDate);
            item.FromTime = value!.FromDate != null ? value!.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.ToDate = TimeCalculator.ConvertToShamsi(value!.ToDate);
            item.ToTime = value!.ToDate != null ? value!.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.ConfirmFromDate = TimeCalculator.ConvertToShamsi(value!.ConfirmFromDate);
            item.ConfirmFromTime = value!.ConfirmFromDate != null ? value!.ConfirmFromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.ConfirmToDate = TimeCalculator.ConvertToShamsi(value!.ConfirmToDate);
            item.ConfirmToTime = value!.ConfirmToDate != null ? value!.ConfirmToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0);
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            item.Creator = usersInfo.Where(x => x.UserId == item.CreatorId).FirstOrDefault()?.FullName;
            if (item.ConfirmUserId is not null && item.ConfirmUserId != 0)
                item.ConfirmUser = usersInfo.Where(x => x.UserId == item.CreatorId).FirstOrDefault()?.FullName;
            item.TotalPrice = value.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.TotalPrice;
            item.UnitPrice = value.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.UnitPrice;
        }

        var file = new FileContentResult(MachineryRequesteExcels.RequestMachineriesToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestMachineries-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsMachineryRequesteExcelExporterResponse(file);
    }

    public async Task<Result<GetsRequestMachineryExcelEnumsResponse?>> GetsRequestMachineryExcelEnums(GetsRequestMachineryExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GetsRequestMachineryExcelEnum>());
        return new GetsRequestMachineryExcelEnumsResponse(response);
    }

    public async Task<Result<GetManagerConfirmMachineryReportsResponse?>> GetManagerConfirmMachineryReports(GetManagerConfirmMachineryReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetManagerConfirmMachineryReportsValidator, GetManagerConfirmMachineryReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetManagerConfirmMachineryReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetManagerConfirmMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetManagerConfirmMachineryReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        if (histories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        allIds.AddRange(IdCollectors(requestMachineries!).Distinct().ToList());
        var userInfo = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(CurrencyIdCollectors(requestMachineries!).Distinct().ToList());
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultcurrency = currencyQuery.Value;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(thirdPartyIdCollectors(requestMachineries!).Distinct().ToList());
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<Guid>? prefrentialGuids = thirdParties?.Where(x => x?.PreferentialReferenceCode != Guid.Empty || x.PreferentialReferenceCode != null)
            .Select(x => (Guid)x?.PreferentialReferenceCode!).Distinct().ToList();
        List<FilteredPreferentialModel>? prefrentials = [];
        if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
        {
            var response = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
            prefrentials = response.Value?.Data;
        }
        var detailModels = CreateDetailResponseModel(requestMachineries, histories, userInfo, currencyInfo, thirdParties, companies, defaultcurrency.Adapt<Currency>())
            .Adapt<List<GetManagerConfirmMachineryDetailReportsModel>>();
        var models = CreateManagerConfirmReportsResponseModel(detailModels, thirdParties, prefrentials);

        return new GetManagerConfirmMachineryReportsResponse(models ?? new List<GetManagerConfirmMachineryReportsModel>(0), getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetSendManagerMachineryReportsResponse?>> GetSendManagerMachineryReports(GetSendManagerMachineryReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetSendManagerMachineryReportsValidator, GetSendManagerMachineryReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSendManagerMachineryReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetSendManagerMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetSendManagerMachineryReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        if (histories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        allIds.AddRange(IdCollectors(requestMachineries!).Distinct().ToList());
        var userInfo = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(CurrencyIdCollectors(requestMachineries!).Distinct().ToList());
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultcurrency = currencyQuery.Value;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(thirdPartyIdCollectors(requestMachineries!).Distinct().ToList());
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<Guid>? prefrentialGuids = thirdParties?.Where(x => x?.PreferentialReferenceCode != Guid.Empty || x.PreferentialReferenceCode != null)
            .Select(x => (Guid)x?.PreferentialReferenceCode!).Distinct().ToList();
        List<FilteredPreferentialModel>? prefrentials = [];
        if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
        {
            var response = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
            prefrentials = response.Value?.Data;
        }
        var detailModels = CreateSendManagerDetailResponseModel(requestMachineries, histories, userInfo, currencyInfo, thirdParties, companies, defaultcurrency.Adapt<Currency>());
        var models = CreateSendManagerReportsResponseModel(detailModels, thirdParties, prefrentials);

        return new GetSendManagerMachineryReportsResponse(models ?? new List<GetSendManagerMachineryReportsModel>(0), getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetTotalSendManagerMachineryReportsResponse?>> GetTotalSendManagerMachineryReports(GetTotalSendManagerMachineryReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetTotalSendManagerMachineryReportsValidator, GetTotalSendManagerMachineryReportsRequest>(ct);
        if (isValidRequest.IsFailure) return Result.Failure<GetTotalSendManagerMachineryReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetSendManagerMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            null,
            companyId,
            0,
            0), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetTotalSendManagerMachineryReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var detailModels = CreateSendManagerDetailResponseModel(requestMachineries, null, null, null, null, null, null);

        var models = CreateSendManagerReportsResponseModel(detailModels, null, null);

        TimeSpan totalTimeSpan = TimeSpan.Zero;
        foreach (var timeSpan in models.Select(x => x.TotalTimeWork).ToList())
        {
            if (timeSpan.HasValue && timeSpan is not null)
                totalTimeSpan = totalTimeSpan.Add(timeSpan.Value);
        }

        return new GetTotalSendManagerMachineryReportsResponse
        {
            TotalCount = models.Sum(x => x.TotalCount),
            TotalDayWork = models.Sum(x => x.TotalDayWork),
            TotalFinalPrice = models.Sum(x => x.TotalFinalPrice),
            TotalRequestedCount = models.Sum(x => x.TotalRequestedCount),
            TotalTimeRequired = models.Sum(x => x.TotalTimeRequired),
            TotalTimeWork = totalTimeSpan,
        };
    }

    public async Task<Result<GetOnProjectMachineryReportsResponse?>> GetOnProjectMachineryReports(GetOnProjectMachineryReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetOnProjectMachineryReportsValidator, GetOnProjectMachineryReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOnProjectMachineryReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetOnProjectMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetOnProjectMachineryReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        if (histories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        allIds.AddRange(IdCollectors(requestMachineries!).Distinct().ToList());
        var userInfo = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(CurrencyIdCollectors(requestMachineries!).Distinct().ToList());
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultcurrency = currencyQuery.Value;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(thirdPartyIdCollectors(requestMachineries!).Distinct().ToList());
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<Guid>? prefrentialGuids = thirdParties?.Where(x => x?.PreferentialReferenceCode != Guid.Empty || x.PreferentialReferenceCode != null)
            .Select(x => (Guid)x?.PreferentialReferenceCode!).Distinct().ToList();
        List<FilteredPreferentialModel>? prefrentials = [];
        if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
        {
            var response = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
            prefrentials = response.Value?.Data;
        }
        var detailModels = CreateDetailResponseModel(requestMachineries, histories, userInfo, currencyInfo, thirdParties, companies, defaultcurrency.Adapt<Currency>());
        var models = CreateReportsResponseModel(detailModels, thirdParties, prefrentials);

        return new GetOnProjectMachineryReportsResponse(models ?? new List<GetOnProjectMachineryReportsModel>(0), getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetOnProjectMachineryReportsExcelExporterResponse?>> GetOnProjectMachineryReportsExcelExporter(GetOnProjectMachineryReportsExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetOnProjectMachineryReportsExcelExporterValidator, GetOnProjectMachineryReportsExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOnProjectMachineryReportsExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getFiltered = await _mediator.Send(new GetOnProjectMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetOnProjectMachineryReportsExcelExporterResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        if (histories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        allIds.AddRange(IdCollectors(requestMachineries!).Distinct().ToList());
        var userInfo = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(CurrencyIdCollectors(requestMachineries!).Distinct().ToList());
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultcurrency = currencyQuery.Value;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(thirdPartyIdCollectors(requestMachineries!).Distinct().ToList());
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var detailModels = CreateDetailResponseModel(requestMachineries, histories, userInfo, currencyInfo, thirdParties, companies, defaultcurrency.Adapt<Currency>());

        var models = CreateReportsResponseModel(detailModels, null, null);

        var detailExcel = detailModels.Adapt<List<GetOnProjectMachineryDetailReportsExcelExporterModel>>();
        var modelsExcel = models.Adapt<List<GetOnProjectMachineryReportsExcelExporterModel>>();

        var file = new FileContentResult(MachineryRequesteExcels.RequestMachineriesReportsToExcel(modelsExcel, detailExcel, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestMachineriesReports-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetOnProjectMachineryReportsExcelExporterResponse(file);
    }

    public async Task<Result<GetOnProjectMachineryReportsExcelEnumsResponse?>> GetOnProjectMachineryReportsExcelEnums(GetOnProjectMachineryReportsExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GetsOnProjectRequestMachineryExcelEnum>());
        return new GetOnProjectMachineryReportsExcelEnumsResponse(response);
    }

    public async Task<Result<GetTotalOnProjectMachineryReportsResponse?>> GetTotalOnProjectMachineryReports(GetTotalOnProjectMachineryReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetTotalOnProjectMachineryReportsValidator, GetTotalOnProjectMachineryReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalOnProjectMachineryReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetOnProjectMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            null,
            companyId,
            0,
            0), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetTotalOnProjectMachineryReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var detailModels = CreateDetailResponseModel(requestMachineries, null, null, null, null, null, null);

        var models = CreateReportsResponseModel(detailModels, null, null);

        TimeSpan totalTimeSpan = TimeSpan.Zero;
        foreach (var timeSpan in models.Select(x => x.TotalTimeWork).ToList())
        {
            if (timeSpan.HasValue && timeSpan is not null)
                totalTimeSpan = totalTimeSpan.Add(timeSpan.Value);
        }

        return new GetTotalOnProjectMachineryReportsResponse
        {
            TotalCount = models.Sum(x => x.TotalCount),
            TotalDayWork = models.Sum(x => x.TotalDayWork),
            TotalFinalPrice = models.Sum(x => x.TotalFinalPrice),
            TotalRequestedCount = models.Sum(x => x.TotalRequestedCount),
            TotalTimeRequired = models.Sum(x => x.TotalTimeRequired),
            TotalTimeWork = totalTimeSpan,
        };
    }

    public async Task<Result<GetTotalManagerConfirmMachineryReportsResponse?>> GetTotalManagerConfirmMachineryReports(GetTotalManagerConfirmMachineryReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetTotalManagerConfirmMachineryReportsValidator, GetTotalManagerConfirmMachineryReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalManagerConfirmMachineryReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetManagerConfirmMachineryReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            null,
            companyId,
            0,
            0), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetTotalManagerConfirmMachineryReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var detailModels = CreateDetailResponseModel(requestMachineries, null, null, null, null, null, null);

        var models = CreateReportsResponseModel(detailModels, null, null);

        TimeSpan totalTimeSpan = TimeSpan.Zero;
        foreach (var timeSpan in models.Select(x => x.TotalTimeWork).ToList())
        {
            if (timeSpan.HasValue && timeSpan is not null)
                totalTimeSpan = totalTimeSpan.Add(timeSpan.Value);
        }

        return new GetTotalManagerConfirmMachineryReportsResponse
        {
            TotalCount = models.Sum(x => x.TotalCount),
            TotalDayWork = models.Sum(x => x.TotalDayWork),
            TotalFinalPrice = models.Sum(x => x.TotalFinalPrice),
            TotalRequestedCount = models.Sum(x => x.TotalRequestedCount),
            TotalTimeRequired = models.Sum(x => x.TotalTimeRequired),
            TotalTimeWork = totalTimeSpan,
        };
    }

    public async Task<Result<GetOnProjectRequestReportsResponse?>> GetOnProjectRequestReports(GetOnProjectRequestReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetOnProjectRequestReportsValidator, GetOnProjectRequestReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOnProjectRequestReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetOnProjectRequestReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.PaymentType,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize),
            ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetOnProjectRequestReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        if (histories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        allIds.AddRange(IdCollectors(requestMachineries!).Distinct().ToList());
        var userInfo = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(CurrencyIdCollectors(requestMachineries!).Distinct().ToList());
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultcurrency = currencyQuery.Value;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(thirdPartyIdCollectors(requestMachineries!).Distinct().ToList());
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var models = CreateResponseModel(requestMachineries, histories, userInfo, currencyInfo, thirdParties, companies, defaultcurrency.Adapt<Currency>());

        return new GetOnProjectRequestReportsResponse(models ?? new List<GetOnProjectRequestReportsModel>(0), getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetOnProjectRequestReportsExcelExporterResponse?>> GetOnProjectRequestReportsExcelExporter(GetOnProjectRequestReportsExcelExporterRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetOnProjectRequestReportsExcelExporterValidator, GetOnProjectRequestReportsExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOnProjectRequestReportsExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getFiltered = await _mediator.Send(new GetOnProjectRequestReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.PaymentType,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetOnProjectRequestReportsExcelExporterResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var histories = requestMachineries.SelectMany(x => x.Histories).Where(c => c.Status == RequestMachineryStatus.Confirmed).ToList();

        var companyIds = requestMachineries?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<long>? allIds = [];
        if (histories is not null)
            allIds.AddRange(RequestMachineryHistoryIdCollectors(histories!).ToList());

        allIds.AddRange(IdCollectors(requestMachineries!).Distinct().ToList());
        var userInfo = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(CurrencyIdCollectors(requestMachineries!).Distinct().ToList());
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var currencyQuery = await _mediator.Send(new GetDefaultCurrencyQuery(), ct);
        var defaultcurrency = currencyQuery.Value;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(thirdPartyIdCollectors(requestMachineries!).Distinct().ToList());
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var models = CreateResponseModel(requestMachineries, histories, userInfo, currencyInfo, thirdParties, companies, defaultcurrency.Adapt<Currency>());

        var modelsExcel = models.Adapt<List<GetOnProjectRequestReportsExcelExporterModel>>();

        var file = new FileContentResult(MachineryRequesteExcels.RequestMachineriesReportsToExcel(modelsExcel, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"MachineriesOnProjectReports-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetOnProjectRequestReportsExcelExporterResponse(file);
    }

    public async Task<Result<GetOnProjectRequestReportsExcelEnumsResponse?>> GetOnProjectRequestReportsExcelEnums(GetOnProjectRequestReportsExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GetsOnProjectRequestExcelEnum>());
        return new GetOnProjectRequestReportsExcelEnumsResponse(response);
    }

    public async Task<Result<GetTotalOnProjectRequestReportsResponse?>> GetTotalOnProjectRequestReports(GetTotalOnProjectRequestReportsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetTotalOnProjectRequestReportsValidator, GetTotalOnProjectRequestReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTotalOnProjectRequestReportsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 ? null : _userInfoService.UserCompanyId;

        var getFiltered = await _mediator.Send(new GetOnProjectRequestReportsQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ContractorId,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.MachineryIds,
            request.Unit,
            request.PaymentType,
            request.StartDate,
            request.EndDate,
            request.ConfirmedFromDate,
            request.ConfirmedToDate,
            request.FilterData,
            request.OwnCompany,
            null,
            companyId,
            0,
            0), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetTotalOnProjectRequestReportsResponse>(getFiltered.Error!);
        var requestMachineries = getFiltered.Value!.Data!;

        var models = CreateResponseModel(requestMachineries, null, null, null, null, null, null);

        var totals = CalculateTotals(models);
        return new GetTotalOnProjectRequestReportsResponse
        {
            TotalDayWork = totals.TotalDayWork,
            TotalFinalPrice = models.Sum(x => x.TotalPrice),
            TotalRequestedCount = models.Sum(x => x.RequestCount),
            TotalTimeWork = totals.TotalTimeWork,
            TotalTimeRequired = totals.TotalHour
        };
    }

}
