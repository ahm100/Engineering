using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractServiceReport;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic : IContractorContractLogic
{
    private async Task<Result<long>> ResolveCompanyId(CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);

        if (companyId is null ||
            await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
        {
            return Result.Failure<long>(GlobalErrors.InvalidCompany);
        }

        return companyId.Value;
    }

    private async Task<GetContractorContractHeaderByIdResponse> PullAllData(
        GetContractorContractHeaderByIdResponse value, CT ct)
    {
        if (value == null || !value.Contracts.HasAny()) return value!;

        // جمع آوری Id ها
        var thirdPartyIds = new List<long>();
        thirdPartyIds.AddRange([value.ContractorId]);
        thirdPartyIds.AddRange(value.Contracts.SelectMany(c => c.CostOvers.Select(x => x.ContractorId)) ?? Enumerable.Empty<long>());
        thirdPartyIds.AddRange(value.Contracts.SelectMany(c => c.Details.SelectMany(d => d.CostOvers.Select(x => x.ContractorId))) ?? Enumerable.Empty<long>());

        var measureUnitIds = new List<long>();
        measureUnitIds.AddRange(value.Contracts.SelectMany(c => c.Details.SelectMany(d => d.Services.Select(s => s.ServiceInfoUnitOfMeasurementId))) ?? Enumerable.Empty<long>());
        measureUnitIds.AddRange(value.Contracts.SelectMany(c => c.Details.SelectMany(d => d.Services.Select(s => s.ProjectOperationUnitOfMeasurementId))) ?? Enumerable.Empty<long>());

        // دریافت داده ها از سرویس ها
        var thirdPartyTask = thirdPartyIds.HasAny() ? WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct)! : Task.FromResult<List<UserModel>>(new());
        var measureUnitTask = measureUnitIds.HasAny() ? WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct)! : Task.FromResult<List<Measureunit>>(new());
        var currencyTask = _mediator.Send(new GetCurrencyByIdQuery(value.CurrencyId), ct);

        await Task.WhenAll(thirdPartyTask, measureUnitTask, currencyTask);

        var thirdParties = thirdPartyTask.Result ?? new List<UserModel>();
        var measureUnits = measureUnitTask.Result ?? new List<Measureunit>();
        var currencyRes = currencyTask.Result.Value!;

        // ایجاد دیکشنری‌ها برای lookup سریع
        var thirdPartyDict = thirdParties.ToDictionary(x => x.Id, x => x.FullName);
        var measureUnitDict = measureUnits.ToDictionary(x => x.Id, x => x.Name);
        var currencyName = currencyRes.Name;

        // ست کردن مقادیر
        if (value.ContractorId != 0 && thirdPartyDict.TryGetValue(value.ContractorId, out var contractorName))
            value.Contractor = contractorName;

        foreach (var contract in value.Contracts)
        {
            foreach (var detail in contract.Details)
            {
                foreach (var costOver in detail.CostOvers)
                    if (costOver.ContractorId != 0 && thirdPartyDict.TryGetValue(costOver.ContractorId, out var dCoName))
                        costOver.ContractorName = dCoName;

                foreach (var service in detail.Services)
                {
                    if (service.ServiceInfoUnitOfMeasurementId != 0 && measureUnitDict.TryGetValue(service.ServiceInfoUnitOfMeasurementId, out var sUnit))
                        service.ServiceInfoUnitOfMeasurement = sUnit;

                    if (service.ProjectOperationUnitOfMeasurementId != 0 && measureUnitDict.TryGetValue(service.ProjectOperationUnitOfMeasurementId, out var pUnit))
                        service.ProjectOperationUnitOfMeasurement = pUnit;
                }

                foreach (var price in detail.Prices)
                    if (!string.IsNullOrEmpty(currencyName))
                        price.Currency = currencyName;
            }

            foreach (var costOver in contract.CostOvers)
                if (costOver.ContractorId != 0 && thirdPartyDict.TryGetValue(costOver.ContractorId, out var coName))
                    costOver.ContractorName = coName;

            if (!string.IsNullOrEmpty(currencyName))
                value.Currency = currencyName;
        }

        return value;
    }

    private async Task<GetCCHByIdResponse> PullCCHByIdData(
        GetCCHByIdResponse value, CT ct)
    {
        var contractor = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([value.ContractorId], null, null, _mediator, ct);
        if (value.ContractorId != 0 && contractor is not null)
            value.Contractor = contractor.FirstOrDefault()?.FullName;

        var currency = await _mediator.Send(new GetCurrencyByIdQuery(value.CurrencyId), ct);
        value.Currency = currency.Value?.Name;

        return value;
    }

    private async Task<GetCCByHeaderIdResponse> PullCCByHeaderIdAllData(
    GetCCByHeaderIdResponse value, CT ct)
    {
        if (value == null || !value.Data.HasAny()) return value!;

        // جمع آوری Id ها

        var thirdPartyIds = new List<long>();
        thirdPartyIds.AddRange([value.Data.FirstOrDefault()!.ContractorId]);
        thirdPartyIds.AddRange(value.Data.SelectMany(c => c.CostOvers.Select(x => x.ContractorId)) ?? Enumerable.Empty<long>());
        thirdPartyIds.AddRange(value.Data.SelectMany(c => c.Details.SelectMany(d => d.CostOvers.Select(x => x.ContractorId))) ?? Enumerable.Empty<long>());

        var measureUnitIds = new List<long>();
        measureUnitIds.AddRange(value.Data.SelectMany(c => c.Details.SelectMany(d => d.Services.Select(s => s.ServiceInfoUnitOfMeasurementId))) ?? Enumerable.Empty<long>());
        measureUnitIds.AddRange(value.Data.SelectMany(c => c.Details.SelectMany(d => d.Services.Select(s => s.ProjectOperationUnitOfMeasurementId))) ?? Enumerable.Empty<long>());

        // دریافت داده ها از سرویس ها
        var thirdPartyTask = thirdPartyIds.HasAny() ? WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct)! : Task.FromResult<List<UserModel>>(new());
        var measureUnitTask = measureUnitIds.HasAny() ? WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct)! : Task.FromResult<List<Measureunit>>(new());
        var currencyTask = _mediator.Send(new GetCurrencyByIdQuery(value.Data.FirstOrDefault()!.CurrencyId), ct);

        await Task.WhenAll(thirdPartyTask, measureUnitTask, currencyTask);

        var thirdParties = thirdPartyTask.Result ?? new List<UserModel>();
        var measureUnits = measureUnitTask.Result ?? new List<Measureunit>();
        var currencyRes = currencyTask.Result.Value!;

        // ایجاد دیکشنری‌ها برای lookup سریع
        var thirdPartyDict = thirdParties.ToDictionary(x => x.Id, x => x.FullName);
        var measureUnitDict = measureUnits.ToDictionary(x => x.Id, x => x.Name);
        var currencyName = currencyRes.Name;

        foreach (var contract in value.Data)
        {
            foreach (var detail in contract.Details)
            {
                foreach (var costOver in detail.CostOvers)
                    if (costOver.ContractorId != 0 && thirdPartyDict.TryGetValue(costOver.ContractorId, out var dCoName))
                        costOver.ContractorName = dCoName;

                foreach (var service in detail.Services)
                {
                    if (service.ServiceInfoUnitOfMeasurementId != 0 && measureUnitDict.TryGetValue(service.ServiceInfoUnitOfMeasurementId, out var sUnit))
                        service.ServiceInfoUnitOfMeasurement = sUnit;

                    if (service.ProjectOperationUnitOfMeasurementId != 0 && measureUnitDict.TryGetValue(service.ProjectOperationUnitOfMeasurementId, out var pUnit))
                        service.ProjectOperationUnitOfMeasurement = pUnit;
                }

                foreach (var price in detail.Prices)
                    if (!string.IsNullOrEmpty(currencyName))
                        price.Currency = currencyName;
            }

            foreach (var costOver in contract.CostOvers)
                if (costOver.ContractorId != 0 && thirdPartyDict.TryGetValue(costOver.ContractorId, out var coName))
                    costOver.ContractorName = coName;
        }

        return value;
    }

    private List<long> GetContractorIds(List<Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService> data)
    {
        var ids = new List<long>();
        ids = data.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => x.ContractorId!.Value).Distinct().ToList();
        return ids;
    }

    private async Task<List<Currency>?> CurrencyDataReceiver(List<ContractorContractHeader>? headers, CT ct)
    {
        var dataResult = new List<Currency>();
        if (headers is not null && headers.Count > 0)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var allIds = headers.Where(x => x.CurrencyId != null && x.CurrencyId != 0).Select(x => (long)x.CurrencyId!).ToList();
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var contractors = headers.SelectMany(x => x.ContractorContracts).ToList();

            foreach (var item in contractors)
            {
                var priceCurrencies = item.Details?.SelectMany(oo => oo.ContractorContractDetailPrices).Select(oo => oo.CurrencyId).ToList();
                if (priceCurrencies is not null && priceCurrencies.Count > 0)
                    allIds.AddRange(priceCurrencies);
            }
            var newCurencyIds = allIds.Distinct().ToList();
            if (allIds is not null && allIds.Count > 0)
            {
                var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, newCurencyIds.Count, newCurencyIds, true), ct);
                dataResult = currenciesData.Value?.Data;
            }
        }
        return dataResult;
    }

    private async Task<List<FilteredUserResponseModel>?> UserDataReceiver(List<long>? ids, CT ct)
    {
        List<FilteredUserResponseModel>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var usersData = await _mediator.Send(new GetFilteredUsersQuery(ids, null, null, null, null, 1, ids.Count), ct);
            result = usersData.Value?.Data;
        }
        return result;
    }

    private List<Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService> GetContractorServices(
        List<Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService>? contractorServices,
        Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService contractorService)
    {
        List<Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService>? services = [];
        if (contractorServices is not null && contractorServices.Count > 0)
            services = contractorServices.Where(x => x.OperationInfoService.ServiceInfo.Id == contractorService.OperationInfoService.ServiceInfo.Id &&
            x.ContractorId == contractorService.ContractorId && x.ProjectOperationDetail.ProjectOperation.Project.Id == contractorService.ProjectOperationDetail.ProjectOperation.Project.Id &&
            x.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.Id == contractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.Id
            ).ToList();

        return services;
    }

    private async Task<Result<GetsContractorContractServiceReportResponse?>> ProcessServiceReportValuesAsync(
        List<long>? Ids,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? ProjectOperationIds,
        List<long>? ProjectOperationDetailIds,
        List<long>? ContractorIds,
        List<long>? ServiceInfoIds,
        List<long>? MeasurUnitIds,
        long? ContractTypeId,
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime? FromDate,
        DateTime? ToDate,
        DateTime? FromCreated,
        DateTime? ToCreated,
        ProjectOperationDetailStatus? Status,
        ContractorContractStatus? ContractStatus,
        string? FilterData,
        string? FilterDescription,
        string? FilterServiceInfo,
        string[]? OrderBy,
        int PageIndex,
        int PageSize,
        IMediator _mediator,
        CT ct = default)
    {
        var responses = await _mediator.Send(new GetsContractorContractServiceReportQuery(
            Ids,
            CostCenterIds,
            ProjectIds,
            ProjectOperationIds,
            ProjectOperationDetailIds,
            ContractorIds,
            ServiceInfoIds,
            MeasurUnitIds,
            ContractTypeId,
            StartDate,
            EndDate,
            FromDate,
            ToDate,
            FromCreated,
            ToCreated,
            Status,
            ContractStatus,
            FilterData,
            FilterDescription,
            FilterServiceInfo,
            OrderBy,
            PageIndex,
            PageSize),
            ct);
        if (responses.IsFailure)
            return Result.Failure<GetsContractorContractServiceReportResponse>(responses.Error!);
        var values = responses.Value?.Data;

        var measurementIds = values.Where(x => x.ServiceInfoMeasureId.HasValue && x.UnitOfMeasurementId.HasValue)
            .SelectMany(x => new[] { x.ServiceInfoMeasureId!.Value, x.UnitOfMeasurementId!.Value })
            .Distinct()
            .ToList();
        var measureunits = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var thirdpartyIds = values.Where(x => x.ContractorId.HasValue && x.ContractorId > 0)
            .Select(x => x.ContractorId!.Value)
            .Distinct()
            .ToList();
        var thirdparties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdpartyIds, null, null, _mediator, ct);

        var userIds = values.Where(x => x.CreatorId.HasValue && x.CreatorId > 0)
            .Select(x => x.CreatorId!.Value)
            .Distinct()
            .ToList();
        var users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        var currencyIds = values.Where(x => x.CurrencyId.HasValue && x.CurrencyId > 0)
            .Select(x => x.CurrencyId!.Value)
            .Distinct()
            .ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        values.ForEach(item =>
        {
            item.MeasurementName = measureunits?.FirstOrDefault(x => x.Id.Equals(item.UnitOfMeasurementId))?.Name;
            item.ServiceInfoMeasure = measureunits?.FirstOrDefault(x => x.Id.Equals(item.ServiceInfoMeasureId))?.Name;

            item.CreatorName = users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.CreatorNickname = users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.Nickname;

            item.Contractor = thirdparties?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.FullName;
            item.ContractorNickName = thirdparties?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId)?.Nickname;

            item.Currency = currencies?.FirstOrDefault(x => x is not null && x.Id == item.CurrencyId)?.Name;

            item.TotalPrice = (item.Price ?? 0) * (item.Volume ?? 0);
        });

        var totals = new GetsContractorContractServiceReportTotalModel()
        {
            Price = values.Where(x => x.Price.HasValue).Sum(x => x.Price!.Value),
            TotalPrice = values.Where(x => x.TotalPrice.HasValue).Sum(x => x.TotalPrice!.Value),
            TotalServiceVolume = values.Where(x => x.Volume.HasValue).Sum(x => x.Volume!.Value),
        };

        return new GetsContractorContractServiceReportResponse(
            totals ?? new GetsContractorContractServiceReportTotalModel(),
            values ?? new List<GetsContractorContractServiceReportModel>(0),
            responses.Value?.RowCount ?? 0);
    }


}
