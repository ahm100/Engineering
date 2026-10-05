using Engineering.Application.ContractorServices.Models.CreateContractors;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.Contractors.Commands.ContractorEmployees.ChangeActiveContractorEmployee;
using Engineering.Application.Services.Contractors.Commands.ContractorEmployees.CreateContractorEmployee;
using Engineering.Application.Services.Contractors.Commands.ContractorEmployees.DeleteContractorEmployee;
using Engineering.Application.Services.Contractors.Commands.ContractorEmployees.UpdateContractorEmployee;
using Engineering.Application.Services.Contractors.Commands.ContractorServices.CreateContractorService;
using Engineering.Application.Services.Contractors.Commands.ContractorServices.DeleteContractorService;
using Engineering.Application.Services.Contractors.Commands.ContractorServices.UpdateContractorService;
using Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetContractorEmployeesByContractorId;
using Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetFilteredContractors;
using Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetContractorEmployeesByContractorId;
using Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetsContractorEmployeeBySkill;
using Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorsByServiceIds;
using Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorServicesByContractorId;
using Engineering.Application.Services.Contractors.Queries.ContractorServices.GetFilteredContractors;
using Engineering.Application.Services.ProjectServices.Queries.GetServiceContractors;
using Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Queries.GetById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillByCode;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Commands.AddSkillForThirdPartyCommand;

namespace Engineering.Application.ContractorServices;

