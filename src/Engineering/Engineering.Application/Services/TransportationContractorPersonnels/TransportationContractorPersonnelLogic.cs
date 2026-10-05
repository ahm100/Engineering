using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.CreateTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.DeleteTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelEnum;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelExporter;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.UpdateTransportationContractorPersonnel;

namespace Engineering.Application.Services.TransportationContractorPersonnels;

public partial class TransportationContractorPersonnelLogic : ITransportationContractorPersonnelLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransportationContractorPersonnelLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly ITransportationContractorPersonnelRepository _repository;
    private readonly ITransportationContractorRepository _contractorRepository;
    private readonly ITransportationContractorMachineRepository _contractorMachineRepository;
    private readonly IMachineTypeRepository _machineTypeRepository;

    public TransportationContractorPersonnelLogic(IMediator mediator,
        ILogger<TransportationContractorPersonnelLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        ITransportationContractorRepository contractorRepository,
        ITransportationContractorPersonnelRepository repository,
        IMachineTypeRepository machineTypeRepository,
        ITransportationContractorMachineRepository contractorMachineRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _contractorRepository = contractorRepository;
        _repository = repository;
        _machineTypeRepository = machineTypeRepository;
        _contractorMachineRepository = contractorMachineRepository;
    }

    public async Task<Result<CreateTransportationContractorPersonnelResponse?>> CreateTransportationContractorPersonnel(CreateTransportationContractorPersonnelRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateTransportationContractorPersonnel");
        var response = await CreateTransportationContractorPersonnelExecute(request, ct);
        if (response.IsFailure) return Result.Failure<CreateTransportationContractorPersonnelResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateTransportationContractorPersonnelResponse(response.Value!.Id);
    }

    public async Task<Result<DeleteTransportationContractorPersonnelResponse?>> DeleteTransportationContractorPersonnel(DeleteTransportationContractorPersonnelRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteTransportationContractorPersonnel, Ids:{Ids}", request.Ids);
        var response = await DeleteTransportationContractorPersonnelExecute(request, ct);
        if (response.IsFailure) return Result.Failure<DeleteTransportationContractorPersonnelResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteTransportationContractorPersonnelResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateTransportationContractorPersonnelResponse?>> UpdateTransportationContractorPersonnel(UpdateTransportationContractorPersonnelRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportationContractorPersonnel");
        var response = await UpdateTransportationContractorPersonnelExecute(request, ct);
        if (response.IsFailure) return Result.Failure<UpdateTransportationContractorPersonnelResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportationContractorPersonnelResponse(response.Value!.Id);
    }

    public async Task<Result<ChangeTransportationContractorPersonnelStateResponse?>> ChangeTransportationContractorPersonnelState(ChangeTransportationContractorPersonnelStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeTransportationContractorPersonnelState");
        var result = await ChangeTransportationContractorPersonnelStateExecute(request, ct);
        if (result.IsFailure) return result.Failure<ChangeTransportationContractorPersonnelStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new ChangeTransportationContractorPersonnelStateResponse(true);
    }

    public async Task<Result<GetTransportationContractorPersonnelByIdResponse?>> GetTransportationContractorPersonnelById(GetTransportationContractorPersonnelByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationContractorPersonnelById, id:{Id}", request.Id);
        var response = await GetTransportationContractorPersonnelByIdExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetTransportationContractorPersonnelByIdResponse>(response.Error!);
        return response.Value!;
    }

    public async Task<Result<GetsActiveTransportationContractorPersonnelResponse?>> GetsActiveTransportationContractorPersonnel(GetsActiveTransportationContractorPersonnelRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveTransportationContractorPersonnel");
        var response = await GetsActiveTransportationContractorPersonnelExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsActiveTransportationContractorPersonnelResponse>(response.Error!);
        return new GetsActiveTransportationContractorPersonnelResponse(
            response.Value!.Data ?? new List<GetsActiveTransportationContractorPersonnelResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredTransportationContractorPersonnelResponse?>> GetsFilteredTransportationContractorPersonnel(GetsFilteredTransportationContractorPersonnelRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorPersonnel");
        var response = await GetsFilteredTransportationContractorPersonnelExecute(request, ct);
        if (response.IsFailure) return Result.Failure<GetsFilteredTransportationContractorPersonnelResponse>(response.Error!);
        return new GetsFilteredTransportationContractorPersonnelResponse(
            response.Value!.Data ?? new List<GetsFilteredTransportationContractorPersonnelResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsTransportationContractorPersonnelExcelExporterResponse?>> GetsTransportationContractorPersonnelExcelExporter(GetsTransportationContractorPersonnelExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorPersonnelExcelExporter");

        var response = await GetsFilteredTransportationContractorPersonnelExecute(request.Adapt<GetsFilteredTransportationContractorPersonnelRequest>(), ct);
        if (response.IsFailure) return Result.Failure<GetsTransportationContractorPersonnelExcelExporterResponse>(response.Error!);

        var file = new FileContentResult(TransportationContractorPersonnelExcels.TransportationContractorPersonnelToExcel(response.Value!.Data!, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"TransportationContractorPersonnels-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsTransportationContractorPersonnelExcelExporterResponse(file);
    }

    public async Task<Result<GetsTransportationContractorPersonnelExcelEnumResponse?>> GetsTransportationContractorPersonnelExcelEnum(GetsTransportationContractorPersonnelExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationContractorPersonnelExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationContractorPersonnelExcelEnum>());
        return new GetsTransportationContractorPersonnelExcelEnumResponse(response);
    }
}