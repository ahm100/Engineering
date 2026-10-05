using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.OperationLocations.Commands.ActiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Commands.CreateOperationLocation;
using Engineering.Application.Services.OperationLocations.Commands.DisableOperationLocation;
using Engineering.Application.Services.OperationLocations.Commands.InactiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Commands.OperationLocationCodeCreator;
using Engineering.Application.Services.OperationLocations.Commands.SetPriority;
using Engineering.Application.Services.OperationLocations.Commands.StateChangerOperationLocations;
using Engineering.Application.Services.OperationLocations.Commands.UpdateOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.ActiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithCostCenter;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithParent;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithCostCenter;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithParent;
using Engineering.Application.Services.OperationLocations.Models.DisableOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.GetActiveOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationById;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationCodeByCostCenter;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationNameByCostCenter;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.GetsByCostCenterId;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationChild;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelEnum;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;
using Engineering.Application.Services.OperationLocations.Models.GetsWithoutParentOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.InactiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.OperationLocationGroupDelete;
using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;
using Engineering.Application.Services.OperationLocations.Models.SetPriority;
using Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.UpdateOperationLocation;
using Engineering.Application.Services.OperationLocations.Queries.GetActiveOperationLocations;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByCode;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationById;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByName;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByCostCenter;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByParent;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByCostCenter;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByParent;
using Engineering.Application.Services.OperationLocations.Queries.GetOperationLocations;
using Engineering.Application.Services.OperationLocations.Queries.GetsByCostCenterId;
using Engineering.Application.Services.OperationLocations.Queries.GetsByIds;
using Engineering.Application.Services.OperationLocations.Queries.GetsOperationLocation;
using Engineering.Application.Services.OperationLocations.Queries.GetsOperationLocationChild;
using Engineering.Application.Services.OperationLocations.Queries.GetsWithoutParentOperationLocation;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.Projects;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.OperationLocations;

