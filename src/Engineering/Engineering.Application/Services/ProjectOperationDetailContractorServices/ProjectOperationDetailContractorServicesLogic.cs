using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.DetailContractorServices.Commands.ActiveDetailContractorService;
using Engineering.Application.Services.DetailContractorServices.Commands.InactiveDetailContractorService;
using Engineering.Application.Services.DetailContractorServices.Commands.StateChangerDetailContractorServices;
using Engineering.Application.Services.DetailContractorServices.Models.ActiveDetailContractorService;
using Engineering.Application.Services.DetailContractorServices.Models.InactiveDetailContractorService;
using Engineering.Application.Services.DetailContractorServices.Models.StateChangerDetailContractorServices;
using Engineering.Application.Services.DetailContractorServices.Queries.GetsDetailContractorServiceByIds;
using Engineering.Application.Services.GetsByProjectOperationDetailId.Queries.GetsByProjectOperationDetailId;
using Engineering.Application.Services.OperationInfoServices.Queries.GetOperationInfoServiceForValidation;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.AppointmentContractor;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.CreateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.DisableContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.OpAssign.Create;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.OpAssign.Update;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetContractorServiceToContract;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetDetailContractorServiceToNew;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.AppointmentContractor;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.CreateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.DisableContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetContractorServiceById;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetFilteredProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByFilter;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Create;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Delete;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Update;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.SetContractorServiceToContract;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.SetDetailContractorServiceToNew;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.UpdateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetFilteredProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetProjectOperationDetailByIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsByFilter;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsServiceByProjectOperationDetailIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetAssignable;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetByPO;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetForUpdate;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForContractorServices;
using Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceDetailByIds;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ContractorServices;

