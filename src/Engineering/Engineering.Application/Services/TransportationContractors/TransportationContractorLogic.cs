using Engineering.Application.Abstractions.Data;
using Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;
using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.DeleteTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsDeliveryMethod;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsDeliveryType;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelEnum;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelExporter;
using Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;
using Engineering.Application.Services.TransportationContractors.Contracts.PriceWeightImportExcel;
using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;
using Engineering.ClientSdk.Enums;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.TransportationContractors;

public partial class TransportationContractorLogic : ITransportationContractorLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransportationContractorLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly ITransportationContractorRepository _repository;
    private readonly ITransportationContractorPersonnelRepository _personnelRepository;
    private readonly ITransportationContractorPriceWeightHistoryRepository _priceWeightRepository;
    private readonly IUserInfoProvider _userInfoProvider;

    public TransportationContractorLogic(IMediator mediator,
        ILogger<TransportationContractorLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        ITransportationContractorPersonnelRepository personnelRepository,
        ITransportationContractorRepository repository,
        ITransportationContractorPriceWeightHistoryRepository priceWeightRepository,
        IUserInfoProvider userInfoProvider)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _personnelRepository = personnelRepository;
        _repository = repository;
        _priceWeightRepository = priceWeightRepository;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateTransportationContractorResponse?>> CreateTransportationContractor(CreateTransportationContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateTransportationContractor");
        var response = await CreateTransportationContractorExecute(request, ct);
        if (response.IsFailure) return Result.Failure<CreateTransportationContractorResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateTransportationContractorResponse(response.Value!.Id);
    }

    public async Task<Result<PriceWeightExcelImportsResponse?>> PriceWeightExcelImports(PriceWeightExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for PriceWeightExcelImports");
        var response = await PriceWeightImportExecute(request, ct);
        if (response.IsFailure) return Result.Failure<PriceWeightExcelImportsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value;
    }

    public async Task<Result<DeleteTransportationContractorResponse?>> DeleteTransportationContractor(DeleteTransportationContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteTransportationContractor, Ids:{Ids}", request.Ids);
        var response = await DeleteTransportationContractorExecute(request, ct);
        if (response.IsFailure) return Result.Failure<DeleteTransportationContractorResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteTransportationContractorResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateTransportationContractorResponse?>> UpdateTransportationContractor(UpdateTransportationContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationContractor");
        var response = await UpdateTransportationContractorExecute(request, ct);
        if (response.IsFailure) return Result.Failure<UpdateTransportationContractorResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportationContractorResponse(response.Value!.Id);
    }

    public async Task<Result<ChangeTransportationContractorStateResponse?>> ChangeTransportationContractorState(ChangeTransportationContractorStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeTransportationContractorState");
        var result = await ChangeTransportationContractorStateExecute(request, ct);
        if (result.IsFailure) return result.Failure<ChangeTransportationContractorStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new ChangeTransportationContractorStateResponse(true);
    }

    public async Task<Result<GetTransportationContractorByIdResponse?>> GetTransportationContractorById(GetTransportationContractorByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationContractorById, id:{Id}", request.Id);
        var response = await GetTransportationContractorByIdExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetTransportationContractorByIdResponse>(response.Error!);
        return response.Value!;
    }

    public async Task<Result<GetsActiveTransportationContractorResponse?>> GetsActiveTransportationContractor(GetsActiveTransportationContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveTransportationContractor");
        var response = await GetsActiveTransportationContractorExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsActiveTransportationContractorResponse>(response.Error!);
        return new GetsActiveTransportationContractorResponse(
            response.Value!.Data ?? new List<GetsActiveTransportationContractorResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredTransportationContractorResponse?>> GetsFilteredTransportationContractor(GetsFilteredTransportationContractorRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractor");
        var response = await GetsFilteredTransportationContractorExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsFilteredTransportationContractorResponse>(response.Error!);
        return new GetsFilteredTransportationContractorResponse(
            response.Value!.Data ?? new List<GetsFilteredTransportationContractorResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsPriceWeightHistoryResponse?>> GetsPriceWeightHistory(GetsPriceWeightHistoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractor");
        var response = await GetsPriceWeightHistoryExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsPriceWeightHistoryResponse>(response.Error!);
        return new GetsPriceWeightHistoryResponse(
            response.Value!.Data ?? new List<GetsPriceWeightHistoryResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<PriceWeightImportExcelResponse?>> PriceWeightImportExcel(PriceWeightImportExcelRequest request, CT ct)
    {
        _logger.LogInformation("Request for PriceWeightImportExcel");

        var file = new FileContentResult(TransportationContractorExcels.PriceWeightImportExcel(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"PriceWeightExcel-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new PriceWeightImportExcelResponse(file);
    }

    public async Task<Result<GetsTransportationContractorExcelExporterResponse?>> GetsTransportationContractorExcelExporter(GetsTransportationContractorExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorExcelExporter");

        var response = await GetsFilteredTransportationContractorExecute(request.Adapt<GetsFilteredTransportationContractorRequest>(), ct);
        if (response.IsFailure) return Result.Failure<GetsTransportationContractorExcelExporterResponse>(response.Error!);

        var file = new FileContentResult(TransportationContractorExcels.TransportationContractorToExcel(response.Value!.Data!, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"TransportationContractors-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsTransportationContractorExcelExporterResponse(file);
    }

    public async Task<Result<GetsTransportationContractorExcelEnumResponse?>> GetsTransportationContractorExcelEnum(GetsTransportationContractorExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationContractorExcelEnum>());
        return new GetsTransportationContractorExcelEnumResponse(response);
    }

    public async Task<Result<GetsDeliveryMethodResponse?>> GetsDeliveryMethod(GetsDeliveryMethodRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsDeliveryMethod");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<DeliveryMethod>());
        return new GetsDeliveryMethodResponse(response);
    }

    public async Task<Result<GetsDeliveryTypeResponse?>> GetsDeliveryType(GetsDeliveryTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsDeliveryType");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<DeliveryType>());
        return new GetsDeliveryTypeResponse(response);
    }
}