public class OperationLocationLogic : IOperationLocationLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationLocationLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public OperationLocationLogic(
        IMediator mediator,
        ILogger<OperationLocationLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateOperationLocationWithCostCenterResponse?>> CreateOperationLocationWithCostCenter(
        CreateOperationLocationWithCostCenterRequest request, CT ct)
    {
        var transactionOptions = new System.Transactions.TransactionOptions();
        transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            _logger.LogInformation("Request for CreateOperationLocationWithCostCenter, PrivateName:{PrivateName}, PrivateCode:{PrivateCode},",
            request.PrivateName, request.PrivateCode);

            var isValidRequest = await request.IsValidAsync<CreateOperationLocationWithCostCenterValidator, CreateOperationLocationWithCostCenterRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreateOperationLocationWithCostCenterResponse>(isValidRequest.Error!);

            CostCenter? costCenter = null;
            if (request.CostCenterId is not null)
            {
                var costCenterValue = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId!.Value), ct);
                if (costCenterValue.IsFailure)
                    return Result.Failure<CreateOperationLocationWithCostCenterResponse>(OperationLocationErrors.OperationLocationCostCenterNotFound);
                costCenter = costCenterValue.Value!;
            }

            Project? project = null;
            if (request.ProjectId is not null)
            {
                var projectValue = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId!.Value), ct);
                if (projectValue.IsFailure)
                    return Result.Failure<CreateOperationLocationWithCostCenterResponse>(ProjectErrors.ProjectWithIdNotFound);
                project = projectValue.Value!;
                costCenter = projectValue.Value!.ProjectCostCenters.FirstOrDefault()?.CostCenter;
            }

            if (request.CostCenterId is null && request.ProjectId is null)
                return Result.Failure<CreateOperationLocationWithCostCenterResponse>(ProjectErrors.ProjectNotInCostCenter);

            long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
            if (companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<CreateOperationLocationWithCostCenterResponse>(companyResponse.Error!);
            }

            var nameIsDuplicate = await _mediator.Send(new GetOperationLocationNameByCostCenterQuery(request.PrivateName, request.CostCenterId, request.ProjectId), ct);
            if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
                return Result.Failure<CreateOperationLocationWithCostCenterResponse>(OperationLocationErrors.NameIsDuplicate);

            var codeIsDuplicate = await _mediator.Send(new GetOperationLocationCodeByCostCenterQuery(request.PrivateCode, request.CostCenterId, request.ProjectId), ct);
            if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
                return Result.Failure<CreateOperationLocationWithCostCenterResponse>(OperationLocationErrors.CodeIsDuplicate);

            var publicName = $"{request.PrivateName}";
            var publicCode = $"{request.PrivateCode}";

            var response = await _mediator.Send(new CreateOperationLocationCommand(costCenter, project, null, request.PrivateName, request.PrivateCode, publicName, publicCode, 1, request.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<CreateOperationLocationWithCostCenterResponse>(response.Error!);
            await _unitOfWork.CommitAsync(ct);

            var entity = response.Value!;
            if (entity.Parent is null)
                entity.SetPath($"{costCenter.Id},{entity.Id}");
            if (entity.Parent is not null)
                entity.SetPath($"{entity.Parent!.Path},{entity.Id}");

            await _unitOfWork.CommitAsync(ct);
            scope.Complete();
            return new CreateOperationLocationWithCostCenterResponse(response.Value!.Id);
        }
    }

    public async Task<Result<CreateOperationLocationWithParentResponse?>> CreateOperationLocationWithParent(
        CreateOperationLocationWithParentRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationLocationWithParent, PrivateName:{PrivateName}, PrivateCode:{PrivateCode},",
            request.PrivateName, request.PrivateCode);

        var isValidRequest = await request.IsValidAsync<CreateOperationLocationWithParentValidator, CreateOperationLocationWithParentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateOperationLocationWithParentResponse>(isValidRequest.Error!);

        var parentValue = await _mediator.Send(new GetOperationLocationByIdQuery(request.ParentId), ct);
        if (parentValue.IsFailure)
            return Result.Failure<CreateOperationLocationWithParentResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);
        var parent = parentValue.Value!;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateOperationLocationWithParentResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetOperationLocationNameByParentQuery(request.PrivateName, request.ParentId, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateOperationLocationWithParentResponse>(OperationLocationErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetOperationLocationCodeByParentQuery(request.PrivateCode, request.ParentId, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateOperationLocationWithParentResponse>(OperationLocationErrors.CodeIsDuplicate);

        var publicName = $"{parent.PublicName}-{request.PrivateName}";
        var publicCode = $"{parent.PublicCode}-{request.PrivateCode}";

        var response = await _mediator.Send(new CreateOperationLocationCommand(parent.CostCenter, parent.Project, parent, request.PrivateName, request.PrivateCode, publicName,
            publicCode, request.Priority, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateOperationLocationWithParentResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateOperationLocationWithParentResponse(response.Value!.Id);
    }

    public async Task<Result<CreateOperationLocationCodeWithCostCenterResponse?>> CreateOperationLocationCodeWithCostCenter(
        CreateOperationLocationCodeWithCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationLocationCodeWithCostCenter, CostCenterId:{CostCenterId},", request.CostCenterId);

        var isValidRequest = await request.IsValidAsync<CreateOperationLocationCodeWithCostCenterValidator, CreateOperationLocationCodeWithCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateOperationLocationCodeWithCostCenterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateOperationLocationCodeWithCostCenterResponse>(companyResponse.Error!);
        }

        if (request.CostCenterId is not null)
        {
            var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId!.Value), ct);
            if (costCenter.IsFailure)
                return Result.Failure<CreateOperationLocationCodeWithCostCenterResponse>(OperationLocationErrors.OperationLocationCostCenterNotFound);
        }

        var newCode = await _mediator.Send(new OperationLocationCodeCreatorCommand(request.CostCenterId, request.ProjectId, null, companyId), ct);

        return new CreateOperationLocationCodeWithCostCenterResponse(newCode.Value!);
    }

    public async Task<Result<StateChangerOperationLocationsResponse?>> StateChangerOperationLocations(
        StateChangerOperationLocationsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerOperationLocations, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerOperationLocationsValidator, StateChangerOperationLocationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerOperationLocationsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerOperationLocationsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsOperationLocationByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerOperationLocationsResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerOperationLocationsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerOperationLocationsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerOperationLocationsResponse(true);
    }

    public async Task<Result<CreateOperationLocationCodeWithParentResponse?>> CreateOperationLocationCodeWithParent(
        CreateOperationLocationCodeWithParentRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationLocationCodeWithParent, ParentId:{ParentId},", request.ParentId);

        var isValidRequest = await request.IsValidAsync<CreateOperationLocationCodeWithParentValidator, CreateOperationLocationCodeWithParentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateOperationLocationCodeWithParentResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateOperationLocationCodeWithParentResponse>(companyResponse.Error!);
        }

        var getParent = await _mediator.Send(new GetOperationLocationByIdQuery(request.ParentId), ct);
        if (getParent.IsFailure)
            return Result.Failure<CreateOperationLocationCodeWithParentResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);

        var newCode = await _mediator.Send(new OperationLocationCodeCreatorCommand(getParent.Value!.CostCenter.Id, getParent.Value!.Project?.Id, request.ParentId, companyId), ct);

        return new CreateOperationLocationCodeWithParentResponse(newCode.Value!);
    }

    public async Task<Result<DisableOperationLocationResponse?>> DisableOperationLocation(
        DisableOperationLocationRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableOperationLocation, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableOperationLocationValidator, DisableOperationLocationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableOperationLocationResponse>(isValidRequest.Error!);

        var haveChidl = await _mediator.Send(new GetsOperationLocationChildQuery(request.Id, 1, 10), ct);
        if (haveChidl.Value?.Data?.Count > 0)
            return Result.Failure<DisableOperationLocationResponse>(OperationLocationErrors.HaveChild);

        var response = await _mediator.Send(new DisableOperationLocationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableOperationLocationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableOperationLocationResponse(response.Value!.Id, true);
    }

    public async Task<Result<OperationLocationGroupDeleteResponse?>> OperationLocationGroupDelete(
        OperationLocationGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationLocationGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<OperationLocationGroupDeleteValidator, OperationLocationGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<OperationLocationGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableOperationLocationCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<OperationLocationGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new OperationLocationGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateOperationLocationResponse?>> UpdateOperationLocation(
        UpdateOperationLocationRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateOperationLocation, id:{Id}, description:{PrivateCode},", request.Id, request.IsActive);

        var isValidRequest = await request.IsValidAsync<UpdateOperationLocationValidator, UpdateOperationLocationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateOperationLocationResponse>(isValidRequest.Error!);

        var haveChidl = await _mediator.Send(new GetsOperationLocationChildQuery(request.Id, 1, 10), ct);

        var getLocationValue = await _mediator.Send(new GetOperationLocationByIdQuery(request.Id), ct);
        if (getLocationValue.IsFailure)
            return Result.Failure<UpdateOperationLocationResponse>(getLocationValue.Error!);
        var getLocation = getLocationValue.Value!;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateOperationLocationResponse>(companyResponse.Error!);
        }

        Project? project = null;
        if (request.ProjectId is not null)
        {
            if (getLocation.Project is null)
            {
                var projectValue = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId!.Value), ct);
                if (projectValue.IsFailure)
                    return Result.Failure<UpdateOperationLocationResponse>(ProjectErrors.ProjectWithIdNotFound);
                project = projectValue.Value!;
                if (project.ProjectCostCenters.FirstOrDefault()?.CostCenterId != getLocation.CostCenter?.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(ProjectErrors.ProjectNotInCostCenter);
            }
            else if (getLocation.Project?.Id != request.ProjectId)
            {
                if (getLocation.ProjectOperationDetails.Any() || getLocation.Children.Any(x => x.ProjectOperationDetails.Any()))
                    return Result.Failure<UpdateOperationLocationResponse>(ProjectErrors.YouHaveProjectOperationDetails);
                else
                {
                    var projectValue = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId!.Value), ct);
                    if (projectValue.IsFailure)
                        return Result.Failure<UpdateOperationLocationResponse>(ProjectErrors.ProjectWithIdNotFound);
                    project = projectValue.Value!;
                    if (project.ProjectCostCenters.FirstOrDefault()?.CostCenterId != getLocation.CostCenterId)
                        return Result.Failure<UpdateOperationLocationResponse>(ProjectErrors.ProjectNotInCostCenter);
                }
            }
            else
                project = getLocation.Project!;

        }

        if (haveChidl.Value?.Data?.Count > 0)
        {
            var response = await _mediator.Send(new UpdateOperationLocationCommand(request.Id, getLocation.Parent, project, getLocation.PrivateName, getLocation.PrivateCode, getLocation.PublicName, getLocation.PublicCode, request.Priority, request.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<UpdateOperationLocationResponse>(response.Error!);
        }
        else
        {
            if (request.ParentId is not null)
            {
                if (request.ParentId == request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.CanNotUseParent);
                var parentValue = await _mediator.Send(new GetOperationLocationByIdQuery((long)request.ParentId!), ct);
                if (parentValue.IsFailure)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);
                var parent = parentValue.Value!;

                var nameIsDuplicate = await _mediator.Send(new GetOperationLocationNameByParentQuery(request.PrivateName, parent.Id, companyId), ct);
                if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.NameIsDuplicate);

                var codeIsDuplicate = await _mediator.Send(new GetOperationLocationCodeByParentQuery(request.PrivateCode, parent.Id, companyId), ct);
                if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.CodeIsDuplicate);

                var publicName = $"{parent.PublicName}-{request.PrivateName}";
                var publicCode = $"{parent.PublicCode}-{request.PrivateCode}";

                var response = await _mediator.Send(new UpdateOperationLocationCommand(request.Id, parent, project, request.PrivateName, request.PrivateCode, publicName, publicCode, request.Priority, request.IsActive, companyId), ct);
                if (response.IsFailure)
                    return Result.Failure<UpdateOperationLocationResponse>(response.Error!);
            }
            else if (request.ParentId is null && getLocation.ParentId is not null)
            {
                var parentValue = await _mediator.Send(new GetOperationLocationByIdQuery((long)getLocation.ParentId!), ct);
                if (parentValue.IsFailure)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.OperationLocationWithIdNotFound);
                var parent = parentValue.Value!;

                var nameIsDuplicate = await _mediator.Send(new GetOperationLocationNameByParentQuery(request.PrivateName, parent.Id, companyId), ct);
                if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.NameIsDuplicate);

                var codeIsDuplicate = await _mediator.Send(new GetOperationLocationCodeByParentQuery(request.PrivateCode, parent.Id, companyId), ct);
                if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.CodeIsDuplicate);

                var publicName = $"{parent.PublicName}-{request.PrivateName}";
                var publicCode = $"{parent.PublicCode}-{request.PrivateCode}";

                var response = await _mediator.Send(new UpdateOperationLocationCommand(request.Id, null, project, request.PrivateName, request.PrivateCode, publicName, publicCode, request.Priority, request.IsActive, companyId), ct);
                if (response.IsFailure)
                    return Result.Failure<UpdateOperationLocationResponse>(response.Error!);
            }
            else
            {
                var publicName = $"{request.PrivateName}";
                var publicCode = $"{request.PrivateCode}";

                var nameIsDuplicate = await _mediator.Send(new GetOperationLocationNameByCostCenterQuery(request.PrivateName, getLocation.CostCenter.Id, project?.Id), ct);
                if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.NameIsDuplicate);

                var codeIsDuplicate = await _mediator.Send(new GetOperationLocationCodeByCostCenterQuery(request.PrivateCode, getLocation.CostCenter.Id, project?.Id), ct);
                if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
                    return Result.Failure<UpdateOperationLocationResponse>(OperationLocationErrors.CodeIsDuplicate);

                var response = await _mediator.Send(new UpdateOperationLocationCommand(request.Id, null, project, request.PrivateName, request.PrivateCode, publicName, publicCode, request.Priority, request.IsActive, companyId), ct);
                if (response.IsFailure)
                    return Result.Failure<UpdateOperationLocationResponse>(response.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateOperationLocationResponse(request.Id);
    }

    public async Task<Result<SetOperationLocationPriorityResponse?>> SetOperationLocationPriority(
        SetOperationLocationPriorityRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetOperationLocationPriority, id:{Id} , Priority:{Priority},", request.Id, request.Priority);

        var isValidRequest = await request.IsValidAsync<SetOperationLocationPriorityValidator, SetOperationLocationPriorityRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetOperationLocationPriorityResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new SetOperationLocationPriorityCommand(request.Id, request.Priority), ct);
        if (response.IsFailure)
            return Result.Failure<SetOperationLocationPriorityResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetOperationLocationPriorityResponse(response.Value!.Id, response.Value.Priority);
    }

    public async Task<Result<InactiveOperationLocationResponse?>> InactiveOperationLocation(
        InactiveOperationLocationRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveOperationLocation, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveOperationLocationValidator, InactiveOperationLocationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveOperationLocationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveOperationLocationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveOperationLocationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveOperationLocationResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveOperationLocationResponse?>> ActiveOperationLocation(
        ActiveOperationLocationRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveOperationLocation, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveOperationLocationValidator, ActiveOperationLocationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveOperationLocationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveOperationLocationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveOperationLocationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveOperationLocationResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<GetOperationLocationByIdResponse?>> GetOperationLocationById(
        GetOperationLocationByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationLocationById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetOperationLocationByIdValidator, GetOperationLocationByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationLocationByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationLocationByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetOperationLocationByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetOperationLocationByNameResponse?>> GetOperationLocationByName(
        GetOperationLocationByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationLocationByName OperationLocationName:{OperationLocationName}", request.PrivateName);

        var isValidRequest = await request.IsValidAsync<GetOperationLocationByNameValidator, GetOperationLocationByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationLocationByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationByNameQuery(request.PrivateName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationLocationByNameResponse>(response.Error!);

        return response.Value!.Adapt<GetOperationLocationByNameResponse>();
    }

    public async Task<Result<GetOperationLocationNameByCostCenterResponse?>> GetOperationLocationNameByCostCenter(
        GetOperationLocationNameByCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationLocationNameByCostCenter OperationLocationName:{OperationLocationName}", request.PrivateName);

        var isValidRequest = await request.IsValidAsync<GetOperationLocationNameByCostCenterValidator, GetOperationLocationNameByCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationLocationNameByCostCenterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationNameByCostCenterQuery(request.PrivateName, request.CostCenterId, request.ProjectId), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationLocationNameByCostCenterResponse>(response.Error!);

        return response.Value!.Adapt<GetOperationLocationNameByCostCenterResponse>();
    }

    public async Task<Result<GetOperationLocationCodeByCostCenterResponse?>> GetOperationLocationCodeByCostCenter(
        GetOperationLocationCodeByCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationLocationCodeByCostCenter OperationLocationName:{OperationLocationName}", request.PrivateCode);

        var isValidRequest = await request.IsValidAsync<GetOperationLocationCodeByCostCenterValidator, GetOperationLocationCodeByCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationLocationCodeByCostCenterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationCodeByCostCenterQuery(request.PrivateCode, request.CostCenterId, request.ProjectId), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationLocationCodeByCostCenterResponse>(response.Error!);

        return response.Value!.Adapt<GetOperationLocationCodeByCostCenterResponse>();
    }

    public async Task<Result<GetOperationLocationByCodeResponse?>> GetOperationLocationByCode(
        GetOperationLocationByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationLocationByCode, PrivateCode:{PrivateCode}", request.PrivateCode);

        var isValidRequest = await request.IsValidAsync<GetOperationLocationByCodeValidator, GetOperationLocationByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationLocationByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationByCodeQuery(request.PrivateCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationLocationByCodeResponse>(response.Error!);

        return response.Value!.Adapt<GetOperationLocationByCodeResponse>();
    }

    public async Task<Result<GetActiveOperationLocationsResponse?>> GetsActiveOperationLocation(
        GetActiveOperationLocationsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveOperationLocation, PrivateCode:{PrivateCode} , PrivateName:{PrivateName}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PrivateCode, request.PrivateName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveOperationLocationsValidator, GetActiveOperationLocationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveOperationLocationsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveOperationLocationsQuery(request.FilterData, request.CostCenterId, request.ProjectId, request.PrivateName,
            request.PrivateCode, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveOperationLocationsResponse>(response.Error!);

        return new GetActiveOperationLocationsResponse(response.Value?.Data?.Adapt<List<GetsActiveOperationLocationModel>>() ?? new List<GetsActiveOperationLocationModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetOperationLocationsResponse?>> GetOperationLocations(
        GetOperationLocationsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationLocation,CostCenterId:{CostCenterId} , ParentId:{ParentId}, IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.CostCenterId, request.ParentId, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetOperationLocationsValidator, GetOperationLocationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationLocationsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationsQuery(null, request.CostCenterId, request.ProjectId, request.ParentId, request.FilterData, request.IsActive, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationLocationsResponse>(response.Error!);
        var values = response.Value?.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = new List<GetOperationLocationsWithChildModel>();
        if (values is not null)
            foreach (var dataItem in values)
                data.Add(dataItem.Adapt<GetOperationLocationsWithChildModel>());

        var dataResult = data?.Adapt<List<GetOperationLocationsResponseModel>>();
        foreach (var item in dataResult!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetOperationLocationsResponse(dataResult ?? new List<GetOperationLocationsResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationLocationResponse?>> GetsOperationLocation(
        GetsOperationLocationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationLocation,PrivateName:{PrivateName} , PrivateCode:{PrivateCode}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PrivateName, request.PrivateCode, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationLocationValidator, GetsOperationLocationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationLocationResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsOperationLocationQuery(request.FilterData, request.PrivateName, request.PrivateCode, request.Ids, companyId,
            request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationLocationResponse>(response.Error!);

        var data = new List<GetOperationLocationsWithChildModel>();
        if (response.Value!.Data is not null)
            foreach (var dataItem in response.Value?.Data!)
                data.Add(dataItem.Adapt<GetOperationLocationsWithChildModel>());

        return new GetsOperationLocationResponse(data?.Adapt<List<GetOperationLocationsWithChildModel>>() ?? new List<GetOperationLocationsWithChildModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationLocationChildResponse?>> GetsOperationLocationChild(
        GetsOperationLocationChildRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationLocation,pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationLocationChildValidator, GetsOperationLocationChildRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationLocationChildResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsOperationLocationChildQuery(request.Id, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationLocationChildResponse>(response.Error!);

        var data = new List<GetOperationLocationsWithChildModel>();
        if (response.Value!.Data is not null)
            foreach (var dataItem in response.Value?.Data!)
                data.Add(dataItem.Adapt<GetOperationLocationsWithChildModel>());

        return new GetsOperationLocationChildResponse(data?.Adapt<List<GetOperationLocationsWithChildModel>>() ?? new List<GetOperationLocationsWithChildModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationLocationByCostCenterIdResponse?>> GetsByCostCenterId(
        GetsOperationLocationByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByCostCenterId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);
        var isValidRequest = await request.IsValidAsync<GetsOperationLocationByCostCenterIdValidator, GetsOperationLocationByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationLocationByCostCenterIdResponse>(isValidRequest.Error!);

        if (request.CostCenterId is not null)
        {
            var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId!.Value), ct);
            if (costCenter.IsFailure)
                return Result.Failure<GetsOperationLocationByCostCenterIdResponse>(costCenter.Error!);
        }

        var response = await _mediator.Send(new GetsByCostCenterIdQuery(request.CostCenterId, request.ProjectId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationLocationByCostCenterIdResponse>(response.Error!);

        return new GetsOperationLocationByCostCenterIdResponse(response.Value?.Data?.Adapt<List<GetsOperationLocationByCostCenterIdModel>>() ??
            new List<GetsOperationLocationByCostCenterIdModel>(0), response.Value?.RowCount ?? 0);

    }

    public async Task<Result<GetsWithoutParentOperationLocationResponse?>> GetsWithoutParentOperationLocation(
        GetsWithoutParentOperationLocationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsWithoutParentOperationLocation, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsWithoutParentOperationLocationValidator, GetsWithoutParentOperationLocationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsWithoutParentOperationLocationResponse>(isValidRequest.Error!);

        if (request.CostCenterId is not null)
        {
            var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId!.Value), ct);
            if (costCenter.IsFailure)
                return Result.Failure<GetsWithoutParentOperationLocationResponse>(costCenter.Error!);
        }

        var response = await _mediator.Send(new GetsWithoutParentOperationLocationQuery(request.CostCenterId, request.ProjectId, request.FilterData, request.IsActive, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsWithoutParentOperationLocationResponse>(response.Error!);

        return new GetsWithoutParentOperationLocationResponse(response.Value?.Data?.Adapt<List<GetsOperationLocationByCostCenterIdModel>>() ??
            new List<GetsOperationLocationByCostCenterIdModel>(0), response.Value?.RowCount ?? 0);

    }

    public async Task<Result<GetsOperationLocationExcelEnumResponse?>> GetsOperationLocationExcelEnum(
        GetsOperationLocationExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationLocationExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<OperationLocationExcelEnum>());
        return new GetsOperationLocationExcelEnumResponse(response);
    }

    public async Task<Result<GetsOperationLocationExcelExporterResponse?>> GetsOperationLocationExcelExporter(
        GetsOperationLocationExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationLocation,CostCenterId:{CostCenterId} , ParentId:{ParentId}, IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}",
           request.CostCenterId, request.ParentId, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationLocationExcelExporterValidator, GetsOperationLocationExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationLocationExcelExporterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationLocationsQuery(request.Ids, request.CostCenterId, request.ProjectId, request.ParentId, request.FilterData, request.IsActive, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationLocationExcelExporterResponse>(response.Error!);
        var values = response.Value?.Data!;

        var data = new List<GetsOperationLocationExcelExporterModel>();
        if (values is not null)
            foreach (var dataItem in values)
                data.Add(dataItem.Adapt<GetsOperationLocationExcelExporterModel>());

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        List<OperationLocation>? parents = [];
        var parentIds = data?.Where(x => x.ParentId != null && x.ParentId > 0).Select(x => x.ParentId).Adapt<List<long>>().Distinct().ToList();
        if (parentIds is not null && parentIds.Count > 0)
        {
            var parentResponse = await _mediator.Send(new GetsOperationLocationByIdsQuery(parentIds), ct);
            if (parentResponse.IsFailure)
                return Result.Failure<GetsOperationLocationExcelExporterResponse>(parentResponse.Error!);
            parents = parentResponse.Value?.Data!;
        }

        var dataResult = data?.Adapt<List<GetsOperationLocationExcelExporterModel>>();
        foreach (var item in dataResult!)
        {
            item.ParentPublicCode = parents?.Where(x => x.Id == item.ParentId).FirstOrDefault()?.PublicCode;
            item.ParentPublicName = parents?.Where(x => x.Id == item.ParentId).FirstOrDefault()?.PublicName;
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
        }

        var file = new FileContentResult(OperationLocationExcels.OperationLocationToExcel(dataResult, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"OperationLocations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsOperationLocationExcelExporterResponse(file);
    }

}