using Engineering.Application.Services.FixAssetMachineries.Commands.ActiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryDocument;
using Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetMachineryDocument;
using Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Commands.InactiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Commands.StateChangerFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.ActiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryDocument;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryGroupDelete;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryModel;
using Engineering.Application.Services.FixAssetMachineries.Models.GetActiveFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryType;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.InactiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetActiveFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryDriverIds;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryByIds;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.Machineries;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.FixAssetMachineries;

public partial class FixAssetMachineryLogic : IFixAssetMachineryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<FixAssetMachineryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    public FixAssetMachineryLogic(
        IMediator mediator,
        ILogger<FixAssetMachineryLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateFixAssetMachineryResponse?>> CreateFixAssetMachinery(
        CreateFixAssetMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateFixAssetMachinery");

        var isValidRequest = await request.IsValidAsync<CreateFixAssetMachineryValidator, CreateFixAssetMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFixAssetMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateFixAssetMachineryResponse>(companyResponse.Error!);
        }

        var machineryQuery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
        if (machineryQuery.IsFailure)
            return Result.Failure<CreateFixAssetMachineryResponse>(MachineryErrors.MachineryWithIdNotFound);
        var machinery = machineryQuery.Value;

        if (!string.IsNullOrEmpty(request.NumberPlates))
        {
            var onlyNumbers = new String(request.NumberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(request.NumberPlates.Where(char.IsLetter).ToArray());

            var onlyLettersCount = onlyLetters.Count();
            if (onlyLetters == "الف")
                onlyLettersCount = 1;

            if (!(onlyNumbers.Count() == 7 && onlyLettersCount == 1))
                return Result.Failure<CreateFixAssetMachineryResponse>(FixAssetMachineryErrors.UnNumberPlates);
        }

        if (request.DriverId is not null && request.DriverName is not null)
            return Result.Failure<CreateFixAssetMachineryResponse>(FixAssetMachineryErrors.DriverInfoUnValid);

        if (request.FixAssetMachineryType == FixAssetMachineryType.rented &&
           (request.ContractorId == null || request.StartDate == null || request.EndDate == null))
            return Result.Failure<CreateFixAssetMachineryResponse>(FixAssetMachineryErrors.ContractorIdAndDatesMustAdd);

        if (request.FixAssetMachineryType == FixAssetMachineryType.rented && request.ContractorId != null && request.ContractorId > 0)
        {
            List<long> ids = [];
            ids.Add(request.ContractorId.Value);
            var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (contractorQuery.IsFailure)
                return Result.Failure<CreateFixAssetMachineryResponse>(contractorQuery.Error!);
        }

        if (request.DriverId != null && request.DriverId > 0)
        {
            List<long> ids = [];
            ids.Add(request.DriverId.Value);
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<CreateFixAssetMachineryResponse>(driverQuery.Error!);
        }

        var response = await _mediator.Send(new CreateFixAssetMachineryCommand(
            machinery!,
            request.MachinerySpecification,
            request.NumberPlates,
            request.FixAssetMachineryType,
            request.MachineryPrice,
            request.IsActive,
            request.ContractorId,
            request.DriverId,
            request.DriverName,
            request.StartDate,
            request.EndDate,
            request.Description,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateFixAssetMachineryResponse>(response.Error!);
        var fixAssetMachinery = response.Value;

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateFixAssetMachineryDocumentCommand(document, response.Value!), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<CreateFixAssetMachineryResponse>(createDocumentResponse.Error!);
            }

        if (request.Rates is not null && request.Rates.Count > 0)
        {
            List<CreateRateDataCommand>? createRateDataCommands = [];
            foreach (var item in request.Rates)
            {
                DateTime? startDate = null;
                if (item.StartTime != null)
                    startDate = item.StartDate.Date.Add(item.StartTime.Value);
                else
                    startDate = item.StartDate;
                DateTime? endDate = null;
                if (item.EndTime != null)
                    endDate = item.EndDate.Date.Add(item.EndTime.Value);
                else
                    endDate = item.EndDate;

                bool hasOverlap = createRateDataCommands.Any(range =>
                    startDate <= range.EndDate && endDate >= range.StartDate);

                if (!hasOverlap)
                {
                    createRateDataCommands.Add(new CreateRateDataCommand(
                        startDate!.Value,
                        endDate!.Value,
                        item.HourlyRate,
                        item.DailyRate,
                        item.ServiceRate,
                        item.VolumeRate));
                }
                else
                    return Result.Failure<CreateFixAssetMachineryResponse>(FixAssetMachineryErrors.InvalidRatesRequest);
            }

            var createDocumentResponse = await _mediator.Send(new CreateFixAssetMachineryRateCommand(fixAssetMachinery!, createRateDataCommands), ct);
            if (createDocumentResponse.IsFailure)
                return Result.Failure<CreateFixAssetMachineryResponse>(createDocumentResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateFixAssetMachineryResponse>();
    }

    public async Task<Result<CreateFixAssetMachineryDocumentResponse?>> CreateFixAssetMachineryDocument(
        CreateFixAssetMachineryDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateFixAssetMachineryDocumentValidator, CreateFixAssetMachineryDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFixAssetMachineryDocumentResponse>(isValidRequest.Error!);

        var getFixAssetMachinery = await _mediator.Send(new GetFixAssetMachineryByIdQuery(request.FixAssetMachineryId), ct);
        if (getFixAssetMachinery.IsFailure)
            return Result.Failure<CreateFixAssetMachineryDocumentResponse>(getFixAssetMachinery.Error!);

        var fixAssetMachinery = getFixAssetMachinery.Value!;

        if (fixAssetMachinery.FixAssetMachineryDocuments is not null && fixAssetMachinery.FixAssetMachineryDocuments.Count > 0)
            foreach (var item in fixAssetMachinery.FixAssetMachineryDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteFixAssetMachineryDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<CreateFixAssetMachineryDocumentResponse>(deleteDocumentResponse.Error!);
            }

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateFixAssetMachineryDocumentCommand(document, fixAssetMachinery), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<CreateFixAssetMachineryDocumentResponse>(createDocumentResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new CreateFixAssetMachineryDocumentResponse(fixAssetMachinery.Id);
    }

    public async Task<Result<DisableFixAssetMachineryResponse?>> DisableFixAssetMachinery(
        DisableFixAssetMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableFixAssetMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableFixAssetMachineryValidator, DisableFixAssetMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableFixAssetMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableFixAssetMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableFixAssetMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableFixAssetMachineryResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<FixAssetMachineryGroupDeleteResponse?>> FixAssetMachineryGroupDelete(
        FixAssetMachineryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for FixAssetMachineryGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<FixAssetMachineryGroupDeleteValidator, FixAssetMachineryGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<FixAssetMachineryGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableFixAssetMachineryCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<FixAssetMachineryGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new FixAssetMachineryGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateFixAssetMachineryResponse?>> UpdateFixAssetMachinery(
        UpdateFixAssetMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateFixAssetMachinery, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateFixAssetMachineryValidator, UpdateFixAssetMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateFixAssetMachineryResponse>(companyResponse.Error!);
        }

        var fixAssetQuery = await _mediator.Send(new GetFixAssetMachineryByIdQuery(request.Id), ct);
        if (fixAssetQuery.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryResponse>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
        var fixAssetMachinery = fixAssetQuery.Value;

        Machinery? machinery = null;
        if (request.MachineryId != fixAssetQuery.Value?.Machinery.Id)
        {
            var machineryQuery = await _mediator.Send(new GetMachineryByIdQuery(request.MachineryId), ct);
            if (machineryQuery.IsFailure)
                return Result.Failure<UpdateFixAssetMachineryResponse>(MachineryErrors.MachineryWithIdNotFound);
            machinery = machineryQuery.Value;
        }
        else
            machinery = fixAssetQuery.Value?.Machinery;

        if (!string.IsNullOrEmpty(request.NumberPlates))
        {
            var onlyNumbers = new String(request.NumberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(request.NumberPlates.Where(char.IsLetter).ToArray());

            var onlyLettersCount = onlyLetters.Count();
            if (onlyLetters == "الف")
                onlyLettersCount = 1;

            if (!(onlyNumbers.Count() == 7 && onlyLettersCount == 1))
                return Result.Failure<UpdateFixAssetMachineryResponse>(FixAssetMachineryErrors.UnNumberPlates);
        }

        if (request.DriverId is not null && request.DriverName is not null)
            return Result.Failure<UpdateFixAssetMachineryResponse>(FixAssetMachineryErrors.DriverInfoUnValid);

        if (request.FixAssetMachineryType == FixAssetMachineryType.rented &&
           (request.ContractorId == null || request.StartDate == null || request.EndDate == null))
            return Result.Failure<UpdateFixAssetMachineryResponse>(FixAssetMachineryErrors.ContractorIdAndDatesMustAdd);

        if (request.FixAssetMachineryType == FixAssetMachineryType.rented && request.ContractorId != null && request.ContractorId > 0)
        {
            List<long> ids = [];
            ids.Add(request.ContractorId.Value);
            var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (contractorQuery.IsFailure)
                return Result.Failure<UpdateFixAssetMachineryResponse>(contractorQuery.Error!);
        }

        if (request.DriverId != null && request.DriverId > 0)
        {
            List<long> ids = [];
            ids.Add(request.DriverId.Value);
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<UpdateFixAssetMachineryResponse>(driverQuery.Error!);
        }

        var response = await _mediator.Send(new UpdateFixAssetMachineryCommand(
            request.Id,
            machinery!,
            request.MachinerySpecification,
            request.NumberPlates,
            request.FixAssetMachineryType,
            request.MachineryPrice,
            request.IsActive,
            request.ContractorId,
            request.DriverId,
            request.DriverName,
            request.StartDate,
            request.EndDate,
            request.Description,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryResponse>(response.Error!);

        if (fixAssetMachinery?.FixAssetMachineryDocuments is not null && fixAssetMachinery?.FixAssetMachineryDocuments.Count > 0)
            foreach (var item in fixAssetMachinery.FixAssetMachineryDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteFixAssetMachineryDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<UpdateFixAssetMachineryResponse>(deleteDocumentResponse.Error!);
            }

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateFixAssetMachineryDocumentCommand(document, fixAssetMachinery!), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<UpdateFixAssetMachineryResponse>(createDocumentResponse.Error!);
            }

        if (request.Rates is not null && request.Rates.Count > 0)
        {
            var deletedRates = request.Rates.Where(x => x.Id != null && x.IsDeleted == true).ToList();
            var updatedRates = request.Rates.Where(x => x.Id != null && x.Id > 0 && x.IsDeleted == false).ToList();
            var createdRates = request.Rates.Where(x => x.Id == null && x.IsDeleted == false).ToList();

            if (deletedRates is not null && deletedRates.Count > 0)
                foreach (var item in deletedRates)
                {
                    var deleteRate = await _mediator.Send(new DisableFixAssetMachineryRateCommand(item.Id!.Value), ct);
                    if (deleteRate.IsFailure)
                        return Result.Failure<UpdateFixAssetMachineryResponse>(deleteRate.Error!);
                }

            if (updatedRates is not null && updatedRates.Count > 0)
            {
                foreach (var item in updatedRates)
                {
                    DateTime? startDate = null;
                    if (item.StartTime != null)
                        startDate = item.StartDate.Date.Add(item.StartTime.Value);

                    DateTime? endDate = null;
                    if (item.EndTime != null)
                        endDate = item.EndDate.Date.Add(item.EndTime.Value);

                    var updateResponse = await _mediator.Send(new UpdateFixAssetMachineryRateCommand(
                        item.Id!.Value,
                        startDate != null ? startDate.Value : item.StartDate.Date,
                        endDate != null ? endDate.Value : item.EndDate.Date,
                        item.HourlyRate,
                        item.DailyRate,
                        item.ServiceRate,
                        item.VolumeRate), ct);
                    if (updateResponse.IsFailure)
                        return Result.Failure<UpdateFixAssetMachineryResponse>(updateResponse.Error!);
                    var Rate = response.Value;
                }
            }

            if (createdRates is not null && createdRates.Count > 0)
            {
                List<CreateRateDataCommand>? createRateDataCommands = [];
                foreach (var item in createdRates)
                {
                    DateTime? startDate = null;
                    if (item.StartTime != null)
                        startDate = item.StartDate.Date.Add(item.StartTime.Value);
                    else
                        startDate = item.StartDate;
                    DateTime? endDate = null;
                    if (item.EndTime != null)
                        endDate = item.EndDate.Date.Add(item.EndTime.Value);
                    else
                        endDate = item.EndDate;

                    bool hasOverlap = createRateDataCommands.Any(range =>
                        startDate <= range.EndDate && endDate >= range.StartDate);

                    if (!hasOverlap)
                    {
                        createRateDataCommands.Add(new CreateRateDataCommand(
                            startDate!.Value,
                            endDate!.Value,
                            item.HourlyRate,
                            item.DailyRate,
                            item.ServiceRate,
                            item.VolumeRate));
                    }
                    else
                        return Result.Failure<UpdateFixAssetMachineryResponse>(FixAssetMachineryErrors.InvalidRatesDuplicate);
                }

                var createDocumentResponse = await _mediator.Send(new CreateFixAssetMachineryRateCommand(fixAssetMachinery!, createRateDataCommands), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<UpdateFixAssetMachineryResponse>(createDocumentResponse.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateFixAssetMachineryResponse>();
    }

    public async Task<Result<InactiveFixAssetMachineryResponse?>> InactiveFixAssetMachinery(
        InactiveFixAssetMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveFixAssetMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveFixAssetMachineryValidator, InactiveFixAssetMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveFixAssetMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveFixAssetMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveFixAssetMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveFixAssetMachineryResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveFixAssetMachineryResponse?>> ActiveFixAssetMachinery(
        ActiveFixAssetMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveFixAssetMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveFixAssetMachineryValidator, ActiveFixAssetMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveFixAssetMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveFixAssetMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveFixAssetMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveFixAssetMachineryResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerFixAssetMachineriesResponse?>> StateChangerFixAssetMachineries(
        StateChangerFixAssetMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerFixAssetMachineries, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerFixAssetMachineriesValidator, StateChangerFixAssetMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerFixAssetMachineriesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerFixAssetMachineriesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsFixAssetMachineryByIdsQuery(request.Ids, 1, request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerFixAssetMachineriesResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerFixAssetMachineriesCommand(values!, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerFixAssetMachineriesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerFixAssetMachineriesResponse(true);
    }

    public async Task<Result<GetFixAssetMachineryByIdResponse?>> GetFixAssetMachineryById(
        GetFixAssetMachineryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFixAssetMachineryById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetFixAssetMachineryByIdValidator, GetFixAssetMachineryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFixAssetMachineryByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFixAssetMachineryByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetFixAssetMachineryByIdResponse>(response.Error!);
        var value = response.Value!;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.Add(value.ContractorId != null && value.ContractorId > 0 ? value.ContractorId.Value : 0);
        thirdPartyIds.Add(value.DriverId != null && value.DriverId > 0 ? value.DriverId.Value : 0);
        thirdPartyIds = thirdPartyIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        List<GetFixAssetMachinerByIdNotWorkModel>? notWorks = [];
        if (value?.FixAssetMachineryNotWorks != null && value?.FixAssetMachineryNotWorks.Count > 0)
            foreach (var item1 in value.FixAssetMachineryNotWorks)
            {
                notWorks.Add(new GetFixAssetMachinerByIdNotWorkModel()
                {
                    Description = item1.Description,
                    Id = item1.Id,
                    FromDate = item1.StartDate,
                    ToDate = item1.EndDate,
                    FromTime = item1.StartDate.TimeOfDay,
                    ToTime = item1.EndDate.TimeOfDay,
                });
            }

        List<GetFixAssetMachineryDocumentByIdResponseModel>? documents = [];
        if (value?.FixAssetMachineryDocuments is not null && value?.FixAssetMachineryDocuments.Count > 0)
            foreach (var item in value.FixAssetMachineryDocuments)
                documents.Add(new GetFixAssetMachineryDocumentByIdResponseModel(item.Id, item.Url));

        var data = value.Adapt<GetFixAssetMachineryByIdResponse>();
        data.CompanyName = company?.NameFa;
        data.MachineryId = value!.Machinery.Id;
        data.MachineryName = value!.Machinery.MachineryName;
        data.MachineryCode = value!.Machinery.MachineryCode;
        data.DriverFullname = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value.DriverId)?.FullName;
        data.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value.ContractorId)?.FullName;
        data.NumberPlatesModel = SetNumberPlates(value.NumberPlates);
        data.NotWorks = notWorks;
        data.Documents = documents;

        return data;
    }

    public async Task<Result<GetActiveFixAssetMachineriesResponse?>> GetsActiveFixAssetMachinery(
        GetActiveFixAssetMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveFixAssetMachinery");

        var isValidRequest = await request.IsValidAsync<GetActiveFixAssetMachineriesValidator, GetActiveFixAssetMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveFixAssetMachineriesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveFixAssetMachineriesQuery(request.MachineryIds, request.Type, request.NumberPlates,
            request.FilterData, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveFixAssetMachineriesResponse>(response.Error!);
        var values = response.Value!.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList());
        thirdPartyIds.AddRange(values!.Where(x => x.DriverId != null && x.DriverId > 0).Select(x => (long)x.DriverId!).Distinct().ToList());
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds.Distinct().ToList(), null, null, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveFixAssetMachineryModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.MachineryId = value!.Machinery.Id;
            item.MachineryName = value!.Machinery.MachineryName;
            item.MachineryCode = value!.Machinery.MachineryCode;
            item.DriverFullname = thirdPartyInfos?.FirstOrDefault(x => x?.Id == item.DriverId)?.FullName;
            item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == item.ContractorId)?.FullName;
            item.NumberPlatesModel = SetNumberPlates(value.NumberPlates);
        }

        return new GetActiveFixAssetMachineriesResponse(data ?? new List<GetsActiveFixAssetMachineryModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetFixAssetMachineriesResponse?>> GetsFixAssetMachinery(
        GetFixAssetMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFixAssetMachinery");

        var isValidRequest = await request.IsValidAsync<GetFixAssetMachineriesValidator, GetFixAssetMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFixAssetMachineriesResponse>(isValidRequest.Error!);

        List<long>? driverIds = [];
        if (!string.IsNullOrEmpty(request.DriverName))
        {
            var driverIdsQuery = await _mediator.Send(new GetFixAssetMachineryDriverIdsQuery(), ct);
            var driverIdss = driverIdsQuery.Value;

            if (driverIdss is not null && driverIdss.Count > 0)
            {
                var thirdPartiesQuery = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(driverIdss, request.DriverName, null, _mediator, ct);
                driverIds = thirdPartiesQuery?.Where(x => x != null).Select(x => x!.Id).ToList();
            }
        }

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFixAssetMachineriesQuery(
            null,
            request.MachineryIds,
            request.ContractorIds,
            request.Type,
            request.NumberPlates,
            request.FromDate,
            request.ToDate,
            driverIds,
            request.DriverName,
            request.FilterData,
            request.IsActive,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetFixAssetMachineriesResponse>(response.Error!);
        var values = response.Value!.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList());
        thirdPartyIds.AddRange(values!.Where(x => x.DriverId != null && x.DriverId > 0).Select(x => (long)x.DriverId!).Distinct().ToList());
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds.Distinct().ToList(), null, null, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetFixAssetMachineriesModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);

            List<GetFixAssetMachineriesNotWorkModel>? notWorks = [];
            if (value?.FixAssetMachineryNotWorks != null && value?.FixAssetMachineryNotWorks.Count > 0)
                foreach (var notwork in value.FixAssetMachineryNotWorks)
                    notWorks.Add(new GetFixAssetMachineriesNotWorkModel()
                    {
                        Description = notwork.Description,
                        Id = notwork.Id,
                        FromDate = notwork.StartDate,
                        ToDate = notwork.EndDate,
                        FromTime = notwork.StartDate.TimeOfDay,
                        ToTime = notwork.EndDate.TimeOfDay,
                    });

            List<GetFixAssetMachineriesRateModel>? rates = [];
            if (value?.FixAssetMachineryRates != null && value?.FixAssetMachineryRates.Count > 0)
                foreach (var rate in value.FixAssetMachineryRates)
                    rates.Add(new GetFixAssetMachineriesRateModel()
                    {
                        Id = rate.Id,
                        FromDate = rate.StartDate,
                        ToDate = rate.EndDate,
                        FromTime = rate.StartDate.TimeOfDay,
                        ToTime = rate.EndDate.TimeOfDay,
                        DailyRate = rate.DailyRate,
                        HourlyRate = rate.HourlyRate,
                        ServiceRate = rate.ServiceRate,
                        VolumeRate = rate.VolumeRate
                    });

            List<GetsFixAssetMachineryDocumentResponseModel>? documents = [];
            if (value?.FixAssetMachineryDocuments is not null && value?.FixAssetMachineryDocuments.Count > 0)
                foreach (var doc in value.FixAssetMachineryDocuments)
                    documents.Add(new GetsFixAssetMachineryDocumentResponseModel(doc.Id, doc.Url));

            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.MachineryId = value!.Machinery.Id;
            item.MachineryName = value!.Machinery.MachineryName;
            item.MachineryCode = value!.Machinery.MachineryCode;
            item.DriverFullname = thirdPartyInfos?.FirstOrDefault(x => x?.Id == item.DriverId)?.FullName;
            item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == item.ContractorId)?.FullName;
            item.NumberPlatesModel = SetNumberPlates(value.NumberPlates);
            item.NotWorks = notWorks;
            item.Documents = documents;
            item.Rates = rates;
        }

        return new GetFixAssetMachineriesResponse(data ?? new List<GetFixAssetMachineriesModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFixAssetMachineryExcelExporterResponse?>> GetsFixAssetMachineryExcelExporter(
        GetsFixAssetMachineryExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFixAssetMachineryExcelExporter");
        var isValidRequest = await request.IsValidAsync<GetsFixAssetMachineryExcelExporterValidator, GetsFixAssetMachineryExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFixAssetMachineryExcelExporterResponse>(isValidRequest.Error!);

        List<long>? driverIds = [];
        if (!string.IsNullOrEmpty(request.DriverName))
        {
            var driverIdsQuery = await _mediator.Send(new GetFixAssetMachineryDriverIdsQuery(), ct);
            var driverIdss = driverIdsQuery.Value;

            if (driverIdss is not null && driverIdss.Count > 0)
            {
                var thirdPartiesQuery = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(driverIdss, request.DriverName, null, _mediator, ct);
                driverIds = thirdPartiesQuery?.Where(x => x != null).Select(x => x!.Id).ToList();
            }
        }
        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetFixAssetMachineriesQuery(
            request.Ids,
            request.MachineryIds,
            request.ContractorIds,
            request.Type,
            request.NumberPlates,
            request.FromDate,
            request.ToDate,
            driverIds,
            request.DriverName,
            request.FilterData,
            request.IsActive,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize),
            ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsFixAssetMachineryExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        List<long>? thirdPartyIds = [];
        thirdPartyIds.AddRange(values!.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList());
        thirdPartyIds.AddRange(values!.Where(x => x.DriverId != null && x.DriverId > 0).Select(x => (long)x.DriverId!).Distinct().ToList());
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds.Distinct().ToList(), null, null, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var machineryNotWorks = values!.Where(x => x.FixAssetMachineryNotWorks is not null && x.FixAssetMachineryNotWorks.Count > 0)
            .SelectMany(x => x.FixAssetMachineryNotWorks).ToList();

        var machineryRates = values!.Where(x => x.FixAssetMachineryRates is not null && x.FixAssetMachineryRates.Count > 0)
            .SelectMany(x => x.FixAssetMachineryRates).ToList();

        List<GetFixAssetMachineriesNotWorkExcelExporterModel>? notWorks = [];
        foreach (var notWork in machineryNotWorks)
            notWorks.Add(new GetFixAssetMachineriesNotWorkExcelExporterModel()
            {
                Description = notWork.Description,
                FixAssetMachineryId = notWork.FixAssetMachinery.Id,
                Id = notWork.Id,
                FromDate = notWork.StartDate,
                ToDate = notWork.EndDate,
                FromTime = notWork.StartDate.TimeOfDay,
                ToTime = notWork.EndDate.TimeOfDay,
            });

        List<GetFixAssetMachineriesRateExcelExporterModel>? rates = [];
        foreach (var rate in machineryRates)
            rates.Add(new GetFixAssetMachineriesRateExcelExporterModel()
            {
                Id = rate.Id,
                FromDate = rate.StartDate,
                ToDate = rate.EndDate,
                FromTime = rate.StartDate.TimeOfDay,
                ToTime = rate.EndDate.TimeOfDay,
                DailyRate = rate.DailyRate,
                HourlyRate = rate.HourlyRate,
                ServiceRate = rate.ServiceRate,
                VolumeRate = rate.VolumeRate,
                FixAssetMachineryId = rate.FixAssetMachinery.Id
            });

        var data = values.Adapt<List<GetsFixAssetMachineryExcelExporterModel>>();
        foreach (var item in data)
        {
            var value = values!.FirstOrDefault(x => x.Id == item.Id);
            var company = companies?.Where(x => x.Id == value!.CompanyId).FirstOrDefault();
            item.CompanyName = company?.NameFa;
            item.MachineryName = value!.Machinery.MachineryName;
            item.MachineryCode = value!.Machinery.MachineryCode;
            item.DriverFullname = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value.DriverId)?.FullName;
            item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x?.Id == value.ContractorId)?.FullName;
        }


        var file = new FileContentResult(FixAssetMachineryExcels.FixAssetMachineryToExcel(data, notWorks, rates, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"FixAssetMachinerys-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsFixAssetMachineryExcelExporterResponse(file);
    }

    public async Task<Result<GetsFixAssetMachineryExcelEnumResponse?>> GetsFixAssetMachineryExcelEnum(
        GetsFixAssetMachineryExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFixAssetMachineryExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<FixAssetMachineryExcelEnum>());
        return new GetsFixAssetMachineryExcelEnumResponse(response);
    }

    public async Task<Result<GetFixAssetMachineryTypeResponse?>> GetFixAssetMachineryType(
        GetFixAssetMachineryTypeRequest request, CT ct)
    {
        var response = await Task.Run(() =>
        {
            return EnumExt.GetEnumObjectList<FixAssetMachineryType>();
        });

        return new GetFixAssetMachineryTypeResponse(response);
    }

    //PrivateMethod
    private NumberPlatesModel? SetNumberPlates(string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());
            string part1 = onlyNumbers.Substring(0, 2); // "12"
            string part2 = onlyNumbers.Substring(2, 3); // "345"
            string part3 = onlyNumbers.Substring(5, 2); // "67"

            return new NumberPlatesModel()
            {
                Letter = onlyLetters,
                Part1 = part1,
                Part2 = part2,
                Part3 = part3,
            };
        }
        else
            return new NumberPlatesModel();

    }
}

