using Engineering.Application.Services.ContractorMachineries.Commands.ActiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Commands.CreateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Commands.DisableContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Commands.InactiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Commands.StateChangerContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Commands.UpdateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.ActiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryGroupDelete;
using Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryModel;
using Engineering.Application.Services.ContractorMachineries.Models.CreateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.DisableContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.GetActiveContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryById;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryUnit;
using Engineering.Application.Services.ContractorMachineries.Models.GetsByContractorId;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelEnum;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelExporter;
using Engineering.Application.Services.ContractorMachineries.Models.InactiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.UpdateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Queries.GetActiveContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineryById;
using Engineering.Application.Services.ContractorMachineries.Queries.GetDuplicateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Queries.GetsByContractorId;
using Engineering.Application.Services.ContractorMachineries.Queries.GetsContractorMachineryByIds;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.ContractorMachineries.Enums;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.ContractorMachineries;

public class ContractorMachineryLogic : IContractorMachineryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractorMachineryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public ContractorMachineryLogic(
        IMediator mediator,
        ILogger<ContractorMachineryLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateContractorMachineryResponse?>> CreateContractorMachinery(
        CreateContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateContractorMachinery");

        var isValidRequest = await request.IsValidAsync<CreateContractorMachineryValidator, CreateContractorMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateContractorMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateContractorMachineryResponse>(companyResponse.Error!);
        }

        var duplicateQuery = await _mediator.Send(new GetDuplicateContractorMachineryQuery(request.ContractorId, request.MachineryId, request.Unit, companyId), ct);
        if (duplicateQuery is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateContractorMachineryResponse>(ContractorMachineryErrors.ContractorMachineryIsDuplicate);

        var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ContractorId], null, false, null), ct);
        if (contractorQuery.IsFailure)
            return Result.Failure<CreateContractorMachineryResponse>(contractorQuery.Error!);

        var currencyQuery = await _mediator.Send(new GetCurrencyByIdQuery(request.CurrencyId), ct);
        if (currencyQuery.IsFailure)
            return Result.Failure<CreateContractorMachineryResponse>(currencyQuery.Error!);

        var machineryQuery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
        if (machineryQuery.IsFailure)
            return Result.Failure<CreateContractorMachineryResponse>(MachineryErrors.MachineryWithIdNotFound);
        var machinery = machineryQuery.Value;

        if (ValidateNumberPlates(request.NumberPlates))
            return Result.Failure<CreateContractorMachineryResponse>(ContractorMachineryErrors.UnNumberPlates);

        var response = await _mediator.Send(new CreateContractorMachineryCommand(
            machinery!, request.ContractorId, request.MachineryPrice, request.CurrencyId, request.Unit, request.NumberPlates,
            request.MachineryIdentifier, request.IsActive, request.Description, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateContractorMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateContractorMachineryResponse>();
    }

    public async Task<Result<DisableContractorMachineryResponse?>> DisableContractorMachinery(
        DisableContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableContractorMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableContractorMachineryValidator, DisableContractorMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableContractorMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableContractorMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableContractorMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableContractorMachineryResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<ContractorMachineryGroupDeleteResponse?>> ContractorMachineryGroupDelete(
        ContractorMachineryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ContractorMachineryGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ContractorMachineryGroupDeleteValidator, ContractorMachineryGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ContractorMachineryGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableContractorMachineryCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ContractorMachineryGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ContractorMachineryGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateContractorMachineryResponse?>> UpdateContractorMachinery(
        UpdateContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateContractorMachinery, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateContractorMachineryValidator, UpdateContractorMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateContractorMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateContractorMachineryResponse>(companyResponse.Error!);
        }

        var duplicateQuery = await _mediator.Send(new GetDuplicateContractorMachineryQuery(request.ContractorId, request.MachineryId, request.Unit, companyId), ct);
        if (duplicateQuery is { IsSuccess: true, Value: not null } && duplicateQuery.Value.Id != request.Id)
            return Result.Failure<UpdateContractorMachineryResponse>(ContractorMachineryErrors.ContractorMachineryIsDuplicate);

        var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ContractorId], null, false, null), ct);
        if (contractorQuery.IsFailure)
            return Result.Failure<UpdateContractorMachineryResponse>(contractorQuery.Error!);

        var currencyQuery = await _mediator.Send(new GetCurrencyByIdQuery(request.CurrencyId), ct);
        if (currencyQuery.IsFailure)
            return Result.Failure<UpdateContractorMachineryResponse>(currencyQuery.Error!);

        var machineryQuery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
        if (machineryQuery.IsFailure)
            return Result.Failure<UpdateContractorMachineryResponse>(MachineryErrors.MachineryWithIdNotFound);
        var machinery = machineryQuery.Value;

        if (ValidateNumberPlates(request.NumberPlates))
            return Result.Failure<UpdateContractorMachineryResponse>(ContractorMachineryErrors.UnNumberPlates);

        var response = await _mediator.Send(new UpdateContractorMachineryCommand(request.Id, machinery!, request.ContractorId, request.MachineryPrice,
            request.CurrencyId, request.Unit, request.NumberPlates, request.MachineryIdentifier, request.IsActive, request.Description, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateContractorMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateContractorMachineryResponse>();
    }

    public async Task<Result<InactiveContractorMachineryResponse?>> InactiveContractorMachinery(
        InactiveContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveContractorMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveContractorMachineryValidator, InactiveContractorMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveContractorMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveContractorMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveContractorMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveContractorMachineryResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveContractorMachineryResponse?>> ActiveContractorMachinery(
        ActiveContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveContractorMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveContractorMachineryValidator, ActiveContractorMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveContractorMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveContractorMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveContractorMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveContractorMachineryResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerContractorMachineriesResponse?>> StateChangerContractorMachineries(
        StateChangerContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerContractorMachineries, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerContractorMachineriesValidator, StateChangerContractorMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerContractorMachineriesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerContractorMachineriesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsContractorMachineryByIdsQuery(request.Ids, 1, request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerContractorMachineriesResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerContractorMachineriesCommand(values!, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerContractorMachineriesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerContractorMachineriesResponse(true);
    }

    public async Task<Result<GetContractorMachineryByIdResponse?>> GetContractorMachineryById(
        GetContractorMachineryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetContractorMachineryById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetContractorMachineryByIdValidator, GetContractorMachineryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorMachineryByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetContractorMachineryByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetContractorMachineryByIdResponse>(response.Error!);
        var value = response.Value!;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.Add(value.ContractorId);
        thirdPartyIds = thirdPartyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<long>? userIds = [];
        userIds.Add(value.CreatorId);
        userIds = userIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.Add(value.CurrencyId);
        currencyIds = currencyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetContractorMachineryByIdResponse>();
        data.CompanyName = company?.NameFa;
        data.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value.ContractorId)?.FullName;
        data.ContractorNickName = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value.ContractorId)?.Nickname;
        data.Creator = users?.FirstOrDefault(x => x?.UserId == value.CreatorId)?.FullName;
        data.Currency = currencies?.FirstOrDefault(x => x?.Id == value.CurrencyId)?.Name;
        data.NumberPlatesModel = SetNumberPlates(value.NumberPlates);

        return data;
    }

    public async Task<Result<GetActiveContractorMachineriesResponse?>> GetsActiveContractorMachinery(
        GetActiveContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveContractorMachinery");

        var isValidRequest = await request.IsValidAsync<GetActiveContractorMachineriesValidator, GetActiveContractorMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveContractorMachineriesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveContractorMachineriesQuery(request.MachineryIds, request.ContractorIds, request.Unit, request.FromDate,
            request.ToDate, request.FilterData, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveContractorMachineriesResponse>(response.Error!);
        var values = response.Value!.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdPartyIds = thirdPartyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<long>? userIds = [];
        userIds.AddRange(values?.Where(x => x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        userIds = userIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(values?.Where(x => x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        currencyIds = currencyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveContractorMachineryModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value?.ContractorId)?.FullName;
            item.ContractorNickName = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value?.ContractorId)?.Nickname;
            item.Creator = users?.FirstOrDefault(x => x?.UserId == value?.CreatorId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x?.Id == value?.CurrencyId)?.Name;
            item.NumberPlatesModel = SetNumberPlates(value?.NumberPlates);
        }

        return new GetActiveContractorMachineriesResponse(data ?? new List<GetsActiveContractorMachineryModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetContractorMachineriesResponse?>> GetsContractorMachinery(
        GetContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractorMachinery");

        var isValidRequest = await request.IsValidAsync<GetContractorMachineriesValidator, GetContractorMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorMachineriesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetContractorMachineriesQuery(null, request.MachineryIds, request.ContractorIds, request.Unit, request.FromDate,
            request.ToDate, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetContractorMachineriesResponse>(response.Error!);
        var values = response.Value!.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdPartyIds = thirdPartyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<long>? userIds = [];
        userIds.AddRange(values?.Where(x => x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        userIds = userIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(values?.Where(x => x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        currencyIds = currencyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetContractorMachineriesModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value?.ContractorId)?.FullName;
            item.ContractorNickName = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value?.ContractorId)?.Nickname;
            item.Creator = users?.FirstOrDefault(x => x?.UserId == value?.CreatorId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x?.Id == value?.CurrencyId)?.Name;
            item.NumberPlatesModel = SetNumberPlates(value?.NumberPlates);
        }

        return new GetContractorMachineriesResponse(data ?? new List<GetContractorMachineriesModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsContractorMachineryExcelExporterResponse?>> GetsContractorMachineryExcelExporter(
        GetsContractorMachineryExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractorMachineryExcelExporter");

        var isValidRequest = await request.IsValidAsync<GetsContractorMachineryExcelExporterValidator, GetsContractorMachineryExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractorMachineryExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetContractorMachineriesQuery(request.Ids, request.MachineryIds, request.ContractorIds, request.Unit, request.FromDate,
            request.ToDate, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorMachineryExcelExporterResponse>(response.Error!);
        var values = response.Value!.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdPartyIds = thirdPartyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<long>? userIds = [];
        userIds.AddRange(values?.Where(x => x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        userIds = userIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(values?.Where(x => x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        currencyIds = currencyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetContractorMachineriesModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value?.ContractorId)?.FullName;
            item.ContractorNickName = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value?.ContractorId)?.Nickname;
            item.Creator = users?.FirstOrDefault(x => x?.UserId == value?.CreatorId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x?.Id == value?.CurrencyId)?.Name;
            item.NumberPlatesModel = SetNumberPlates(value?.NumberPlates);
        }

        var excelData = data.Adapt<List<GetsContractorMachineryExcelExporterModel>>();

        var file = new FileContentResult(ContractorMachineryExcels.ContractorMachineryToExcel(excelData, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ContractorMachinerys-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsContractorMachineryExcelExporterResponse(file);
    }

    public async Task<Result<GetsContractorMachineryExcelEnumResponse?>> GetsContractorMachineryExcelEnum(
        GetsContractorMachineryExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractorMachineryExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ContractorMachineryExcelEnum>());
        return new GetsContractorMachineryExcelEnumResponse(response);
    }

    public async Task<Result<GetContractorMachineryUnitResponse?>> GetContractorMachineryUnit(
        GetContractorMachineryUnitRequest request, CT ct)
    {
        var response = await Task.Run(() =>
        {
            return EnumExt.GetEnumObjectList<ContractorMachineryUnit>();
        });

        return new GetContractorMachineryUnitResponse(response);
    }

    public async Task<Result<GetsByContractorIdResponse?>> GetsByContractorId(
        GetsByContractorIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByContractorId");

        var isValidRequest = await request.IsValidAsync<GetsByContractorIdValidator, GetsByContractorIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByContractorIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsByContractorIdQuery(request.ContractorId, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByContractorIdResponse>(response.Error!);
        var values = response.Value!.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values?.Where(x => x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList() ?? []);
        thirdPartyIds = thirdPartyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        List<long>? userIds = [];
        userIds.AddRange(values?.Where(x => x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList() ?? []);
        userIds = userIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        List<long>? currencyIds = [];
        currencyIds.AddRange(values?.Where(x => x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).Distinct().ToList() ?? []);
        currencyIds = currencyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsByContractorIdModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.Creator = users?.FirstOrDefault(x => x?.UserId == value?.CreatorId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x?.Id == value?.CurrencyId)?.Name;
            item.NumberPlatesModel = SetNumberPlates(value?.NumberPlates);
        }

        var contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == request.ContractorId);

        return new GetsByContractorIdResponse(contractor?.Id, contractor?.FullName, contractor?.Nickname, contractor?.OrganizationCode, contractor?.DefaultPhoneNo,
            data ?? new List<GetsByContractorIdModel>(0), response.Value?.RowCount ?? 0);
    }

    //PrivateMethod
    private ContractorMachineryNumberPlatesModel? SetNumberPlates(
        string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());
            string part1 = onlyNumbers.Substring(0, 2); // "12"
            string part2 = onlyNumbers.Substring(2, 3); // "345"
            string part3 = onlyNumbers.Substring(5, 2); // "67"

            return new ContractorMachineryNumberPlatesModel()
            {
                Letter = onlyLetters,
                Part1 = part1,
                Part2 = part2,
                Part3 = part3,
            };
        }
        else
            return new ContractorMachineryNumberPlatesModel();
    }

    private bool ValidateNumberPlates(
        string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());

            var onlyLettersCount = onlyLetters.Count();
            if (onlyLetters == "الف")
                onlyLettersCount = 1;

            if (!(onlyNumbers.Count() == 7 && onlyLettersCount == 1))
                return true;
            else
                return false;
        }

        return false;
    }
}

