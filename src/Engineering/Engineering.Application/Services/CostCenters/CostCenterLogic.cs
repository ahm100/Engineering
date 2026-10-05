using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.ProjectCostCenterRequests;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.IdentityServices.Roles.Models;
using Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;
using Engineering.Application.IdentityServices.Users.Models;
using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.Services.CostCenters.Commands.ActiveCostCenter;
using Engineering.Application.Services.CostCenters.Commands.CodeCreator;
using Engineering.Application.Services.CostCenters.Commands.CreateCostCenter;
using Engineering.Application.Services.CostCenters.Commands.Delete;
using Engineering.Application.Services.CostCenters.Commands.InactiveCostCenter;
using Engineering.Application.Services.CostCenters.Commands.StateChangerCostCenters;
using Engineering.Application.Services.CostCenters.Commands.UpdateCostCenter;
using Engineering.Application.Services.CostCenters.Models.ActiveCostCenter;
using Engineering.Application.Services.CostCenters.Models.CodeCreator;
using Engineering.Application.Services.CostCenters.Models.CostCenterGroupDelete;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels.Responses;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels.Services;
using Engineering.Application.Services.CostCenters.Models.CreateCostCenter;
using Engineering.Application.Services.CostCenters.Models.Delete;
using Engineering.Application.Services.CostCenters.Models.GetActiveCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;
using Engineering.Application.Services.CostCenters.Models.GetContractorCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterByCode;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterById;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterByName;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;
using Engineering.Application.Services.CostCenters.Models.GetCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetFilteredCostCenterCities;
using Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedRoleId;
using Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedUserId;
using Engineering.Application.Services.CostCenters.Models.GetsByCityId;
using Engineering.Application.Services.CostCenters.Models.GetsByEmployerId;
using Engineering.Application.Services.CostCenters.Models.GetsByNameOrCode;
using Engineering.Application.Services.CostCenters.Models.GetsByTypeId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByIds;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByProjectManagerId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByWarehouse;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelEnum;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelExporter;
using Engineering.Application.Services.CostCenters.Models.InactiveCostCenter;
using Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;
using Engineering.Application.Services.CostCenters.Models.UpdateCostCenter;
using Engineering.Application.Services.CostCenters.Queries.GetActiveCostCenters;
using Engineering.Application.Services.CostCenters.Queries.GetCompaniesWork;
using Engineering.Application.Services.CostCenters.Queries.GetContractorCostCenters;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCode;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterById;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterByName;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterHistory;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenters;
using Engineering.Application.Services.CostCenters.Queries.GetFilteredCostCenterCities;
using Engineering.Application.Services.CostCenters.Queries.GetsActiveAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Queries.GetsActiveMainWarehouseCostCenter;
using Engineering.Application.Services.CostCenters.Queries.GetsAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Queries.GetsByAuthorizedRoleId;
using Engineering.Application.Services.CostCenters.Queries.GetsByAuthorizedUserId;
using Engineering.Application.Services.CostCenters.Queries.GetsByCityId;
using Engineering.Application.Services.CostCenters.Queries.GetsByEmployerId;
using Engineering.Application.Services.CostCenters.Queries.GetsByNameOrCode;
using Engineering.Application.Services.CostCenters.Queries.GetsByTypeId;
using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByContractorId;
using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByIds;
using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByProjectManagerId;
using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByWarehouse;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterWarehouses.Commands.Create;
using Engineering.Application.Services.CostCenterWarehouses.Commands.DeleteWarehouseFromCostCenter;
using Engineering.Application.Services.CostCenterWarehouses.Commands.UpdateWarehouseFromCostCenter;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCityById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetsMainWarehouseIds;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.PreferentialTemporary.Commands.CreatePreferentialTemporary;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;
using Gita.Backend.Shared.Domain.Constants;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;
using WarehouseEntity = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Models.Warehouse;

namespace Engineering.Application.Services.CostCenters;

