using Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetNotWorkDocument;
using Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetNotWorkDocument;
using Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetNotWorkDocument;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryNotWorkGroupDelete;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorkById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorks;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryDriverIds;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorkById;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorks;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryNotWorkForExcel;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries;

public partial class FixAssetMachineryLogic : IFixAssetMachineryLogic
{
    //NotWork
    public async Task<Result<CreateFixAssetMachineryNotWorkResponse?>> CreateFixAssetMachineryNotWork(
        CreateFixAssetMachineryNotWorkRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateFixAssetMachineryNotWork");

        var isValidRequest = await request.IsValidAsync<CreateFixAssetMachineryNotWorkValidator, CreateFixAssetMachineryNotWorkRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFixAssetMachineryNotWorkResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateFixAssetMachineryNotWorkResponse>(companyResponse.Error!);
        }

        var fixAssetmachineryQuery = await _mediator.Send(new GetFixAssetMachineryByIdQuery(request.FixAssetMachineryId), ct);
        if (fixAssetmachineryQuery.IsFailure)
            return Result.Failure<CreateFixAssetMachineryNotWorkResponse>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
        var fixAssetmachinery = fixAssetmachineryQuery.Value;

        DateTime? startDate = null;
        if (request.StartTime != null)
            startDate = request.StartDate.Date.Add(request.StartTime.Value);

        DateTime? endDate = null;
        if (request.EndTime != null)
            endDate = request.EndDate.Date.Add(request.EndTime.Value);

        var response = await _mediator.Send(new CreateFixAssetMachineryNotWorkCommand(fixAssetmachinery!,
            startDate != null ? startDate.Value : request.StartDate.Date,
            endDate != null ? endDate.Value : request.EndDate.Date,
            request.Description), ct);
        if (response.IsFailure)
            return Result.Failure<CreateFixAssetMachineryNotWorkResponse>(response.Error!);

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateFixAssetNotWorkDocumentCommand(document, response.Value!), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<CreateFixAssetMachineryNotWorkResponse>(createDocumentResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateFixAssetMachineryNotWorkResponse>();
    }

