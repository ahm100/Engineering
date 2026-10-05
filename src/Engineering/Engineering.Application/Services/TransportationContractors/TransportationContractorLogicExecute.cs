using Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;
using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.DeleteTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;
using Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;
using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdPartyPersonnel;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.RemoveThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Services.TransportationContractors;

public partial class TransportationContractorLogic : ITransportationContractorLogic
{
    public async Task<Result<TransportationContractor?>> CreateTransportationContractorExecute(
        CreateTransportationContractorRequest request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<TransportationContractor>(GlobalErrors.InvalidCompany);

            TransportationContractor? response = null;
            if (request.Type == ClientSdk.Enums.TransportationContractorCalculateType.Price)
            {
                var contractor = new TransportationContractor(request.Title,
                    request.DeliveryMethod != null ? request.DeliveryMethod?.ToArray() : null,
                    request.DeliveryType != null ? request.DeliveryType?.ToArray() : null, request.Type,
                    request.FirstPrefix, request.SecondPrefix, companyId!.Value, request.StartOfContract,
                    request.EndOfContract, request.LagacyId);
                response = await _repository.Create(contractor, ct);
            }
            else
            {
                var createThirdParty = await _mediator.Send(new CreateThirdPartyCommand(
                   request, companyId!.Value), ct);
                if (createThirdParty.IsFailure || createThirdParty.Value is null)
                    return Result.Failure<TransportationContractor>(createThirdParty.Error!);
                var thirdPartyId = createThirdParty.Value.Id;

                var contractor = new TransportationContractor(request.Title, thirdPartyId!.Value, request.StartOfContract,
                    request.EndOfContract, request.DeliveryMethod != null ? request.DeliveryMethod?.ToArray() : null,
                    request.DeliveryType != null ? request.DeliveryType?.ToArray() : null, request.Type,
                    request.PercentageValue, request.FixedNumber, request.FirstPrefix, request.SecondPrefix,
                    request.PercentageValue, request.ServicePrice, companyId!.Value, request.LagacyId);
                response = await _repository.Create(contractor, ct);

                if (request.ContractorPersonnels != null && request.ContractorPersonnels.Count > 0)
                    foreach (var item in request.ContractorPersonnels)
                    {
                        var createPersonnel = await _mediator.Send(new CreateThirdPartyPersonnelCommand(
                           item, companyId!.Value), ct);
                        if (createPersonnel.IsFailure || createPersonnel.Value is null)
                            return Result.Failure<TransportationContractor>(createPersonnel.Error!);
                        var personnelId = createPersonnel.Value.Id;

                        contractor.AddPersonnels(new TransportationContractorPersonnel(
                            personnelId!.Value, item.CertificateNumber, item.LagacyId, contractor));
                    }

                if (request.Documents is not null && request.Documents.Count > 0)
                    contractor.AddDocuments(request.Documents.Select(x => new TransportationContractorDocument(x, contractor)).ToList());

                if (request.Managers is not null && request.Managers.Count > 0)
                    contractor.AddManagers(request.Managers.Select(x => new TransportationContractorManager(x, contractor)).ToList());

                if (request.PriceWeights is not null && request.PriceWeights.Count > 0)
                {
                    var xx = request.PriceWeights.Where(x => x.IsFixed == true).Select(x => x).Count();
                    if (xx > 1)
                        return Result.Failure<TransportationContractor>(TransportationContractorErrors.MoreThanOneIsFixed);

                    var duplicateWeights = request.PriceWeights.Where(x => !x.IsFixed).GroupBy(x => x.UntilWeight)
                        .Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                    if (duplicateWeights.Any()) return Result.Failure<TransportationContractor>(
                        TransportationContractorErrors.DuplicateUntilWeight);

                    foreach (var item in request.PriceWeights)
                        contractor.AddPriceWeight(new TransportationContractorPriceWeight(
                            contractor, item.UntilWeight, item.IsFixed, item.Price));
                }

                if (request.Insurances is not null && request.Insurances.Count > 0)
                    contractor.AddInsurances(request.Insurances
                            .Select(item => new TransportationContractorInsurance(
                                contractor, item.MinProductPrice, item.MaxProductPrice,
                                item.FixedPrice, item.Multiplication, item.Division,
                                item.Subtraction, item.Addition))
                            .ToList());
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractor?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<PriceWeightExcelImportsResponse?>> PriceWeightImportExecute(
        PriceWeightExcelImportsRequest request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<PriceWeightExcelImportsResponse>(GlobalErrors.InvalidCompany);

            var transportationContractor = await _repository.GetTransportationContractorForImport(request.TransportationContractorId, ct);
            if (transportationContractor is null) return Result.Failure<PriceWeightExcelImportsResponse?>
                    (TransportationContractorErrors.TransportationContractorNotFound);

            var priceWeights = ExcelImporter.Import<PriceWeightExcelImportsModel>(request.DocumentFile);
            if (priceWeights is null)
                return Result.Failure<PriceWeightExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);

            (bool flowControl, Result<PriceWeightExcelImportsResponse?>? value) = ValidatePriceWeights(priceWeights);
            if (!flowControl)
                return value!;

            foreach (var item in priceWeights)
            {
                (bool flows, Result<(decimal, decimal)> numerics) = GetNumbericData(item);
                if (!flows)
                    return value!;

                var entity = new TransportationContractorPriceWeight(transportationContractor, numerics.Value.Item2,
                    item.IsFixed == 1 ? true : false, numerics.Value.Item1);

                transportationContractor.AddPriceWeight(entity);
                await _repository.Update(transportationContractor);
            }

            var countIsFixed = transportationContractor.PriceWeights.Where(x => x.IsFixed == true).Select(x => x).Count();
            if (countIsFixed > 1)
                return Result.Failure<PriceWeightExcelImportsResponse>(TransportationContractorErrors.MoreThanOneIsFixed);

            var duplicateWeights = transportationContractor.PriceWeights.Where(x => !x.IsFixed).GroupBy(x => x.UntilWeight)
                .Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (duplicateWeights.Any()) return Result.Failure<PriceWeightExcelImportsResponse>(
                TransportationContractorErrors.DuplicateUntilWeight);

            return new PriceWeightExcelImportsResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<PriceWeightExcelImportsResponse?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractor?>> DeleteTransportationContractorExecute(
        DeleteTransportationContractorRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities is null && entities?.Count != request.Ids.Count)
                return Result.Failure<TransportationContractor>(TransportationContractorErrors.TransportationContractorNotFound);

            foreach (var entity in entities)
            {
                entity.SetDelete();
                await _repository.Update(entity);

                if (entity.Type != TransportationContractorCalculateType.Price)
                {
                    List<long>? thirdPartiesId = (new[] { entity.ThirdPartyId!.Value })
                        .Concat(entity.ContractorPersonnels.Select(x => x.ThirdPartyId)).Distinct().ToList();
                    foreach (var item in thirdPartiesId)
                    {
                        var deletePersonnel = await _mediator.Send(new RemoveThirdPartyCommand(item), ct);
                        if (deletePersonnel.IsFailure || deletePersonnel.Value is null)
                            return Result.Failure<TransportationContractor>(deletePersonnel.Error!);
                    }
                }
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractor>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractor?>> UpdateTransportationContractorExecute(
        UpdateTransportationContractorRequest request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<TransportationContractor>(GlobalErrors.InvalidCompany);

            var entity = await _repository.GetTransportationContractorForUpdate(request.Id, ct);
            if (entity is null) return Result.Failure<TransportationContractor>(TransportationContractorErrors.TransportationContractorNotFound);

            if (request.IsDeleted == true)
            {
                entity.SetDelete();
                if (entity.Type != TransportationContractorCalculateType.Price)
                {
                    List<long>? thirdPartiesId = (new[] { entity.ThirdPartyId!.Value })
                        .Concat(entity.ContractorPersonnels.Select(x => x.ThirdPartyId)).Distinct().ToList();
                    foreach (var item in thirdPartiesId)
                    {
                        var deletePersonnel = await _mediator.Send(new RemoveThirdPartyCommand(item), ct);
                        if (deletePersonnel.IsFailure || deletePersonnel.Value is null)
                            return Result.Failure<TransportationContractor>(deletePersonnel.Error!);
                    }
                }
            }
            else
            {
                if (entity.Type == TransportationContractorCalculateType.Price)
                {
                    entity.Update(request.Title, request.DeliveryMethod != null ? request.DeliveryMethod?.ToArray() : [],
                        request.DeliveryType != null ? request.DeliveryType?.ToArray() : [], request.Type,
                        request.FirstPrefix, request.SecondPrefix, request.LagacyId, request.StartOfContract,
                        request.EndOfContract, request.IsActive);
                }
                else
                {
                    var thirdParty = await _mediator.Send(new GetThirdPartyByIdQuery(entity.ThirdPartyId!.Value), ct);
                    if (thirdParty.IsFailure) return Result.Failure<TransportationContractor>(TransportationContractorErrors.ThirdPartiesNotFound);

                    var updateThirdParty = await _mediator.Send(new UpdateThirdPartyCommand(request, thirdParty.Value), ct);
                    if (updateThirdParty.IsFailure || updateThirdParty.Value is null)
                        return Result.Failure<TransportationContractor>(updateThirdParty.Error!);
                    var thirdPartyId = updateThirdParty.Value.Id;

                    entity.Update(request.Title, thirdPartyId!.Value, request.StartOfContract, request.EndOfContract,
                        request.DeliveryMethod != null ? request.DeliveryMethod?.ToArray() : [],
                        request.DeliveryType != null ? request.DeliveryType?.ToArray() : [],
                        request.Type, request.PercentageValue, request.FixedNumber, request.FirstPrefix,
                        request.SecondPrefix, request.PercentageValue, request.ServicePrice,
                        request.LagacyId, request.IsActive);

                    var documents = request.Documents?.Select(x => new TransportationContractorDocument(x, entity)).ToList() ?? [];
                    entity.AddDocuments(documents);

                    var managers = request.Managers?.Select(x => new TransportationContractorManager(x, entity)).ToList() ?? [];
                    entity.AddManagers(managers);

                    (bool flow2, Result<TransportationContractor?>? value2) =
                        UpsertPriceWeight(request, entity);
                    if (!flow2) return value2!;

                    UpsertInsurance(request, entity);
                }
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationContractor>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractor?>> ChangeTransportationContractorStateExecute(
        ChangeTransportationContractorStateRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIdsIncludeLess(request.Ids, ct);
            if (entities.Count < request.Ids.Count)
                return Result.Failure<TransportationContractor>(TransportationContractorErrors.TransportationContractorNotFound);

            foreach (var entity in entities)
            {
                if (request.IsActive)
                {
                    if (entity.IsActive) return Result.Failure<TransportationContractor>(GlobalErrors.IsActive);
                    entity.SetActive();
                }
                else
                {
                    if (!entity.IsActive) return Result.Failure<TransportationContractor>(GlobalErrors.InActive);
                    entity.SetDeactivate();
                }
                await _repository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractor>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetTransportationContractorByIdResponse?>> GetTransportationContractorByIdExecute(
        GetTransportationContractorByIdRequest request, CT ct)
    {
        try
        {
            var item = await _repository.GetTransportationContractorById(request.Id, ct);
            return item ?? Result.Failure<GetTransportationContractorByIdResponse?>(TransportationContractorErrors.TransportationContractorNotFound); ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetTransportationContractorByIdResponse?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsFilteredTransportationContractorResponseModel>>?>> GetsFilteredTransportationContractorExecute(
        GetsFilteredTransportationContractorRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetFilteredTransportationContractors(
                request.Ids,
                request.ThirdPartyIds,
                request.StartDate,
                request.EndDate,
                request.DeliveryMethods,
                request.DeliveryTypes,
                request.Types,
                request.PackingShippingType,
                request.FilterData,
                request.IsActive,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsFilteredTransportationContractorResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredTransportationContractorResponseModel>>>(TransportationContractorErrors.FilteredTransportationContractorNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredTransportationContractorResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsPriceWeightHistoryResponseModel>>?>> GetsPriceWeightHistoryExecute(
        GetsPriceWeightHistoryRequest request, CT ct)
    {
        try
        {
            var companyId = _userInfoProvider.CompanyId;
            var items = await _priceWeightRepository.GetsPriceWeightHistory(
                request.Id,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsPriceWeightHistoryResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsPriceWeightHistoryResponseModel>>>(
                    TransportationContractorErrors.HistoryNotfound);

            if (response.Value?.Data != null && response.Value.Data.Count > 0)
            {
                var creatorIds = items.Data.NullListed(x => x.CreatorId);
                var creators = await _mediator.Send(new GetFilteredUsersQuery(creatorIds, null, null, null, null, 1, creatorIds.Count));

                response.Value.Data.ForEach(x =>
                {
                    x.Creator = creators.Value?.Data?.FirstOrDefault(z => z.UserId == x.CreatorId)?.FullName;
                });
            }

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsPriceWeightHistoryResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsActiveTransportationContractorResponseModel>>?>> GetsActiveTransportationContractorExecute(
        GetsActiveTransportationContractorRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetAllActiveTransportationContractors(
                request.Ids,
                request.ThirdPartyIds,
                request.StartDate,
                request.EndDate,
                request.DeliveryMethods,
                request.DeliveryTypes,
                request.Types,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsActiveTransportationContractorResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsActiveTransportationContractorResponseModel>>>(TransportationContractorErrors.FilteredTransportationContractorNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveTransportationContractorResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    #region PrivateMethods
    private static (bool flowControl, Result<PriceWeightExcelImportsResponse?>? value) ValidatePriceWeights(
        List<PriceWeightExcelImportsModel> PriceWeights)
    {
        if (PriceWeights.Any(x => string.IsNullOrEmpty(x.Price)))
            return (flowControl: false, value: Result.Failure<PriceWeightExcelImportsResponse>(ShippingCostErrors.PriceIsEmpty));

        if (PriceWeights.Any(x => string.IsNullOrEmpty(x.UntilWeight)))
            return (flowControl: false, value: Result.Failure<PriceWeightExcelImportsResponse>(ShippingCostErrors.UntilWeightIsNull));

        return (flowControl: true, value: null);
    }
   
    private static (bool flowControl, Result<(decimal price, decimal weight)> value) GetNumbericData(
        PriceWeightExcelImportsModel item)
    {
        decimal price = 0m;
        if (!string.IsNullOrEmpty(item.Price))
        {
            var priceResult = DataExtensions.ValidateDecimal(item.Price, "Price");
            if (!priceResult.Success)
                return (flowControl: false, value: Result.Failure<(decimal, decimal)>(ShippingCostErrors.PriceIsNull));
            price = priceResult.Value;
        }

        decimal untilWeight = 0m;
        if (!string.IsNullOrEmpty(item.UntilWeight))
        {
            var untilWeightResult = DataExtensions.ValidateDecimal(item.UntilWeight, "UntilWeight");
            if (!untilWeightResult.Success)
                return (flowControl: false, value: Result.Failure<(decimal, decimal)>(ShippingCostErrors.UntilWeightIsNull));
            untilWeight = untilWeightResult.Value;
        }

        return (flowControl: true, value: (price, untilWeight));
    }

    private async Task<(bool flowControl, Result<TransportationContractor?>? value)> UpsertContractorPersonnel(
        UpdateTransportationContractorRequest request, long? companyId, TransportationContractor entity, CT ct)
    {
        if (request.ContractorPersonnels != null && request.ContractorPersonnels.Count > 0)
        {
            foreach (var item in request.ContractorPersonnels)
            {
                if (item.Id == null || item.Id < 0)
                {
                    var createPersonnel = await _mediator.Send(new CreateThirdPartyPersonnelCommand(
                       item.Adapt<CreateContractorPersonnelModel>(), companyId!.Value), ct);
                    if (createPersonnel.IsFailure || createPersonnel.Value is null)
                        return (flowControl: false, value: Result.Failure<TransportationContractor>(createPersonnel.Error!));
                    var personnelId = createPersonnel.Value.Id;

                    entity.AddPersonnels(new TransportationContractorPersonnel(personnelId!.Value, item.CertificateNumber, item.LegacyId, entity));
                }
                else if (item.ThirdPartyId != null && item.ThirdPartyId > 0 && item.IsDeleted != true)
                {
                    var updatePersonnel = await _mediator.Send(new UpdateThirdPartyPersonnelCommand(item), ct);
                    if (updatePersonnel.IsFailure || updatePersonnel.Value is null)
                        return (flowControl: false, value: Result.Failure<TransportationContractor>(updatePersonnel.Error!));
                    var personnelId = updatePersonnel.Value.Id;
                }
                else if (item.ThirdPartyId != null && item.ThirdPartyId > 0 && item.IsDeleted == true)
                {
                    var deletePersonnel = await _mediator.Send(new RemoveThirdPartyCommand(item.ThirdPartyId!.Value), ct);
                    if (deletePersonnel.IsFailure || deletePersonnel.Value is null)
                        return (flowControl: false, value: Result.Failure<TransportationContractor>(deletePersonnel.Error!));
                }
            }
        }

        return (flowControl: true, value: null);
    }

    private static void UpsertInsurance(UpdateTransportationContractorRequest request, TransportationContractor entity)
    {
        if (request.Insurances is not null && request.Insurances.Count > 0)
            entity.AddInsurances(request.Insurances
                    .Select(item => new TransportationContractorInsurance(
                        entity, item.MinProductPrice, item.MaxProductPrice,
                        item.FixedPrice, item.Multiplication, item.Division,
                        item.Subtraction, item.Addition))
                    .ToList());

        if (request.UpdateInsurances is not null && request.UpdateInsurances.Count > 0)
            foreach (var item in request.UpdateInsurances)
                entity.TransportationContractorInsurances.FirstOrDefault(x => x.Id == item.Id)?
                    .Update(item.MinProductPrice, item.MaxProductPrice, item.FixedPrice,
                    item.Multiplication, item.Division, item.Subtraction, item.Addition);

        if (request.DeleteInsurances is not null && request.DeleteInsurances.Count > 0)
            foreach (var item in request.DeleteInsurances)
                entity.TransportationContractorInsurances.FirstOrDefault(x => x.Id == item)?.SoftDelete();
    }

    private static (bool flowControl, Result<TransportationContractor?> value) UpsertPriceWeight(
        UpdateTransportationContractorRequest request, TransportationContractor entity)
    {
        if (request.PriceWeights is not null && request.PriceWeights.Count > 0)
            foreach (var item in request.PriceWeights)
                entity.AddPriceWeight(new TransportationContractorPriceWeight(entity, item.UntilWeight, item.IsFixed, item.Price));

        if (request.UpdatePriceWeights is not null && request.UpdatePriceWeights.Count > 0)
            foreach (var item in request.UpdatePriceWeights)
                entity.PriceWeights.FirstOrDefault(x => x.Id == item.Id)?.Update(entity, item.UntilWeight, item.IsFixed, item.Price);

        if (request.DeletePriceWeights is not null && request.DeletePriceWeights.Count > 0)
            foreach (var item in request.DeletePriceWeights)
                entity.PriceWeights.FirstOrDefault(x => x.Id == item)?.SoftDelete();

        if (entity.PriceWeights.Where(x => !x.IsDeleted && x.IsFixed).Select(x => x).Distinct().Count() > 1)
            return (flowControl: false, value: Result.Failure<TransportationContractor>(TransportationContractorErrors.MoreThanOneIsFixed));

        var duplicateWeights = entity.PriceWeights.Where(x => !x.IsDeleted && !x.IsFixed).GroupBy(x => x.UntilWeight)
            .Where(g => g.Count() > 1).Select(g => g.Key).ToList();
        if (duplicateWeights.Any()) return (flowControl: false, value: Result.Failure<TransportationContractor>(
            TransportationContractorErrors.DuplicateUntilWeight));

        return (flowControl: true, value: null);
    }

    #endregion
}