public class ContractorLogic : IContractorLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractorLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public ContractorLogic(
        IMediator mediator,
        ILogger<ContractorLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateContractorsResponse?>> CreateContractors(
        CreateContractorsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateContractorsValidator, CreateContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateContractorsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateContractorsResponse>(companyResponse.Error!);
        }

        if (request.ServiceInfoIds is not null)
        {
            var services = request.ServiceInfoIds.OrderByDescending(x => x.Id != null).ThenByDescending(x => x.IsDeleted).ToList();
            foreach (var contractorService in services)
                if (contractorService.IsDeleted && contractorService.Id.HasValue)
                {
                    var deleteResponse = await _mediator.Send(new DeleteContractorServiceCommand(contractorService.Id!.Value!), ct);
                    if (deleteResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(deleteResponse.Error!);
                }
                else if (!contractorService.IsDeleted && contractorService.Id.HasValue)
                {
                    var updateResponse = await _mediator.Send(new UpdateContractorServiceCommand(contractorService.Id.Value!, contractorService.ServiceInfoId, request.ContractorId, contractorService.IsActive, companyId), ct);
                    if (updateResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(updateResponse.Error!);
                }
                else if (!contractorService.Id.HasValue)
                {
                    var createResponse = await _mediator.Send(new CreateContractorServiceCommand(contractorService.ServiceInfoId, request.ContractorId, companyId), ct);
                    if (createResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(createResponse.Error!);
                }
        }

        if (request.Employees is not null)
        {
            var employees = request.Employees.OrderByDescending(x => x.Id != null).ThenByDescending(x => x.IsDeleted).ToList();
            foreach (var item in employees)
            {
                if (item.IsDeleted && item.Id.HasValue)
                {
                    var deleteResponse = await _mediator.Send(new DeleteContractorEmployeeCommand(item.Id.Value!), ct);
                    if (deleteResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(deleteResponse.Error!);
                }

                var employeeQuery = await _mediator.Send(new ContractorEmployeesByIdQuery(item.EmployeeId, 1, 10), ct);
                if (employeeQuery.IsFailure)
                    return Result.Failure<CreateContractorsResponse>(employeeQuery.Error!);

                if (!item.IsDeleted && item.Id.HasValue)
                {
                    var updateResponse = await _mediator.Send(new UpdateContractorEmployeeCommand(item.Id.Value!, item.EmployeeId, request.ContractorId, companyId), ct);
                    if (updateResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(updateResponse.Error!);

                    var changeActiveResponse = await _mediator.Send(new ChangeActiveContractorEmployeeCommand(item.Id.Value!, item.IsActive, item.IsConfirm), ct);
                    if (changeActiveResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(changeActiveResponse.Error!);
                }
                else if (item.Id is null)
                {
                    var createResponse = await _mediator.Send(new CreateContractorEmployeeCommand(item.EmployeeId, request.ContractorId, item.IsConfirm, item.IsActive, companyId), ct);
                    if (createResponse.IsFailure)
                        return Result.Failure<CreateContractorsResponse>(createResponse.Error!);
                }
            }
        }

        var contractorsResponse = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 10, new List<long> { request.ContractorId }, null, false, null), ct);
        if (contractorsResponse.IsFailure || contractorsResponse.Value is null || contractorsResponse.Value.Data is null)
            return Result.Failure<CreateContractorsResponse>(ContractorServicesErrors.ContractorNotFound!);
        var contractorResponse = contractorsResponse.Value.Data.FirstOrDefault();
        if ((contractorResponse!.Skills is null) || (contractorResponse!.Skills is not null && !contractorResponse!.Skills.Any(x => x!.Code == "2")))
        {
            var contractorSkillResponse = await _mediator.Send(new GetSkillByCodeQuery("2"), ct);
            if (contractorSkillResponse.IsFailure)
                return Result.Failure<CreateContractorsResponse>(contractorSkillResponse.Error!);

            var addSkillToContractor = await _mediator.Send(new AddSkillForThirdPartyCommand(request.ContractorId, contractorSkillResponse.Value!.Id!.Value), ct);
            if (addSkillToContractor.IsFailure)
                return Result.Failure<CreateContractorsResponse>(addSkillToContractor.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateContractorsResponse(true);
    }

    public async Task<Result<GetContractorsByServiceIdsResponse?>> GetContractorsByServiceIds(
        GetContractorsByServiceIdsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetContractorsByServiceIdsValidator, GetContractorsByServiceIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorsByServiceIdsResponse>(isValidRequest.Error!);

        List<long>? contractorIds = [];
        if (request.ProjectId is not null && request.ProjectId > 0)
        {
            var getContractorIdsQuery = await _mediator.Send(new GetServiceContractorsQuery(request.ProjectId, request.Ids), ct);
            contractorIds = getContractorIdsQuery.Value;
        }

        if (contractorIds?.Count <= 0 || contractorIds is null)
        {
            var getResponseQuery = await _mediator.Send(new GetContractorsByServiceIdsQuery(request.Ids), ct);
            if (getResponseQuery.IsFailure || getResponseQuery.Value is null)
                return Result.Failure<GetContractorsByServiceIdsResponse>(ContractorServiceErrors.NotFound);
            contractorIds = getResponseQuery.Value;
        }

        var contractors = await GetContractors(contractorIds, request.FilterData, ct);
        if (contractors?.Count <= 0 || contractors is null)
            return Result.Failure<GetContractorsByServiceIdsResponse>(ContractorServiceErrors.ContactorsNotActive);

        var data = new List<GetContractorsByServiceIdsModel>();
        contractors.ForEach(item =>
            data.Add(new GetContractorsByServiceIdsModel(item!.Id, item.FirstName + " " + item.LastName, item.Nickname, item.OrganizationCode, item!.DefaultPhoneNo, item.IsActive)));

        var result = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetContractorsByServiceIdsResponse(result ?? new List<GetContractorsByServiceIdsModel>(0), result!.Count);
    }

    public async Task<Result<GetContractorServicesByContractorIdResponse?>> GetContractorServicesByContractorId(
        GetContractorServicesByContractorIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetContractorServicesByContractorIdValidator, GetContractorServicesByContractorIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorServicesByContractorIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getResponse = await _mediator.Send(new GetContractorServicesByContractorIdQuery(request.Id, request.FilterData, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getResponse.IsFailure || getResponse.Value is null || getResponse.Value.Data is null)
            return Result.Failure<GetContractorServicesByContractorIdResponse>(ContractorServicesErrors.ContractorServiceIdNotFound!);
        var values = getResponse.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = values?.Where(x => x.ServiceInfo.UnitOfMeasurementId > 0).Select(x => x.ServiceInfo.UnitOfMeasurementId).ToList(); //متد برای جمع آوری همه شناسه ها
        var measureUnitsData = await WebServicesLogic.MeasurementDataReceiver(allIds, _mediator, ct); // بره سراغ متا دیتا

        var data = values.Adapt<List<GetContractorServicesByContractorIdModel>>();
        foreach (var item in data)
        {
            item.ServiceInfo!.MeasurementName = measureUnitsData?.FirstOrDefault(x => x.Id == item.ServiceInfo?.UnitOfMeasurementId)?.Name;

            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetContractorServicesByContractorIdResponse(data ?? new List<GetContractorServicesByContractorIdModel>(0), getResponse.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetContractorEmployeesByContractorIdResponse?>> GetContractorEmployeesByContractorId(
        GetContractorEmployeesByContractorIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetContractorEmployeesByContractorIdValidator, GetContractorEmployeesByContractorIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorEmployeesByContractorIdResponse>(isValidRequest.Error!);

        var getResponse = await _mediator.Send(new GetContractorEmployeesByContractorIdQuery(new List<long>() { request.Id }, request.EmployeeId), ct);
        if (getResponse.IsFailure)
            return Result.Failure<GetContractorEmployeesByContractorIdResponse>(getResponse.Error!);
        var values = getResponse.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var ids = values?.Select(x => x.EmployeeId).ToList();
        var employeeQuery = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids, null, null, _mediator, ct);

        var data = new List<GetContractorEmployeesByContractorIdModel>();
        foreach (var contractorEmployee in values!)
        {
            var company = companies?.Where(x => x.Id == contractorEmployee.CompanyId).FirstOrDefault();
            var employee = employeeQuery?.Where(x => x?.Id == contractorEmployee.EmployeeId).FirstOrDefault();
            data.Add(new GetContractorEmployeesByContractorIdModel(contractorEmployee.Id, contractorEmployee.EmployeeId, employee?.FullName, employee?.OrganizationCode, contractorEmployee.IsActive, contractorEmployee.CompanyId, company?.NameFa));
        }

        return new GetContractorEmployeesByContractorIdResponse(data, getResponse.Value!.RowCount!);
    }

    public async Task<Result<GetsContractorEmployeeBySkillResponse?>> GetsContractorEmployeeBySkill(
        GetsContractorEmployeeBySkillRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsContractorEmployeeBySkillValidator, GetsContractorEmployeeBySkillRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorEmployeeBySkillResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var getResponse = await _mediator.Send(new GetsContractorEmployeeBySkillQuery(request.ContractorId, companyId), ct);
        if (getResponse.IsFailure)
            return Result.Failure<GetsContractorEmployeeBySkillResponse>(getResponse.Error!);

        var employeeIds = getResponse.Value!.Data!.Select(x => x.EmployeeId).Distinct().ToList();
        if (employeeIds.Count <= 0)
            return Result.Failure<GetsContractorEmployeeBySkillResponse>(ContractorServiceErrors.EmploeeNotFound);
        var userInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(employeeIds, null, null, _mediator, ct);

        var getUsers = userInfos?.Where(u => u?.Skills != null && u.Skills.Any(s => s?.Id == request.SkillId)).ToList();
        if (getUsers?.Count <= 0)
            return Result.Failure<GetsContractorEmployeeBySkillResponse>(ContractorServiceErrors.EmploeeNotFound);

        var data = new List<GetsContractorEmployeeBySkillModel>();
        foreach (var item in getUsers!)
        {
            var user = getResponse.Value.Data!.Where(x => x.EmployeeId == item!.Id).FirstOrDefault();
            if (user is not null && !user.IsActive)
                continue;

            data.Add(new(user!.Id, user.EmployeeId, item!.FullName, item.Nickname, item.OrganizationCode, user.IsActive));
        }

        var result = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsContractorEmployeeBySkillResponse(result, data.Count);
    }

    public async Task<Result<GetFilteredContractorsResponse?>> GetFilteredContractors(
        GetFilteredContractorsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredContractorsValidator, GetFilteredContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredContractorsResponse>(isValidRequest.Error!);

        var getResponseQuery = await _mediator.Send(new GetFilteredContractorsQuery(request.ProjectId, request.ProjectOperationIds), ct);
        if (getResponseQuery.IsFailure)
            return Result.Failure<GetFilteredContractorsResponse>(ContractorServiceErrors.NotFound);
        if (getResponseQuery.Value is null || getResponseQuery.Value.Count == 0)
            return Result.Failure<GetFilteredContractorsResponse>(ContractorServiceErrors.NotFound);

        var distinctContractorsIds = getResponseQuery.Value!.Distinct().ToList();
        var getContractorsQuery = await WebServicesLogic.GetFilteredByIdsDataReceiver(distinctContractorsIds, request.FilterData, _mediator, ct);
        var contractors = getContractorsQuery?.Where(x => x?.IsActive == true).ToList();
        if (contractors?.Count <= 0 || contractors is null)
            return Result.Failure<GetFilteredContractorsResponse>(ContractorServiceErrors.ContactorsNotActive);

        var data = new List<GetFilteredContractorsModel>();
        foreach (var item in contractors)
        {
            data.Add(new GetFilteredContractorsModel()
            {
                Id = item!.Id,
                DefaultPhoneNo = item!.DefaultPhoneNo,
                FullName = item.FirstName + " " + item.LastName,
                OrganizationCode = item.OrganizationCode,
                IsActive = item.IsActive,
            });
        }
        var result = data.SetPaging(request.PageIndex - 1, request.PageSize);

        return new GetFilteredContractorsResponse(result ?? new List<GetFilteredContractorsModel>(0), result!.Count);
    }

    private async Task<List<FilteredUserModel>?> GetContractors(
        List<long>? contractorIds, string? filterData, CT ct)
    {
        List<FilteredUserModel>? data = [];
        if (contractorIds is not null && contractorIds.Count > 0)
        {
            var getContractorsQuery = await WebServicesLogic.GetFilteredByIdsDataReceiver(contractorIds.Distinct().ToList(), filterData, _mediator, ct);
            var contractors = getContractorsQuery?.Where(x => x?.IsActive == true).ToList();
            data = contractors;
        }
        return data;
    }
}
