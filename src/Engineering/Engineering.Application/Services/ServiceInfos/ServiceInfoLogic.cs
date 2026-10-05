using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIds;
using Engineering.Application.Services.ServiceInfos.Commands.Active;
using Engineering.Application.Services.ServiceInfos.Commands.CodeCreator;
using Engineering.Application.Services.ServiceInfos.Commands.CreateService;
using Engineering.Application.Services.ServiceInfos.Commands.DeleteServiceInfo;
using Engineering.Application.Services.ServiceInfos.Commands.InactiveService;
using Engineering.Application.Services.ServiceInfos.Commands.SetServiceInfoDetail;
using Engineering.Application.Services.ServiceInfos.Commands.StateChangerServiceInfos;
using Engineering.Application.Services.ServiceInfos.Commands.UpdateService;
using Engineering.Application.Services.ServiceInfos.Models.ActiveService;
using Engineering.Application.Services.ServiceInfos.Models.CodeCreator;
using Engineering.Application.Services.ServiceInfos.Models.CreateService;
using Engineering.Application.Services.ServiceInfos.Models.DeleteServiceInfo;
using Engineering.Application.Services.ServiceInfos.Models.GetActiveServices;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceById;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByName;
using Engineering.Application.Services.ServiceInfos.Models.GetServices;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByOperationInfo;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByProjectOperationIds;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelEnum;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;
using Engineering.Application.Services.ServiceInfos.Models.InactiveService;
using Engineering.Application.Services.ServiceInfos.Models.ServiceInfoExcelImports;
using Engineering.Application.Services.ServiceInfos.Models.ServiceInfoGroupDelete;
using Engineering.Application.Services.ServiceInfos.Models.ServiceModels;
using Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;
using Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;
using Engineering.Application.Services.ServiceInfos.Models.UpdateService;
using Engineering.Application.Services.ServiceInfos.Queries.FindServiceInfoByNamesOrCodes;
using Engineering.Application.Services.ServiceInfos.Queries.GetActiveServices;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Application.Services.ServiceInfos.Queries.GetServiceByCode;
using Engineering.Application.Services.ServiceInfos.Queries.GetServiceById;
using Engineering.Application.Services.ServiceInfos.Queries.GetServiceByName;
using Engineering.Application.Services.ServiceInfos.Queries.GetServices;
using Engineering.Application.Services.ServiceInfos.Queries.GetsServiceInfoByOperationInfo;
using Engineering.Application.Services.ServiceInfos.Queries.GetsServiceInfoByProjectOperationIds;
using Engineering.Application.Services.ServiceInfos.Queries.ValidateServiceByName;
using Engineering.Domain.Constants;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ServiceInfos;
using Engineering.Domain.Entities.ServiceInfos.Enums;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.PreferentialTemporary.Commands.CreatePreferentialTemporary;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.ServiceInfos;

