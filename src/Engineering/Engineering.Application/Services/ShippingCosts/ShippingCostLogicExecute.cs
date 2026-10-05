using Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;
using Engineering.Application.Services.ShippingCosts.Contracts.CreateShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.DeleteShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;
using Engineering.Application.Services.ShippingCosts.Contracts.UpdateShippingCost;
using Engineering.Application.Services.ShippingCosts.Models.ShippingCostExcelImports;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetsThirdPartyById;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Gita.Backend.Shared.Domain.Exceptions;
using IdentityServer.ClientSdk.Models.ThirdParty;
using System.Globalization;

namespace Engineering.Application.Services.ShippingCosts;

public partial class ShippingCostLogic : IShippingCostLogic
{
    public async Task<Result<bool?>> CreateShippingCostExecute(
        CreateShippingCostRequest request, CT ct)
    {
        try
        {
            var transportationContractor = await _transportationContractorRepository.FindById(request.TransportationContractorId, ct);
            if (transportationContractor is null) return Result.Failure<bool?>(TransportationContractorErrors.TransportationContractorNotFound);

            if (request.Shippingcosts.Count > 0)
                foreach (var req in request.Shippingcosts)
                {
                    var machineType = await _machineTypeRepository.FindById(req.MachineTypeId, ct);
                    if (machineType is null) return Result.Failure<bool?>(MachineErrors.MachineWithIdNotFound);

                    if (req.ThirdPartyCompanyId == null && req.SourceCityId == null && req.DestinationCityId == null)
                        return Result.Failure<bool?>(ShippingCostErrors.CityIdIsEmpty);

                    List<long?>? cityIds = [req.SourceCityId, req.DestinationCityId];
                    if (cityIds is not null && cityIds.Count > 0 && cityIds.Any(x => x is not null && x > 0))
                    {
                        var cities = await _cityRepository.GetByIds(cityIds.Adapt<List<long>>().Distinct().ToList(), ct);
                        if (cities is null || cities.Distinct().Count() != cityIds.Distinct().Count())
                            return Result.Failure<bool?>(ShippingCostErrors.CityIdIsEmpty);
                    }

                    if (req.RegionId != null && req.RegionId > 0)
                    {
                        var region = await _regionRepository.FindById(req.RegionId!.Value, ct);
                        if (region is null) return Result.Failure<bool?>(ShippingCostErrors.RegionIdIsEmpty);
                    }

                    if (req.ThirdPartyId != null && req.ThirdPartyId > 0)
                    {
                        var region = await _thirdPartyRepository.FindById(req.ThirdPartyId!.Value, ct);
                        if (region is null) return Result.Failure<bool?>(ShippingCostErrors.ThirdPartyIdIsEmpty);
                    }

                    if (req.ThirdPartyCompanyId != null && req.ThirdPartyCompanyId > 0)
                    {
                        var company = await _companyClient.GetCompanyById(req.ThirdPartyCompanyId!.Value, ct);
                        if (company is null) return Result.Failure<bool?>(ShippingCostErrors.ThirdPartyCompanyIsEmpty);
                    }

                    var entity = new ShippingCost(transportationContractor, machineType, req.SourceCityId, req.DestinationCityId,
                        req.RegionId, req.Count, req.LoadWeight, req.Price, req.Tax, req.Description, req.FromDate, req.ToDate,
                        req.ThirdPartyId, req.ThirdPartyCompanyId, req.Latitude, req.Longitude, req.LegacyId);

                    var result = await _repository.Create(entity, ct);

                    entity.AddHistory();
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ShippingCostExcelImportsResponse?>> ShippingCostExcelImportsExecute(
        ShippingCostExcelImportsRequest request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<ShippingCostExcelImportsResponse>(GlobalErrors.InvalidCompany);

            var transportationContractor = await _transportationContractorRepository.FindById(request.TransportationContractorId, ct);
            if (transportationContractor is null) return Result.Failure<ShippingCostExcelImportsResponse?>
                    (TransportationContractorErrors.TransportationContractorNotFound);

            var shippingCosts = ExcelImporter.Import<ShippingCostExcelImportsModel>(request.DocumentFile);
            if (shippingCosts is null)
                return Result.Failure<ShippingCostExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);

            (bool flowControl, Result<ShippingCostExcelImportsResponse?>? value) = ValidateShippings(shippingCosts);
            if (!flowControl)
                return value?.Value;

            var importsData = await GetImportsData(shippingCosts, companyId!.Value, ct);
            if (importsData.IsFailure) return Result.Failure<ShippingCostExcelImportsResponse>(importsData.Error!);

            foreach (var item in shippingCosts)
            {
                (bool flows, Result<(decimal, decimal?, decimal?, int?)> numerics) = GetNumbericData(item);
                if (!flows)
                    return value?.Value;

                DateTime? fromDate = null;
                if (!string.IsNullOrEmpty(item.FromDate))
                    fromDate = GetMiladiDate(item.FromDate);

                DateTime? toDate = null;
                if (!string.IsNullOrEmpty(item.ToDate))
                    toDate = GetMiladiDate(item.ToDate);

                var thirdParty = importsData.Value?.ThirdParties?.FirstOrDefault(x => x.OrganizationCode == item.ThirdPartyCode);
                var source = importsData.Value?.SourceCities?.FirstOrDefault(x => x.Code == item.SourceCityCode);
                var destination = importsData.Value?.DestinationCities?.FirstOrDefault(x => x.Code == item.DestinationCityCode);
                var region = importsData.Value?.Regions?.FirstOrDefault(x => x.Code == item.RegionCode);
                var machineType = importsData.Value?.MachineTypes?.FirstOrDefault(x => x.MachineTypeCode == item.MachineTypeCode);
                var company = importsData.Value?.ThirdPartiesCompany?.FirstOrDefault(x => x.Code == item.ThirdPartyCompanyCode);

                var entity = new ShippingCost(transportationContractor, machineType!, source?.Id, destination?.Id,
                    region?.Id, numerics.Value.Item4, numerics.Value.Item3, numerics.Value.Item1, numerics.Value.Item2,
                    item.Description, fromDate, toDate, thirdParty?.Id, company?.Id, null, null, null);

                var result = await _repository.Create(entity, ct);
            }

            return new ShippingCostExcelImportsResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ShippingCostExcelImportsResponse?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ShippingCost?>> DeleteShippingCostExecute(
        DeleteShippingCostRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities is null && entities?.Count != request.Ids.Count)
                return Result.Failure<ShippingCost>(ShippingCostErrors.ShippingCostNotFound);

            foreach (var entity in entities)
            {
                entity.SoftDelete();
                await _repository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ShippingCost>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ShippingCost?>> UpdateShippingCostExecute(
        UpdateShippingCostRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetShippingCost(request.Id, ct);
            if (entity is null) return Result.Failure<ShippingCost>(ShippingCostErrors.ShippingCostNotFound);

            var transportationContractor = await _transportationContractorRepository.FindById(request.TransportationContractorId, ct);
            if (transportationContractor is null) return Result.Failure<ShippingCost>(TransportationContractorErrors.TransportationContractorNotFound);

            var machineType = await _machineTypeRepository.FindById(request.MachineTypeId, ct);
            if (machineType is null) return Result.Failure<ShippingCost>(MachineErrors.MachineWithIdNotFound);

            if (request.ThirdPartyCompanyId == null && request.SourceCityId == null && request.DestinationCityId == null)
                return Result.Failure<ShippingCost?>(ShippingCostErrors.CityIdIsEmpty);

            List<long?>? cityIds = [request.SourceCityId, request.DestinationCityId];
            if (cityIds is not null && cityIds.Count > 0 && cityIds.Any(x => x is not null && x > 0))
            {
                var cities = await _cityRepository.GetByIds(
                    cityIds.Where(x => x is not null && x > 0).Adapt<List<long>>().Distinct().ToList(), ct);
                if (cities is null || cities.Distinct().Count() != cityIds.Distinct().Count())
                    return Result.Failure<ShippingCost?>(ShippingCostErrors.CityIdIsEmpty);
            }

            if (request.RegionId != null && request.RegionId > 0)
            {
                var region = await _regionRepository.FindById(request.RegionId!.Value, ct);
                if (region is null) return Result.Failure<ShippingCost>(ShippingCostErrors.RegionIdIsEmpty);
            }

            if (request.ThirdPartyId != null && request.ThirdPartyId > 0)
            {
                var region = await _thirdPartyRepository.FindById(request.ThirdPartyId!.Value, ct);
                if (region is null) return Result.Failure<ShippingCost?>(ShippingCostErrors.ThirdPartyIdIsEmpty);
            }

            if (request.ThirdPartyCompanyId != null && request.ThirdPartyCompanyId > 0)
            {
                var company = await _companyClient.GetCompanyById(request.ThirdPartyCompanyId!.Value, ct);
                if (company is null) return Result.Failure<ShippingCost?>(ShippingCostErrors.ThirdPartyCompanyIsEmpty);
            }

            entity.Update(transportationContractor, machineType, request.SourceCityId,
                request.DestinationCityId, request.RegionId, request.Count, request.LoadWeight,
                request.Price, request.Tax, request.Description, request.FromDate, request.ToDate,
                request.ThirdPartyId, request.ThirdPartyCompanyId, request.Latitude, request.Longitude,
                request.IsActive, request.LegacyId);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ShippingCost>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ShippingCost?>> ChangeShippingCostStateExecute(
        ChangeShippingCostStateRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities.Count < request.Ids.Count)
                return Result.Failure<ShippingCost>(ShippingCostErrors.ShippingCostNotFound);

            foreach (var entity in entities)
            {
                if (request.IsActive)
                {
                    if (entity.IsActive) return Result.Failure<ShippingCost>(GlobalErrors.IsActive);
                    entity.SetActive();
                }
                else
                {
                    if (!entity.IsActive) return Result.Failure<ShippingCost>(GlobalErrors.InActive);
                    entity.SetDeactivate();
                }
                await _repository.Update(entity);
                entity.AddHistory();
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ShippingCost>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetShippingCostByIdResponse?>> GetShippingCostByIdExecute(
        GetShippingCostByIdRequest request, CT ct)
    {
        try
        {
            var item = await _repository.GetShippingCostById(request.Id, ct);
            if (item is null) return Result.Failure<GetShippingCostByIdResponse?>(ShippingCostErrors.ShippingCostNotFound);

            if (item.ThirdPartyCompanyId != null && item.ThirdPartyCompanyId > 0)
            {
                var company = await _companyClient.GetCompanyById(item.ThirdPartyCompanyId!.Value, ct);
                item.ThirdPartyCompanyCode = company.Code;
                item.ThirdPartyCompanyNameFa = company.NameFa;
                item.ThirdPartyCompanyNameEn = company.NameEn;
            }

            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetShippingCostByIdResponse?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsFilteredShippingCostResponseModel>>?>> GetsFilteredShippingCostExecute(
        GetsFilteredShippingCostRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetFilteredShippingCosts(
                request.Ids,
                request.ContractorIds,
                request.MachineTypeIds,
                request.ThirdpartyIds,
                request.FromDate,
                request.ToDate,
                request.FilterData,
                request.IsActive,
                request.PageIndex,
                request.PageSize,
                ct);

            if (items.Data != null && items.Data.Count > 0)
            {
                var companyIds = items.Data.NullListed(c => c.ThirdPartyCompanyId);
                if (companyIds != null && companyIds.Count > 0)
                {
                    var companies = await _companyClient.GetCompanyByIds(new GetCompanyByIdsRequestDto()
                    {
                        Ids = companyIds,
                        IgnoreQuery = true,
                        PageIndex = 1,
                        PageSize = companyIds.Count

                    }, ct);

                    if (companies.Data != null && companies.Data.Count > 0)
                        items.Data.ForEach(x =>
                        {
                            if (x.ThirdPartyCompanyId != null && x.ThirdPartyCompanyId > 0)
                            {
                                var company = companies.Data.FirstOrDefault(z => z.Id == x.ThirdPartyCompanyId!.Value);
                                x.ThirdPartyCompanyCode = company?.Code;
                                x.ThirdPartyCompanyNameFa = company?.NameFa;
                                x.ThirdPartyCompanyNameEn = company?.NameEn;
                            }
                        });
                }
            }

            var response = items.Data.Any() ?
                new DataResult<List<GetsFilteredShippingCostResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredShippingCostResponseModel>>>(ShippingCostErrors.FilteredShippingCostNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredShippingCostResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetShippingCostHistoryResponseModel>>?>> GetShippingCostHistoryExecute(
        GetShippingCostHistoryRequest request, CT ct)
    {
        try
        {
            var items = await _historyRpository.GetShippingCostHistory(
                request.Id,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetShippingCostHistoryResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetShippingCostHistoryResponseModel>>>(
                    ShippingCostErrors.HistoryNotfound);

            if (response.Value?.Data != null && response.Value.Data.Count > 0)
            {
                var creatorIds = items.Data.NullListed(x => x.CreatorId);
                var creators = await _mediator.Send(new GetFilteredUsersQuery(creatorIds, null, null, null, null, 1, creatorIds.Count));
                var thirdPartiesId = items.Data.NullListed(x => x.ShippingThirdPartyId);
                var thirdParties = await _mediator.Send(new GetsThirdPartyByIdQuery(1, thirdPartiesId.Count, thirdPartiesId, true));

                GetCompanyByIdsResponseDto? companies = null;
                var companyIds = items.Data.NullListed(c => c.ThirdPartyCompanyId);
                if (companyIds != null && companyIds.Count > 0)
                {
                    companies = await _companyClient.GetCompanyByIds(new GetCompanyByIdsRequestDto()
                    {
                        Ids = companyIds,
                        IgnoreQuery = true,
                        PageIndex = 1,
                        PageSize = companyIds.Count

                    }, ct);
                }

                response.Value.Data.ForEach(x =>
                {
                    x.Creator = creators.Value?.Data?.FirstOrDefault(z => z.UserId == x.CreatorId)?.FullName;
                    x.ShippingThirdParty = creators.Value?.Data?.FirstOrDefault(z => z.Id == x.ShippingThirdPartyId)?.FullName;

                    if (companies?.Data != null && companies.Data.Count > 0)
                        if (x.ThirdPartyCompanyId != null && x.ThirdPartyCompanyId > 0)
                        {
                            var company = companies.Data.FirstOrDefault(z => z.Id == x.ThirdPartyCompanyId!.Value);
                            x.ThirdPartyCompanyCode = company?.Code;
                            x.ThirdPartyCompanyNameFa = company?.NameFa;
                            x.ThirdPartyCompanyNameEn = company?.NameEn;
                        }
                });
            }

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetShippingCostHistoryResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsActiveShippingCostResponseModel>>?>> GetsActiveShippingCostExecute(
        GetsActiveShippingCostRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetAllActiveShippingCosts(
                request.Ids,
                request.ContractorIds,
                request.MachineTypeIds,
                request.ThirdpartyIds,
                request.FromDate,
                request.ToDate,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            if (items.Data != null && items.Data.Count > 0)
            {
                var companyIds = items.Data.NullListed(c => c.ThirdPartyCompanyId);
                if (companyIds != null && companyIds.Count > 0)
                {
                    var companies = await _companyClient.GetCompanyByIds(new GetCompanyByIdsRequestDto()
                    {
                        Ids = companyIds,
                        IgnoreQuery = true,
                        PageIndex = 1,
                        PageSize = companyIds.Count

                    }, ct);

                    if (companies.Data != null && companies.Data.Count > 0)
                        items.Data.ForEach(x =>
                        {
                            if (x.ThirdPartyCompanyId != null && x.ThirdPartyCompanyId > 0)
                            {
                                var company = companies.Data.FirstOrDefault(z => z.Id == x.ThirdPartyCompanyId!.Value);
                                x.ThirdPartyCompanyCode = company?.Code;
                                x.ThirdPartyCompanyNameFa = company?.NameFa;
                                x.ThirdPartyCompanyNameEn = company?.NameEn;
                            }
                        });
                }
            }

            var response = items.Data.Any() ?
                new DataResult<List<GetsActiveShippingCostResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsActiveShippingCostResponseModel>>>(ShippingCostErrors.FilteredShippingCostNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveShippingCostResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    #region Privates
    private static (bool flowControl, Result<(decimal price, decimal? tax, decimal? weight, int? count)> value) GetNumbericData(
        ShippingCostExcelImportsModel item)
    {
        int? count = null;
        if (!string.IsNullOrEmpty(item.Count))
        {
            var countResult = DataExtensions.ValidateInteger(item.Count, "Count");
            if (!countResult.Success)
                return (flowControl: false, value: Result.Failure<(decimal, decimal?, decimal?, int?)>(ShippingCostErrors.CountIsNull));
            count = countResult.Value;
        }

        decimal price = 0m;
        if (!string.IsNullOrEmpty(item.Price))
        {
            var priceResult = DataExtensions.ValidateDecimal(item.Price, "Price");
            if (!priceResult.Success)
                return (flowControl: false, value: Result.Failure<(decimal, decimal?, decimal?, int?)>(ShippingCostErrors.PriceIsNull));
            price = priceResult.Value;
        }

        decimal? tax = null;
        if (!string.IsNullOrEmpty(item.Tax))
        {
            var taxResult = DataExtensions.ValidateDecimal(item.Tax, "Tax");
            if (!taxResult.Success)
                return (flowControl: false, value: Result.Failure<(decimal, decimal?, decimal?, int?)>(ShippingCostErrors.TaxIsNull));
            tax = taxResult.Value;
        }

        decimal? loadWeight = null;
        if (!string.IsNullOrEmpty(item.LoadWeight))
        {
            var loadWeightResult = DataExtensions.ValidateDecimal(item.LoadWeight, "LoadWeight");
            if (!loadWeightResult.Success)
                return (flowControl: false, value: Result.Failure<(decimal, decimal?, decimal?, int?)>(ShippingCostErrors.LoadWeightIsNull));
            loadWeight = loadWeightResult.Value;
        }

        return (flowControl: true, value: (price, tax, loadWeight, count));
    }

    private DateTime GetMiladiDate(string date)
    {
        string persianDate = date;

        var parts = persianDate.Split('-');
        if (parts.Length != 3)
            throw new GitaValidationException("فرمت تاریخ باید yyyy-MM-dd باشد");

        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        PersianCalendar pc = new PersianCalendar();
        DateTime miladiDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);

        return miladiDate;
    }

    private static (bool flowControl, Result<ShippingCostExcelImportsResponse?>? value) ValidateShippings(
        List<ShippingCostExcelImportsModel> shippingCosts)
    {
        if (shippingCosts.Any(x => string.IsNullOrEmpty(x.MachineTypeCode)))
            return (flowControl: false, value: Result.Failure<ShippingCostExcelImportsResponse>(MachineErrors.CodeIsEmpty));
        if (shippingCosts.Any(x => string.IsNullOrEmpty(x.ThirdPartyCode) && string.IsNullOrEmpty(x.SourceCityCode)))
            return (flowControl: false, value: Result.Failure<ShippingCostExcelImportsResponse>(ShippingCostErrors.CityIdIsEmpty));
        if (shippingCosts.Any(x => string.IsNullOrEmpty(x.ThirdPartyCode) && string.IsNullOrEmpty(x.DestinationCityCode)))
            return (flowControl: false, value: Result.Failure<ShippingCostExcelImportsResponse>(ShippingCostErrors.CityIdIsEmpty));
        if (shippingCosts.Any(x => string.IsNullOrEmpty(x.Price)))
            return (flowControl: false, value: Result.Failure<ShippingCostExcelImportsResponse>(ShippingCostErrors.PriceIsEmpty));
        return (flowControl: true, value: null);
    }

    private async Task<Result<GetImportsExcelDataModel?>> GetImportsData(
        List<ShippingCostExcelImportsModel> shippingCosts,
        long companyId,
        CT ct)
    {
        var machineTypesCode = shippingCosts.Where(x => !string.IsNullOrEmpty(x.MachineTypeCode)).Select(s => s.MachineTypeCode!).Distinct().ToList();
        var sourceCitiesCode = shippingCosts.Where(x => !string.IsNullOrEmpty(x.SourceCityCode)).Select(s => s.SourceCityCode!).Distinct().ToList();
        var desCitiesCode = shippingCosts.Where(x => !string.IsNullOrEmpty(x.DestinationCityCode)).Select(s => s.DestinationCityCode!).Distinct().ToList();
        var regionsCode = shippingCosts.Where(x => !string.IsNullOrEmpty(x.RegionCode)).Select(s => s.RegionCode!).Distinct().ToList();
        var thirdPartiesCode = shippingCosts.Where(x => !string.IsNullOrEmpty(x.ThirdPartyCode)).Select(s => s.ThirdPartyCode!).Distinct().ToList();
        var thirdPartiesCompanyCode = shippingCosts.Where(x => !string.IsNullOrEmpty(x.ThirdPartyCompanyCode)).Select(s => s.ThirdPartyCompanyCode!).Distinct().ToList();

        List<MachineType>? machineTypes = [];
        if (machineTypesCode.Count > 0)
        {
            machineTypes = await _machineTypeRepository.GetsMachineTypeByCodes(machineTypesCode, companyId, ct);
            if (machineTypes is null || machineTypes.Count != machineTypesCode.Count)
                return Result.Failure<GetImportsExcelDataModel>(MachineErrors.MachineWithCodeNotFound);
        }

        List<ViewCity>? sourceCities = [];
        if (sourceCitiesCode.Count > 0)
        {
            sourceCities = await _cityRepository.GetByCodes(sourceCitiesCode, ct);
            if (sourceCities is null || sourceCities.Count != sourceCitiesCode.Count)
                return Result.Failure<GetImportsExcelDataModel>(ShippingCostErrors.SourceNotFound);
        }

        List<ViewCity>? desCities = [];
        if (desCitiesCode.Count > 0)
        {
            desCities = await _cityRepository.GetByCodes(desCitiesCode, ct);
            if (desCities is null || desCities.Count != desCitiesCode.Count)
                return Result.Failure<GetImportsExcelDataModel>(ShippingCostErrors.DestinationNotFound);
        }

        List<ViewRegion>? regions = [];
        if (regionsCode.Count > 0)
        {
            regions = await _regionRepository.GetByCodes(regionsCode, ct);
            if (regions is null || regions.Count != regionsCode.Count)
                return Result.Failure<GetImportsExcelDataModel>(ShippingCostErrors.RegionIdIsEmpty);
        }

        List<ViewThirdParty>? thirdParties = [];
        if (thirdPartiesCode.Count > 0)
        {
            thirdParties = await _thirdPartyRepository.GetByCodes(thirdPartiesCode, ct);
            if (thirdParties is null || thirdParties.Count != thirdPartiesCode.Count)
                return Result.Failure<GetImportsExcelDataModel>(ShippingCostErrors.ThirdPartiesNotFound);
        }

        List<GetCompaniesByCodesResponseModel>? companyData = [];
        if (thirdPartiesCompanyCode.Count > 0)
        {
            var thirdPartiesCompany = await _companyClient.GetCompaniesByCodes(
                new GetCompaniesByCodesRequest()
                {
                    Codes = thirdPartiesCompanyCode
                }, ct);
            if (thirdPartiesCompany is null || thirdPartiesCompany.Data.Count != thirdPartiesCompanyCode.Count)
                return Result.Failure<GetImportsExcelDataModel>(ShippingCostErrors.ThirdPartiesNotFound);
            companyData = thirdPartiesCompany.Data;
        }

        return Result.Success<GetImportsExcelDataModel?>(new GetImportsExcelDataModel
        {
            MachineTypes = machineTypes,
            SourceCities = sourceCities,
            DestinationCities = desCities,
            Regions = regions,
            ThirdParties = thirdParties,
            ThirdPartiesCompany = companyData
        });
    }
    #endregion

}
