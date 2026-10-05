using Engineering.Application.Extensions.Excels.LetterheadExcel;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.EmployerStatusStatements.Commands.ChangeEmployerStatusStatementStatus;
using Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatement;
using Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperation;
using Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperationDetailDaily;
using Engineering.Application.Services.EmployerStatusStatements.Commands.EmployerStatusStatementCodeCreator;
using Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateEmployerStatusStatementDocuments;
using Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyDocuments;
using Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDocuments;
using Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationsZeroVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;
using Engineering.Application.Services.EmployerStatusStatements.Models.CreateEmployerStatusStatement;
using Engineering.Application.Services.EmployerStatusStatements.Models.EmployerStatusStatementCodeCreator;
using Engineering.Application.Services.EmployerStatusStatements.Models.EmployerStatusStatementStatus;
using Engineering.Application.Services.EmployerStatusStatements.Models.EmployerStatusStatementType;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementById;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelEnums;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementHistory;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperation;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetail;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailDaily;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerRequester;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerStatusStatement;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateEmployerStatusStatementDocuments;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailiesDocument;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsDocument;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementById;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementByIdForDocs;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementLetterheadExcel;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementHistory;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperation;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperationDetail;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperationDetailDaily;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsESSProjectOperationByIds;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsESSProjectOperationDetailDailyByIds;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsFilteredEmployerRequester;
using Engineering.Application.Services.EmployerStatusStatements.Queries.GetsFilteredEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIds;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.EmployerStatusStatements;