public class ServiceInfoLogic : IServiceInfoLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ServiceInfoLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IMeasureUnitRepository _measureRepo;

    public ServiceInfoLogic(IMediator mediator,
        ILogger<ServiceInfoLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IMeasureUnitRepository measureRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _measureRepo = measureRepo;
    }

    public async Task<Result<CreateServiceInfoResponse?>> CreateServiceInfo(CreateServiceInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateServiceInfo, ServiceInfoName:{ServiceInfoName}, ServiceInfoCode:{ServiceInfoCode},",
            request.ServiceInfoName, request.ServiceInfoCode);

        var isValidRequest = await request.IsValidAsync<CreateServiceInfoValidator, CreateServiceInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateServiceInfoResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateServiceInfoResponse>(companyResponse.Error!);
        }

        if (request.MeasurementData is null && request.Type == ServiceInfoType.Engineering)
            return Result.Failure<CreateServiceInfoResponse?>(ServiceInfoErrors.UnitOfMeasurementIdIsEmpty);

        long? measurementId = null;
        if (request.MeasurementData is null && request.Type == ServiceInfoType.Administrative)
        {
            var measure = await _measureRepo.GetByName("اداری", ct);
            if (measure is null)
                return Result.Failure<CreateServiceInfoResponse?>(ServiceInfoErrors.AdministrativeMeasureNotFound);
            measurementId = measure?.Id;
        }

        var measureId = request.Type == ServiceInfoType.Engineering ? request.MeasurementData!.Id : measurementId!.Value;

        var codeIsDuplicate = await _mediator.Send(new GetServiceInfoByCodeQuery(request.ServiceInfoCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateServiceInfoResponse>(ServiceInfoErrors.CodeIsDuplicate);

        var nameIsDuplicate = await _mediator.Send(new ValidateServiceByNameQuery(request.ServiceInfoName, measureId, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateServiceInfoResponse>(ServiceInfoErrors.NameandUnitIsDuplicate);

        if (request.MeasurementData is not null)
        {
            var measureunitData = await _measureRepo.GetByIdAsync(measureId, ct);
            if (measureunitData is null)
                return Result.Failure<CreateServiceInfoResponse>(MetaDataErrors.MeasureunitWithIdNotFound!);
        }

        List<OperationInfo>? operationInfos = null;
        if (request.OperationInfoIds is not null)
            if (request.OperationInfoIds.Count > 0)
            {
                var operationInfoData = await _mediator.Send(new GetsOperationInfoByIdsQuery(request.OperationInfoIds, 1, request.OperationInfoIds.Count), ct);
                if (operationInfoData.IsFailure)
                    return Result.Failure<CreateServiceInfoResponse>(ServiceInfoErrors.OperationInfoIds);
                operationInfos = operationInfoData.Value!.Data;
            }

        if (!string.IsNullOrEmpty(request.TimeSpant))
        {
            if (!(request.TimeSpant.Split(':')[0].Count() >= 2 && request.TimeSpant.Split(':')[1].Count() == 2))
                return Result.Failure<CreateServiceInfoResponse>(MachineryStandardErrors.TimeSpantCountError);

            if (int.Parse(request.TimeSpant.Split(':')[1]) > 59)
                return Result.Failure<CreateServiceInfoResponse>(MachineryStandardErrors.MoreThan59Min);
        }

        var response = await _mediator.Send(new CreateServiceInfoCommand(
            request.ServiceInfoCode,
            request.ServiceInfoName,
            request.ServiceInfoEnName,
            request.DescriptionFa,
            request.DescriptionEn,
            measureId,
            operationInfos,
            TimeCalculator.StringToTicks(request.TimeSpant),
            request.IsActive,
            request.Type ?? ServiceInfoType.Engineering,
            request.DocumentUrls,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateServiceInfoResponse>(response.Error!);
        var service = response.Value;

        await _unitOfWork.CommitAsync(ct);

        await CreateTemporary(service!, ct);

        return new CreateServiceInfoResponse(response.Value!.Id, true);
    }

    public async Task<Result<ServiceInfoExcelImportsResponse?>> ServiceInfoExcelImports(ServiceInfoExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ServiceInfoExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<ServiceInfoExcelImportsValidator, ServiceInfoExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ServiceInfoExcelImportsResponse>(isValidRequest.Error!);

        var serviceInfos = ExcelImporter.Import<ServiceInfoExcelImportsModel>(request.DocumentFile);
        if (serviceInfos is null)
            return Result.Failure<ServiceInfoExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (serviceInfos.Count != serviceInfos.Select(x => x.ServiceInfoName).Distinct().Count())
            return Result.Failure<ServiceInfoExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (serviceInfos.Count != serviceInfos.Select(x => x.ServiceInfoCode).Distinct().Count())
            return Result.Failure<ServiceInfoExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<ServiceInfoExcelImportsResponse>(companyResponse.Error!);
        }

        var names = serviceInfos.Select(x => x.ServiceInfoName).ToList();
        var codes = serviceInfos.Select(x => x.ServiceInfoCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindServiceInfoByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<ServiceInfoExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in serviceInfos)
        {
            var response = await _mediator.Send(new CreateServiceInfoCommand(item.ServiceInfoCode, item.ServiceInfoName, null, null, null, item.MeasurementId, null, 0, item.IsActive, ServiceInfoType.Engineering, null, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<ServiceInfoExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ServiceInfoExcelImportsResponse(true);
    }

    public async Task<Result<ServiceInfoCodeCreatorResponse?>> CodeCreator(ServiceInfoCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<ServiceInfoCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<ServiceInfoCodeCreatorResponse>(response.Error!);

        return new ServiceInfoCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerServiceInfosResponse?>> StateChangerServiceInfos(StateChangerServiceInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerServiceInfos, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerServiceInfosValidator, StateChangerServiceInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerServiceInfosResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerServiceInfosResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsServiceInfoByIdsQuery(1, request.Ids.Count(), request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerServiceInfosResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerServiceInfosCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerServiceInfosResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerServiceInfosResponse(true);
    }

    public async Task<Result<UpdateServiceInfoResponse?>> UpdateServiceInfo(UpdateServiceInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateServiceInfo, id:{Id}, ServiceInfo:{ServiceInfoName} , description:{ServiceInfoCode},", request.Id, request.ServiceInfoName, request.ServiceInfoCode);

        var isValidRequest = await request.IsValidAsync<UpdateServiceInfoValidator, UpdateServiceInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateServiceInfoResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateServiceInfoResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new ValidateServiceByNameQuery(request.ServiceInfoName, request.MeasurementData.Id, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateServiceInfoResponse>(ServiceInfoErrors.NameandUnitIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetServiceInfoByCodeQuery(request.ServiceInfoCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateServiceInfoResponse>(ServiceInfoErrors.CodeIsDuplicate);

        //Find MeasureunitById
        var measureunitData = await _measureRepo.GetByIdAsync(request.MeasurementData.Id, ct);
        if (measureunitData is null)
            return Result.Failure<UpdateServiceInfoResponse>(MetaDataErrors.MeasureunitWithIdNotFound!);

        var response = await _mediator.Send(new UpdateServiceInfoCommand(request.Id,
            request.ServiceInfoCode,
            request.ServiceInfoName,
            request.ServiceInfoEnName,
            request.DescriptionFa,
            request.DescriptionEn,
            request.MeasurementData.Id,
            request.IsActive,
            request.Type,
            request.DocumentUrls,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateServiceInfoResponse>(response.Error!);
        var service = response.Value;

        await _unitOfWork.CommitAsync(ct);

        if (!service!.HavePreferentialReferenceCode && service.PreferentialReferenceCode != null && service.PreferentialReferenceCode != Guid.Empty)
            await CreateTemporary(service!, ct);

        return new UpdateServiceInfoResponse(response.Value!.Id, true);
    }

    public async Task<Result<ServiceInfoGroupDeleteResponse?>> ServiceInfoGroupDelete(ServiceInfoGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ServiceInfoGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ServiceInfoGroupDeleteValidator, ServiceInfoGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ServiceInfoGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteServiceInfoCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ServiceInfoGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ServiceInfoGroupDeleteResponse(true);
    }

    public async Task<Result<InactiveServiceInfoResponse?>> InactiveServiceInfo(InactiveServiceInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveServiceInfo, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveServiceInfoValidator, InactiveServiceInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveServiceInfoResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveServiceInfoCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveServiceInfoResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveServiceInfoResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveServiceInfoResponse?>> ActiveServiceInfo(ActiveServiceInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveServiceInfo, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveServiceInfoValidator, ActiveServiceInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveServiceInfoResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveServiceInfoCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveServiceInfoResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveServiceInfoResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<DeleteServiceInfoResponse?>> DeleteServiceInfo(DeleteServiceInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteServiceInfo, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteServiceInfoValidator, DeleteServiceInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteServiceInfoResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteServiceInfoCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteServiceInfoResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteServiceInfoResponse(response.Value!.Id);
    }

    public async Task<Result<GetServiceInfoByIdResponse?>> GetServiceInfoById(GetServiceInfoByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetServiceInfoById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetServiceInfoByIdValidator, GetServiceInfoByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetServiceInfoByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetServiceInfoByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetServiceInfoByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        //Find MeasureunitById
        var measureunitData = await WebServicesLogic.MeasureUnitDataReceiver(value.UnitOfMeasurementId, _mediator, ct);
        var measurementData = new OperationInfoMeasurementModel(value.UnitOfMeasurementId, measureunitData?.Name);

        return new GetServiceInfoByIdResponse()
        {
            CompanyId = value.CompanyId,
            Id = value.Id,
            CompanyNameFa = company?.NameFa,
            IsActive = value.IsActive,
            MeasurementData = measurementData,
            ServiceInfoCode = value.ServiceInfoCode,
            ServiceInfoName = value.ServiceInfoName,
            ServiceInfoEnName = value.ServiceInfoEnName,
            DescriptionFa = value.DescriptionFa,
            DescriptionEn = value.DescriptionEn,
            Type = value.Type,
            DocumentUrls = value.ServiceInfoDocuments.Select(x => x.Url).ToList(),
        };
    }

    public async Task<Result<GetServiceInfoByNameResponse?>> GetServiceInfoByName(GetServiceInfoByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetServiceInfoByName, ServiceInfoName:{ServiceInfoName}", request.ServiceInfoName);

        var isValidRequest = await request.IsValidAsync<GetServiceInfoByNameValidator, GetServiceInfoByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetServiceInfoByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetServiceInfoByNameQuery(request.ServiceInfoName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetServiceInfoByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        return new GetServiceInfoByNameResponse()
        {
            CompanyId = value.CompanyId,
            Id = value.Id,
            CompanyNameFa = company?.NameFa,
            IsActive = value.IsActive,
            ServiceInfoCode = value.ServiceInfoCode,
            ServiceInfoName = value.ServiceInfoName,
        };
    }

    public async Task<Result<GetServiceInfoByCodeResponse?>> GetServiceInfoByCode(GetServiceInfoByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetServiceInfoByCode, ServiceInfoCode:{ServiceInfoCode}", request.ServiceInfoCode);

        var isValidRequest = await request.IsValidAsync<GetServiceInfoByCodeValidator, GetServiceInfoByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetServiceInfoByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetServiceInfoByCodeQuery(request.ServiceInfoCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetServiceInfoByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        return new GetServiceInfoByCodeResponse()
        {
            CompanyId = value.CompanyId,
            Id = value.Id,
            CompanyNameFa = company?.NameFa,
            IsActive = value.IsActive,
            ServiceInfoCode = value.ServiceInfoCode,
            ServiceInfoName = value.ServiceInfoName,
        };
    }

    public async Task<Result<GetActiveServiceInfosResponse?>> GetActiveServiceInfos(GetActiveServiceInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveServiceInfos,ServiceInfoCode:{ServiceInfoCode} , ServiceInfoName:{ServiceInfoName}, pageIndex:{PageIndex} , pageSize:{PageSize}",
           request.ServiceInfoCode, request.ServiceInfoName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveServiceInfosValidator, GetActiveServiceInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveServiceInfosResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveServiceInfosQuery(request.FilterData, request.ServiceInfoCode, request.ServiceInfoName, request.ProjectId, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveServiceInfosResponse>(response.Error!);
        var values = response.Value?.Data!;

        var allIds = IdCollectors(response.Value!.Data!); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = new List<GetsActiveServiceInfoModel>();
        foreach (var serviceInfoData in values!)
        {
            var company = companies?.Where(x => x.Id == serviceInfoData.CompanyId).FirstOrDefault();
            var measurement = measureUnitsData?.Where(m => m.Id == serviceInfoData.UnitOfMeasurementId).FirstOrDefault();
            data.Add(new GetsActiveServiceInfoModel
            {
                Id = serviceInfoData.Id,
                ServiceInfoName = serviceInfoData.ServiceInfoName,
                ServiceInfoCode = serviceInfoData.ServiceInfoCode,
                MeasurementId = serviceInfoData.UnitOfMeasurementId,
                MeasurementName = measurement?.Name,
                MeasurementData = new OperationInfoMeasurementModel(serviceInfoData.UnitOfMeasurementId, measurement?.Name),
                CompanyId = serviceInfoData.CompanyId,
                IsActive = serviceInfoData.IsActive,
                CompanyNameFa = company?.NameFa
            });
        }

        return new GetActiveServiceInfosResponse(data ?? new List<GetsActiveServiceInfoModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetServiceInfosResponse?>> GetServiceInfos(GetServiceInfosRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetServiceInfos, ServiceInfoCode:{ServiceInfoCode} , ServiceInfoName:{ServiceInfoName}, IsActive:{IsActive} ,pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.ServiceInfoCode, request.ServiceInfoName, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetServiceInfosValidator, GetServiceInfosRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetServiceInfosResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetServiceInfosQuery(null, request.OperationInfoId, request.CategoryId, request.BranchId, request.SeasonId, request.FilterData, request.ServiceInfoCode, request.ServiceInfoName,
            request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetServiceInfosResponse>(response.Error!);
        var values = response.Value!.Data!;

        var allIds = IdCollectors(values); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct);

        var data = new List<GetServiceInfosModel>();
        foreach (var value in values!)
        {
            var operationInfoService = value.OperationInfoServices.FirstOrDefault(x => x.OperationInfo.Id.Equals(request.OperationInfoId));
            string time = "00:00";
            if (operationInfoService is not null)
                time = TimeCalculator.TicksToStringHM(operationInfoService.TimeSpant);

            var measurement = new OperationInfoMeasurementModel(
                value.UnitOfMeasurementId,
                measureUnitsData?.Where(m => m.Id == value.UnitOfMeasurementId).FirstOrDefault()?.Name);

            data.Add(new GetServiceInfosModel
            {
                Id = value.Id,
                ServiceInfoName = value.ServiceInfoName,
                ServiceInfoCode = value.ServiceInfoCode,
                ServiceInfoEnName = value.ServiceInfoEnName,
                DescriptionEn = value.DescriptionEn,
                DescriptionFa = value.DescriptionFa,
                Type = value.Type,
                MeasurementId = value.UnitOfMeasurementId,
                MeasurementName = measurement.Name,
                MeasurementData = measurement,
                TimeSpant = time,
                IsActive = value.IsActive
            });
        }

        return new GetServiceInfosResponse(data.Adapt<List<GetServiceInfosModel>>() ?? new List<GetServiceInfosModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsServiceInfoByOperationInfoResponse?>> GetsServiceInfoByOperationInfo(GetsServiceInfoByOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsServiceInfoByOperationInfo, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsServiceInfoByOperationInfoValidator, GetsServiceInfoByOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsServiceInfoByOperationInfoResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsServiceInfoByOperationInfoQuery(request.OperationInfoIds, request.FilterData, request.IsActive, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsServiceInfoByOperationInfoResponse>(response.Error!);
        var values = response.Value!.Data!;

        var allIds = IdCollectors(values); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct);

        var data = new List<GetsServiceInfoByOperationInfoResponseModel>();
        foreach (var serviceInfoData in values!)
        {
            var measurement = new OperationInfoMeasurementModel(serviceInfoData.UnitOfMeasurementId, measureUnitsData?.Where(m => m.Id == serviceInfoData.UnitOfMeasurementId).FirstOrDefault()?.Name);
            data.Add(new GetsServiceInfoByOperationInfoResponseModel(serviceInfoData.Id, serviceInfoData.ServiceInfoName, serviceInfoData.ServiceInfoCode, measurement.Id, measurement.Name,
                measurement, serviceInfoData.IsActive));
        }

        return new GetsServiceInfoByOperationInfoResponse(data.Adapt<List<GetsServiceInfoByOperationInfoResponseModel>>() ?? new List<GetsServiceInfoByOperationInfoResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsServiceInfoByProjectOperationIdsResponse?>> GetsServiceInfoByProjectOperationIds(GetsServiceInfoByProjectOperationIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsServiceInfoByProjectOperationIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsServiceInfoByProjectOperationIdsValidator, GetsServiceInfoByProjectOperationIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsServiceInfoByProjectOperationIdsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsServiceInfoByProjectOperationIdsQuery(request.ProjectOperationIds, request.FilterData, request.IsActive, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsServiceInfoByProjectOperationIdsResponse>(response.Error!);
        var values = response.Value!.Data!;

        var allIds = IdCollectors(values); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = new List<GetsServiceInfoByProjectOperationIdsResponseModel>();
        foreach (var serviceInfoData in values!)
        {
            var companyName = companies?.FirstOrDefault(x => x.Id == serviceInfoData.CompanyId)?.NameFa;
            var measurement = new OperationInfoMeasurementModel(serviceInfoData.UnitOfMeasurementId, measureUnitsData?.Where(m => m.Id == serviceInfoData.UnitOfMeasurementId).FirstOrDefault()?.Name);
            data.Add(new GetsServiceInfoByProjectOperationIdsResponseModel(serviceInfoData.Id, serviceInfoData.ServiceInfoName, serviceInfoData.ServiceInfoCode,
                measurement.Id, measurement.Name, measurement, serviceInfoData.IsActive, serviceInfoData.CompanyId, companyName));
        }

        return new GetsServiceInfoByProjectOperationIdsResponse(data.Adapt<List<GetsServiceInfoByProjectOperationIdsResponseModel>>() ?? new List<GetsServiceInfoByProjectOperationIdsResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsServiceInfoExcelExporterResponse?>> GetsServiceInfoExcelExporter(GetsServiceInfoExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsServiceInfoExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsServiceInfoExcelExporterValidator, GetsServiceInfoExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsServiceInfoExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetServiceInfosQuery(request.Ids, request.OperationInfoId, request.CategoryId, request.BranchId, request.SeasonId, request.FilterData,
            request.ServiceInfoCode, request.ServiceInfoName, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsServiceInfoExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var measurementIds = values.Where(x => x.UnitOfMeasurementId != 0).Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var measureunits = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var data = values.Adapt<List<GetsServiceInfoExcelExporterModel>>();
        foreach (var item in data)
        {
            item.MeasurementName = measureunits?.FirstOrDefault(x => x.Id == item.MeasurementId)?.Name;
        }

        var file = new FileContentResult(ServiceInfoExcels.ServiceInfoToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ServiceInfos-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsServiceInfoExcelExporterResponse(file);
    }

    public async Task<Result<GetsServiceInfoExcelEnumResponse?>> GetsServiceInfoExcelEnum(GetsServiceInfoExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsServiceInfoExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ServiceInfoExcelEnum>());
        return new GetsServiceInfoExcelEnumResponse(response);
    }

    //Private Method
    private List<long> IdCollectors(List<ServiceInfo> serviceInfo)
    {
        var dataResult = new List<long>();
        if (serviceInfo is not null)
            if (serviceInfo!.Count > 0)
                dataResult.AddRange(serviceInfo!.Select(x => x.UnitOfMeasurementId).ToList());

        return dataResult.Where(x => x! != 0).ToList();
    }

    private async Task CreateTemporary(ServiceInfo service, CT ct)
    {
        try
        {
            var createPreferentialTemporary = new CreatePreferentialTemporaryCommand(service!.GetPreferentialName(),
                DepartmentNames.Engineering.ServiceInfo, service!.PreferentialReferenceCode!.Value);

            var createPreferentialTemporaryResponse = await _mediator.Send(createPreferentialTemporary, ct);
            if (createPreferentialTemporaryResponse.IsFailure)
                _logger.LogError("CreatePreferentialTemporary for ServiceInfo with id of {ServiceInfo} failed with Code:{Code}, Msg:{Msg}", service!.Id, createPreferentialTemporaryResponse.Error!.Code, createPreferentialTemporaryResponse.Error!.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    public async Task<Result<SetServiceInfoDetailResponse?>> SetServiceInfoDetail(
    SetServiceInfoDetailRequest request, CT ct)
    {
        _logger.LogInformation("SetServiceInfoDetail started. ServiceInfoId: {ServiceInfoId}", request.Id);

        var result = await _mediator.Send(
            new SetServiceInfoDetailCommand(
                request.Id,
                request.ServiceInfoEnName,
                request.DescriptionFa,
                request.DescriptionEn),
            ct);

        if (result.IsFailure)
            return result;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }
}