public class ProjectOperationDetailContractorServicesLogic : IProjectOperationDetailContractorServicesLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationDetailContractorServicesLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public ProjectOperationDetailContractorServicesLogic(
        IMediator mediator,
        ILogger<ProjectOperationDetailContractorServicesLogic> logger,
        IUnitOfWork unitOfWork,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<CreateContractorServiceResponse?>> CreateContractorService(CreateContractorServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateContractorService, ProjectOperationDetailId:{ProjectOperationDetailId}, ServiceInfoId:{ServiceInfoId},",
            request.ProjectOperationDetailId, request.ServiceInfoId);

        var isValidRequest = await request.IsValidAsync<CreateContractorServiceValidator, CreateContractorServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateContractorServiceResponse>(isValidRequest.Error!);

        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailForContractorServicesQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure)
            return Result.Failure<CreateContractorServiceResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        if (projectOperationDetail.Value is null)
            return Result.Failure<CreateContractorServiceResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectDetail = projectOperationDetail.Value;

        var serviceInfo = await _mediator.Send(
            new GetOperationInfoServiceForValidationQuery(
                projectDetail.ProjectOperation.OperationInfo.Id,
                request.ServiceInfoId),
            ct);

        if (serviceInfo.IsFailure)
        {
            return Result.Failure<CreateContractorServiceResponse>(
                serviceInfo.Error!);
        }

        if (serviceInfo.Value is null)
        {
            return Result.Failure<CreateContractorServiceResponse>(
                ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);
        }

        var serviceInfoData = serviceInfo.Value;

        if (request.ContractorId is not null)
            if (request.ContractorId != 0)
            {
                var ids = new List<long> { (long)request.ContractorId! };
                var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, ids, null, false, null), ct); // بره سراغ متا دیتا
                if (contractorsData.IsFailure)
                    return Result.Failure<CreateContractorServiceResponse>(ProjectOperationDetailErrors.UnValidContractors);
            }

        if (!string.IsNullOrEmpty(request.TimeSpant))
        {
            if (!(request.TimeSpant.Split(':')[0].Count() >= 2 && request.TimeSpant.Split(':')[1].Count() == 2))
                return Result.Failure<CreateContractorServiceResponse>(MachineryStandardErrors.TimeSpantCountError);

            if (int.Parse(request.TimeSpant.Split(':')[1]) > 59)
                return Result.Failure<CreateContractorServiceResponse>(MachineryStandardErrors.MoreThan59Min);
        }

        ProjectServiceDetail? projectServiceDetail = null;
        if (request.ProjectServiceDetailId is not null && request.ProjectServiceDetailId > 0)
        {
            var getProjectServices = await _mediator.Send(new GetsProjectServiceDetailByIdsQuery([request.ProjectServiceDetailId.Value], 1, 10), ct); // بره سراغ متا دیتا
            if (getProjectServices.IsFailure)
                return Result.Failure<CreateContractorServiceResponse>(getProjectServices.Error!);
            var projectServiceValues = getProjectServices.Value!.Data!;
            projectServiceDetail = projectServiceValues.FirstOrDefault();
        }

        var response = await _mediator.Send(new CreateContractorServiceCommand(projectDetail, serviceInfoData, projectServiceDetail, request.ContractorId, request.Volume, TimeCalculator.StringToTicks(request.TimeSpant), request.IsActive, request.Type ?? PODContractorServiceType.ServiceBased), ct);
        if (response.IsFailure)
            return Result.Failure<CreateContractorServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateContractorServiceResponse(response.Value!.Id);
    }

    public async Task<Result<AppointmentContractorResponse?>> AppointmentContractor(AppointmentContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for AppointmentContractor, ContactorId:{ContactorId}", request.ContractorId);

        var isValidRequest = await request.IsValidAsync<AppointmentContractorValidator, AppointmentContractorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<AppointmentContractorResponse>(isValidRequest.Error!);

        var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long> { request.ContractorId }, null, false, null), ct); // بره سراغ متا دیتا
        if (contractorsData.IsFailure)
            return Result.Failure<AppointmentContractorResponse>(ProjectOperationDetailErrors.UnValidContractors);

        foreach (var item in request.ContractorServiceIds)
        {
            var response = await _mediator.Send(new AppointmentContractorCommand(item, request.ContractorId), ct);
            if (response.IsFailure)
                return Result.Failure<AppointmentContractorResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new AppointmentContractorResponse(true);
    }

    public async Task<Result<UpdateContractorServiceResponse?>> UpdateContractorService(UpdateContractorServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateContractorService, ProjectOperationDetailId:{ProjectOperationDetailId}", request.ProjectOperationDetailId);

        var isValidRequest = await request.IsValidAsync<UpdateContractorServiceValidator, UpdateContractorServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateContractorServiceResponse>(isValidRequest.Error!);

        var projectOperationDetail = await _mediator.Send(new GetProjectOperationDetailForContractorServicesQuery(request.ProjectOperationDetailId), ct);
        if (projectOperationDetail.IsFailure)
            return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        if (projectOperationDetail.Value is null)
            return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        var projectDetail = projectOperationDetail.Value;

        var contractorServices = request.ContractorServiceRequests.OrderByDescending(x => x.Id != null).ToList();
        contractorServices = contractorServices.OrderByDescending(x => x.IsDeleted).ToList();

        var projectServiceDetails = new List<ProjectServiceDetail>();
        var projectServiceIds = request.ContractorServiceRequests.Where(x => x.ProjectServiceDetailId is not null && x.ProjectServiceDetailId > 0).Select(x => x.ProjectServiceDetailId!.Value).Distinct().ToList();
        if (projectServiceIds.Any())
        {
            var getProjectServices = await _mediator.Send(new GetsProjectServiceDetailByIdsQuery(projectServiceIds, 1, projectServiceIds.Count), ct); // بره سراغ متا دیتا
            if (getProjectServices.IsFailure)
                return Result.Failure<UpdateContractorServiceResponse>(getProjectServices.Error!);
            var projectServiceValues = getProjectServices.Value!.Data!;
            if (projectServiceIds.Count > projectServiceValues!.Count)
                return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.ProjectServicesNotValidate);
            projectServiceDetails = projectServiceValues;
        }

        foreach (var item in contractorServices)
        {
            var projectServiceDetail = projectServiceDetails.FirstOrDefault(x => x.Id == item.ProjectServiceDetailId);
            if (item.IsDeleted is not null && item.IsDeleted == true)
            {
                if (item.Id is not null)
                {
                    var deleteData = await _mediator.Send(new DisableContractorServiceCommand((long)item.Id, projectDetail.Id), ct);
                    if (deleteData.IsFailure)
                        return Result.Failure<UpdateContractorServiceResponse>(deleteData.Error!);
                }
                else
                    return Result.Failure<UpdateContractorServiceResponse>(ContractorServiceErrors.CanNotDelete);
            }
            else if (item.Id is not null)
            {
                var serviceInfo = await _mediator.Send(new GetOperationInfoServiceForValidationQuery(projectDetail.ProjectOperation.OperationInfo.Id, item.ServiceInfoId), ct);
                if (serviceInfo.IsFailure || serviceInfo.Value is null)
                    return Result.Failure<UpdateContractorServiceResponse>(serviceInfo.Error!);
                var serviceInfoData = projectDetail.ProjectOperation.OperationInfo.OperationInfoServices.Where(c => c.ServiceInfo.Id == item.ServiceInfoId && !c.IsDeleted).FirstOrDefault();
                if (serviceInfoData is null)
                    return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);

                if (item.ContractorId is not null)
                    if (item.ContractorId != 0)
                    {
                        var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [(long)item.ContractorId], null, false, null), ct); // بره سراغ متا دیتا
                        if (contractorsData.IsFailure)
                            return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.UnValidContractors);
                    }

                if (!string.IsNullOrEmpty(item.TimeSpant))
                {
                    if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                        return Result.Failure<UpdateContractorServiceResponse>(MachineryStandardErrors.TimeSpantCountError);

                    if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                        return Result.Failure<UpdateContractorServiceResponse>(MachineryStandardErrors.MoreThan59Min);
                }

                var updateData = await _mediator.Send(new UpdateContractorServiceCommand((long)item.Id, serviceInfoData, projectServiceDetail, item.ContractorId, item.Volume, TimeCalculator.StringToTicks(item.TimeSpant), item.IsActive), ct);
                if (updateData.IsFailure)
                    return Result.Failure<UpdateContractorServiceResponse>(updateData.Error!);
            }
            else
            {
                var serviceInfo = await _mediator.Send(new GetOperationInfoServiceForValidationQuery(projectDetail.ProjectOperation.OperationInfo.Id, item.ServiceInfoId), ct);
                if (serviceInfo.IsFailure || serviceInfo.Value is null)
                    return Result.Failure<UpdateContractorServiceResponse>(serviceInfo.Error!);
                var serviceInfoData = projectDetail.ProjectOperation.OperationInfo.OperationInfoServices.Where(c => c.ServiceInfo.Id == item.ServiceInfoId && !c.IsDeleted).FirstOrDefault();
                if (serviceInfoData is null)
                    return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.UnValidServiceInfoInOperationInfo);

                if (item.ContractorId is not null)
                    if (item.ContractorId != 0)
                    {
                        var ids = new List<long> { (long)item.ContractorId };
                        var contractorsData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, ids, null, false, null), ct); // بره سراغ متا دیتا
                        if (contractorsData.IsFailure)
                            return Result.Failure<UpdateContractorServiceResponse>(ProjectOperationDetailErrors.UnValidContractors);
                    }

                if (!string.IsNullOrEmpty(item.TimeSpant))
                {
                    if (!(item.TimeSpant.Split(':')[0].Count() >= 2 && item.TimeSpant.Split(':')[1].Count() == 2))
                        return Result.Failure<UpdateContractorServiceResponse>(MachineryStandardErrors.TimeSpantCountError);

                    if (int.Parse(item.TimeSpant.Split(':')[1]) > 59)
                        return Result.Failure<UpdateContractorServiceResponse>(MachineryStandardErrors.MoreThan59Min);
                }

                var createData = await _mediator.Send(new CreateContractorServiceCommand(projectDetail, serviceInfoData, projectServiceDetail, item.ContractorId, item.Volume, TimeCalculator.StringToTicks(item.TimeSpant), item.IsActive, item.Type ?? PODContractorServiceType.ServiceBased), ct);
                if (createData.IsFailure)
                    return Result.Failure<UpdateContractorServiceResponse>(createData.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateContractorServiceResponse(true);
    }

    public async Task<Result<InactiveDetailContractorServiceResponse?>> InactiveDetailContractorService(InactiveDetailContractorServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveDetailContractorService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveDetailContractorServiceValidator, InactiveDetailContractorServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveDetailContractorServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveDetailContractorServiceCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveDetailContractorServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveDetailContractorServiceResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveDetailContractorServiceResponse?>> ActiveDetailContractorService(ActiveDetailContractorServiceRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveDetailContractorService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveDetailContractorServiceValidator, ActiveDetailContractorServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveDetailContractorServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveDetailContractorServiceCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveDetailContractorServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveDetailContractorServiceResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerDetailContractorServicesResponse?>> StateChangerDetailContractorServices(StateChangerDetailContractorServicesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerDetailContractorServices, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerDetailContractorServicesValidator, StateChangerDetailContractorServicesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerDetailContractorServicesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerDetailContractorServicesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsDetailContractorServiceByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerDetailContractorServicesResponse>(responses.Error!);
        var values = responses.Value;

        var response = await _mediator.Send(new StateChangerDetailContractorServicesCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerDetailContractorServicesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerDetailContractorServicesResponse(true);
    }

    public async Task<Result<DisableContractorServiceResponse?>> DisableContractorService(DisableContractorServiceRequest request, CT ct)
    {
        _logger.LogInformation("Disable ContractorService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableContractorServiceValidator, DisableContractorServiceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableContractorServiceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableContractorServiceCommand(request.Id, null), ct);
        if (response.IsFailure)
            return Result.Failure<DisableContractorServiceResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableContractorServiceResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<SetContractorServiceToContractResponse?>> SetContractorServiceToContract(SetContractorServiceToContractRequest request, CT ct)
    {
        _logger.LogInformation("Disable ContractorService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<SetContractorServiceToContractValidator, SetContractorServiceToContractRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetContractorServiceToContractResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new SetContractorServiceToContractCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<SetContractorServiceToContractResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetContractorServiceToContractResponse(response.Value!.Id);
    }

    public async Task<Result<SetDetailContractorServiceToNewResponse?>> SetDetailContractorServiceToNew(SetDetailContractorServiceToNewRequest request, CT ct)
    {
        _logger.LogInformation("Disable ContractorService, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<SetDetailContractorServiceToNewValidator, SetDetailContractorServiceToNewRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetDetailContractorServiceToNewResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new SetDetailContractorServiceToNewCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<SetDetailContractorServiceToNewResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetDetailContractorServiceToNewResponse(response.Value!.Id);
    }

    public async Task<Result<GetContractorServiceByIdResponse?>> GetContractorServiceById(GetContractorServiceByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetContractorServiceById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetContractorServiceByIdValidator, GetContractorServiceByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorServiceByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetContractorServiceByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetContractorServiceByIdResponse>(response.Error!);

        var value = response.Value!;

        if (value.Type != PODContractorServiceType.ServiceBased ||
            value.OperationInfoService is null)
        {
            return Result.Failure<GetContractorServiceByIdResponse>(
                ContractorServiceErrors.ContractorServiceWithIdNotFound);
        }

        var contractorInfo = new List<UserModel?>();
        if (value.ContractorId is not null)
            if (value.ContractorId != 0)
            {
                var ids = new List<long> { (long)value.ContractorId };
                var contractorsData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids, null, null, _mediator, ct);
                contractorInfo = contractorsData;
            }

        return new GetContractorServiceByIdResponse(value.Id, value.ProjectOperationDetail.Id, value.OperationInfoService.ServiceInfo.Id, value.OperationInfoService.ServiceInfo.ServiceInfoName,
            value.OperationInfoService.ServiceInfo.ServiceInfoCode, value.ContractorId, contractorInfo?.FirstOrDefault()?.FullName, contractorInfo?.FirstOrDefault()?.OrganizationCode, value.Volume, TimeCalculator.TicksToStringHM(value.TimeSpant), value.IsActive, value.Type);
    }

    public async Task<Result<GetsContractorServiceByFilterResponse?>> GetsContractorServiceByFilter(GetsContractorServiceByFilterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractorServiceByFilter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsContractorServiceByFilterValidator, GetsContractorServiceByFilterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorServiceByFilterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsContractorServiceByFilterQuery(request.FilterData, request.CostCenterId, request.ProjectId, request.ProjectOperationIds,
            request.ServiceInfoIds, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorServiceByFilterResponse>(response.Error!);
        if (response.Value?.Data is null)
            return Result.Failure<GetsContractorServiceByFilterResponse>(response.Error!);

        var value = response.Value.Data;

        var allIds = new List<long>();
        if (value.Any(x => x.ContractorId != 0 && x.ContractorId is not null))
            allIds.AddRange(value!.Where(x => x.ContractorId != 0 && x.ContractorId is not null).Select(x => (long)x.ContractorId!).Distinct().ToList());
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct);

        var measureunitsData = new List<Measureunit?>();
        if (value.Count > 0)
        {
            var measureunitIds = value.Select(c => c.OperationInfoService.ServiceInfo.UnitOfMeasurementId).Where(x => x != 0).ToList();
            var measureUnitData = await WebServicesLogic.MeasurementDataReceiver(measureunitIds, _mediator, ct);
            if (measureUnitData is not null)
                measureunitsData = measureUnitData!;
        }

        var data = value.Adapt<List<GetsContractorServiceByFilterModel>>();
        foreach (var item in data)
        {
            var val = value.Where(x => x.Id == item.Id).FirstOrDefault();
            var user = metaDataInfos?.Where(x => x?.Id == item.ContractorId)?.FirstOrDefault();
            var measureunit = measureunitsData?.Where(x => x?.Id == val?.OperationInfoService.ServiceInfo.UnitOfMeasurementId)?.FirstOrDefault();
            item.FullName = user?.FullName;
            item.OrganizationCode = user?.OrganizationCode;
            item.UnitOfMeasurementId = val?.OperationInfoService.ServiceInfo.UnitOfMeasurementId;
            item.MeasurementName = measureunit?.Name;
        }

        return new GetsContractorServiceByFilterResponse(data.Adapt<List<GetsContractorServiceByFilterModel>>() ?? new List<GetsContractorServiceByFilterModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsContractorServiceByProjectOperationDetailIdResponse?>> GetsContractorServiceByProjectOperationDetailId(GetsContractorServiceByProjectOperationDetailIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractorServiceByProjectOperationDetailId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsContractorServiceByProjectOperationDetailIdValidator, GetsContractorServiceByProjectOperationDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorServiceByProjectOperationDetailIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsContractorServiceByProjectOperationDetailIdQuery(
            request.ProjectOperationDetailId,
            request.ServiceInfoName,
            request.ServiceInfoCode,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsContractorServiceByProjectOperationDetailIdResponse>(response.Error!);
        var values = response.Value.Data;

        var allIds = values.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct);

        var data = values.Adapt<List<GetsContractorServiceByProjectOperationDetailIdModel>>();
        var measureUnitIds = data!.SelectMany(x => new[] { x.UnitOfMeasurementId, x.ProjectServiceUnitOfMeasurementId }.Where(id => id.HasValue)
            .Select(id => id!.Value)).Distinct().ToList();
        var measureunitsData = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);
        foreach (var item in data)
        {
            item.FullName = metaDataInfos?.FirstOrDefault(x => x?.Id == item.ContractorId)?.FullName;
            item.OrganizationCode = metaDataInfos?.FirstOrDefault(x => x?.Id == item.ContractorId)?.OrganizationCode;
            item.MeasurementName = measureunitsData?.FirstOrDefault(x => x?.Id == item.UnitOfMeasurementId)?.Name;
            if (item.ProjectServiceUnitOfMeasurementId is not null)
                item.ProjectServiceMeasurementName = measureunitsData?.FirstOrDefault(x => x?.Id == item.ProjectServiceUnitOfMeasurementId)?.Name;
        }

        return new GetsContractorServiceByProjectOperationDetailIdResponse(data ?? new List<GetsContractorServiceByProjectOperationDetailIdModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectOperationDetailContractorsResponse?>> GetsProjectOperationDetailContractors(GetsProjectOperationDetailContractorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectOperationDetailContractors, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectOperationDetailContractorsValidator, GetsProjectOperationDetailContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectOperationDetailContractorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectOperationDetailContractorsQuery(request.CostCenterId, request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value?.Data is null)
            return Result.Failure<GetsProjectOperationDetailContractorsResponse>(response.Error!);
        if (response.Value?.Data.Count <= 0)
            return Result.Failure<GetsProjectOperationDetailContractorsResponse>(ProjectOperationDetailErrors.ContractorsNotFound);
        var value = response.Value!.Data.Where(x => x.HasValue).ToList();

        var allIds = value.Where(x => x.HasValue!).Select(x => x!.Value).ToList();
        var userInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, request.FilterData, true, _mediator, ct); // بره سراغ متا دیتا
        if (userInfos is null)
            return Result.Failure<GetsProjectOperationDetailContractorsResponse>(ProjectOperationDetailErrors.ContractorsNotFound);

        var newValue = userInfos.Where(x => x is not null).Select(x => x!.Id).ToList();
        var data = new List<GetsProjectOperationDetailContractorsResponseModel?>();
        if (!string.IsNullOrEmpty(request.FilterData))
            foreach (var item in newValue)
            {
                if (!userInfos!.Any(x => x?.Id == item))
                    continue;

                var user = userInfos?.Where(x => x?.Id == item)?.FirstOrDefault();
                data.Add(new GetsProjectOperationDetailContractorsResponseModel()
                {
                    Id = (long)item!,
                    FullName = user?.FullName,
                    Nickname = user?.Nickname,
                    OrganizationCode = user?.OrganizationCode,
                });
            }
        else
            foreach (var item in newValue)
            {
                var user = userInfos?.Where(x => x?.Id == item)?.FirstOrDefault();
                data.Add(new GetsProjectOperationDetailContractorsResponseModel()
                {
                    Id = (long)item!,
                    FullName = user?.FullName,
                    Nickname = user?.Nickname,
                    OrganizationCode = user?.OrganizationCode,
                });
            }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsProjectOperationDetailContractorsResponse(responseData ?? new List<GetsProjectOperationDetailContractorsResponseModel?>(0), userInfos?.Count ?? 0);
    }

    public async Task<Result<GetsServiceByProjectOperationDetailIdsResponse?>> GetsServiceByProjectOperationDetailIds(GetsServiceByProjectOperationDetailIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsServiceByProjectOperationDetailIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsServiceByProjectOperationDetailIdsValidator, GetsServiceByProjectOperationDetailIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsServiceByProjectOperationDetailIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsServiceByProjectOperationDetailIdsQuery(request.ProjectOperationDetailIds, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsServiceByProjectOperationDetailIdsResponse>(response.Error!);
        var values = response.Value?.Data;

        return new GetsServiceByProjectOperationDetailIdsResponse(values ?? new List<GetsServiceByProjectOperationDetailIdsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetFilteredProjectOperationDetailContractorsResponse?>> GetFilteredProjectOperationDetailContractors(GetFilteredProjectOperationDetailContractorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFilteredProjectOperationDetailContractors, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetFilteredProjectOperationDetailContractorsValidator, GetFilteredProjectOperationDetailContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredProjectOperationDetailContractorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFilteredProjectOperationDetailContractorsQuery(request.CostCenterIds, request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value?.Data is null)
            return Result.Failure<GetFilteredProjectOperationDetailContractorsResponse>(response.Error!);
        if (response.Value?.Data.Count <= 0)
            return Result.Failure<GetFilteredProjectOperationDetailContractorsResponse>(ProjectOperationDetailErrors.ContractorsNotFound);
        var value = response.Value!.Data.Where(x => x.HasValue).ToList();

        var allIds = value.Where(x => x.HasValue!).Select(x => x!.Value).ToList();
        var userInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, request.FilterData, null, _mediator, ct); // بره سراغ متا دیتا
        if (userInfos is null)
            return Result.Failure<GetFilteredProjectOperationDetailContractorsResponse>(ProjectOperationDetailErrors.ContractorsNotFound);

        var data = new List<GetFilteredProjectOperationDetailContractorsResponseModel?>();
        if (!string.IsNullOrEmpty(request.FilterData))
            foreach (var item in value)
            {
                if (!userInfos!.Any(x => x?.Id == item))
                    continue;

                var user = userInfos?.Where(x => x?.Id == item)?.FirstOrDefault();
                data.Add(new GetFilteredProjectOperationDetailContractorsResponseModel()
                {
                    Id = (long)item!,
                    FullName = user?.FullName,
                    Nickname = user?.Nickname,
                    OrganizationCode = user?.OrganizationCode,
                });
            }
        else
            foreach (var item in value)
            {
                var user = userInfos?.Where(x => x?.Id == item)?.FirstOrDefault();
                data.Add(new GetFilteredProjectOperationDetailContractorsResponseModel()
                {
                    Id = (long)item!,
                    FullName = user?.FullName,
                    Nickname = user?.Nickname,
                    OrganizationCode = user?.OrganizationCode,
                });
            }

        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetFilteredProjectOperationDetailContractorsResponse(responseData ?? new List<GetFilteredProjectOperationDetailContractorsResponseModel?>(0), userInfos?.Count ?? 0);
    }


    public async Task<Result<List<GetInfoByContractorServiceIdResponse>?>> GetInfoByContractorServiceId(GetInfoByContractorServiceIdRequest request, CT ct)
    {
        var response = await _mediator.Send(new GetInfoByContractorServiceIdQuery(request.GetInfoByContractorServiceId), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<List<GetInfoByContractorServiceIdResponse>>(response.Error!);
        return Result.Success(response.Value.Data);
    }


    public async Task<Result<GetOpAssignByPOResponse?>> GetOpAssignByPO(
    GetOpAssignByPORequest request,
    CT ct)
    {
        var isValidRequest = await request.IsValidAsync<
            GetOpAssignByPOValidator,
            GetOpAssignByPORequest>(ct);

        if (isValidRequest.IsFailure)
            return Result.Failure<GetOpAssignByPOResponse>(
                isValidRequest.Error!);

        var response = await _mediator.Send(
            new GetOpAssignByPOQuery(
                request.ProjectOperationId,
                request.PageIndex,
                request.PageSize),
            ct);

        if (response.IsFailure)
            return Result.Failure<GetOpAssignByPOResponse>(
                response.Error!);

        var data = response.Value?.Data ?? new List<OpAssignModel>();

        var contractorIds = data
            .Where(x => x.ContractorId.HasValue && x.ContractorId > 0)
            .Select(x => x.ContractorId!.Value)
            .Distinct()
            .ToList();

        if (contractorIds.Count > 0)
        {
            var contractors =
                await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(
                    contractorIds,
                    null,
                    null,
                    _mediator,
                    ct);

            foreach (var item in data)
            {
                var contractor = contractors?
                    .FirstOrDefault(x => x?.Id == item.ContractorId);

                item.ContractorName = contractor?.FullName;
                item.ContractorOrganizationCode = contractor?.OrganizationCode;
            }
        }

        return new GetOpAssignByPOResponse(
            data,
            response.Value?.RowCount ?? 0);
    }
    public async Task<Result<GetAssignablePODsResponse?>> GetAssignablePODs(
    GetAssignablePODsRequest request,
    CT ct)
    {
        var isValidRequest =
            await request.IsValidAsync<
                GetAssignablePODsValidator,
                GetAssignablePODsRequest>(ct);

        if (isValidRequest.IsFailure)
            return Result.Failure<GetAssignablePODsResponse>(
                isValidRequest.Error!);

        var response = await _mediator.Send(
            new GetAssignablePODsQuery(
                request.ProjectOperationId,
                request.PageIndex,
                request.PageSize),
            ct);

        if (response.IsFailure)
            return Result.Failure<GetAssignablePODsResponse>(
                response.Error!);

        return new GetAssignablePODsResponse(
            response.Value?.Data ?? new List<AssignablePODModel>(),
            response.Value?.RowCount ?? 0);
    }
    public async Task<Result<CreateOpAssignResponse?>> CreateOpAssign(
    CreateOpAssignRequest request,
    CT ct)
    {
        var isValidRequest =
            await request.IsValidAsync<
                CreateOpAssignValidator,
                CreateOpAssignRequest>(ct);

        if (isValidRequest.IsFailure)
            return Result.Failure<CreateOpAssignResponse>(
                isValidRequest.Error!);

        var detailIds = request.Items
            .Select(x => x.ProjectOperationDetailId)
            .ToList();

        if (detailIds.Count != detailIds.Distinct().Count())
            return Result.Failure<CreateOpAssignResponse>(
                GlobalErrors.IdsNotEqual);

        var contractorsData = await _mediator.Send(
            new GetWithSkillOnlyByIdsQuery(
                1,
                1,
                new List<long> { request.ContractorId },
                null,
                false,
                null),
            ct);

        if (contractorsData.IsFailure ||
            contractorsData.Value?.Data is null ||
            !contractorsData.Value.Data.Any(x => x?.Id == request.ContractorId))
        {
            return Result.Failure<CreateOpAssignResponse>(
                ProjectOperationDetailErrors.UnValidContractors);
        }

        var detailsResponse = await _mediator.Send(
            new GetOpAssignDetailsByIdsQuery(
                request.ProjectOperationId,
                detailIds),
            ct);

        if (detailsResponse.IsFailure ||
            detailsResponse.Value is null)
        {
            return Result.Failure<CreateOpAssignResponse>(
                ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }

        var projectOperationDetails = detailsResponse.Value;

        if (projectOperationDetails.Count != detailIds.Count)
            return Result.Failure<CreateOpAssignResponse>(
                ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

        foreach (var item in request.Items)
        {
            var projectOperationDetail =
                projectOperationDetails.FirstOrDefault(x =>
                    x.Id == item.ProjectOperationDetailId);

            if (projectOperationDetail is null)
                return Result.Failure<CreateOpAssignResponse>(
                    ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

            var contractAllocatedQuantity =
                await _repository.GetContractAllocatedConstructionQuantity(
                    projectOperationDetail.Id,
                    null,
                    ct);

            var createResponse = await _mediator.Send(
                new CreateOpAssignCommand(
                    projectOperationDetail,
                    request.ContractorId,
                    item.Volume,
                    contractAllocatedQuantity),
                ct);

            if (createResponse.IsFailure)
                return Result.Failure<CreateOpAssignResponse>(
                    createResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new CreateOpAssignResponse(true);
    }
    public async Task<Result<UpdateOpAssignResponse?>> UpdateOpAssign(
    UpdateOpAssignRequest request,
    CT ct)
    {
        var isValidRequest =
            await request.IsValidAsync<
                UpdateOpAssignValidator,
                UpdateOpAssignRequest>(ct);

        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateOpAssignResponse>(
                isValidRequest.Error!);

        var contractorsData = await _mediator.Send(
            new GetWithSkillOnlyByIdsQuery(
                1,
                1,
                new List<long> { request.ContractorId },
                null,
                false,
                null),
            ct);

        if (contractorsData.IsFailure)
            return Result.Failure<UpdateOpAssignResponse>(
                ProjectOperationDetailErrors.UnValidContractors);

        var assignmentResponse = await _mediator.Send(
            new GetOpAssignForUpdateQuery(request.Id),
            ct);

        if (assignmentResponse.IsFailure ||
            assignmentResponse.Value is null)
        {
            return Result.Failure<UpdateOpAssignResponse>(
                ContractorServiceErrors.OperationBasedAssignmentNotFound);
        }

        var contractAllocatedQuantity =
            await _repository.GetContractAllocatedConstructionQuantity(
                assignmentResponse.Value.ProjectOperationDetailId,
                null,
                ct);

        var updateResponse = await _mediator.Send(
            new UpdateOpAssignCommand(
                assignmentResponse.Value,
                request.ContractorId,
                request.Volume,
                contractAllocatedQuantity),
            ct);

        if (updateResponse.IsFailure ||
            updateResponse.Value is null)
        {
            return Result.Failure<UpdateOpAssignResponse>(
                updateResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new UpdateOpAssignResponse(
            updateResponse.Value.Id,
            true);
    }
    public async Task<Result<DeleteOpAssignResponse?>> DeleteOpAssign(
    DeleteOpAssignRequest request,
    CT ct)
    {
        var isValidRequest =
            await request.IsValidAsync<
                DeleteOpAssignValidator,
                DeleteOpAssignRequest>(ct);

        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteOpAssignResponse>(
                isValidRequest.Error!);

        var assignmentResponse = await _mediator.Send(
            new GetOpAssignForUpdateQuery(request.Id),
            ct);

        if (assignmentResponse.IsFailure ||
            assignmentResponse.Value is null)
        {
            return Result.Failure<DeleteOpAssignResponse>(
                ContractorServiceErrors.OperationBasedAssignmentNotFound);
        }

        var assignment = assignmentResponse.Value;

        if (assignment.ContractorContractDetailServices
            .Any(x => !x.IsDeleted))
        {
            return Result.Failure<DeleteOpAssignResponse>(
                ContractorServiceErrors.OperationBasedAssignmentHasContract);
        }

        var deleteResponse = await _mediator.Send(
            new DisableContractorServiceCommand(
                request.Id,
                assignment.ProjectOperationDetailId),
            ct);

        if (deleteResponse.IsFailure ||
            deleteResponse.Value is null)
        {
            return Result.Failure<DeleteOpAssignResponse>(
                deleteResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new DeleteOpAssignResponse(
            deleteResponse.Value.Id,
            deleteResponse.Value.IsDeleted);
    }
}
