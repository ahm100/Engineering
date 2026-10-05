using Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryRateById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsRateByFixAssetMachineryId;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryRateById;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetsRateByFixAssetMachineryId;

namespace Engineering.Application.Services.FixAssetMachineries;

public partial class FixAssetMachineryLogic : IFixAssetMachineryLogic
{
    //Rate
    public async Task<Result<CreateFixAssetMachineryRateResponse?>> CreateFixAssetMachineryRate(
        CreateFixAssetMachineryRateRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateFixAssetMachineryRate");

        var isValidRequest = await request.IsValidAsync<CreateFixAssetMachineryRateValidator, CreateFixAssetMachineryRateRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFixAssetMachineryRateResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateFixAssetMachineryRateResponse>(companyResponse.Error!);
        }

        var fixAssetmachineryQuery = await _mediator.Send(new GetFixAssetMachineryByIdQuery(request.FixAssetMachineryId), ct);
        if (fixAssetmachineryQuery.IsFailure)
            return Result.Failure<CreateFixAssetMachineryRateResponse>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
        var fixAssetmachinery = fixAssetmachineryQuery.Value;

        List<CreateRateDataCommand>? createRateDataCommands = [];
        if (request.Rates is not null && request.Rates.Count > 0)
        {
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
                    return Result.Failure<CreateFixAssetMachineryRateResponse>(FixAssetMachineryErrors.InvalidRatesDuplicate);
            }
        }
        else
            return Result.Failure<CreateFixAssetMachineryRateResponse>(FixAssetMachineryErrors.InvalidRatesRequest);

        var response = await _mediator.Send(new CreateFixAssetMachineryRateCommand(fixAssetmachinery!, createRateDataCommands), ct);
        if (response.IsFailure)
            return Result.Failure<CreateFixAssetMachineryRateResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateFixAssetMachineryRateResponse>();
    }

    public async Task<Result<DisableFixAssetMachineryRateResponse?>> DisableFixAssetMachineryRate(
        DisableFixAssetMachineryRateRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableFixAssetMachineryRate, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableFixAssetMachineryRateValidator, DisableFixAssetMachineryRateRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableFixAssetMachineryRateResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableFixAssetMachineryRateCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableFixAssetMachineryRateResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableFixAssetMachineryRateResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateFixAssetMachineryRateResponse?>> UpdateFixAssetMachineryRate(
        UpdateFixAssetMachineryRateRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateFixAssetMachineryRate, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateFixAssetMachineryRateValidator, UpdateFixAssetMachineryRateRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryRateResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateFixAssetMachineryRateResponse>(companyResponse.Error!);
        }

        var fixAssetQuery = await _mediator.Send(new GetFixAssetMachineryRateByIdQuery(request.Id), ct);
        if (fixAssetQuery.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryRateResponse>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);

        DateTime? startDate = null;
        if (request.StartTime != null)
            startDate = request.StartDate.Date.Add(request.StartTime.Value);

        DateTime? endDate = null;
        if (request.EndTime != null)
            endDate = request.EndDate.Date.Add(request.EndTime.Value);

        var response = await _mediator.Send(new UpdateFixAssetMachineryRateCommand(
            request.Id,
            startDate != null ? startDate.Value : request.StartDate.Date,
            endDate != null ? endDate.Value : request.EndDate.Date,
            request.HourlyRate,
            request.DailyRate,
            request.ServiceRate,
            request.VolumeRate), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateFixAssetMachineryRateResponse>(response.Error!);
        var Rate = response.Value;

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateFixAssetMachineryRateResponse>();
    }

    public async Task<Result<GetFixAssetMachineryRateByIdResponse?>> GetFixAssetMachineryRateById(
        GetFixAssetMachineryRateByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFixAssetMachineryRateById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetFixAssetMachineryRateByIdValidator, GetFixAssetMachineryRateByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFixAssetMachineryRateByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFixAssetMachineryRateByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetFixAssetMachineryRateByIdResponse>(response.Error!);
        var value = response.Value!;

        var data = new GetFixAssetMachineryRateByIdResponse()
        {
            Id = value!.Id,
            EndDate = value.EndDate,
            EndTime = value.EndDate.TimeOfDay,
            MachineryCode = value.FixAssetMachinery.Machinery.MachineryCode,
            MachineryId = value.FixAssetMachinery.Machinery.Id,
            MachineryName = value.FixAssetMachinery.Machinery.MachineryName,
            StartDate = value.StartDate,
            StartTime = value.StartDate.TimeOfDay,
            DailyRate = value.DailyRate,
            HourlyRate = value.HourlyRate,
            ServiceRate = value.ServiceRate,
            VolumeRate = value.VolumeRate,
        };

        return data;
    }

    public async Task<Result<GetsRateByFixAssetMachineryIdResponse?>> GetsRateByFixAssetMachineryId(
        GetsRateByFixAssetMachineryIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsRateByFixAssetMachineryId, id:{Id}", request.FixAssetMachineryId);

        var isValidRequest = await request.IsValidAsync<GetsRateByFixAssetMachineryIdValidator, GetsRateByFixAssetMachineryIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsRateByFixAssetMachineryIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsRateByFixAssetMachineryIdQuery(
            request.FixAssetMachineryId,
            request.StartDate,
            request.EndDate,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsRateByFixAssetMachineryIdResponse>(response.Error!);
        var values = response.Value!.Data!;

        List<GetsRateByFixAssetMachineryIdModel>? data = [];
        if (values is not null && values.Count > 0)
            foreach (var value in values)
            {
                data.Add(new GetsRateByFixAssetMachineryIdModel()
                {
                    Id = value!.Id,
                    EndDate = value.EndDate,
                    EndTime = value.EndDate.TimeOfDay,
                    MachineryCode = value.FixAssetMachinery.Machinery.MachineryCode,
                    MachineryId = value.FixAssetMachinery.Machinery.Id,
                    MachineryName = value.FixAssetMachinery.Machinery.MachineryName,
                    StartDate = value.StartDate,
                    StartTime = value.StartDate.TimeOfDay,
                    DailyRate = value.DailyRate,
                    HourlyRate = value.HourlyRate,
                    ServiceRate = value.ServiceRate,
                    VolumeRate = value.VolumeRate,
                });
            }

        return new GetsRateByFixAssetMachineryIdResponse(data ?? new List<GetsRateByFixAssetMachineryIdModel>(0), response.Value?.RowCount ?? 0);
    }
}

