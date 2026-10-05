using Engineering.Application.Services.ProjectTypes.Commands.ActiveProjectType;
using Engineering.Application.Services.ProjectTypes.Commands.CodeCreator;
using Engineering.Application.Services.ProjectTypes.Commands.CreateProjectType;
using Engineering.Application.Services.ProjectTypes.Commands.DisableProjectType;
using Engineering.Application.Services.ProjectTypes.Commands.InactiveProjectType;
using Engineering.Application.Services.ProjectTypes.Commands.StateChangerProjectTypes;
using Engineering.Application.Services.ProjectTypes.Commands.UpdateProjectType;
using Engineering.Application.Services.ProjectTypes.Models.ActiveProjectType;
using Engineering.Application.Services.ProjectTypes.Models.CodeCreator;
using Engineering.Application.Services.ProjectTypes.Models.CreateProjectType;
using Engineering.Application.Services.ProjectTypes.Models.DisableProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetActiveProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByCode;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeById;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByName;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelEnum;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;
using Engineering.Application.Services.ProjectTypes.Models.InactiveProjectType;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeExcelImports;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeGroupDelete;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeModels;
using Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;
using Engineering.Application.Services.ProjectTypes.Queries.FindProjectTypeByNamesOrCodes;
using Engineering.Application.Services.ProjectTypes.Queries.GetActiveProjectTypes;
using Engineering.Application.Services.ProjectTypes.Queries.GetByCode;
using Engineering.Application.Services.ProjectTypes.Queries.GetByName;
using Engineering.Application.Services.ProjectTypes.Queries.GetProjectTypeById;
using Engineering.Application.Services.ProjectTypes.Queries.GetsProjectType;
using Engineering.Application.Services.ProjectTypes.Queries.GetsProjectTypeByIds;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.ProjectTypes;