public class EmployerStatusStatementLogic : IEmployerStatusStatementLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmployerStatusStatementLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    public EmployerStatusStatementLogic(
        IMediator mediator,
        ILogger<EmployerStatusStatementLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateEmployerStatusStatementResponse?>> CreateEmployerStatusStatement(
        CreateEmployerStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateEmployerStatusStatement, ProjectId:{ProjectId}, StartDate:{StartDate}, EndDate:{EndDate}, ", request.ProjectId, request.StartDate, request.EndDate);

        var isValidRequest = await request.IsValidAsync<CreateEmployerStatusStatementValidator, CreateEmployerStatusStatementRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateEmployerStatusStatementResponse>(isValidRequest.Error!);

        if (request.EndDate < request.StartDate)
            return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.DateTimeNotValid);
        if (request.StartDate > DateTime.Now.Date || request.EndDate > DateTime.Now.Date)
            return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.DateTimeNotValidToDay);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateEmployerStatusStatementResponse>(companyResponse.Error!);
        }

        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (project.IsFailure || project.Value is null)
            return Result.Failure<CreateEmployerStatusStatementResponse>(ProjectErrors.ProjectWithIdNotFound);

        ///TODO Employers
        //var employerContract = await _mediator.Send(new GetEmployerContractWithoutIncludeQuery(request.EmployerContractId), ct);
        //if (employerContract.IsFailure || employerContract.Value is null)
        //    return Result.Failure<CreateEmployerStatusStatementResponse>(EContractErrors.EmployerContractWithIdNotFound);
        //var contractValue = employerContract.Value;

        var projectOperationIds = request.ProjectOperations.Select(x => x.Id).ToList();
        var projectOperationQueries = await _mediator.Send(new GetsProjectOperationByIdsQuery(projectOperationIds, null, companyId, 1, projectOperationIds.Count), ct);
        if (projectOperationQueries.IsFailure || projectOperationQueries.Value?.Data is null || projectOperationQueries.Value?.Data.Count <= 0)
            return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.ProjectOperationIdsNotValid);
        var projectOperations = projectOperationQueries.Value!.Data;
        ///TODO Employers
        //if (projectOperations.All(x => x.EmployerOperations is null))
        //    return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.ProjectOperationIdsNotValid);

        //var contractedProjectOperations = projectOperations.Where(x => x.EmployerOperations is not null).ToList();
        //if (contractedProjectOperations.Any(x => x.EmployerOperations! != contractValue))
        //    return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.UnvalidContractNotValid);

        EmployerStatusStatement? lastStatement = null;
        if (request.LastEmployerStatusStatementId is not null)
        {
            var findLastStatusStatement = await _mediator.Send(new GetEmployerStatusStatementByIdQuery((long)request.LastEmployerStatusStatementId!), ct);
            if (findLastStatusStatement.IsFailure || findLastStatusStatement.Value is null)
                return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.LastEmployerStatusStatementInValid);
            if (request.StartDate < findLastStatusStatement.Value.EndDate && findLastStatusStatement.Value.Status != EmployerStatusStatementStatus.Invalidated)
                return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.DateTimeOfLastEmployerStatusStatementNotValid);
            lastStatement = findLastStatusStatement.Value;
        }

        var statusStatementCode = $"{project.Value.ProjectCode}";
        if (lastStatement is not null)
            statusStatementCode = $"{project.Value.ProjectCode}-{lastStatement.Id}";

        ///TODO Employers
        var entity = new EmployerStatusStatement(null, project.Value, lastStatement,
            request.StartDate, request.EndDate, 0, 0, 0, statusStatementCode, request.Description, companyId);

        var statementProjectOperations = new List<EmployerStatusStatementProjectOperation>();
        foreach (var projectOperation in projectOperations)
        {
            var requestedPO = request.ProjectOperations.First(x => x.Id == projectOperation.Id);
            if (projectOperation.Price is null && requestedPO.UnitConfirmePrice is null)
                return Result.Failure<CreateEmployerStatusStatementResponse>(EmployerStatusStatementErrors.PONoHavePrice);

            var entityProjectOperation = new EmployerStatusStatementProjectOperation(entity, projectOperation, requestedPO.UnitConfirmePrice, 0, projectOperation.Description);
            if (projectOperation.ProjectOperationDetails.Any() == false)
                continue;

            var statementProjectOperationDetails = new List<EmployerStatusStatementProjectOperationDetail>();
            foreach (var projectOperationDetail in projectOperation.ProjectOperationDetails)
            {
                var getDaily = projectOperationDetail.DailyOperations.Where(oo => oo.StartDate.Date >= request.StartDate.Date && oo.StartDate.Date <= request.EndDate.Date).ToList();
                if (!getDaily.Any())
                    continue;

                var entityProjectOperationDetail = new EmployerStatusStatementProjectOperationDetail(entityProjectOperation, projectOperationDetail, projectOperation.Description);

                var statementProjectOperationDetailDailies = new List<EmployerStatusStatementProjectOperationDetailDaily>();
                foreach (var projectOperationDetailDaily in projectOperationDetail.DailyOperations)
                {
                    var responseProjectOperationDetailDaily = await _mediator.Send(new CreateEmployerStatusStatementProjectOperationDetailDailyCommand(entityProjectOperationDetail, projectOperationDetailDaily), ct);
                    if (responseProjectOperationDetailDaily.IsFailure)
                        return Result.Failure<CreateEmployerStatusStatementResponse>(responseProjectOperationDetailDaily.Error!);
                    statementProjectOperationDetailDailies.Add(responseProjectOperationDetailDaily.Value!);
                }

                entityProjectOperationDetail.SetContractorWorkVolume();
                entityProjectOperationDetail.SetTotalDailyWorkVolume();
                entityProjectOperationDetail.SetSupervisorWorkVolume();
                entityProjectOperationDetail.SetConsultantWorkVolume();
                entityProjectOperationDetail.SetEmployerRepresentativeWorkVolume();
                statementProjectOperationDetails.Add(entityProjectOperationDetail);
            }

            entityProjectOperation.SetTotalDetailWorkVolume();
            entityProjectOperation.SetTotalDailyWorkVolume();
            entityProjectOperation.SetContractorWorkVolume();
            entityProjectOperation.SetSupervisorWorkVolume();
            entityProjectOperation.SetConsultantWorkVolume();
            entityProjectOperation.SetEmployerRepresentativeWorkVolume();

            var responseProjectOperation = await _mediator.Send(new CreateEmployerStatusStatementProjectOperationCommand(entityProjectOperation), ct);
            if (responseProjectOperation.IsFailure)
                return Result.Failure<CreateEmployerStatusStatementResponse>(responseProjectOperation.Error!);
            statementProjectOperations.Add(responseProjectOperation.Value!);
        }

        var totalWorkVolume = statementProjectOperations.Sum(oo => oo.TotalWorkVolume);
        var doneWorkVolume = statementProjectOperations.Sum(oo => oo.ContractorWorkVolume);
        var statementPercentageOfWorkDone = (doneWorkVolume / totalWorkVolume) * 100;
        var statementCalculatedAmount = statementProjectOperations.Sum(oo => oo.ContractorWorkVolume);

        entity.SetPercentageOfWorkDone(Math.Round(statementPercentageOfWorkDone, 8));
        entity.SetStatusStatementVolume(Math.Round(doneWorkVolume, 8));
        entity.SetCalculatedAmount(Math.Round((decimal)statementCalculatedAmount, 8));

        var response = await _mediator.Send(new CreateEmployerStatusStatementCommand(entity), ct);
        if (response.IsFailure)
            return Result.Failure<CreateEmployerStatusStatementResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateEmployerStatusStatementResponse(response.Value!.Id, true);
    }

    public async Task<Result<ChangeESSStatusResponse?>> ChangeESSStatus(
        ChangeESSStatusRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeEmployerStatusStatementStatus, id:{Id},", request.Id);

        var isValidRequest = await request.IsValidAsync<ChangeESSStatusValidator, ChangeESSStatusRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ChangeESSStatusResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ChangeEmployerStatusStatementStatusCommand(
            request.Id,
            request.Status,
            request.ESSProjectOperations,
            request.ESSProjectOperationDetailDailies,
            request.Description), ct);
        if (response.IsFailure)
            return Result.Failure<ChangeESSStatusResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new ChangeESSStatusResponse(response.Value!.Id, (int)response.Value!.Status, response.Value!.Status.GetEnumDescription());
    }

    public async Task<Result<UpdateEmployerStatusStatementDocumentsResponse?>> UpdateEmployerStatusStatementDocuments(
        UpdateEmployerStatusStatementDocumentsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeEmployerStatusStatementStatus, id:{Id},", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateEmployerStatusStatementDocumentsValidator, UpdateEmployerStatusStatementDocumentsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateEmployerStatusStatementDocumentsResponse>(isValidRequest.Error!);

        var getResponse = await _mediator.Send(new GetEmployerStatusStatementByIdForDocsQuery(request.Id), ct);
        if (getResponse.IsFailure)
            return Result.Failure<UpdateEmployerStatusStatementDocumentsResponse>(getResponse.Error!);
        var value = getResponse.Value!;
        var response = await _mediator.Send(new UpdateEmployerStatusStatementDocumentsCommand(
            value,
            request.Urls), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateEmployerStatusStatementDocumentsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new UpdateEmployerStatusStatementDocumentsResponse(true);
    }

    public async Task<Result<UpdateESSProjectOperationsDocumentResponse?>> UpdateESSProjectOperationsDocument(
        UpdateESSProjectOperationsDocumentRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateESSProjectOperationsDocument");

        var isValidRequest = await request.IsValidAsync<UpdateESSProjectOperationsDocumentValidator, UpdateESSProjectOperationsDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateESSProjectOperationsDocumentResponse>(isValidRequest.Error!);

        var ids = request.ESSProjectOperations.Select(x => x.Id).Distinct().ToList();
        var getResponse = await _mediator.Send(new GetsESSProjectOperationByIdsQuery(ids), ct);
        if (getResponse.IsFailure)
            return Result.Failure<UpdateESSProjectOperationsDocumentResponse>(getResponse.Error!);
        var values = getResponse.Value!.Data!;

        foreach (var item in values)
        {
            var urls = request.ESSProjectOperations.FirstOrDefault(x => x.Id == item.Id)!.Urls;
            var response = await _mediator.Send(new UpdateESSProjectOperationDocumentsCommand(
                item,
                urls), ct);
            if (response.IsFailure)
                return Result.Failure<UpdateESSProjectOperationsDocumentResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new UpdateESSProjectOperationsDocumentResponse(true);
    }

    public async Task<Result<UpdateESSProjectOperationsZeroVolumeResponse?>> UpdateESSProjectOperationsZeroVolume(
        UpdateESSProjectOperationsZeroVolumeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateESSProjectOperationsZeroVolume");

        var isValidRequest = await request.IsValidAsync<UpdateESSProjectOperationsZeroVolumeValidator, UpdateESSProjectOperationsZeroVolumeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateESSProjectOperationsZeroVolumeResponse>(isValidRequest.Error!);

        var ids = request.ESSProjectOperations.Select(x => x.Id).Distinct().ToList();
        var getResponse = await _mediator.Send(new GetsESSProjectOperationByIdsQuery(ids), ct);
        if (getResponse.IsFailure)
            return Result.Failure<UpdateESSProjectOperationsZeroVolumeResponse>(getResponse.Error!);
        var values = getResponse.Value!.Data!;

        foreach (var item in values)
        {
            var requestOP = request.ESSProjectOperations.FirstOrDefault(x => x.Id == item.Id)!;
            var response = await _mediator.Send(new UpdateESSProjectOperationsZeroVolumeCommand(
                item,
                requestOP.Urls,
                requestOP.Description), ct);
            if (response.IsFailure)
                return Result.Failure<UpdateESSProjectOperationsZeroVolumeResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new UpdateESSProjectOperationsZeroVolumeResponse(true);
    }

    public async Task<Result<UpdateESSProjectOperationDetailDailyVolumeResponse?>> UpdateESSProjectOperationDetailDailyVolume(
        UpdateESSProjectOperationDetailDailyVolumeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateESSProjectOperationDetailDailyVolume");

        var isValidRequest = await request.IsValidAsync<UpdateESSProjectOperationDetailDailyVolumeValidator, UpdateESSProjectOperationDetailDailyVolumeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateESSProjectOperationDetailDailyVolumeResponse>(isValidRequest.Error!);

        var ids = request.ESSProjectOperationDailies.Select(x => x.Id).Distinct().ToList();
        var getResponse = await _mediator.Send(new GetsESSProjectOperationDetailDailyByIdsQuery(ids), ct);
        if (getResponse.IsFailure)
            return Result.Failure<UpdateESSProjectOperationDetailDailyVolumeResponse>(getResponse.Error!);
        var values = getResponse.Value!.Data!;

        foreach (var item in values)
        {
            var requestDaily = request.ESSProjectOperationDailies.FirstOrDefault(x => x.Id == item.Id)!;
            var response = await _mediator.Send(new UpdateESSProjectOperationDetailDailyVolumeCommand(
                item,
                requestDaily.Urls, requestDaily.Length, requestDaily.Width, requestDaily.Height, requestDaily.Weight, requestDaily.Number, requestDaily.Description), ct);
            if (response.IsFailure)
                return Result.Failure<UpdateESSProjectOperationDetailDailyVolumeResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new UpdateESSProjectOperationDetailDailyVolumeResponse(true);
    }

    public async Task<Result<UpdateESSProjectOperationDetailDailiesDocumentResponse?>> UpdateESSProjectOperationDetailDailiesDocument(
        UpdateESSProjectOperationDetailDailiesDocumentRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateESSProjectOperationDetailDailiesDocument");

        var isValidRequest = await request.IsValidAsync<UpdateESSProjectOperationDetailDailiesDocumentValidator, UpdateESSProjectOperationDetailDailiesDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateESSProjectOperationDetailDailiesDocumentResponse>(isValidRequest.Error!);

        var ids = request.ESSProjectOperationDailies.Select(x => x.Id).Distinct().ToList();
        var getResponse = await _mediator.Send(new GetsESSProjectOperationDetailDailyByIdsQuery(ids), ct);
        if (getResponse.IsFailure)
            return Result.Failure<UpdateESSProjectOperationDetailDailiesDocumentResponse>(getResponse.Error!);
        var values = getResponse.Value!.Data!;

        foreach (var item in values)
        {
            var urls = request.ESSProjectOperationDailies.FirstOrDefault(x => x.Id == item.Id)!.Urls;
            var response = await _mediator.Send(new UpdateESSProjectOperationDetailDailyDocumentsCommand(
                item,
                urls), ct);
            if (response.IsFailure)
                return Result.Failure<UpdateESSProjectOperationDetailDailiesDocumentResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new UpdateESSProjectOperationDetailDailiesDocumentResponse(true);
    }

    public async Task<Result<EmployerStatusStatementCodeCreatorResponse?>> EmployerStatusStatementCodeCreator(
        EmployerStatusStatementCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for EmployerStatusStatementCodeCreator");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<EmployerStatusStatementCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new EmployerStatusStatementCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<EmployerStatusStatementCodeCreatorResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new EmployerStatusStatementCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<GetEmployerStatusStatementByIdResponse?>> GetEmployerStatusStatementById(
        GetEmployerStatusStatementByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetEmployerStatusStatementById");

        var isValidRequest = await request.IsValidAsync<GetEmployerStatusStatementByIdValidator, GetEmployerStatusStatementByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetEmployerStatusStatementByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetEmployerStatusStatementByIdQuery(request.Id), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetEmployerStatusStatementByIdResponse>(EmployerStatusStatementErrors.DataNotFound!);
        var responseValue = response.Value;

        Company? company = null;
        if (responseValue.CompanyId is not null && responseValue.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(responseValue.CompanyId, _mediator, ct);

        var currencyData = await WebServicesLogic.CurrenciesDataReceiver([responseValue.EmployerContract.EmployerContractHead.CurrencyId], _mediator, ct);
        List<UserModel?>? employerData = null;
        if (responseValue.Project.EmployerId is not null)
            employerData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([responseValue.Project.EmployerId.Value], null, null, _mediator, ct);


        var data = response.Value.Adapt<GetEmployerStatusStatementByIdResponse>();
        data.CurrencyName = currencyData?.Where(x => x.Id == responseValue.EmployerContract.EmployerContractHead.CurrencyId).Select(x => x.Name).FirstOrDefault();
        data.EmployerName = employerData?.Where(x => x?.Id == responseValue.Project.EmployerId).Select(x => x?.FullName).FirstOrDefault();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetsFilteredEmployerStatusStatementResponse?>> GetsFilteredEmployerStatusStatement(
        GetsFilteredEmployerStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFilteredEmployerStatusStatement, ProjectId:{ProjectId}, CostCenterId:{CostCenterId},", request.ProjectId, request.CostCenterId);

        var isValidRequest = await request.IsValidAsync<GetsFilteredEmployerStatusStatementValidator, GetsFilteredEmployerStatusStatementRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredEmployerStatusStatementResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredEmployerStatusStatementQuery(request.EmployerId, request.CostCenterId, request.ProjectId, request.EmployerContractId,
            request.StatusStatementCode, request.ProjectOperationIds, request.StartDate, request.EndDate, request.Status, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredEmployerStatusStatementResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsFilteredEmployerStatusStatementResponse>(EmployerStatusStatementErrors.DataNotFound!);
        var values = response.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = values?.Select(x => x.EmployerContract.EmployerContractHead.CurrencyId).Distinct().ToList();
        var currencyData = await WebServicesLogic.CurrenciesDataReceiver(allIds, _mediator, ct);

        var allEmployerIds = values?.NullListed(x => x.Project.EmployerId);
        var employerData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allEmployerIds, null, null, _mediator, ct);

        var data = values.Adapt<List<GetsFilteredEmployerStatusStatementResponseModel>>();
        foreach (var item in data)
        {
            item.CurrencyName = currencyData?.Where(x => x.Id == item.CurrencyId).Select(x => x.Name).FirstOrDefault();
            item.EmployerName = employerData?.Where(x => x?.Id == item.EmployerId).Select(x => x?.FullName).FirstOrDefault();
            item.CompanyNameFa = companies?.Where(x => x?.Id == item.CompanyId).Select(x => x.NameFa).FirstOrDefault();
        }
        return new GetsFilteredEmployerStatusStatementResponse(data ?? new List<GetsFilteredEmployerStatusStatementResponseModel>(0), response?.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredEmployerRequesterResponse?>> GetsFilteredEmployerRequester(
        GetsFilteredEmployerRequesterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestRequesters");

        var isValidRequest = await request.IsValidAsync<GetsFilteredEmployerRequesterRequestValidator, GetsFilteredEmployerRequesterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredEmployerRequesterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredEmployerRequesterQuery(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredEmployerRequesterResponse>(response.Error!);
        if (response is null)
            return Result.Failure<GetsFilteredEmployerRequesterResponse>(EmployerStatusStatementErrors.RequesterIdsAreEmpty);
        var value = response!.Value!;

        List<FilteredUserModel?> employers = new();
        var data = new List<GetsFilteredEmployerRequesterResponseModel>();
        if (value.Data?.Count > 0)
        {
            var responseValue = await WebServicesLogic.GetFilteredByIdsDataReceiver(value.Data!, request.FilterData, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                employers.AddRange(responseValue);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in value.Data!)
                {
                    if (!employers.Any(x => x?.Id == id))
                        continue;

                    var requester = employers.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetsFilteredEmployerRequesterResponseModel()
                    {
                        Id = id,
                        FirstName = requester?.FirstName,
                        LastName = requester?.LastName,
                        DefaultPhoneNo = requester?.DefaultPhoneNo,
                        OrganizationCode = requester?.OrganizationCode,
                        IdentityNo = requester?.IdentityNo
                    });
                }
            }
            else
            {
                foreach (var id in value.Data!)
                {
                    var requester = employers.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetsFilteredEmployerRequesterResponseModel()
                    {
                        Id = id,
                        FirstName = requester?.FirstName,
                        LastName = requester?.LastName,
                        DefaultPhoneNo = requester?.DefaultPhoneNo,
                        OrganizationCode = requester?.OrganizationCode,
                        IdentityNo = requester?.IdentityNo
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsFilteredEmployerRequesterResponse(responseData ?? new List<GetsFilteredEmployerRequesterResponseModel>(0), employers?.Count ?? 0);

    }

    public async Task<Result<GetsEmployerStatusStatementStatusResponse?>> GetsEmployerStatusStatementStatus(
        GetsEmployerStatusStatementStatusRequest request, CT ct)
    {
        _logger.LogInformation("Request for EmployerStatusStatementStatus");

        return await Task.FromResult(new GetsEmployerStatusStatementStatusResponse(EnumExt.GetEnumObjectList<EmployerStatusStatementStatus>()));
    }

    public async Task<Result<GetsEmployerStatusStatementTypeResponse?>> GetsEmployerStatusStatementType(
        GetsEmployerStatusStatementTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for EmployerStatusStatementType");

        return await Task.FromResult(new GetsEmployerStatusStatementTypeResponse(EnumExt.GetEnumObjectList<EmployerStatusStatementType>()));
    }

    public async Task<Result<GetsEmployerStatusStatementExcelEnumsResponse?>> GetsEmployerStatusStatementExcelEnums(
        GetsEmployerStatusStatementExcelEnumsRequest request, CT ct)
    {
        _logger.LogInformation("EmployerStatusStatementExcelEnums");
        return await Task.FromResult(new GetsEmployerStatusStatementExcelEnumsResponse(EnumExt.GetEnumObjectList<EmployerStatusStatementExcelEnum>()));
    }

    public async Task<Result<GetsEmployerStatusStatementProjectOperationResponse?>> GetsEmployerStatusStatementProjectOperation(
        GetsEmployerStatusStatementProjectOperationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsEmployerStatusStatementProjectOperation, EmployerStatusStatementId:{EmployerStatusStatementId}, ", request.EmployerStatusStatementId);

        var isValidRequest = await request.IsValidAsync<GetsEmployerStatusStatementProjectOperationValidator, GetsEmployerStatusStatementProjectOperationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsEmployerStatusStatementProjectOperationQuery(request.EmployerStatusStatementId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null || response.Value.Data.Count <= 0)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationResponse>(response.Error!);

        var data = response.Value.Data.Adapt<List<GetsEmployerStatusStatementProjectOperationResponseModel>>();

        var measurementIds = data.Select(x => x.UnitOfMeasurementId).ToList();
        var measurementData = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);
        foreach (var item in data)
            item.UnitOfMeasurementName = measurementData?.Where(x => x.Id == item.UnitOfMeasurementId).Select(x => x.Name).FirstOrDefault();

        return new GetsEmployerStatusStatementProjectOperationResponse(data ?? new List<GetsEmployerStatusStatementProjectOperationResponseModel>(0),
            response?.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsEmployerStatusStatementHistoryResponse?>> GetsEmployerStatusStatementHistory(
        GetsEmployerStatusStatementHistoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsEmployerStatusStatementHistory, EmployerStatusStatementId:{EmployerStatusStatementId}, ", request.EmployerStatusStatementId);

        var isValidRequest = await request.IsValidAsync<GetsEmployerStatusStatementHistoryValidator, GetsEmployerStatusStatementHistoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementHistoryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsEmployerStatusStatementHistoryQuery(request.EmployerStatusStatementId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementHistoryResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null || response.Value.Data.Count <= 0)
            return Result.Failure<GetsEmployerStatusStatementHistoryResponse>(response.Error!);

        var data = response.Value.Data.Adapt<List<GetsEmployerStatusStatementHistoryResponseModel>>();

        return new GetsEmployerStatusStatementHistoryResponse(data ?? new List<GetsEmployerStatusStatementHistoryResponseModel>(0),
            response?.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsEmployerStatusStatementProjectOperationDetailResponse?>> GetsEmployerStatusStatementProjectOperationDetail(
        GetsEmployerStatusStatementProjectOperationDetailRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsEmployerStatusStatementProjectOperationDetail, EmployerStatusStatementProjectOperationId:{EmployerStatusStatementProjectOperationId}, ", request.EmployerStatusStatementProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsEmployerStatusStatementProjectOperationDetailValidator, GetsEmployerStatusStatementProjectOperationDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationDetailResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsEmployerStatusStatementProjectOperationDetailQuery(
            request.EmployerStatusStatementId, request.EmployerStatusStatementProjectOperationId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationDetailResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null || response.Value.Data.Count <= 0)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationDetailResponse>(response.Error!);

        var data = response.Value.Data.Adapt<List<GetsEmployerStatusStatementProjectOperationDetailResponseModel>>();

        return new GetsEmployerStatusStatementProjectOperationDetailResponse(data ??
            new List<GetsEmployerStatusStatementProjectOperationDetailResponseModel>(0), response?.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsEmployerStatusStatementProjectOperationDetailDailyResponse?>> GetsEmployerStatusStatementProjectOperationDetailDaily(
        GetsEmployerStatusStatementProjectOperationDetailDailyRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsEmployerStatusStatementProjectOperationDetailDaily, EmployerStatusStatementProjectOperationId:{EmployerStatusStatementProjectOperationId}, ", request.EmployerStatusStatementProjectOperationId);

        var isValidRequest = await request.IsValidAsync<GetsEmployerStatusStatementProjectOperationDetailDailyValidator, GetsEmployerStatusStatementProjectOperationDetailDailyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationDetailDailyResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsEmployerStatusStatementProjectOperationDetailDailyQuery(
            request.EmployerStatusStatementId, request.EmployerStatusStatementProjectOperationId, request.EmployerStatusStatementProjectOperationDetailId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationDetailDailyResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null || response.Value.Data.Count <= 0)
            return Result.Failure<GetsEmployerStatusStatementProjectOperationDetailDailyResponse>(response.Error!);

        var data = response.Value.Data.Adapt<List<GetsEmployerStatusStatementProjectOperationDetailDailyResponseModel>>();

        return new GetsEmployerStatusStatementProjectOperationDetailDailyResponse(data ??
            new List<GetsEmployerStatusStatementProjectOperationDetailDailyResponseModel>(0), response?.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsEmployerStatusStatementExcelExporterResponse?>> GetsEmployerStatusStatementExcelExporter(
        GetsEmployerStatusStatementExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsEmployerStatusStatementExcelExporter, ProjectId:{ProjectId}, CostCenterId:{CostCenterId},", request.ProjectId, request.CostCenterId);

        var isValidRequest = await request.IsValidAsync<GetsEmployerStatusStatementExcelExporterValidator, GetsEmployerStatusStatementExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementExcelExporterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsEmployerStatusStatementExcelExporterQuery(request.Ids, request.CostCenterId, request.ProjectId, request.EmployerId, request.EmployerContractId,
            request.StatusStatementCode, request.ProjectOperationIds, request.StartDate, request.EndDate, request.Status, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsEmployerStatusStatementExcelExporterResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsEmployerStatusStatementExcelExporterResponse>(EmployerStatusStatementErrors.DataNotFound!);
        var values = response.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = values?.Select(x => x.EmployerContract.EmployerContractHead.CurrencyId).Distinct().ToList();
        var currencyData = await WebServicesLogic.CurrenciesDataReceiver(allIds, _mediator, ct);

        var allEmployerIds = values?.NullListed(x => x.Project.EmployerId);
        var employerData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allEmployerIds, null, null, _mediator, ct);

        var data = values.Adapt<List<GetsEmployerStatusStatementExcelExporterResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
            item.CurrencyName = currencyData?.Where(x => x.Id == item.CurrencyId).Select(x => x.Name).FirstOrDefault();
            item.EmployerName = employerData?.Where(x => x?.Id == item.EmployerId).Select(x => x?.FullName).FirstOrDefault();
        }

        var file = new FileContentResult(EmployerStatusStatementExcels.EmployerStatusStatementsToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"EmployerStatusStatements-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsEmployerStatusStatementExcelExporterResponse(file);
    }

    public async Task<Result<GetEmployerStatusStatementExcelExporterResponse?>> GetEmployerStatusStatementExcelExporter(
        GetEmployerStatusStatementExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetEmployerStatusStatementExcelExporter");

        var isValidRequest = await request.IsValidAsync<GetEmployerStatusStatementExcelExporterValidator, GetEmployerStatusStatementExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetEmployerStatusStatementExcelExporterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetEmployerStatusStatementExcelExporterQuery(request.Id), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetEmployerStatusStatementExcelExporterResponse>(EmployerStatusStatementErrors.DataNotFound!);
        var responseValue = response.Value;

        Company? company = null;
        if (responseValue.CompanyId is not null && responseValue.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(responseValue.CompanyId, _mediator, ct);

        var currencyData = await WebServicesLogic.CurrenciesDataReceiver([responseValue.EmployerContract.EmployerContractHead.CurrencyId], _mediator, ct);

        List<UserModel?>? employerData = null;
        if (responseValue.Project.EmployerId is not null)
            employerData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([responseValue.Project.EmployerId.Value!], null, null, _mediator, ct);

        var employerStatusStatement = response.Value.Adapt<GetEmployerStatusStatementExcelExporterResponseModel>();
        employerStatusStatement.CurrencyName = currencyData?.Where(x => x.Id == responseValue.EmployerContract.EmployerContractHead.CurrencyId).Select(x => x.Name).FirstOrDefault();
        employerStatusStatement.EmployerName = employerData?.Where(x => x?.Id == responseValue.Project.EmployerId).Select(x => x?.FullName).FirstOrDefault();
        employerStatusStatement.CompanyNameFa = company?.NameFa;

        var projectOperations = responseValue.EmployerStatusStatementProjectOperations.Adapt<List<EmployerStatusStatementProjectOperations>>();
        var measurementIds = projectOperations.Select(x => x.UnitOfMeasurementId).ToList();
        var measurementData = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);
        foreach (var item in projectOperations)
            item.UnitOfMeasurementName = measurementData?.Where(x => x.Id == item.UnitOfMeasurementId).Select(x => x.Name).FirstOrDefault();

        List<EmployerStatusStatementProjectOperationDetails> projectOperationDetails = [];
        foreach (var item in responseValue.EmployerStatusStatementProjectOperations)
            projectOperationDetails.AddRange(item.EmployerStatusStatementProjectOperationDetails.Adapt<List<EmployerStatusStatementProjectOperationDetails>>());

        var file = new FileContentResult(EmployerStatusStatementExcels.EmployerStatusStatementToExcel(employerStatusStatement, projectOperations, projectOperationDetails), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"EmployerStatusStatement-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetEmployerStatusStatementExcelExporterResponse(file);
    }

    public async Task<Result<GetEmployerStatusStatementLetterheadExcelResponse?>> GetEmployerStatusStatementLetterheadExcel(
        GetEmployerStatusStatementLetterheadExcelRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetEmployerStatusStatementLetterheadExcel");

        var isValidRequest = await request.IsValidAsync<GetEmployerStatusStatementLetterheadExcelValidator, GetEmployerStatusStatementLetterheadExcelRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetEmployerStatusStatementLetterheadExcelResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetEmployerStatusStatementLetterheadExcelQuery(request.Id), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetEmployerStatusStatementLetterheadExcelResponse>(EmployerStatusStatementErrors.DataNotFound!);
        var value = response.Value;

        var currencyData = await WebServicesLogic.CurrenciesDataReceiver([value.CurrencyId], _mediator, ct);

        List<UserModel?>? employerData = null;
        if (value.EmployerId is not null)
            employerData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([value.EmployerId.Value], null, null, _mediator, ct);

        value.CurrencyName = currencyData?.Where(x => x.Id == value.CurrencyId).Select(x => x.Name).FirstOrDefault();
        value.EmployerName = employerData?.Where(x => x?.Id == value.EmployerId).Select(x => x?.FullName).FirstOrDefault();

        var measurementIds = value.ProjectOperations!.Select(x => x.UnitOfMeasurementId).ToList();
        var measurementData = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);
        foreach (var item in value.ProjectOperations)
            item.UnitOfMeasurementName = measurementData?.Where(x => x.Id == item.UnitOfMeasurementId).Select(x => x.Name).FirstOrDefault();

        var file = new FileContentResult(EmployerStatusStatementLetterheadExcelToExcels.EmployerStatusStatementLetterheadExcelToExcel(
            value), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"EmployerStatusStatement-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetEmployerStatusStatementLetterheadExcelResponse(file);
    }

}