    public async Task<Result<CreateFixAssetNotWorkDocumentResponse?>> CreateFixAssetNotWorkDocument(
        CreateFixAssetNotWorkDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateFixAssetNotWorkDocumentValidator, CreateFixAssetNotWorkDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFixAssetNotWorkDocumentResponse>(isValidRequest.Error!);

        var getFixAssetNotWork = await _mediator.Send(new GetFixAssetMachineryNotWorkByIdQuery(request.FixAssetNotWorkId), ct);
        if (getFixAssetNotWork.IsFailure)
            return Result.Failure<CreateFixAssetNotWorkDocumentResponse>(getFixAssetNotWork.Error!);

        var fixAssetNotWork = getFixAssetNotWork.Value!;

        if (fixAssetNotWork.FixAssetNotWorkDocuments is not null && fixAssetNotWork.FixAssetNotWorkDocuments.Count > 0)
            foreach (var item in fixAssetNotWork.FixAssetNotWorkDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteFixAssetNotWorkDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<CreateFixAssetNotWorkDocumentResponse>(deleteDocumentResponse.Error!);
            }

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateFixAssetNotWorkDocumentCommand(document, fixAssetNotWork), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<CreateFixAssetNotWorkDocumentResponse>(createDocumentResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new CreateFixAssetNotWorkDocumentResponse(fixAssetNotWork.Id);
    }

    public async Task<Result<DisableFixAssetMachineryNotWorkResponse?>> DisableFixAssetMachineryNotWork(
        DisableFixAssetMachineryNotWorkRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableFixAssetMachineryNotWork, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableFixAssetMachineryNotWorkValidator, DisableFixAssetMachineryNotWorkRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableFixAssetMachineryNotWorkResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableFixAssetMachineryNotWorkCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableFixAssetMachineryNotWorkResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableFixAssetMachineryNotWorkResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateFixAssetMachineryNotWorkResponse?>> UpdateFixAssetMachineryNotWork(
        UpdateFixAssetMachineryNotWorkRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateFixAssetMachineryNotWork, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateFixAssetMachineryNotWorkValidator, UpdateFixAssetMachineryNotWorkRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryNotWorkResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateFixAssetMachineryNotWorkResponse>(companyResponse.Error!);
        }

        var fixAssetQuery = await _mediator.Send(new GetFixAssetMachineryNotWorkByIdQuery(request.Id), ct);
        if (fixAssetQuery.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryNotWorkResponse>(FixAssetMachineryErrors.FixAssetMachineryNotWorkNotFoundWithId);

        DateTime? startDate = null;
        if (request.StartTime != null)
            startDate = request.StartDate.Date.Add(request.StartTime.Value);

        DateTime? endDate = null;
        if (request.EndTime != null)
            endDate = request.EndDate.Date.Add(request.EndTime.Value);

        var response = await _mediator.Send(new UpdateFixAssetMachineryNotWorkCommand(request.Id, startDate != null ? startDate.Value : request.StartDate.Date,
            endDate != null ? endDate.Value : request.EndDate.Date, request.Description), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryNotWorkResponse>(response.Error!);
        var notWork = response.Value;

        if (notWork?.FixAssetNotWorkDocuments is not null && notWork?.FixAssetNotWorkDocuments.Count > 0)
            foreach (var item in notWork.FixAssetNotWorkDocuments)
            {
                var deleteDocumentResponse = await _mediator.Send(new DeleteFixAssetNotWorkDocumentCommand(item.Id), ct);
                if (deleteDocumentResponse.IsFailure)
                    return Result.Failure<UpdateFixAssetMachineryNotWorkResponse>(deleteDocumentResponse.Error!);
            }

        if (request.Documents is not null && request.Documents.Count > 0)
            foreach (var document in request.Documents)
            {
                var createDocumentResponse = await _mediator.Send(new CreateFixAssetNotWorkDocumentCommand(document, notWork!), ct);
                if (createDocumentResponse.IsFailure)
                    return Result.Failure<UpdateFixAssetMachineryNotWorkResponse>(createDocumentResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateFixAssetMachineryNotWorkResponse>();
    }

    public async Task<Result<FixAssetMachineryNotWorkGroupDeleteResponse?>> FixAssetMachineryNotWorkGroupDelete(
        FixAssetMachineryNotWorkGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for FixAssetMachineryNotWorkGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<FixAssetMachineryNotWorkGroupDeleteValidator, FixAssetMachineryNotWorkGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<FixAssetMachineryNotWorkGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableFixAssetMachineryNotWorkCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<FixAssetMachineryNotWorkGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new FixAssetMachineryNotWorkGroupDeleteResponse(true);
    }

    public async Task<Result<GetFixAssetMachineryNotWorkByIdResponse?>> GetFixAssetMachineryNotWorkById(
        GetFixAssetMachineryNotWorkByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFixAssetMachineryNotWorkById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetFixAssetMachineryNotWorkByIdValidator, GetFixAssetMachineryNotWorkByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFixAssetMachineryNotWorkByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFixAssetMachineryNotWorkByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetFixAssetMachineryNotWorkByIdResponse>(response.Error!);
        var value = response.Value!;

        List<GetFixAssetNotWorkDocumentByIdResponseModel>? documents = [];
        if (value?.FixAssetNotWorkDocuments is not null && value?.FixAssetNotWorkDocuments.Count > 0)
            foreach (var doc in value.FixAssetNotWorkDocuments)
                documents.Add(new GetFixAssetNotWorkDocumentByIdResponseModel(doc.Id, doc.Url));

        var data = new GetFixAssetMachineryNotWorkByIdResponse()
        {
            Id = value!.Id,
            Description = value.Description,
            EndDate = value.EndDate,
            EndTime = value.EndDate.TimeOfDay,
            MachineryCode = value.FixAssetMachinery.Machinery.MachineryCode,
            MachineryId = value.FixAssetMachinery.Machinery.Id,
            MachineryName = value.FixAssetMachinery.Machinery.MachineryName,
            StartDate = value.StartDate,
            StartTime = value.StartDate.TimeOfDay,
            Documents = documents
        };

        return data;
    }

    public async Task<Result<GetFixAssetMachineryNotWorksResponse?>> GetsFixAssetMachineryNotWork(
        GetFixAssetMachineryNotWorksRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFixAssetMachineryNotWork");

        var isValidRequest = await request.IsValidAsync<GetFixAssetMachineryNotWorksValidator, GetFixAssetMachineryNotWorksRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFixAssetMachineryNotWorksResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetFixAssetMachineryNotWorksQuery(request.FixAssetMachineryIds, request.MachineryIds, request.Type,
            request.StartDate, request.EndDate, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetFixAssetMachineryNotWorksResponse>(response.Error!);
        var values = response.Value!.Data;

        List<GetFixAssetMachineryNotWorksModel>? data = [];
        foreach (var item in values!)
        {
            List<GetsFixAssetNotWorkDocumentResponseModel>? documents = [];
            if (item?.FixAssetNotWorkDocuments is not null && item?.FixAssetNotWorkDocuments.Count > 0)
                foreach (var doc in item.FixAssetNotWorkDocuments)
                    documents.Add(new GetsFixAssetNotWorkDocumentResponseModel(doc.Id, doc.Url));

            data.Add(new GetFixAssetMachineryNotWorksModel()
            {
                Id = item.Id,
                Description = item.Description,
                EndDate = item.EndDate,
                EndTime = item.EndDate.TimeOfDay,
                MachineryCode = item.FixAssetMachinery.Machinery.MachineryCode,
                MachineryId = item.FixAssetMachinery.Machinery.Id,
                MachineryName = item.FixAssetMachinery.Machinery.MachineryName,
                StartDate = item.StartDate,
                StartTime = item.StartDate.TimeOfDay,
                Documents = documents
            });
        }

        return new GetFixAssetMachineryNotWorksResponse(data ?? new List<GetFixAssetMachineryNotWorksModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFixAssetMachineryNotWorkExcelExporterResponse?>> GetsFixAssetMachineryNotWorkExcelExporter(
        GetsFixAssetMachineryNotWorkExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFixAssetMachineryNotWorkExcelExporter");
        var isValidRequest = await request.IsValidAsync<GetsFixAssetMachineryNotWorkExcelExporterValidator, GetsFixAssetMachineryNotWorkExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFixAssetMachineryNotWorkExcelExporterResponse>(isValidRequest.Error!);

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
        var responses = await _mediator.Send(new GetsFixAssetMachineryNotWorkForExcelQuery(
            request.Ids,
            request.NotWorkIds,
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
            request.PageIndex,
            request.PageSize),
            ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsFixAssetMachineryNotWorkExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data!;

        List<long>? thirdPartyIds = values.Where(x => x.ContractorId is not null && x.ContractorId > 0)
            .SelectMany(x => new[] { (long)x.ContractorId! }).Distinct().ToList();
        var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds.Distinct().ToList(), null, null, _mediator, ct);

        List<long>? userIds = values.Where(x => x.CreatorId is not null && x.CreatorId > 0)
            .SelectMany(x => new[] { (long)x.CreatorId! }).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        values!.ForEach(item =>
        {
            item.Company = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
            item.Creator = users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.FromTime = item.FromDate.TimeOfDay.ToString();
            item.ToTime = item.ToDate.TimeOfDay.ToString();
            if (item.FixAssetMachineryType == FixAssetMachineryType.rented)
                item.Contractor = thirdPartyInfos?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            else
                item.Contractor = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
        });

        var file = new FileContentResult(FixAssetMachineryExcels.FixAssetMachineryNotworksToExcel(values, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"FixAssetMachinerys-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };
        return new GetsFixAssetMachineryNotWorkExcelExporterResponse(file);
    }

    public async Task<Result<GetsFixAssetMachineryNotWorkExcelEnumResponse?>> GetsFixAssetMachineryNotworkExcelEnum(
        GetsFixAssetMachineryNotWorkExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsFixAssetMachineryNotWorkExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<FixAssetMachineryNotworkExcelEnum>());
        return new GetsFixAssetMachineryNotWorkExcelEnumResponse(response);
    }

}