public class ProjectTypeLogic : IProjectTypeLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectTypeLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public ProjectTypeLogic(IMediator mediator, ILogger<ProjectTypeLogic> logger, IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateProjectTypeResponse?>> CreateProjectType(CreateProjectTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectType, ProjectTypeName:{ProjectTypeName}, ProjectTypeCode:{ProjectTypeCode},",
            request.ProjectTypeName, request.ProjectTypeCode);

        var isValidRequest = await request.IsValidAsync<CreateProjectTypeValidator, CreateProjectTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectTypeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateProjectTypeResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetProjectTypeByNameQuery(request.ProjectTypeName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateProjectTypeResponse>(ProjectErrors.TypeNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetProjectTypeByCodeQuery(request.ProjectTypeCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateProjectTypeResponse>(ProjectErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new CreateProjectTypeCommand(request.ProjectTypeName, request.ProjectTypeCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateProjectTypeResponse>(response.Error!);
        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateProjectTypeResponse>();
    }

    public async Task<Result<ProjectTypeExcelImportsResponse?>> ProjectTypeExcelImports(ProjectTypeExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectTypeExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<ProjectTypeExcelImportsValidator, ProjectTypeExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectTypeExcelImportsResponse>(isValidRequest.Error!);

        var projectTypes = ExcelImporter.Import<ProjectTypeExcelImportsModel>(request.DocumentFile);
        if (projectTypes is null)
            return Result.Failure<ProjectTypeExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (projectTypes.Count != projectTypes.Select(x => x.ProjectTypeName).Distinct().Count())
            return Result.Failure<ProjectTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (projectTypes.Count != projectTypes.Select(x => x.ProjectTypeCode).Distinct().Count())
            return Result.Failure<ProjectTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<ProjectTypeExcelImportsResponse>(companyResponse.Error!);
        }

        var names = projectTypes.Select(x => x.ProjectTypeName).ToList();
        var codes = projectTypes.Select(x => x.ProjectTypeCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindProjectTypeByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<ProjectTypeExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in projectTypes)
        {
            var response = await _mediator.Send(new CreateProjectTypeCommand(item.ProjectTypeName, item.ProjectTypeCode, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<ProjectTypeExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectTypeExcelImportsResponse(true);
    }

    public async Task<Result<ProjectTypeCodeCreatorResponse?>> ProjectTypeCodeCreator(ProjectTypeCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<ProjectTypeCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectTypeCodeCreatorResponse>(response.Error!);

        return new ProjectTypeCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerProjectTypesResponse?>> StateChangerProjectTypes(StateChangerProjectTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerProjectTypes, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerProjectTypesValidator, StateChangerProjectTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerProjectTypesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerProjectTypesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsProjectTypeByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerProjectTypesResponse>(responses.Error!);
        var values = responses.Value;

        var response = await _mediator.Send(new StateChangerProjectTypesCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerProjectTypesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerProjectTypesResponse(true);
    }

    public async Task<Result<UpdateProjectTypeResponse?>> UpdateProjectType(UpdateProjectTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectType, Id:{Id}, ProjectTypeName:{ProjectTypeName}, ProjectTypeCode:{ProjectTypeCode}",
           request.Id, request.ProjectTypeName, request.ProjectTypeCode);

        var isValidRequest = await request.IsValidAsync<UpdateProjectTypeValidator, UpdateProjectTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectTypeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateProjectTypeResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetProjectTypeByNameQuery(request.ProjectTypeName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateProjectTypeResponse>(ProjectErrors.TypeNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetProjectTypeByCodeQuery(request.ProjectTypeCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateProjectTypeResponse>(ProjectErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new UpdateProjectTypeCommand(request.Id, request.ProjectTypeName, request.ProjectTypeCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateProjectTypeResponse>();
    }

    public async Task<Result<DisableProjectTypeResponse?>> DisableProjectType(DisableProjectTypeRequest request, CT ct)
    {
        _logger.LogInformation("Disable ProjectType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableProjectTypeValidator, DisableProjectTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableProjectTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableProjectTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableProjectTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableProjectTypeResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<ProjectTypeGroupDeleteResponse?>> ProjectTypeGroupDelete(ProjectTypeGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectTypeGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectTypeGroupDeleteValidator, ProjectTypeGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectTypeGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableProjectTypeCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ProjectTypeGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectTypeGroupDeleteResponse(true);
    }

    public async Task<Result<InactiveProjectTypeResponse?>> InactiveProjectType(InactiveProjectTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveProjectType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveProjectTypeValidator, InactiveProjectTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveProjectTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveProjectTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveProjectTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveProjectTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveProjectTypeResponse?>> ActiveProjectType(ActiveProjectTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveProjectType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveProjectTypeValidator, ActiveProjectTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveProjectTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveProjectTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveProjectTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveProjectTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<GetProjectTypeByIdResponse?>> GetProjectTypeById(GetProjectTypeByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectTypeById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetProjectTypeByIdValidator, GetProjectTypeByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectTypeByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectTypeByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectTypeByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetProjectTypeByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetsProjectTypeResponse?>> GetsProjectType(GetsProjectTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectType, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectTypeValidator, GetsProjectTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectTypeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsProjectTypeQuery(null, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectTypeResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsProjectTypeModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsProjectTypeResponse(data ?? new List<GetsProjectTypeModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProjectTypeByNameResponse?>> GetProjectTypeByName(GetProjectTypeByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectTypeByName, ProjectTypeName:{ProjectTypeName}", request.ProjectTypeName);

        var isValidRequest = await request.IsValidAsync<GetProjectTypeByNameValidator, GetProjectTypeByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectTypeByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectTypeByNameQuery(request.ProjectTypeName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectTypeByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetProjectTypeByNameResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetProjectTypeByCodeResponse?>> GetProjectTypeByCode(GetProjectTypeByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectTypeByCode, ProjectTypeCode:{ProjectTypeCode}", request.ProjectTypeCode);

        var isValidRequest = await request.IsValidAsync<GetProjectTypeByCodeValidator, GetProjectTypeByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectTypeByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectTypeByCodeQuery(request.ProjectTypeCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectTypeByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetProjectTypeByCodeResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetActiveProjectTypesResponse?>> GetActiveProjectTypes(GetActiveProjectTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveProjectTypes,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.Code, request.Name, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveProjectTypesValidator, GetActiveProjectTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveProjectTypesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveProjectTypesQuery(request.FilterData, request.Code, request.Name, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveProjectTypesResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveProjectTypeModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetActiveProjectTypesResponse(data ?? new List<GetsActiveProjectTypeModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectTypeExcelExporterResponse?>> GetsProjectTypeExcelExporter(GetsProjectTypeExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectTypeExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectTypeExcelExporterValidator, GetsProjectTypeExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectTypeExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsProjectTypeQuery(request.Ids, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsProjectTypeExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsProjectTypeExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(ProjectTypeExcels.ProjectTypeToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectTypes-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsProjectTypeExcelExporterResponse(file);
    }

    public async Task<Result<GetsProjectTypeExcelEnumResponse?>> GetsProjectTypeExcelEnum(GetsProjectTypeExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectTypeExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectTypeExcelEnum>());
        return new GetsProjectTypeExcelEnumResponse(response);
    }
}