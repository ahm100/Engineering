using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;
using Engineering.Application.Services.ShippingCosts.Contracts.CreateShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.DeleteShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelEnum;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelExporter;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcel;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Application.Services.ShippingCosts.Contracts.UpdateShippingCost;
using Engineering.Application.Services.ShippingCosts.Models.ShippingCostExcelImports;
using IdentityServer.ClientSdk.Services.ServiceClients;

namespace Engineering.Application.Services.ShippingCosts;

public partial class ShippingCostLogic : IShippingCostLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ShippingCostLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IShippingCostRepository _repository;
    private readonly IShippingCostHistoryRepository _historyRpository;
    private readonly IMachineTypeRepository _machineTypeRepository;
    private readonly ITransportationContractorRepository _transportationContractorRepository;
    private readonly IViewCityRepository _cityRepository;
    private readonly IViewRegionRepository _regionRepository;
    private readonly IViewThirdPartyRepository _thirdPartyRepository;
    private readonly IUserInfoService _userInfoService;
    private readonly ICompanyClient _companyClient;

    public ShippingCostLogic(IMediator mediator,
        ILogger<ShippingCostLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        IShippingCostRepository repository,
        IMachineTypeRepository machineTypeRepository,
        ITransportationContractorRepository transportationContractorRepository,
        IViewCityRepository cityRepository,
        IViewRegionRepository regionRepository,
        IViewThirdPartyRepository thirdPartyRepository,
        IShippingCostHistoryRepository historyRpository,
        ICompanyClient companyClient)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _machineTypeRepository = machineTypeRepository;
        _transportationContractorRepository = transportationContractorRepository;
        _cityRepository = cityRepository;
        _regionRepository = regionRepository;
        _thirdPartyRepository = thirdPartyRepository;
        _userInfoService = userInfoService;
        _historyRpository = historyRpository;
        _companyClient = companyClient;
    }

    public async Task<Result<CreateShippingCostResponse?>> CreateShippingCost(CreateShippingCostRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateShippingCost");
        var response = await CreateShippingCostExecute(request, ct);
        if (response.IsFailure) return Result.Failure<CreateShippingCostResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateShippingCostResponse(true);
    }

    public async Task<Result<ShippingCostExcelImportsResponse?>> ShippingCostExcelImports(ShippingCostExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for ShippingCostExcelImports");
        var response = await ShippingCostExcelImportsExecute(request, ct);
        if (response.IsFailure) return Result.Failure<ShippingCostExcelImportsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ShippingCostExcelImportsResponse(true);
    }

    public async Task<Result<DeleteShippingCostResponse?>> DeleteShippingCost(DeleteShippingCostRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteShippingCost, Ids:{Ids}", request.Ids);
        var response = await DeleteShippingCostExecute(request, ct);
        if (response.IsFailure) return Result.Failure<DeleteShippingCostResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteShippingCostResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateShippingCostResponse?>> UpdateShippingCost(UpdateShippingCostRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateShippingCost");
        var response = await UpdateShippingCostExecute(request, ct);
        if (response.IsFailure) return Result.Failure<UpdateShippingCostResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateShippingCostResponse(response.Value!.Id);
    }

    public async Task<Result<ChangeShippingCostStateResponse?>> ChangeShippingCostState(ChangeShippingCostStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeShippingCostState");
        var result = await ChangeShippingCostStateExecute(request, ct);
        if (result.IsFailure) return result.Failure<ChangeShippingCostStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new ChangeShippingCostStateResponse(true);
    }

    public async Task<Result<GetShippingCostByIdResponse?>> GetShippingCostById(GetShippingCostByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetShippingCostById, id:{Id}", request.Id);
        var response = await GetShippingCostByIdExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetShippingCostByIdResponse>(response.Error!);
        return response.Value!;
    }

    public async Task<Result<GetsActiveShippingCostResponse?>> GetsActiveShippingCost(GetsActiveShippingCostRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveShippingCost");
        var response = await GetsActiveShippingCostExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsActiveShippingCostResponse>(response.Error!);
        return new GetsActiveShippingCostResponse(
            response.Value!.Data ?? new List<GetsActiveShippingCostResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredShippingCostResponse?>> GetsFilteredShippingCost(GetsFilteredShippingCostRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsShippingCost");
        var response = await GetsFilteredShippingCostExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsFilteredShippingCostResponse>(response.Error!);
        return new GetsFilteredShippingCostResponse(
            response.Value!.Data ?? new List<GetsFilteredShippingCostResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetShippingCostHistoryResponse?>> GetShippingCostHistory(GetShippingCostHistoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetShppingCostHistory");
        var response = await GetShippingCostHistoryExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetShippingCostHistoryResponse>(response.Error!);
        return new GetShippingCostHistoryResponse(
            response.Value!.Data ?? new List<GetShippingCostHistoryResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<ShippingCostImportExcelResponse?>> ShippingCostImportExcel(ShippingCostImportExcelRequest request, CT ct)
    {
        _logger.LogInformation("Request for ShippingCostImportExcel");

        var file = new FileContentResult(ShippingCostExcels.ShippingCostImportExcel(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ShippingCostsImports-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new ShippingCostImportExcelResponse(file);
    }

    public async Task<Result<ShippingCostImportExcelHelperResponse?>> ShippingCostImportExcelHelper(ShippingCostImportExcelHelperRequest request, CT ct)
    {
        _logger.LogInformation("Request for ShippingCostImportExcelHelper");

        var cities = await _cityRepository.GetAllActiveCitiesData(null, 0, 0, ct);
        var regions = await _regionRepository.GetAllActiveRegionsData(null, 0, 0, ct);
        var machineTypes = await _machineTypeRepository.GetsActiveMachineTypeData(null, null, 0, 0, ct);

        var file = new FileContentResult(ShippingCostExcels.ShippingCostHelpImportExcel(cities.Data, regions.Data, machineTypes.Data),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ShippingCostsImportsHelper-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new ShippingCostImportExcelHelperResponse(file);
    }

    public async Task<Result<GetsShippingCostExcelExporterResponse?>> GetsShippingCostExcelExporter(GetsShippingCostExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsShippingCostExcelExporter");

        var response = await GetsFilteredShippingCostExecute(request.Adapt<GetsFilteredShippingCostRequest>(), ct);
        if (response.IsFailure) return Result.Failure<GetsShippingCostExcelExporterResponse>(response.Error!);

        var file = new FileContentResult(ShippingCostExcels.ShippingCostToExcel(response.Value!.Data!, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ShippingCosts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsShippingCostExcelExporterResponse(file);
    }

    public async Task<Result<GetsShippingCostExcelEnumResponse?>> GetsShippingCostExcelEnum(GetsShippingCostExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsShippingCostExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ShippingCostExcelEnum>());
        return new GetsShippingCostExcelEnumResponse(response);
    }
}