public class CostCenterLogic : ICostCenterLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CostCenterLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly IViewWarehouseRepository _viewWarehouseRepo;
    private readonly IViewThirdPartyRepository _viewThirdPartyRepo;
    private readonly IProjectCostCenterRequestRepository _pcrRepository;

    public CostCenterLogic(IMediator mediator,
        ILogger<CostCenterLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IViewWarehouseRepository viewWarehouseRepo,
        IViewThirdPartyRepository viewThirdPartyRepo,
        IUserInfoService userInfoService,
        IProjectCostCenterRequestRepository pcrRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _viewWarehouseRepo = viewWarehouseRepo;
        _userInfoService = userInfoService;
        _viewThirdPartyRepo = viewThirdPartyRepo;
        _pcrRepository = pcrRepository;
    }

    public async Task<Result<CreateCostCenterResponse?>> CreateCostCenter(
    CreateCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for new CostCenter, " +
            "CostCenterCode:{CostCenterCode}, CostCenterName:{CostCenterName}, NoOperationDays:{NoOperationDays}," +
            "Address:{Address},Latitude:{Latitude},Longitude:{Longitude},Description:{Description}," +
            "WeatherState:{WeatherState},IsActive:{IsActive}",
            request.CostCenterCode, request.CostCenterName, request.NoOperationDays, request.Address, request.Latitude,
            request.Longitude, request.Description, request.WeatherState, request.IsActive);

        var isValidRequest = await request.IsValidAsync<CreateCostCenterValidator, CreateCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCostCenterResponse>(isValidRequest.Error!);

        ProjectCostCenterRequest? pcr = null;
        if (request.ProjectCostCenterRequestId.HasValue && request.ProjectCostCenterRequestId.Value > 0)
        {
            // Project and its ProjectCostCenters are included via Repository
            pcr = await _pcrRepository.GetById(request.ProjectCostCenterRequestId.Value, ct);
            if (pcr is null)
                return Result.Failure<CreateCostCenterResponse>(ProjectErrors.CostCenterRequestNotFound);

            if (!pcr.CanEdit())
                return Result.Failure<CreateCostCenterResponse>(ProjectErrors.CostCenterRequestIsNotActive);

            // Enforce city lock (City from request must match Project's city)
            if (pcr.Project.CityId != request.CityId)
                return Result.Failure<CreateCostCenterResponse>(ProjectErrors.CostCenterCityMustMatchProject);
        }

        long? companyId = _userInfoService.UserCompanyId <= 0 ||
            _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateCostCenterResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCostCenterByNameQuery(request.CostCenterName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetCostCenterByCodeQuery(request.CostCenterCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.CodeIsDuplicate);

        //Validate CostCenterType
        var costCenterType = await _mediator.Send(new GetCostCenterTypeByIdQuery(request.CostCenterTypeId), ct);
        if (costCenterType.IsFailure)
            return Result.Failure<CreateCostCenterResponse>(costCenterType.Error!);

        //Validate City
        var cityData = await _mediator.Send(new GetCityByIdQuery(request.CityId), ct);
        if (cityData.IsFailure)
            return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.CityIdNotValid);

        //Validate Warehouse
        if (request.CostCenterWarehouses is not null)
            if (request.CostCenterWarehouses.Count == 1)
            {
                var requestWarehouse = request.CostCenterWarehouses.FirstOrDefault();
                var warehousesIsValid = await requestWarehouse!.IsValidAsync<CostCenterWarehouseValidator, CostCenterWarehouseRequest>(ct);
                if (warehousesIsValid.IsFailure)
                    return Result.Failure<CreateCostCenterResponse>(warehousesIsValid.Error!);

                var warehouseDatas = await _mediator.Send(new GetsWarehouseByIdQuery(1, 1, [requestWarehouse!.Id]), ct);
                if (warehouseDatas.IsFailure)
                    return Result.Failure<CreateCostCenterResponse>(warehouseDatas.Error!);
            }
            else
            {
                if (request.CostCenterWarehouses.Where(x => x!.IsDefault).Count() > 1)
                    return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.CostCenterWarehousesHaveMoreDefault!);
                if (request.CostCenterWarehouses.Where(x => x!.IsDefault).Count() < 1)
                    return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.CostCenterWarehousesHaveNoDefault!);

                var ids = new List<long?>();
                foreach (var item in request.CostCenterWarehouses)
                {
                    var warehousesIsValid = await item!.IsValidAsync<CostCenterWarehouseValidator, CostCenterWarehouseRequest>(ct);
                    if (warehousesIsValid.IsFailure)
                        return Result.Failure<CreateCostCenterResponse>(warehousesIsValid.Error!);
                    ids.Add(item!.Id);
                }
                var warehouseDatas = await _mediator.Send(new GetsWarehouseByIdQuery(1, ids.Count, ids), ct);
                if (warehouseDatas.IsFailure)
                    return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.UnValidWarehouses);
            }

        //Validate InformedUsers
        if (request.InformedUsers is not null)
            if (request.InformedUsers.Count > 0)
            {
                var Ids = request.InformedUsers.Select(c => (long)c!).Where(x => x != 0).ToList();
                var informedUsersData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, Ids.Count, Ids, null, false, null), ct);
                if (informedUsersData.IsFailure)
                    return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.UnValidInformedUsers);
            }

        //Validate AuthorizedRoles
        if (request.AuthorizedRoles is not null)
            if (request.AuthorizedRoles.Count > 0)
            {
                var ids = new List<long>();
                foreach (var item in request.AuthorizedRoles)
                    ids.Add((long)item!);
                var authorizedRolesData = await _mediator.Send(new GetsRoleByIdQuery(ids), ct);
                if (authorizedRolesData.IsFailure)
                    return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.UnValidAuthorizedRoles);
            }

        //Validate AuthorizedUsers
        if (request.AuthorizedUsers is not null)
            if (request.AuthorizedUsers.Count > 0)
            {
                var ids = new List<long>();
                foreach (var item in request.AuthorizedUsers)
                    ids.Add((long)item!);
                var authorizedUsersData = await _mediator.Send(new GetsUserByIdQuery(ids), ct);
                if (authorizedUsersData.IsFailure)
                    return Result.Failure<CreateCostCenterResponse>(CostCenterErrors.UnValidAuthorizedUsers);
            }

        var response = await _mediator.Send(new CreateCostCenterCommand(costCenterType.Value!,
            request.CostCenterCode,
            request.CostCenterName,
            request.CostCenterEnName,
            request.CostCenterWarehouses!,
            request.InformedUsers,
            request.AuthorizedRoles,
            request.AuthorizedUsers,
            request.ProjectIds,
            request.NoOperationDays,
            request.CityId,
            request.Address,
            request.PostalCode,
            request.Latitude,
            request.Longitude,
            request.Description,
            request.DescriptionEn,
            request.WeatherState,
            request.IsActive,
            request.IsDefault,
            companyId,
            pcr), ct);
        if (response.IsFailure)
            return Result.Failure<CreateCostCenterResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        var value = response.Value!;

        try
        {
            var createPreferentialTemporary = new CreatePreferentialTemporaryCommand(
                value.GetPreferentialName(),
                DepartmentNames.Engineering.CostCenter,
                value.PreferentialReferenceCode);

            var createPreferentialTemporaryResponse = await _mediator.Send(createPreferentialTemporary, ct);
            if (createPreferentialTemporaryResponse.IsFailure)
                _logger.LogError("CreatePreferentialTemporary for CostCenter with id of {CostCenterId} failed with Code:{Code}, Msg:{Msg}",
                    value.Id,
                    createPreferentialTemporaryResponse.Error!.Code,
                    createPreferentialTemporaryResponse.Error!.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return new CreateCostCenterResponse(value.Id, true);
    }

    public async Task<Result<CostCenterCodeCreatorResponse?>> CodeCreator(
        CostCenterCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CostCenterCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CostCenterCodeCreatorResponse>(response.Error!);

        return new CostCenterCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<UpdateCostCenterResponse?>> UpdateCostCenter(
        UpdateCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for new CostCenter, CostCenterCode:{CostCenterCode}, CostCenterName:{CostCenterName},NoOperationDays:{NoOperationDays},Address:{Address},Latitude:{Latitude},Longitude:{Longitude},Description:{Description},WeatherState:{WeatherState},IsActive:{IsActive}",
            request.CostCenterCode, request.CostCenterName, request.NoOperationDays, request.Address, request.Latitude, request.Longitude, request.Description, request.WeatherState, request.IsActive);

        var isValidRequest = await request.IsValidAsync<UpdateCostCenterValidator, UpdateCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCostCenterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateCostCenterResponse>(companyResponse.Error!);
        }

        var costCenterResponse = await _mediator.Send(new GetCostCenterByIdQuery(request.Id), ct);
        if (costCenterResponse.IsFailure)
            return Result.Failure<UpdateCostCenterResponse>(costCenterResponse.Error!);
        var costCenter = costCenterResponse.Value!;

        var nameDuplicate = await _mediator.Send(new GetCostCenterByNameQuery(request.CostCenterName, companyId), ct);
        if (nameDuplicate is { IsSuccess: true, Value: not null, } && nameDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.NameIsDuplicate);

        var codeDuplicate = await _mediator.Send(new GetCostCenterByCodeQuery(request.CostCenterCode, companyId), ct);
        if (codeDuplicate is { IsSuccess: true, Value: not null, } && codeDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.CodeIsDuplicate);

        //Validate CostCenterType
        var costCenterType = await _mediator.Send(new GetCostCenterTypeByIdQuery(request.CostCenterTypeId), ct);
        if (costCenterType.IsFailure)
            return Result.Failure<UpdateCostCenterResponse>(costCenterType.Error!);
        if (costCenterType.Value is null)
            return Result.Failure<UpdateCostCenterResponse>(costCenterType.Error!);

        //Validate City
        var cityData = await _mediator.Send(new GetCityByIdQuery(request.CityId), ct); // بره سراغ متا دیتا
        if (cityData.IsFailure)
            return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.CityIdNotValid);

        //Validate Warehouse
        var warehouseData = new List<CostCenterWarehouseResponse?>();
        if (request.CostCenterWarehouses is not null && request.CostCenterWarehouses.Count > 0)
        {
            if (request.CostCenterWarehouses.Where(x => x!.IsDefault).Count() > 1)
                return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.CostCenterWarehousesHaveMoreDefault!);
            if (request.CostCenterWarehouses.Where(x => x!.IsDefault).Count() < 1)
                return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.CostCenterWarehousesHaveNoDefault!);
            if (request.CostCenterWarehouses.Where(x => x!.IsDefault && x.IsDeleted).Count() == 1)
                return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.CostCenterDefaultWarehouseCantDelete!);

            var ids = new List<long?>();
            ids = request.CostCenterWarehouses.Select(x => (long?)x!.Id).ToList();
            if (costCenter.CostCenterWarehouses.Count > 0)
                ids.AddRange(costCenter.CostCenterWarehouses.Select(x => (long?)x.WarehouseId));

            var warehouseDatas = await _viewWarehouseRepo.GetByIds(ids.NullListed(x => x), ct); // بره سراغ انبار
            if (warehouseDatas is null || warehouseDatas.Count < 1)
                return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.UnValidWarehouses);

            var managers = await _viewThirdPartyRepo.GetByIds(warehouseData.NullListed(x => x.ManagerId), ct);

            if (costCenter.CostCenterWarehouses is not null && costCenter.CostCenterWarehouses.Count > 0)
            {
                var costCenterDefault = costCenter.CostCenterWarehouses.Where(x => x.IsDefault).FirstOrDefault();
                var requestDefault = request.CostCenterWarehouses.Where(x => x!.IsDefault).FirstOrDefault();
                foreach (var item in request.CostCenterWarehouses)
                {
                    if (item!.IsDeleted == true && item.CostCenterWarehouseId is not null && item.CostCenterWarehouseId != 0)
                    {
                        if (requestDefault!.CostCenterWarehouseId == item.CostCenterWarehouseId)
                            return Result.Failure<UpdateCostCenterResponse>(CostCenterWarehouseErrors.IsDefaultWarehouse);

                        var deleteResponse = await _mediator.Send(new DeleteWarehouseFromCostCenterCommand((long)item.CostCenterWarehouseId!), ct);
                        if (deleteResponse.IsFailure)
                            return Result.Failure<UpdateCostCenterResponse>(deleteResponse.Error!);
                    }
                    else
                    {
                        if (item.IsDefault && item.CostCenterWarehouseId is not null && item.CostCenterWarehouseId != 0 && item.CostCenterWarehouseId != costCenterDefault?.Id)
                        {
                            var updateResponse = await _mediator.Send(new UpdateWarehouseFromCostCenterCommand((long)item.CostCenterWarehouseId, item.Id, item.IsDefault), ct);
                            if (updateResponse.IsFailure)
                                return Result.Failure<UpdateCostCenterResponse>(updateResponse.Error!);
                            var warehouse = warehouseDatas?.Where(x => x.Id == item?.Id).FirstOrDefault();
                            var manager = managers.FirstOrDefault(x => warehouse?.ManagerId == x.Id);
                            warehouseData.Add(new CostCenterWarehouseResponse(updateResponse.Value!.Id, item!.Id, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                                warehouse?.Contact, manager?.FirstName + " " + manager?.LastName, warehouse?.Code, warehouse?.Name, item.IsDefault));
                        }
                        else if (!item.IsDefault && item.CostCenterWarehouseId is not null && item.CostCenterWarehouseId != 0 && item.CostCenterWarehouseId == costCenterDefault?.Id)
                        {
                            var updateResponse = await _mediator.Send(new UpdateWarehouseFromCostCenterCommand((long)item.CostCenterWarehouseId, item.Id, item.IsDefault), ct);
                            if (updateResponse.IsFailure)
                                return Result.Failure<UpdateCostCenterResponse>(updateResponse.Error!);
                            var warehouse = warehouseDatas?.Where(x => x.Id == item?.Id).FirstOrDefault();
                            var manager = managers.FirstOrDefault(x => warehouse?.ManagerId == x.Id);
                            warehouseData.Add(new CostCenterWarehouseResponse(updateResponse.Value!.Id, item!.Id, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                                warehouse?.Contact, manager?.FirstName + " " + manager?.LastName, warehouse?.Code, warehouse?.Name, item.IsDefault));
                        }
                        else if (!costCenter.CostCenterWarehouses.Any(x => x.WarehouseId == item.Id))
                        {
                            var warehouse = warehouseDatas?.Where(x => x.Id == item?.Id).FirstOrDefault();
                            var createResponse = await _mediator.Send(new CreateCostCenterWarehouseCommand(costCenter, item.Id, item.IsDefault), ct);
                            if (createResponse.IsFailure)
                                return Result.Failure<UpdateCostCenterResponse>(createResponse.Error!);
                            var manager = managers.FirstOrDefault(x => warehouse?.ManagerId == x.Id);
                            warehouseData.Add(new CostCenterWarehouseResponse(createResponse.Value!.Id, item!.Id, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                                warehouse?.Contact, manager?.FirstName + " " + manager?.LastName, warehouse?.Code, warehouse?.Name, item.IsDefault));
                        }
                        else if (costCenter.CostCenterWarehouses.Any(x => x.WarehouseId == item.Id) && (item.CostCenterWarehouseId is null || item.CostCenterWarehouseId == 0))
                        {
                            return Result.Failure<UpdateCostCenterResponse>(CostCenterWarehouseErrors.WarehouseIdIsDuplicate);
                        }
                        else if (costCenter.CostCenterWarehouses.Any(x => x.WarehouseId == item.Id))
                        {
                            var warehouse = warehouseDatas?.Where(x => x.Id == item?.Id).FirstOrDefault();
                            var manager = managers.FirstOrDefault(x => warehouse?.ManagerId == x.Id);
                            warehouseData.Add(new CostCenterWarehouseResponse((long)item.CostCenterWarehouseId!, item!.Id, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                                warehouse?.Contact, manager?.FirstName + " " + manager?.LastName, warehouse?.Code, warehouse?.Name, item.IsDefault));
                        }
                        else
                            continue;
                    }
                }
            }
            else
            {
                foreach (var item in request.CostCenterWarehouses)
                {
                    if (costCenter.CostCenterWarehouses is not null)
                        if (costCenter.CostCenterWarehouses.Any(x => x.WarehouseId == item!.Id))
                            continue;

                    var createResponse = await _mediator.Send(new CreateCostCenterWarehouseCommand(costCenter, item!.Id, item.IsDefault), ct);
                    if (createResponse.IsFailure)
                        return Result.Failure<UpdateCostCenterResponse>(createResponse.Error!);
                    var warehouse = warehouseDatas?.Where(x => x.Id == item?.Id).FirstOrDefault();
                    var manager = managers.FirstOrDefault(x => warehouse?.ManagerId == x.Id);
                    warehouseData.Add(new CostCenterWarehouseResponse(createResponse.Value!.Id, item!.Id, warehouse!.WarehouseTypeId, warehouse?.ManagerId,
                        warehouse?.Contact, manager?.FirstName + " " + manager?.LastName, warehouse?.Code, warehouse?.Name, item!.IsDefault));
                }
            }
        }

        //Validate InformedUsers
        var informedUserRequests = new List<InformedUserServiceModel?>();
        if (request.InformedUsers is not null)
            if (request.InformedUsers.Count > 0)
            {
                var informedUserIds = request.InformedUsers.Select(c => (long)c!).Where(x => x != 0)!.ToList();
                var informedUsersData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, informedUserIds.Count, informedUserIds, null, false, null), ct); // بره سراغ متا دیتا
                if (informedUsersData.IsFailure)
                    return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.UnValidInformedUsers);
                foreach (var item in request.InformedUsers)
                    informedUserRequests.Add(new InformedUserServiceModel((long)item!, informedUsersData.Value?.Data?.Where(c => c?.Id == item).FirstOrDefault()?.FullName,
                        informedUsersData.Value?.Data?.Where(c => c?.Id == item).FirstOrDefault()?.OrganizationCode));
            }

        //Validate AuthorizedRoles
        var authorizedRoleRequests = new List<AuthorizedRoleServiceModel?>();
        if (request.AuthorizedRoles is not null)
            if (request.AuthorizedRoles.Count > 0)
            {
                var ids = new List<long>();
                foreach (var item in request.AuthorizedRoles)
                    ids.Add((long)item!);
                var authorizedRolesData = await _mediator.Send(new GetsRoleByIdQuery(ids), ct); // بره سراغ متا دیتا
                if (authorizedRolesData.IsFailure)
                    return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.UnValidAuthorizedRoles);
                foreach (var item in request.AuthorizedRoles)
                    authorizedRoleRequests.Add(new AuthorizedRoleServiceModel((long)item!, authorizedRolesData.Value?.Data?.Where(c => c?.Id == item).FirstOrDefault()?.Name));
            }

        //Validate AuthorizedUsers
        var authorizedUsersRequests = new List<AuthorizedUserServiceModel?>();
        if (request.AuthorizedUsers is not null)
            if (request.AuthorizedUsers.Count > 0)
            {
                var ids = new List<long>();
                foreach (var item in request.AuthorizedUsers)
                    ids.Add((long)item!);
                var authorizedUsersData = await _mediator.Send(new GetsUserByIdQuery(ids), ct); // بره سراغ متا دیتا
                if (authorizedUsersData.IsFailure)
                    return Result.Failure<UpdateCostCenterResponse>(CostCenterErrors.UnValidAuthorizedUsers);
                foreach (var item in request.AuthorizedUsers)
                    authorizedUsersRequests.Add(new AuthorizedUserServiceModel((long)item!, authorizedUsersData.Value?.Data?.Where(c => c?.Id == item).FirstOrDefault()?.FullName,
                        authorizedUsersData.Value?.Data?.Where(c => c?.Id == item).FirstOrDefault()?.OrganizationCode));
            }

        var response = await _mediator.Send(new UpdateCostCenterCommand(request.Id,
            costCenterType.Value,
            request.CostCenterCode,
            request.CostCenterName,
            request.CostCenterEnName,
            request.InformedUsers,
            request.AuthorizedRoles,
            request.AuthorizedUsers,
            request.ProjectIds,
            warehouseData,
            request.NoOperationDays,
            request.CityId,
            request.Address,
            request.PostalCode,
            request.Latitude,
            request.Longitude,
            request.Description,
            request.DescriptionEn,
            request.WeatherState,
            request.IsActive,
            request.IsDefault,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateCostCenterResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateCostCenterResponse(response.Value!.Id, true);
    }

    public async Task<Result<InactiveCostCenterResponse?>> InactiveCostCenter(
        InactiveCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveCostCenter, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveCostCenterValidator, InactiveCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveCostCenterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveCostCenterCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveCostCenterResponse>(response.Error!);

        var costCenter = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new InactiveCostCenterResponse(costCenter.Id, costCenter.CostCenterCode, costCenter.CostCenterName, costCenter.IsActive);
    }

    public async Task<Result<DeleteCostCenterResponse?>> DeleteCostCenter(
        DeleteCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteCostCenter, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteCostCenterValidator, DeleteCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteCostCenterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteCostCenterCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteCostCenterResponse>(response.Error!);

        var costCenter = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new DeleteCostCenterResponse(costCenter.Id, costCenter.IsDeleted);
    }

    public async Task<Result<CostCenterGroupDeleteResponse?>> CostCenterGroupDelete(
        CostCenterGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for CostCenterGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<CostCenterGroupDeleteValidator, CostCenterGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CostCenterGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteCostCenterCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<CostCenterGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CostCenterGroupDeleteResponse(true);
    }

    public async Task<Result<ActiveCostCenterResponse?>> ActiveCostCenter(
        ActiveCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveCostCenter, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveCostCenterValidator, ActiveCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveCostCenterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveCostCenterCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveCostCenterResponse>(response.Error!);

        var activedCenter = response.Value!;
        await _unitOfWork.CommitAsync(ct);
        return new ActiveCostCenterResponse(activedCenter.Id, activedCenter.CostCenterCode, activedCenter.CostCenterName, activedCenter.IsActive);
    }

    public async Task<Result<StateChangerCostCentersResponse?>> StateChangerCostCenters(
        StateChangerCostCentersRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerCostCenters, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerCostCentersValidator, StateChangerCostCentersRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerCostCentersResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerCostCentersResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsCostCenterByIdsQuery(request.Ids, PreferentialReferenceCodes: null, FilterData: null, 1, request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerCostCentersResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerCostCentersCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerCostCentersResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerCostCentersResponse(true);
    }

    public async Task<Result<GetCostCenterByIdResponse?>> GetCostCenterById(
        GetCostCenterByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for etCostCenterById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetCostCenterByIdValidator, GetCostCenterByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var cityData = await _mediator.Send(new GetCityByIdQuery(value.CityId), ct); // بره سراغ متا دیتا
        var cityInfo = new CostCenterCityModel(value.CityId, cityData?.Value?.Name, cityData?.Value?.Code, cityData?.Value?.IsActive, cityData?.Value?.Province);

        var warehouseData = new List<CostCenterWarehouseResponse>();
        if (value.CostCenterWarehouses is not null && value.CostCenterWarehouses.Count > 0)
        {
            var costCenterWarehouses = value.CostCenterWarehouses.ToList();
            warehouseData = await CostCenterWarehouseDataReceiver(costCenterWarehouses, ct);
        }

        var informedUsers = new List<InformedUserDataModel>();
        if (value.InformedUsers is not null && value.InformedUsers.Count > 0)
        {
            var costCenterInformedUser = value.InformedUsers.ToList();
            informedUsers = await CostCenterInformedUserDataReceiver(costCenterInformedUser, ct);
        }

        var authorizedRoles = new List<AuthorizedRoleDataModel>();
        if (value.CostCenterAuthorizedRoles is not null && value.CostCenterAuthorizedRoles.Count > 0)
        {
            var costCenterAuthorizeRoles = value.CostCenterAuthorizedRoles.ToList();
            authorizedRoles = await CostCenterAuthorizedRoleDataReceiver(costCenterAuthorizeRoles, ct);
        }

        var costCenterProjects = new List<CostCenterProjectModel>();
        if (value.Projects is not null && value.Projects.Count > 0)
        {
            var projects = value.Projects.ToList();
            costCenterProjects = await CostCenterProjectDataReceiver(projects, ct);
        }

        var authorizedUsers = new List<AuthorizedUserDataModel>();
        if (value.CostCenterAuthorizedUsers is not null && value.CostCenterAuthorizedUsers.Count > 0)
            authorizedUsers = await CostCenterAuthorizedUserDataReceiver(value, ct);

        var costCenterType = new CostCenterTypeModel(value.CostCenterType!.Id, value.CostCenterType.CostCenterTypeTitle);

        return new GetCostCenterByIdResponse(
            value.Id, value.CostCenterType!.Id, value.CostCenterType.CostCenterTypeTitle, value.CostCenterCode,
            value.CostCenterName, value.CostCenterEnName, value.NoOperationDays, value.CityId, cityData?.Value?.Name, cityData?.Value?.Code,
            value.Address, value.PostalCode, value.Latitude, value.Longitude, value.Description, value.DescriptionEn, value.WeatherState,
            value.IsActive, value.IsDefault, warehouseData, informedUsers, authorizedRoles, authorizedUsers, costCenterProjects,
            costCenterType, cityInfo, value.CompanyId, company?.NameFa, value.PreferentialReferenceCode);
    }

    public async Task<Result<GetCostCenterByNameResponse?>> GetCostCenterByName(
        GetCostCenterByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterByName, costCenterName:{CostCenterName}", request.CostCenterName);

        var isValidRequest = await request.IsValidAsync<GetCostCenterByNameValidator, GetCostCenterByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterByNameQuery(request.CostCenterName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        return new GetCostCenterByNameResponse(value.Id, value.CostCenterType!.CostCenterTypeTitle, value.CostCenterCode, value.CostCenterName, value.IsActive, value.CompanyId, company?.NameFa);
    }

    public async Task<Result<GetCostCenterByCodeResponse?>> GetCostCenterByCode(
        GetCostCenterByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterByCode, costCenterCode:{CostCenterCode}", request.CostCenterCode);

        var isValidRequest = await request.IsValidAsync<GetCostCenterByCodeValidator, GetCostCenterByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterByCodeQuery(request.CostCenterCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        return new GetCostCenterByCodeResponse(value.Id, value.CostCenterType!.CostCenterTypeTitle, value.CostCenterCode, value.CostCenterName, value.IsActive, value.CompanyId, company?.NameFa);
    }

    public async Task<Result<GetActiveCostCentersResponse?>> GetActiveCostCenters(
        GetActiveCostCentersRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveCostCenters , pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveCostCentersValidator, GetActiveCostCentersRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveCostCentersResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetActiveCostCentersQuery(request.FilterData, request.Name, request.Code, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetActiveCostCentersResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveCostCentersModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetActiveCostCentersResponse(data ?? new List<GetsActiveCostCentersModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsActiveMainWarehouseCostCenterResponse?>> GetsActiveMainWarehouseCostCenter(
        GetsActiveMainWarehouseCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsActiveMainWarehouseCostCenter , pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveMainWarehouseCostCenterValidator, GetsActiveMainWarehouseCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveMainWarehouseCostCenterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responseIds = await _mediator.Send(new GetsMainWarehouseIdsQuery(), ct);
        if (responseIds.IsFailure)
            return Result.Failure<GetsActiveMainWarehouseCostCenterResponse>(responseIds.Error!);
        var warehouseIds = responseIds.Value!.Data!;

        var responses = await _mediator.Send(new GetsActiveMainWarehouseCostCenterQuery(request.FilterData, warehouseIds, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsActiveMainWarehouseCostCenterResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        return new GetsActiveMainWarehouseCostCenterResponse(values ?? new List<GetsActiveMainWarehouseCostCenterModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetContractorCostCentersResponse?>> GetContractorCostCenters(
        GetContractorCostCentersRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetContractorCostCenters , pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetContractorCostCentersValidator, GetContractorCostCentersRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorCostCentersResponse>(isValidRequest.Error!);

        var contractor = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ContractorId], null, true, null), ct);
        if (contractor.IsFailure)
            return Result.Failure<GetContractorCostCentersResponse>(contractor.Error!);

        var responses = await _mediator.Send(new GetContractorCostCentersQuery(request.ContractorId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetContractorCostCentersResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var data = values.Adapt<List<GetContractorCostCentersModel>>();

        return new GetContractorCostCentersResponse(data ?? new List<GetContractorCostCentersModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsActiveAuthorizedCostCenterResponse?>> GetsActiveAuthorizedCostCenter(
        GetsActiveAuthorizedCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsActiveAuthorizedCostCenter , pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveAuthorizedCostCenterValidator, GetsActiveAuthorizedCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveAuthorizedCostCenterResponse>(isValidRequest.Error!);

        var user = _userProfileService.GetProfileInfo();
        if (user is null)
            return Result.Failure<GetsActiveAuthorizedCostCenterResponse>(CostCenterErrors.UserIdIsEmpty);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsActiveAuthorizedCostCenterQuery(request.FilterData, user.UserId, request.EmployerId, request.CostCenterTypeId, request.CityId, request.Code,
            request.Name, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsActiveAuthorizedCostCenterResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var cityIds = values!.Where(x => x.CityId != 0).Select(c => c.CityId).Distinct().ToList();
        var cities = await WebServicesLogic.CityDataReceiver(cityIds, _mediator, ct); // بره سراغ متا دیتا

        var data = new List<GetsActiveAuthorizedCostCenterModel>();
        foreach (var costCenter in values!)
        {
            var company = companies?.Where(x => x.Id == costCenter.CompanyId).FirstOrDefault();

            List<long>? warehouseIds = [];
            if (costCenter.CostCenterWarehouses is not null)
                warehouseIds.AddRange(costCenter.CostCenterWarehouses.Select(x => x.WarehouseId).ToList());

            var city = cities?.Where(c => c.Id == costCenter.CityId).FirstOrDefault();
            var cityInfo = new CostCenterCityModel(costCenter.CityId, city?.Name, city?.Code, city?.IsActive, city?.Province);
            data.Add(new GetsActiveAuthorizedCostCenterModel(costCenter.Id, costCenter.CostCenterCode, costCenter.CostCenterName, warehouseIds, cityInfo, costCenter.Address, costCenter.CompanyId, company?.NameFa));
        }

        return new GetsActiveAuthorizedCostCenterResponse(data ?? new List<GetsActiveAuthorizedCostCenterModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsAuthorizedCostCenterResponse?>> GetsAuthorizedCostCenter(
        GetsAuthorizedCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsAuthorizedCostCenter , pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsAuthorizedCostCenterValidator, GetsAuthorizedCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsAuthorizedCostCenterResponse>(isValidRequest.Error!);

        var user = _userProfileService.GetProfileInfo();
        if (user is null)
            return Result.Failure<GetsAuthorizedCostCenterResponse>(CostCenterErrors.UserIdIsEmpty);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsAuthorizedCostCenterQuery(
            request.FilterData,
            user.UserId,
            request.EmployerId,
            request.CostCenterTypeId,
            request.CityId,
            request.Code,
            request.Name,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsAuthorizedCostCenterResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var cityIds = values!.Where(x => x.CityId != 0).Select(c => c.CityId).Distinct().ToList();
        var cities = await WebServicesLogic.CityDataReceiver(cityIds, _mediator, ct); // بره سراغ متا دیتا

        var data = new List<GetsAuthorizedCostCenterModel>();
        foreach (var costCenter in values!)
        {
            var company = companies?.Where(x => x.Id == costCenter.CompanyId).FirstOrDefault();

            List<long>? warehouseIds = [];
            if (costCenter.CostCenterWarehouses is not null)
                warehouseIds.AddRange(costCenter.CostCenterWarehouses.Select(x => x.WarehouseId).ToList());

            var city = cities?.Where(c => c.Id == costCenter.CityId).FirstOrDefault();
            var cityInfo = new CostCenterCityModel(costCenter.CityId, city?.Name, city?.Code, city?.IsActive, city?.Province);
            data.Add(new GetsAuthorizedCostCenterModel(costCenter.Id, costCenter.CostCenterCode, costCenter.CostCenterName, warehouseIds, cityInfo, costCenter.Address, costCenter.CompanyId, company?.NameFa));
        }

        return new GetsAuthorizedCostCenterResponse(data ?? new List<GetsAuthorizedCostCenterModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetCostCentersResponse?>> GetCostCenters(
        GetCostCentersRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenters, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetCostCentersValidator, GetCostCentersRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCentersResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetCostCentersQuery(null, request.FilterData, request.CostCenterName, request.CostCenterCode, request.CostCenterTypeId, request.InformedUserId,
            request.AuthorizedRoleId, request.AuthorizedUserId, request.WarehouseId, request.CityId, request.IsActive, request.OrderBy, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<GetCostCentersResponse>(CostCenterErrors.FilteredCostCenterNotFound);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = await FullModeling(values!, companies, ct);
        return new GetCostCentersResponse(data, responses.Value.RowCount);
    }

    public async Task<Result<GetsCostCenterByWarehouseResponse?>> GetsCostCenterByWarehouse(
        GetsCostCenterByWarehouseRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterByWarehouse, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterByWarehouseValidator, GetsCostCenterByWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterByWarehouseResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsCostCenterByWarehouseQuery(request.WarehouseId, request.FilterData, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<GetsCostCenterByWarehouseResponse>(CostCenterErrors.FilteredCostCenterNotFound);
        var values = responses.Value.Data;

        var data = values.Adapt<List<GetsCostCenterByWarehouseModel>>();
        return new GetsCostCenterByWarehouseResponse(data, responses.Value.RowCount);
    }

    public async Task<Result<GetsCostCenterByNameOrCodeResponse?>> GetsByNameOrCode(
        GetsCostCenterByNameOrCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByNameOrCode, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterByNameOrCodeValidator, GetsCostCenterByNameOrCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterByNameOrCodeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsByNameOrCodeQuery(request.FilterData, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<GetsCostCenterByNameOrCodeResponse>(CostCenterErrors.FilteredCostCenterNotFound);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = await FullModeling(values!, companies, ct);
        return new GetsCostCenterByNameOrCodeResponse(data, responses.Value.RowCount);
    }

    public async Task<Result<GetsByAuthorizedUserIdResponse?>> GetsByAuthorizedUserId(
        GetsByAuthorizedUserIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByAuthorizedUserId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByAuthorizedUserIdValidator, GetsByAuthorizedUserIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByAuthorizedUserIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsByAuthorizedUserIdQuery(request.FilterData, request.UserId, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsByAuthorizedUserIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<CostCentersByAuthorizedUserIdModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByAuthorizedUserIdResponse(data ?? new List<CostCentersByAuthorizedUserIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByAuthorizedRoleIdResponse?>> GetsByAuthorizedRoleId(
        GetsByAuthorizedRoleIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByAuthorizedRoleId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByAuthorizedRoleIdValidator, GetsByAuthorizedRoleIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByAuthorizedRoleIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsByAuthorizedRoleIdQuery(request.FilterData, request.RoleId, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsByAuthorizedRoleIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<CostCentersByAuthorizedRoleIdModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByAuthorizedRoleIdResponse(data ?? new List<CostCentersByAuthorizedRoleIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterByEmployerIdResponse?>> GetsByEmployerId(
        GetsCostCenterByEmployerIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByEmployerId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterByEmployerIdValidator, GetsCostCenterByEmployerIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterByEmployerIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsByEmployerIdQuery(request.FilterData, request.EmployerId, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsCostCenterByEmployerIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsCostCenterByEmployerIdModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsCostCenterByEmployerIdResponse(data ?? new List<GetsCostCenterByEmployerIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterByContractorIdResponse?>> GetsCostCenterByContractorId(
        GetsCostCenterByContractorIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterByContractorId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterByContractorIdValidator, GetsCostCenterByContractorIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterByContractorIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsCostCenterByContractorIdQuery(request.FilterData, request.ContractorId, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsCostCenterByContractorIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var data = values.Adapt<List<GetsCostCenterByContractorIdModel>>();
        return new GetsCostCenterByContractorIdResponse(data ?? new List<GetsCostCenterByContractorIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterByProjectManagerIdResponse?>> GetsCostCenterByProjectManagerId(
        GetsCostCenterByProjectManagerIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterByProjectManagerId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterByProjectManagerIdValidator, GetsCostCenterByProjectManagerIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterByProjectManagerIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsCostCenterByProjectManagerIdQuery(request.FilterData, request.ProjectManagerId, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsCostCenterByProjectManagerIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var data = values.Adapt<List<GetsCostCenterByProjectManagerIdModel>>();
        return new GetsCostCenterByProjectManagerIdResponse(data ?? new List<GetsCostCenterByProjectManagerIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByCityIdResponse?>> GetsByCityId(
        GetsByCityIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByCityId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByCityIdValidator, GetsByCityIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByCityIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsByCityIdQuery(request.CityId, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsByCityIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsByCityIdModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByCityIdResponse(data ?? new List<GetsByCityIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByTypeIdResponse?>> GetsByTypeId(
        GetsByTypeIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByTypeId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByTypeIdValidator, GetsByTypeIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByTypeIdResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsByTypeIdQuery(request.CostCenterTypeId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsByTypeIdResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsByTypeIdModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByTypeIdResponse(data ?? new List<GetsByTypeIdModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterByIdsResponse?>> GetsCostCenterByIds(
        GetsCostCenterByIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterByIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterByIdsValidator, GetsCostCenterByIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterByIdsResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsCostCenterByIdsQuery(request.Ids, request.PreferentialReferenceCodes, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsCostCenterByIdsResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsCostCenterByIdsResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsCostCenterByIdsResponse(data ?? new List<GetsCostCenterByIdsResponseModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetCostCenterHistoriesResponse?>> GetCostCenterHistories(
        GetCostCenterHistoriesRequest request, CT ct)
    {
        _logger.LogInformation("GetsCostCenterHistories");

        var responses = await _mediator.Send(new GetCostCenterHistoriesQuery(request.Id, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses is null)
            return Result.Failure<GetCostCenterHistoriesResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        var values = responses.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetCostCenterHistoriesModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetCostCenterHistoriesResponse(data, data.Count);
    }

    public async Task<Result<GetsCostCenterExcelExporterResponse?>> GetsCostCenterExcelExporter(
        GetsCostCenterExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterExcelExporterValidator, GetsCostCenterExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetCostCentersQuery(request.Ids, request.FilterData, request.CostCenterName, request.CostCenterCode, request.CostCenterTypeId,
            request.InformedUserId, request.AuthorizedRoleId, request.AuthorizedUserId, request.WarehouseId, request.CityId, request.IsActive, request.OrderBy, companyId, request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsCostCenterExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var warehouseIds = values!.Where(x => x.WarehouseId != null && x.WarehouseId > 0).Select(x => (long?)x.WarehouseId!).Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);
        var cityIds = values!.Select(c => c.CityId).Where(x => x != 0).Distinct().ToList();
        var cities = await WebServicesLogic.CityDataReceiver(cityIds, _mediator, ct);

        var data = values.Adapt<List<GetsCostCenterExcelExporterModel>>();
        foreach (var item in data)
        {
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            item.CityName = cities?.FirstOrDefault(x => x?.Id == item.CityId)?.Name;
            if (item.WarehouseId is not null)
            {
                var warehouse = warehouses?.FirstOrDefault(x => x?.Id == item.WarehouseId);
                item.WarehouseName = warehouse?.Name;
                item.WarehouseManagement = warehouse?.ManagerFullName;
            }
        }

        var file = new FileContentResult(CostCenterExcels.CostCenterToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CostCenters-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsCostCenterExcelExporterResponse(file);
    }

    public async Task<Result<GetsCostCenterExcelEnumResponse?>> GetsCostCenterExcelEnum(
        GetsCostCenterExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<CostCenterExcelEnum>());
        return new GetsCostCenterExcelEnumResponse(response);
    }

    //PrivateMethod
    private async Task<List<CostCenterWarehouseResponse>?> CostCenterWarehouseDataReceiver(
        List<CostCenterWarehouse> costCenterWarehouses, CT ct)
    {
        List<WarehouseEntity>? warehouses = null;
        var warehouseIds = costCenterWarehouses.Select(c => (long?)c.WarehouseId).Where(x => x != 0).ToList();
        if (warehouseIds.Count > 0 && warehouseIds != null)
            warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);

        var warehouseData = new List<CostCenterWarehouseResponse>();
        foreach (var item in costCenterWarehouses)
        {
            var warehouse = warehouses?.Where(x => x.Id == item.WarehouseId).FirstOrDefault();
            warehouseData.Add(new CostCenterWarehouseResponse(item.Id, item.WarehouseId, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                warehouse?.Contact, warehouse?.ManagerFullName, warehouse?.Code, warehouse?.Name, item.IsDefault));
        }
        return warehouseData;
    }

    private async Task<List<InformedUserDataModel>?> CostCenterInformedUserDataReceiver(
        List<CostCenterInformedUser> costCenterInformedUsers, CT ct)
    {
        var informedUserRequests = new List<InformedUserDataModel>();

        List<UserModel?>? informedUsersData = null;
        var ids = costCenterInformedUsers.Select(c => c.EmployeeId).Where(x => x != 0).Distinct().ToList();
        if (ids.Count > 0 && ids is not null)
            informedUsersData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids, null, null, _mediator, ct);

        foreach (var item in costCenterInformedUsers)
            informedUserRequests.Add(new InformedUserDataModel(item.EmployeeId, informedUsersData?.Where(c => c!.Id == item.EmployeeId).FirstOrDefault()?.FullName,
                informedUsersData?.Where(c => c!.Id == item.EmployeeId).FirstOrDefault()?.OrganizationCode));

        return informedUserRequests;
    }

    private async Task<List<AuthorizedRoleDataModel>?> CostCenterAuthorizedRoleDataReceiver(
        List<CostCenterAuthorizedRole> costCenterAuthorizedRoles, CT ct)
    {
        List<AuthorizedRoleDataModel> authorizedRoles = [];

        List<Role>? authorizedRolesData = null;
        var ids = costCenterAuthorizedRoles.Select(x => x.AuthorizedRoleId).Where(c => c != 0).ToList();
        if (ids.Count > 0 && ids != null)
            authorizedRolesData = await WebServicesLogic.RoleDataReceiver(ids, _mediator, ct);

        foreach (var item in costCenterAuthorizedRoles)
            authorizedRoles.Add(new AuthorizedRoleDataModel(item.AuthorizedRoleId, authorizedRolesData?.Where(c => c!.Id == item.AuthorizedRoleId).FirstOrDefault()?.Name));

        return authorizedRoles;
    }

    private async Task<List<CostCenterProjectModel>?> CostCenterProjectDataReceiver(
        List<Project> costCenterAuthorizedRoles, CT ct)
    {
        List<CostCenterProjectModel> projects = [];
        foreach (var item in costCenterAuthorizedRoles)
            projects.Add(new CostCenterProjectModel(item.Id, item.ProjectName, item.ProjectCode));
        return projects;
    }

    private async Task<List<AuthorizedUserDataModel>?> CostCenterAuthorizedUserDataReceiver(
        CostCenter costCenter, CT ct)
    {
        List<AuthorizedUserDataModel> authorizedUsers = [];

        var userIds = costCenter.CostCenterAuthorizedUsers!.Select(c => c.AuthorizedUserId).Where(x => x != 0).ToList();
        var userData = new List<User>();

        if (costCenter.CostCenterAuthorizedRoles.Count > 0)
        {
            var roleIds = costCenter.CostCenterAuthorizedRoles!.Select(c => c.AuthorizedRoleId).Where(x => x != 0).ToList();
            var usersData = await WebServicesLogic.GetUsersByRoleIdsDataReceiver(roleIds, userIds, null, _mediator, ct);
            userData = usersData;
        }
        else
        {
            var usersData = await WebServicesLogic.GetUsersDataReceiver(userIds!, _mediator, ct); // بره سراغ متا دیتا
            userData = usersData;
        }
        if (userData is not null && userData.Count > 0)
            foreach (var item in userData)
                authorizedUsers.Add(new(item.Id, item?.FullName, item?.OrganizationCode, item?.RoleIds));

        return authorizedUsers;
    }

    private async Task<List<GetCostCentersModel>> FullModeling(
        List<CostCenter> values, List<Company>? companies, CT ct)
    {
        var data = values.Adapt<List<GetCostCentersModel>>();

        foreach (var item in data)
        {
            var costcenter = values.Where(x => x.Id == item.Id).FirstOrDefault();
            if (costcenter is not null && costcenter.CostCenterWarehouses.Any())
            {
                if (costcenter.CostCenterWarehouses.Any(x => x.IsDefault))
                {
                    var warehouse = costcenter.CostCenterWarehouses.FirstOrDefault(x => x.IsDefault);
                    item.WarehouseId = warehouse!.WarehouseId;
                    item.CostCenterWarehouseId = warehouse!.Id;
                }
                else
                {
                    var warehouse = costcenter.CostCenterWarehouses.FirstOrDefault();
                    item.WarehouseId = warehouse!.WarehouseId;
                    item.CostCenterWarehouseId = warehouse!.Id;
                }
            }
        }

        var warehouseIds = data.Where(x => x.WarehouseId is not null).Select(x => x.WarehouseId!).Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);
        var cityIds = values.Select(c => c.CityId).Where(x => x != 0).Distinct().ToList();
        var cities = await WebServicesLogic.CityDataReceiver(cityIds, _mediator, ct);

        foreach (var item in data)
        {
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            item.CityName = cities?.Where(x => x?.Id == item.CityId).FirstOrDefault()?.Name;
            item.CityCode = cities?.Where(x => x?.Id == item.CityId).FirstOrDefault()?.Code;
            if (item.WarehouseId is not null)
            {
                item.WarehouseName = warehouses?.Where(x => x?.Id == item.WarehouseId).FirstOrDefault()?.Name;
                item.WarehouseCode = warehouses?.Where(x => x?.Id == item.WarehouseId).FirstOrDefault()?.Code;
                item.WarehouseManagement = warehouses?.Where(x => x?.Id == item.WarehouseId).FirstOrDefault()?.ManagerFullName;
            }
        }

        return data;
    }

    public async Task<Result<GetFilteredCostCenterCitiesResponse?>> GetFilteredCostCenterCities(
        GetFilteredCostCenterCitiesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFilteredCostCenterCities");

        var isValidRequest = await request.IsValidAsync<GetFilteredCostCenterCitiesValidator, GetFilteredCostCenterCitiesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredCostCenterCitiesResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFilteredCostCenterCitiesQuery(request.CostCenterIds), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetFilteredCostCenterCitiesResponse>(response.Error!);
        var ids = response.Value!;

        List<City>? cities = [];
        var data = new List<GetFilteredCostCenterCitiesModel>();
        if (ids?.Count > 0)
        {
            var responseValue = await WebServicesLogic.CityDataReceiver(ids!, request.FilterData, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                    if (item != null)
                        cities.Add(item);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in ids)
                {
                    if (!cities.Any(x => x?.Id == id))
                        continue;

                    var city = cities.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetFilteredCostCenterCitiesModel()
                    {
                        CityId = city?.Id,
                        CityName = city?.Name,
                        CityCode = city?.Code,
                    });
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    if (!cities.Any(x => x?.Id == id))
                        continue;

                    var city = cities.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetFilteredCostCenterCitiesModel()
                    {
                        CityId = city?.Id,
                        CityName = city?.Name,
                        CityCode = city?.Code,
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetFilteredCostCenterCitiesResponse(responseData ?? new List<GetFilteredCostCenterCitiesModel>(0), cities?.Count ?? 0);

    }

    public async Task<Result<GetCompaniesWorkResponse?>> GetCompaniesWork(
        GetCompaniesWorkRequest request, CT ct)
    {
        var result = await _mediator.Send(new GetCompaniesWorkQuery(), ct);
        if (result.IsBad()) return result.Failure<GetCompaniesWorkResponse>()!;

        return result;
    }

    private async Task<List<GetCostCentersModel>> FullModeling(
        List<GetCostCentersModel> values, List<Company>? companies, CT ct)
    {
        var data = values.Adapt<List<GetCostCentersModel>>();

        var warehouseIds = data.Where(x => x.WarehouseId is not null && x.WarehouseId > 0).Select(x => x.WarehouseId!).Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);

        var cityIds = values.Select(c => c.CityId).Where(x => x != 0).Distinct().ToList();
        var cities = await WebServicesLogic.CityDataReceiver(cityIds, _mediator, ct);

        foreach (var item in data)
        {
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            item.CityName = cities?.Where(x => x?.Id == item.CityId).FirstOrDefault()?.Name;
            item.CityCode = cities?.Where(x => x?.Id == item.CityId).FirstOrDefault()?.Code;
            if (item.WarehouseId is not null && item.WarehouseId > 0)
            {
                item.WarehouseName = warehouses?.Where(x => x?.Id == item.WarehouseId).FirstOrDefault()?.Name;
                item.WarehouseCode = warehouses?.Where(x => x?.Id == item.WarehouseId).FirstOrDefault()?.Code;
                item.WarehouseManagement = warehouses?.Where(x => x?.Id == item.WarehouseId).FirstOrDefault()?.ManagerFullName;
            }
        }

        return data;
    }
}