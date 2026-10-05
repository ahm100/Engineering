using Engineering.Application.Services.TransportationCargos.Commands.AddTransportationCargoBill;
using Engineering.Application.Services.TransportationRequests.Commands.AddTransportationRequestBill;
using Engineering.Application.Services.TransportationRequests.Commands.CalculatePriceOfTransport;
using Engineering.Application.Services.TransportationRequests.Commands.CreateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Commands.Disable;
using Engineering.Application.Services.TransportationRequests.Commands.PackingRivision;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateAfterReforms;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateAggregateTransportationWarehouse;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateMachineDriver;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportLoadWeight;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportPalletLoadWeight;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportVolume;
using Engineering.Application.Services.TransportationRequests.Models.AddTransportationRequestBill;
using Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;
using Engineering.Application.Services.TransportationRequests.Models.CargoReformsTransport;
using Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoWithoutContractor;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationContractorCalculateType;
using Engineering.Application.Services.TransportationRequests.Models.PackingReleaseFromTransport;
using Engineering.Application.Services.TransportationRequests.Models.PackingRivision;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAfterCargoDeclaration;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAggregateTransportationWarehouse;
using Engineering.Application.Services.TransportationRequests.Models.UpdateFreeCargosTransportInfo;
using Engineering.Application.Services.TransportationRequests.Models.UpdateMachineDriver;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportLoadWeight;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportVolume;
using Engineering.Application.Services.TransportationRequests.Queries.IsDuplicateTransportWithPackingIds;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Warehouse.ClientSdks.Enums;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    public async Task<Result<CreateWarehouseTransportationResponse?>> CreateWarehouseTransportation(
        CreateWarehouseTransportationRequest request, CT ct, bool update = false)
    {
        _logger.LogInformation("Request for CreateWarehouseTransportation");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateWarehouseTransportationResponse>(GlobalErrors.InvalidCompany);

        if (request.PostageDate != null)
            if (request.PostageDate.Value.Date < DateTime.UtcNow.Date)
                return Result.Failure<CreateWarehouseTransportationResponse>(TransportationRequestErrors.PostageDateIsNotValid);

        if (update == false)
        {
            var packingDuplicate = await _mediator.Send(new IsDuplicateTransportWithPackingIdsQuery(request.PackingIds), ct);
            if (packingDuplicate.Value == true)
                return Result.Failure<CreateWarehouseTransportationResponse>(TransportationRequestErrors.PackingsDuplicate);
        }

        TransportationContractor? contractor = null;
        List<TransportationContractorPriceWeight>? priceWeights = [];
        if (request.TransportationContractorId != null && request.TransportationContractorId > 0)
        {
            var transportContractor = await _transportationContractorRepository.GetTransportationContractor(request.TransportationContractorId.Value, ct);
            if (transportContractor is null) return Result.Failure<CreateWarehouseTransportationResponse>
                    (TransportationContractorErrors.TransportationContractorNotFound);
            contractor = transportContractor;

            var (flowControl, pricesData) = await GetTransportContractorPrices(contractor, ct);
            if (!flowControl)
                return Result.Failure<CreateWarehouseTransportationResponse>(ShippingCostErrors.ShippingCostNotFound);

            if (contractor?.Type == TransportationContractorCalculateType.Weight)
                priceWeights = pricesData!.PriceWeights;
        }

        var validationResult = await ValidatePackings(request, ct);
        if (!validationResult.flowControl)
            return Result.Failure<CreateWarehouseTransportationResponse>(validationResult.error!);

        List<CreateWarehouseTransportationWarehouseModel>? warehouseCommands = [];
        var packNumbers = validationResult.value.packings!.NullListed(x => x.RequestNumber);
        var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers(packNumbers, ct);
        if (validationResult.value.packings! is not null && validationResult.value.packings!.Count > 0)
            foreach (var item in validationResult.value.packings!)
            {
                var invoice = invoices?.FirstOrDefault(x => x.PackingNumber == item.RequestNumber);
                var shipping = item?.PackingShippingDetails.FirstOrDefault(x => x.IsDeleted == false);
                warehouseCommands.Add(new CreateWarehouseTransportationWarehouseModel(
                    item!, shipping, invoice, contractor, priceWeights));
            }

        var createShipment = await _mediator.Send(new CreateWarehouseTransportationCommand(request, warehouseCommands), ct);
        if (createShipment.IsFailure) return Result.Failure<CreateWarehouseTransportationResponse>(createShipment.Error!);

        if (update == false)
            if (validationResult.value.shippingDetails!.Any())
            {
                var shippingIds = validationResult.value.shippingDetails!.Select(x => x.Id).Distinct().ToList();
                var updatePac = await _packingService.UpdateDeliveryPacking(
                    new(shippingIds, (Warehouse.ClientSdks.Enums.DeliveryMethod?)(int?)request.DeliveryMethod,
                        (Warehouse.ClientSdks.Enums.DeliveryType?)(int?)request.DeliveryType,
                        request.TransportationContractorId,
                        (Warehouse.ClientSdks.Enums.PackingShippingType?)(int?)request.PackingShippingType,
                        request.VehicleName,
                        request.NumberPlate,
                        request.Driver,
                        request.DriverPhoneNumber,
                        request.PostageDate), ct);
                if (updatePac == null || updatePac.IsDone == false)
                    return Result.Failure<CreateWarehouseTransportationResponse>(TransportationContractorErrors.UpdateFeild);
            }

        if (update == false)
            await _unitOfWork.CommitAsync(ct);

        return new CreateWarehouseTransportationResponse(true);
    }

    public async Task<Result<AggregateWarehouseTransportationResponse?>> AggregateWarehouseTransportation(
        AggregateWarehouseTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for AggregateWarehouseTransportation");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<AggregateWarehouseTransportationResponse>(GlobalErrors.InvalidCompany);

        var validateRequestData = await ValidateAggregateWarehouseTransportation(request, ct);
        if (validateRequestData.IsFailure)
            return Result.Failure<AggregateWarehouseTransportationResponse>(validateRequestData.Error!);
        var validateData = validateRequestData.Value;

        List<TransportationRequest>? transportationRequests = [];
        (bool InPersonDeliveryFlowControl, Result<AggregateWarehouseTransportationResponse?> InPersonDeliveryValue) =
            await AddInPersonDeliveryTransportRequests(request, validateData!, transportationRequests, companyId!.Value, ct);
        if (!InPersonDeliveryFlowControl)
            return InPersonDeliveryValue;

        (bool agancyFlowControl, Result<AggregateWarehouseTransportationResponse?> agancyValue) =
            await AddAgancyTransportRequests(request, validateData!, transportationRequests, companyId!.Value, ct);
        if (!agancyFlowControl)
            return agancyValue;

        (bool contractorFlowControl, Result<AggregateWarehouseTransportationResponse?> contractorValue) =
            await AddContractorTransportRequests(request, validateData!, transportationRequests, companyId!.Value, ct);
        if (!contractorFlowControl)
            return contractorValue;

        await _unitOfWork.CommitAsync(ct);
        return new AggregateWarehouseTransportationResponse(true);
    }

    public async Task<Result<CargoReformsTransportResponse?>> CargoReformsTransport(
        CargoReformsTransportRequest request, CT ct)
    {
        _logger.LogInformation("Request for Cargo Reforms Transport");

        var transportationRequestData = await _repository.GetLogesticById(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<CargoReformsTransportResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(ValidateTransportationRequestStatus.AllowStatusForReforms.Any(x => x == transportationRequestData.TransportationRequestStatus)))
            return Result.Failure<CargoReformsTransportResponse>(TransportationRequestErrors.UnValidStatus);

        List<ShippingCost>? shippingCosts = [];
        List<TransportationContractorPriceWeight>? priceWeights = [];
        var (flowControl, pricesData) = await GetTransportContractorPrices(transportationRequestData.TransportationContractor, ct);
        if (!flowControl)
            return Result.Failure<CargoReformsTransportResponse>(ShippingCostErrors.ShippingCostNotFound);

        if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Distance)
            shippingCosts = pricesData!.ShippingCosts;
        else if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Weight)
            priceWeights = pricesData!.PriceWeights;

        var palletsData = await _transportationCargoPalletRepository.GetByIds(request.PalletIds!, ct);
        if (palletsData is null || palletsData.Distinct().Count() != request.PalletIds!.Distinct().Count())
            return Result.Failure<CargoReformsTransportResponse>(TransportationRequestErrors.CargosPalletWithNotfound);

        if (palletsData.Any(x => x.TransportationRequest is not null && x.TransportationRequestId > 0))
            return Result.Failure<CargoReformsTransportResponse>(TransportationRequestErrors.CargosPalletHaveTransport);

        (bool pacFlowControl, Result<List<TransportationCargoPallet>?> pacValue) = await ReformPallets(
            request, palletsData, transportationRequestData, shippingCosts, priceWeights, ct);
        if (!pacFlowControl)
            return Result.Failure<CargoReformsTransportResponse>(pacValue.Error!);

        var CalcReforms = await CalcTransportAfterReforms(
            transportationRequestData, shippingCosts, priceWeights, pricesData!, palletsData, ct);
        if (CalcReforms.IsFailure)
            return Result.Failure<CargoReformsTransportResponse>(CalcReforms.Error!);

        var updateTransport = await _mediator.Send(new UpdateAfterReformsCommand(transportationRequestData,
        CalcReforms.Value.extraInfo!, CalcReforms.Value.totalTransferPrice, palletsData.Sum(x => x.Weight) ?? 0), ct);
        if (updateTransport.IsFailure)
            return Result.Failure<CargoReformsTransportResponse>(updateTransport.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CargoReformsTransportResponse(true);
    }

    public async Task<Result<UpdateAfterCargoDeclarationResponse?>> UpdateAfterCargoDeclaration(
        UpdateAfterCargoDeclarationRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationRequest");

        var cargoPallets = await _transportationCargoPalletRepository.GetTransportationPallets([request.CargoId], ct);
        if (cargoPallets is null) return Result.Failure<UpdateAfterCargoDeclarationResponse>(TransportationRequestErrors.CargosPalletWithNotfound);

        if (cargoPallets.Any(x => x.TransportationRequest is not null && x.TransportationRequestId != null))
            return Result.Failure<UpdateAfterCargoDeclarationResponse>(TransportationRequestErrors.CargosPalletHaveTransport);

        (bool flowControl, Result<UpdateAfterCargoDeclarationResponse?> value) = await UpdatePalletsAfterCargoDeclare(request, cargoPallets, ct);
        if (!flowControl)
            return value;

        var packingIds = cargoPallets.Listed(x => x.TransportationCargo.PackingId);
        var packings = await _packingRepository.GetByIdsWithInclude(packingIds, ct);
        var shippingIds = packings!.SelectMany(x => x.PackingShippingDetails).Distinct()
            .Listed(x => x.Id);

        if (shippingIds.Any())
        {
            var updatePac = await _packingService.UpdateDeliveryPacking(
                    new(shippingIds,
                       (Warehouse.ClientSdks.Enums.DeliveryMethod?)(int?)request.DeliveryMethod,
                       (Warehouse.ClientSdks.Enums.DeliveryType?)(int?)request.DeliveryType,
                       request.TransportationContractorId,
                       (Warehouse.ClientSdks.Enums.PackingShippingType?)(int?)request.PackingShippingType,
                       request.VehicleName,
                       request.NumberPlate,
                       request.Driver,
                       request.DriverPhoneNumber,
                       request.PostageDate), ct);
            if (updatePac == null || updatePac.IsDone == false)
                return Result.Failure<UpdateAfterCargoDeclarationResponse>(TransportationContractorErrors.UpdateFeild);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateAfterCargoDeclarationResponse(0, true);
    }

    public async Task<Result<UpdateFreeCargosTransportInfoResponse?>> UpdateFreeCargosTransportInfo(
        UpdateFreeCargosTransportInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationRequest");

        var cargoPallets = await _transportationCargoPalletRepository.GetTransportationPallets([request.CargoId], ct);
        if (cargoPallets is null || !cargoPallets.Any()) return Result.Failure<UpdateFreeCargosTransportInfoResponse>
                (TransportationRequestErrors.CargosPalletWithNotfound);

        if (cargoPallets.All(x => x.TransportationRequest is not null && x.TransportationRequestId != null))
            return Result.Failure<UpdateFreeCargosTransportInfoResponse>(TransportationRequestErrors.CargosPalletHaveTransport);

        var cargoNotHaveTransport = cargoPallets
            .Where(x => x.TransportationRequestId == null && x.TransportationRequest is null &&
            (request.PalletIds == null || request.PalletIds.Count == 0 || request.PalletIds.Contains(x.Id)))
            .ToList();

        (bool flowControl, Result<UpdateFreeCargosTransportInfoResponse?> value) =
            await UpdatePalletsAfterCargoDeclare(request, cargoNotHaveTransport, ct);
        if (!flowControl)
            return value;

        (bool flowControlShipping, Result<UpdateFreeCargosTransportInfoResponse?> updateShippings) =
            await UpdatePackingShippingDetails(request, cargoNotHaveTransport, cargoPallets, ct);
        if (!flowControlShipping)
        {
            return updateShippings;
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateFreeCargosTransportInfoResponse(request.CargoId, true);
    }

    public async Task<Result<UpdateAggregateTransportationWarehouseResponse?>> UpdateAggregateTransportationWarehouse(
        UpdateAggregateTransportationWarehouseRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateAggregateTransportationWarehouse");

        var transportationRequestData = await _repository.GetByIdIncludeLess(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<UpdateAggregateTransportationWarehouseResponse>(TransportationRequestErrors
                .TransportationRequestWithIdNotFound!);

        if (ValidateTransportationRequestStatus.DontAllowForUpdate.Any(z => z == transportationRequestData.TransportationRequestStatus))
            return Result.Failure<UpdateAggregateTransportationWarehouseResponse>(TransportationRequestErrors.UnValidStatus);

        var machineType = await _machineTypeRepository.FindById(request.MachineTypeId, ct);
        if (machineType is null)
            return Result.Failure<UpdateAggregateTransportationWarehouseResponse>(MachineErrors.MachineWithIdNotFound);

        var driver = await _thirdPartyRepository.FindById(request.DriverId, ct);
        if (driver is null)
            return Result.Failure<UpdateAggregateTransportationWarehouseResponse>(TransportationRequestErrors
                .DriverIdNotValid);

        var pallets = transportationRequestData.TransportationCargoPallets.ToList();
        List<UpdatePackingWarehousePrice>? warehousePrices = [];
        if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Weight)
        {
            var totalWeight = pallets.Sum(x => x.Weight);
            var unitPrice = request.TransferPrice / totalWeight;
            foreach (var item in pallets)
            {
                var price = (unitPrice ?? 0) * (item.Weight ?? 1);
                warehousePrices.Add(new UpdatePackingWarehousePrice(item.Id, price));
            }
        }

        var response = await _mediator.Send(new UpdateAggregateTransportationWarehouseCommand(
            transportationRequestData, request.TransferPrice, request.Description, machineType, driver.Id,
            request.NumberPlate, request.PostageDate, request.DetailId, request.GlobalFreightNumber,
            request.ClassifiedFreightNumber, request.Tax, request.DetailTransferPrice, request.ServicePrice,
            request.InsuranceNumber, request.InsurancePrice, request.ShippingCost, request.ProductTotalPrice,
            request.OutofRange, request.LoadWeight, request.OrderNumber, request.CertificateNumber, request.Documents,
            warehousePrices), ct);
        if (response.IsFailure) return Result.Failure<UpdateAggregateTransportationWarehouseResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateAggregateTransportationWarehouseResponse(0, true);
    }

    public async Task<Result<PackingReleaseFromTransportResponse?>> PackingReleaseFromTransport(
        PackingReleaseFromTransportRequest request, CT ct)
    {
        _logger.LogInformation("Request for Packing Release From Transport");

        var transportationRequestData = await _repository.GetLogesticById(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<PackingReleaseFromTransportResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(ValidateTransportationRequestStatus.AllowForPackingRelease.Any(x => x == transportationRequestData.TransportationRequestStatus)))
            return Result.Failure<PackingReleaseFromTransportResponse>(TransportationRequestErrors.UnValidStatus);

        var palletsData = await _transportationCargoPalletRepository.GetTransportationPallets(request.CargoIds, ct);
        if (palletsData is null)
            return Result.Failure<PackingReleaseFromTransportResponse>(TransportationRequestErrors.CargosPalletWithNotfound);

        var allPalletsInTransportation = transportationRequestData.TransportationCargoPallets.Select(x => x.Id).ToHashSet();
        var requestedPalletIds = palletsData.Listed(x => x.Id).Distinct().ToHashSet();
        bool shouldDeleteTransport = allPalletsInTransportation.IsSubsetOf(requestedPalletIds);

        if (shouldDeleteTransport)
        {
            var deleteTransport = await _mediator.Send(new DisableTransportationRequestCommand(request.Id), ct);
            if (deleteTransport.IsFailure)
                return Result.Failure<PackingReleaseFromTransportResponse>(deleteTransport.Error!);
        }

        var palletForRelease = transportationRequestData.TransportationCargoPallets
            .Where(x => requestedPalletIds.Contains(x.Id)).ToList();

        List<ShippingCost>? shippingCosts = [];
        List<TransportationContractorPriceWeight>? priceWeights = [];
        var (flowControl, pricesData) = await GetTransportContractorPrices(
            transportationRequestData.TransportationContractor, ct);
        if (!flowControl)
            return Result.Failure<PackingReleaseFromTransportResponse>(ShippingCostErrors.ShippingCostNotFound);

        if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Distance)
            shippingCosts = pricesData!.ShippingCosts;
        else if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Weight)
            priceWeights = pricesData!.PriceWeights;

        var releaseReponse = await ReleasePackingData(transportationRequestData, palletForRelease, shippingCosts, priceWeights, ct);

        await _unitOfWork.CommitAsync(ct);
        return new PackingReleaseFromTransportResponse(true);
    }

    public async Task<Result<AddTransportationRequestBillResponse?>> AddTransportationRequestBill(
        AddTransportationRequestBillRequest request, CT ct)
    {
        _logger.LogInformation("Request for AddTransportationRequestBill");
        if (request.Id is not null && request.Id > 0)
        {
            var transportationRequestData = await _repository.GetByIdIncludeLess(request.Id.Value, ct);
            if (transportationRequestData is null)
                return Result.Failure<AddTransportationRequestBillResponse>(TransportationRequestErrors
                    .TransportationRequestWithIdNotFound!);

            if (ValidateTransportationRequestStatus.AllowStatusForAddBill.Any(z => z == transportationRequestData.TransportationRequestStatus))
                return Result.Failure<AddTransportationRequestBillResponse>(TransportationRequestErrors.UnValidStatus);

            var response = await _mediator.Send(new AddTransportationRequestBillCommand(
                transportationRequestData, request.Documents), ct);
            if (response.IsFailure) return Result.Failure<AddTransportationRequestBillResponse>(response.Error!);
        }

        if (request.CargoId is not null && request.CargoId > 0)
        {
            var cargo = await _transportationCargoRepository.GetById(request.CargoId.Value, ct);
            if (cargo is null) return Result.Failure<AddTransportationRequestBillResponse>
                    (TransportationRequestErrors.CargosNotfound!);

            if (cargo.SecurityConfirm == false) return Result.Failure<AddTransportationRequestBillResponse>
                    (TransportationRequestErrors.UnvalidCargoState!);

            var response = await _mediator.Send(new AddTransportationCargoBillCommand(
                cargo, request.Documents), ct);
            if (response.IsFailure) return Result.Failure<AddTransportationRequestBillResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new AddTransportationRequestBillResponse(request.Id, request.CargoId, true);
    }

    public async Task<Result<CalculatePriceOfTransportResponse?>> CalculatePriceOfTransport(
        CalculatePriceOfTransportRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateAggregateTransportationWarehouse");

        var transportationRequestData = await _repository.GetByIdIncludeLess(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<CalculatePriceOfTransportResponse>(TransportationRequestErrors
                .TransportationRequestWithIdNotFound!);

        List<ShippingCost>? shippingCosts = [];
        List<TransportationContractorPriceWeight>? priceWeights = [];
        var (flowControl, pricesData) = await GetTransportContractorPrices(transportationRequestData.TransportationContractor, ct);
        if (!flowControl)
            return Result.Failure<CalculatePriceOfTransportResponse>(ShippingCostErrors.ShippingCostNotFound);

        if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Distance)
            shippingCosts = pricesData.ShippingCosts;
        else if (transportationRequestData.TransportationContractor?.Type == TransportationContractorCalculateType.Weight)
            priceWeights = pricesData.PriceWeights;

        var response = await _mediator.Send(new CalculatePriceOfTransportCommand(
            transportationRequestData, request.LoadWeight, priceWeights), ct);
        if (response.IsFailure) return Result.Failure<CalculatePriceOfTransportResponse>(response.Error!);

        return response.Value;
    }

    public async Task<Result<PackingRivisionResponse?>> PackingRivision(
        PackingRivisionRequest request, CT ct)
    {
        _logger.LogInformation("Request for PackingRivision");

        var response = await _mediator.Send(new PackingRivisionCommand(
           request.CargoIds, request.PackingIds, request.Description), ct);
        if (response.IsFailure) return Result.Failure<PackingRivisionResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new PackingRivisionResponse(true);
    }

    public async Task<Result<UpdateMachineDriverResponse?>> UpdateMachineDriver(
        UpdateMachineDriverRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationRequest");

        var transportationRequestData = await _repository.GetByIdIncludeLess(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<UpdateMachineDriverResponse>(TransportationRequestErrors
                .TransportationRequestWithIdNotFound!);

        if (ValidateTransportationRequestStatus.DontAllowForUpdate.Any(z => z == transportationRequestData.TransportationRequestStatus))
            return Result.Failure<UpdateMachineDriverResponse>(TransportationRequestErrors.UnValidStatus);

        var machineType = await _machineTypeRepository.FindById(request.MachineTypeId, ct);
        if (machineType is null)
            return Result.Failure<UpdateMachineDriverResponse>(MachineErrors.MachineWithIdNotFound);

        ViewThirdParty? driver = null;
        if (request.DriverId != null && request.DriverId > 0)
        {
            driver = await _thirdPartyRepository.FindById(request.DriverId!.Value, ct);
            if (driver is null) return Result.Failure<UpdateMachineDriverResponse>
                    (TransportationRequestErrors.DriverIdNotValid);
        }

        var response = await _mediator.Send(new UpdateMachineDriverCommand(transportationRequestData,
            machineType, request.DriverId != null && request.DriverId > 0 ? driver!.Id : null,
            request.Driver, request.NumberPlates, request.CertificateNumber), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateMachineDriverResponse>(response.Error!);
        var entity = response.Value;

        (bool flowControl, Result<UpdateMachineDriverResponse?> value) = await ChangeDistanceTypePrice(request, entity!, ct);
        if (!flowControl)
            return value;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateMachineDriverResponse(response.Value!.Id, true);
    }

    public async Task<Result<UpdateTransportLoadWeightResponse?>> UpdateTransportLoadWeight(
        UpdateTransportLoadWeightRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationRequest");

        var transportationRequestData = await _repository.GetByIdIncludeLess(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<UpdateTransportLoadWeightResponse>(TransportationRequestErrors
                .TransportationRequestWithIdNotFound!);

        if (ValidateTransportationRequestStatus.DontAllowForUpdate.Any(z => z == transportationRequestData.TransportationRequestStatus))
            return Result.Failure<UpdateTransportLoadWeightResponse>(TransportationRequestErrors.UnValidStatus);

        var response = await _mediator.Send(new UpdateTransportLoadWeightCommand(transportationRequestData, request.LoadWeight), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTransportLoadWeightResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportLoadWeightResponse(response.Value!.Id, true);
    }

    public async Task<Result<UpdateTransportPalletLoadWeightResponse?>> UpdateTransportPalletLoadWeight(
        UpdateTransportPalletLoadWeightRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationRequest");

        var requestPallets = request.TransportPallets.Distinct().ToList();
        var response = await _mediator.Send(new UpdateTransportPalletLoadWeightCommand(requestPallets), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTransportPalletLoadWeightResponse>(response.Error!);

        TransportationRequest? transportationRequest = null;
        if (request.TransportationRequestId != null && request.TransportationRequestId > 0)
        {
            var transportationRequestData = await _repository.GetLogesticById(request.TransportationRequestId.Value, ct);
            if (transportationRequestData is null) return Result.Failure<UpdateTransportPalletLoadWeightResponse>
                    (TransportationRequestErrors.TransportationRequestWithIdNotFound!);
            transportationRequest = transportationRequestData;

            if (ValidateTransportationRequestStatus.DontAllowForUpdate.Any(z => z == transportationRequest.TransportationRequestStatus))
                return Result.Failure<UpdateTransportPalletLoadWeightResponse>(TransportationRequestErrors.UnValidStatus);

            var palletWeights = transportationRequest.TransportationCargoPallets.Sum(x => x.Weight ?? 0);
            var updateTransport = await _mediator.Send(new UpdateTransportLoadWeightCommand(transportationRequestData, palletWeights, true), ct);
            if (updateTransport.IsFailure)
                return Result.Failure<UpdateTransportPalletLoadWeightResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportPalletLoadWeightResponse(true);
    }

    public async Task<Result<UpdateTransportVolumeResponse?>> UpdateTransportVolume(
        UpdateTransportVolumeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationRequest");

        var transportationRequestData = await _repository.GetByIdIncludeLess(request.Id, ct);
        if (transportationRequestData is null)
            return Result.Failure<UpdateTransportVolumeResponse>(TransportationRequestErrors
                .TransportationRequestWithIdNotFound!);

        if (ValidateTransportationRequestStatus.DontAllowForUpdate.Any(z => z == transportationRequestData.TransportationRequestStatus))
            return Result.Failure<UpdateTransportVolumeResponse>(TransportationRequestErrors.UnValidStatus);

        var response = await _mediator.Send(new UpdateTransportVolumeCommand(transportationRequestData, request.Volume), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTransportVolumeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportVolumeResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetsWarehouseTransportationResponse?>> GetsWarehouseTransportation(
        GetsWarehouseTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequest pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var response = await _repository.GetsWarehouseTransportation(request.Ids, request.ContractorId,
            request.TransportationRequestStatus, request.TransportationId, request.RequestById,
            request.FromDate, request.ToDate, request.FilterData, request.PageIndex, request.PageSize, ct);
        if (response.Data is null)
            return Result.Failure<GetsWarehouseTransportationResponse>(TransportationRequestErrors
                .FilteredTransportationRequestNotFound);
        var values = response.Data;

        await GetWarehouseTransportationSeedData(values, ct);

        return new GetsWarehouseTransportationResponse(values ?? [], response.RowCount);
    }

    public async Task<Result<GetsAggregateWarehouseTransportationResponse?>> GetsAggregateWarehouseTransportation(
        GetsAggregateWarehouseTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequest");

        var response = await _repository.GetsAggregateWarehouseTransportation(request.Ids, request.ProductIds,
            request.ContractorId, request.TransportationRequestStatus, request.TransportationId,
            request.MachineTypeId, request.RequestById, request.FromDate, request.ToDate,
            request.FreightNumber, request.FilterData, request.PageIndex, request.PageSize, ct);
        if (response.Data is null)
            return Result.Failure<GetsAggregateWarehouseTransportationResponse>(TransportationRequestErrors
                .FilteredTransportationRequestNotFound);
        var values = response.Data;

        await AggregateSeedData(values, ct);

        return new GetsAggregateWarehouseTransportationResponse(values ?? [], response.RowCount);
    }

    public async Task<Result<GetsAggregateWarehouseTransportationByIdResponse?>> GetsAggregateWarehouseTransportationById(
        GetsAggregateWarehouseTransportationByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsAggregateWarehouseTransportationById");

        var response = await _repository.GetsAggregateWarehouseTransportationById(request.Id, ct);
        if (response is null)
            return Result.Failure<GetsAggregateWarehouseTransportationByIdResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
        var value = response;

        await GetByIdSeedData(value, ct);

        return value;
    }

    public async Task<Result<GetTransportationContractorCalculateTypeResponse?>> GetTransportationContractorCalculateType(
        GetTransportationContractorCalculateTypeRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationContractorCalculateType>());
        return new GetTransportationContractorCalculateTypeResponse(result);
    }

    public async Task<Result<GetPackingLogesticDetailResponse?>> GetPackingLogesticDetail(
        GetPackingLogesticDetailRequest request, CT ct)
    {
        var transportationRequestData = await _transportationCargoPalletRepository.GetLogesticByPackingId(request.PackingId, ct);
        if (transportationRequestData is null)
            return Result.Failure<GetPackingLogesticDetailResponse>(TransportationRequestErrors
                .TransportationRequestWithIdNotFound);

        return transportationRequestData;
    }

    public async Task<Result<GetsTransportationCargoPalletResponse?>> GetsTransportationCargoWithoutContractor(
        GetsTransportationCargoWithoutContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequest pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsTransportationCargoPalletResponse>(GlobalErrors.InvalidCompany);

        var response = await _transportationCargoPalletRepository.GetsTransportationCargoWithoutContractor(request.Ids,
            request.CargoIds, request.TransportationRequestIds, request.PackingIds, request.DeliveryMethods,
            request.DeliveryTypes, request.PackingShippingTypes, request.TransportationRequestStatus,
            request.SalesChannelTypes, request.ThirdPartyIds, request.ProductIds, request.WarehouseIds, request.DesWarehouseIds,
            request.CityIds, request.CreatorId, request.FromDate, request.ToDate, request.FilterData,
            companyId, request.HaveShippingType, request.PageIndex, request.PageSize, ct);
        if (response.Data is null)
            return Result.Failure<GetsTransportationCargoPalletResponse>(TransportationRequestErrors
                .CargosPalletNotfound);
        var values = response.Data;

        await GetWarehouseTransportationSeedData(values, ct);

        return new GetsTransportationCargoPalletResponse(values ?? [], response.RowCount);
    }

    public async Task<Result<GetsTransportationCargoPalletResponse?>> GetsTransportationCargoPallet(
        GetsTransportationCargoPalletRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequest pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsTransportationCargoPalletResponse>(GlobalErrors.InvalidCompany);

        var response = await _transportationCargoPalletRepository.GetsTransportationCargo(request.Ids, request.ContractorIds,
            request.CargoIds, request.TransportationRequestIds, request.PackingIds, request.DeliveryMethods,
            request.DeliveryTypes, request.PackingShippingTypes, request.TransportationRequestStatus,
            request.SalesChannelTypes, request.ThirdPartyIds, request.ProductIds, request.WarehouseIds, request.DesWarehouseIds,
            request.CityIds, request.CreatorId, request.FromDate, request.ToDate, request.FilterData,
            companyId, request.HaveShippingType, request.PageIndex, request.PageSize, ct);
        if (response.Data is null)
            return Result.Failure<GetsTransportationCargoPalletResponse>(TransportationRequestErrors
                .CargosPalletNotfound);
        var values = response.Data;

        await GetWarehouseTransportationSeedData(values, ct);

        return new GetsTransportationCargoPalletResponse(values ?? [], response.RowCount);
    }

    public async Task<Result<GetTransportationCargoPalletResponse?>> GetTransportationCargoPallet(
        GetTransportationCargoPalletRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestWarehouse");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetTransportationCargoPalletResponse>(GlobalErrors.InvalidCompany);

        var response = await _transportationCargoPalletRepository.GetPalletById(request.Id, ct);
        if (response is null) return Result.Failure<GetTransportationCargoPalletResponse>
                (TransportationRequestErrors.CargosPalletWithNotfound);
        var value = response;
        await GetWarehouseTransportationPalletSeedData(value, ct);

        return value;
    }

    public async Task<Result<GetsFilteredTransportationCargoResponse?>> GetsFilteredTransportationCargo(
        GetsFilteredTransportationCargoRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequest pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsFilteredTransportationCargoResponse>(GlobalErrors.InvalidCompany);

        long? thirdPartyId = null;
        if (request.PackingShippingTypes != null && request.PackingShippingTypes.Any())
            if (request.PackingShippingTypes.Any(x => x == ClientSdk.Enums.PackingShippingType.Contracting))
            {
                thirdPartyId = _userInfoProvider.ThirdPartyId;
            }

        var response = await _transportationCargoRepository.GetsFilteredTransportationCargo(request.Ids,
            request.ContractorIds, request.TransportationRequestIds, request.PackingIds, request.DeliveryMethods,
            request.DeliveryTypes, request.PackingShippingTypes, request.TransportationRequestStatus,
            request.SalesChannelTypes, request.PalletTransportStatus, request.ThirdPartyIds, request.ProductIds,
            request.WarehouseIds, request.DesWarehouseIds, request.CityIds, request.CreatorId, thirdPartyId, request.FromDate,
            request.ToDate, request.FilterData, companyId, request.HaveShippingType, request.PageIndex, request.PageSize, ct);
        if (response.Data is null || response.Data.Count <= 0)
            return Result.Failure<GetsFilteredTransportationCargoResponse>(TransportationRequestErrors
                .CargosPalletNotfound);
        var values = response.Data;

        await GetCargoWarehouseTransportationSeedData(values, ct);

        return new GetsFilteredTransportationCargoResponse(values ?? [], response.RowCount);
    }

    public async Task<Result<GetTransportationCargoByIdResponse?>> GetTransportationCargoById(
        GetTransportationCargoByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestWarehouse");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetTransportationCargoByIdResponse>(GlobalErrors.InvalidCompany);

        var response = await _transportationCargoRepository.GetCargoById(request.Id, ct);
        if (response is null) return Result.Failure<GetTransportationCargoByIdResponse>
                (TransportationRequestErrors.CargosPalletWithNotfound);
        var value = response;
        await GetCargoWarehouseTransportationPalletSeedData(value, ct);

        return value;
    }
}